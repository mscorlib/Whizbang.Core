using System;
using System.Collections.Generic;
using System.Linq;

namespace Whizbang.Core.Config
{
    /// <summary>
    ///     配置区域
    /// </summary>
    public class Section
    {
        private readonly List<Setting> _settings;

        public Section(string name)
        {
            Name = name;
            _settings = new List<Setting>();
            Comment = new Comment();
        }

        public string Name { get; private set; }

        public IEnumerable<Setting> Settings
        {
            get { return _settings; }
        }

        /// <summary>
        ///     添加配置到区域
        /// </summary>
        /// <param name="setting">配置信息</param>
        public void AddSetting(Setting setting)
        {
            _settings.Add(setting);
        }

        /// <summary>
        ///     获取当前区域的配置信息
        /// </summary>
        /// <param name="name">配置名称</param>
        /// <returns></returns>
        public string this[string name]
        {
            get
            {
                var setting = _settings.FirstOrDefault(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

                return null == setting
                    ? string.Empty
                    : setting.Value;
            }
        }

        /// <summary>
        ///     区域注释
        /// </summary>
        public Comment Comment { get; private set; }

        /// <summary>
        ///     添加头部注释
        /// </summary>
        /// <param name="contents"></param>
        public void AddHeadComments(params string[] contents)
        {
            foreach (var content in contents)
            {
                Comment.AddHeadComments(content);
            }
        }

        /// <summary>
        ///     创建行注释
        /// </summary>
        /// <param name="content"></param>
        public void CreateLineComments(string content)
        {
            Comment.CreateLineComment(content);
        }
    }

    public class FrameworkConfig
    {
        public string MessageDistributorType { get; set; }
        public string CommandBusType { get; set; }
        public string EventBusType { get; set; }
        public string TypeResolverType { get; set; }
    }
}