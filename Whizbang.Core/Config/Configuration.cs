using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Whizbang.Core.Exceptions;

namespace Whizbang.Core.Config
{
    public sealed class Configuration
    {
        public const string DefaultSectionName = "Default";

        private readonly static string DefaultPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.cfg");

        private static IList<Section> _sections;

        private Configuration()
        {
            _sections = new List<Section>();
        }

        #region default section

        /// <summary>
        ///     默认节点
        /// </summary>
        internal Section Default { get; private set; }

        /// <summary>
        ///     添加配置到默认节点
        /// </summary>
        /// <param name="setting"></param>
        internal void AddSettingToDefault(Setting setting)
        {
            if (null == Default)
                Default = new Section(DefaultSectionName);

            Default.AddSetting(setting);
        }

        ///// <summary>
        /////     从默认节点获取配置信息
        ///// <para>  无默认节点将抛出异常信息</para>
        ///// </summary>
        ///// <param name="name">配置名称</param>
        ///// <returns></returns>
        //internal string this[string name]
        //{
        //    get
        //    {
        //        if(null == Default)
        //            throw new NotFoundException("默认节点未找到");

        //        var setting = Default.Settings.FirstOrDefault(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

        //        return null == setting
        //            ? string.Empty
        //            : setting.Value;
        //    }
        //}

        #endregion default section

        #region section member

        public IEnumerable<Section> Sections
        {
            get { return _sections; }
        }

        /// <summary>
        ///     根据区域名称获取节点信息
        /// </summary>
        /// <param name="name">节点名称</param>
        /// <returns></returns>
        public Section this[string name]
        {
            get
            {
                if (!_sections.Any(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
                    throw new NotFoundException(string.Format("节点未找到,节点名称：{0}", name));

                return _sections.First(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            }
        }

        /// <summary>
        ///     添加区域
        /// </summary>
        /// <param name="section"></param>
        internal void AddSection(Section section)
        {
            _sections.Add(section);
        }

        #endregion section member

        #region static member

        /// <summary>
        ///     加载默认配置文件
        /// <para>默认配置为当前运用程序目录下的config.cfg文件</para>
        /// </summary>
        /// <returns></returns>
        public static Configuration Load()
        {
            return Load(DefaultPath);
        }

        /// <summary>
        ///     加载配置文件
        /// </summary>
        /// <param name="path">配置文件路径</param>
        /// <returns></returns>
        public static Configuration Load(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentNullException("path", "文件路径不能为空！");

            if (!File.Exists(path))
                throw new FileNotFoundException("配置文件未找到！", path);

            var source = File.ReadAllText(path);

            return Parse(source);
        }

        /// <summary>
        ///     解析配置文件
        /// </summary>
        /// <param name="source">文件源</param>
        /// <returns></returns>
        private static Configuration Parse(string source)
        {
            var config = new Configuration();

            var comments = new List<string>(); //临时存储[顶部注释]内容

            var lines = source.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            Section currentSection = config.Default;

            //遍历所有的非空行
            foreach (var line in lines.Where(line => !string.IsNullOrWhiteSpace(line)))
            {
                if (line.StartsWith(CommentToken))   //当前行为注释
                {
                    var comment = ParseComment(line);

                    if (!string.IsNullOrWhiteSpace(comment))
                        comments.Add(comment);

                    continue;
                }

                if (line.StartsWith(SectionStartToken))   //当前行为区域标识
                {
                    currentSection = ParseSection(line);

                    currentSection.AddHeadComments(comments.ToArray());

                    comments.Clear();

                    config.AddSection(currentSection);

                    continue;
                }

                //当前为配置行的处理

                var setting = ParseSetting(line);

                if (null == setting)
                    continue;

                if (comments.Any()) //如果存在顶部注释
                {
                    setting.AddHeadComments(comments.ToArray());

                    comments.Clear();
                }

                currentSection.AddSetting(setting);
            }

            return config;
        }

        /// <summary>
        ///     解析注释
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private static string ParseComment(string line)
        {
            if (string.IsNullOrWhiteSpace(line))
                return string.Empty;

            var index = line.IndexOf(CommentToken, StringComparison.Ordinal);

            if (index < 0)
                return string.Empty;

            return line.Substring(index + 1, (line.Length - index - 1));
        }

        /// <summary>
        ///     解析区域信息
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private static Section ParseSection(string line)
        {
            var content = RemoveComment(line).Trim();

            if (!content.StartsWith(SectionStartToken, StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException(string.Format("区域缺少开始符号: '{0}'", SectionStartToken), content);

            if (!content.EndsWith(SectionEndToken, StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException(string.Format("区域缺少结束符号: '{0}'", SectionEndToken), content);

            var name = content.Substring(1, content.Length - 2).Trim(); //获取区域名称

            if (string.IsNullOrWhiteSpace(name))
                throw new ValidationException("区域名称获取失败！");

            var section = new Section(name);

            var comment = ParseComment(line);//获取区域的行注释

            if (!string.IsNullOrWhiteSpace(comment))
                section.CreateLineComments(comment);

            return section;
        }

        /// <summary>
        ///     解析配置信息
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private static Setting ParseSetting(string line)
        {
            if (string.IsNullOrWhiteSpace(line))
                return null;

            var content = RemoveComment(line);

            var name = ExtractSettingName(content);

            var value = ExtractSettingValue(content);

            var setting = new Setting(name, value);

            var comment = ParseComment(line);

            if (!string.IsNullOrWhiteSpace(comment))
                setting.CreateLineComments(comment);

            return setting;
        }

        private static string RemoveComment(string line)
        {
            var index = line.IndexOf(CommentToken, StringComparison.OrdinalIgnoreCase);

            if (index < 0)
                return line;

            var length = line.Length - (line.Length - index);

            return line.Substring(0, length);
        }

        /// <summary>
        ///     提取设置名称
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private static string ExtractSettingName(string line)
        {
            int index = line.IndexOf(SettingToken, 0, StringComparison.InvariantCultureIgnoreCase);

            return line.Substring(0, index).Trim();
        }

        /// <summary>
        ///     提取设置内容
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private static string ExtractSettingValue(string line)
        {
            int index = line.IndexOf(SettingToken, 0, StringComparison.InvariantCultureIgnoreCase);

            return line.Substring(index + 1, line.Length - index - 1).Trim();
        }

        private const string CommentToken = "#";
        private const string SectionStartToken = "[";
        private const string SectionEndToken = "]";
        private const string SettingToken = "=";

        #endregion static member
    }
}