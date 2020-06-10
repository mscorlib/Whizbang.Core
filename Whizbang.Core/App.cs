using System;
using System.Linq;
using System.Reflection;
using Whizbang.Core.Commands;
using Whizbang.Core.Config;
using Whizbang.Core.EventSource;
using Whizbang.Core.EventSource.Storage;
using Whizbang.Core.Exceptions;
using Whizbang.Core.Ioc;
using Whizbang.Core.MessageBus;
using Whizbang.Core.Serializer;

namespace Whizbang.Core
{
    public sealed class App
    {
        private static IContainer _container;

        public static void Init()
        {
            var config = Configuration.Load();

            var builder = CreateContainerBuilder(config);

            InitFramework(builder, config);

            InitProviders(builder, config);

            _container = builder.Build();

            InitHandlers(config);
        }

        public static IContainer Container
        {
            get { return _container; }
        }

        /// <summary>
        ///     从配置创建容器构造器
        /// </summary>
        /// <param name="config">配置</param>
        /// <returns></returns>
        private static IContainerBuilder CreateContainerBuilder(Configuration config)
        {
            var section = config["Framework"];

            var builderType = LoadTypeFromSetting(section["ContainerBuilder"]);

            var instance = (IContainerBuilder)Activator.CreateInstance(builderType);

            return instance;
        }

        #region init framework

        /// <summary>
        ///     初始化框架
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="config"></param>
        private static void InitFramework(IContainerBuilder builder, Configuration config)
        {
            var section = config["Framework"];

            RegisterMessageDistributor(section, builder);
            RegisterEventBus(section, builder);
            RegisterCommandBus(section, builder);
            RegisterTypeResolver(section, builder);
            RegisterDomainRepository(section, builder);
        }

        private static void RegisterMessageDistributor(Section section, IContainerBuilder builder)
        {
            var assemblyInfo = section["MessageDistributor"];

            var type = LoadTypeFromSetting(assemblyInfo);

            builder.Register(typeof(IMessageDistributor), type, Scope.Singleton);
        }

        private static void RegisterEventBus(Section section, IContainerBuilder builder)
        {
            var assemblyInfo = section["EventBus"];

            var type = LoadTypeFromSetting(assemblyInfo);

            builder.Register(typeof(IEventBus), type, Scope.Singleton);
        }

        private static void RegisterCommandBus(Section section, IContainerBuilder builder)
        {
            var assemblyInfo = section["CommandBus"];

            var type = LoadTypeFromSetting(assemblyInfo);

            builder.Register(typeof(ICommandBus), type, Scope.Singleton);
        }

        private static void RegisterTypeResolver(Section section, IContainerBuilder builder)
        {
            var assemblyInfo = section["TypeResolver"];

            var type = LoadTypeFromSetting(assemblyInfo);

            builder.Register(typeof(ITypeResolver), type, Scope.Transient);
        }

        private static void RegisterDomainRepository(Section section, IContainerBuilder builder)
        {
            var assemblyInfo = section["DomainRepository"];

            var type = LoadTypeFromSetting(assemblyInfo);

            builder.Register(typeof(IDomainRepository<>), type, Scope.PerExecutionScope);
        }

        #endregion init framework

        #region init providers

        /// <summary>
        ///     初始化提供者
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="config"></param>
        public static void InitProviders(IContainerBuilder builder, Configuration config)
        {
            var section = config["Providers"];

            RegisterObjectSerializer(section, builder);
            RegisterEventStorage(section, builder);
        }

        private static void RegisterObjectSerializer(Section section, IContainerBuilder builder)
        {
            var assemblyInfo = section["ObjectSerializer"];

            var type = LoadTypeFromSetting(assemblyInfo);

            builder.Register(typeof(IObjectSerializer<string>), type);
        }

        private static void RegisterEventStorage(Section section, IContainerBuilder builder)
        {
            var assemblyInfo = section["EventStorage"];

            var type = LoadTypeFromSetting(assemblyInfo);

            builder.Register(typeof(IEventStorage), type);
        }

        #endregion init providers

        #region init handlers (after container build)

        /// <summary>
        ///     初始化消息处理器
        /// </summary>
        /// <param name="config"></param>
        private static void InitHandlers(Configuration config)
        {
            var section = config["Handlers"];

            RegisteEventHandler(section);

            RegisteCommandHandler(section);
        }

        private static void RegisteEventHandler(Section section)
        {
            var eventHandlerAssemblyName = section["EventHandler"];

            var assembly = Assembly.Load(eventHandlerAssemblyName);

            var distributor = _container.Resolve<IMessageDistributor>();

            foreach (Type type in
                from t in assembly.GetExportedTypes()
                from it in t.GetInterfaces()
                where t.IsClass
                      && it.IsGenericType
                      && it.GetGenericTypeDefinition() == typeof(IEventHandler<>)
                select t)
            {
                DistributorHelper.RegisterType(distributor, type);
            }
        }

        private static void RegisteCommandHandler(Section section)
        {
            var eventHandlerAssemblyName = section["CommandHandler"];

            var assembly = Assembly.Load(eventHandlerAssemblyName);

            var distributor = _container.Resolve<IMessageDistributor>();

            foreach (Type type in
                from t in assembly.GetExportedTypes()
                from it in t.GetInterfaces()
                where t.IsClass
                      && it.IsGenericType
                      && it.GetGenericTypeDefinition() == typeof(ICommandHandler<>)
                select t)
            {
                DistributorHelper.RegisterType(distributor, type);
            }
        }

        #endregion init handlers (after container build)

        /// <summary>
        ///     从配置加载类型
        /// </summary>
        /// <param name="assemblyInfo"></param>
        /// <returns></returns>
        private static Type LoadTypeFromSetting(string assemblyInfo)
        {
            var arr = assemblyInfo.Split(new[] { ',' });

            if (2 != arr.Length)
            {
                throw new ValidationException(string.Format("配置信息错误！错误配置参数：{0}", assemblyInfo));
            }

            return Assembly.Load(arr[1].Trim()).GetType(arr[0].Trim());
        }
    }
}