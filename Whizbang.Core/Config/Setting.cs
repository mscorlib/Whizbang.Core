namespace Whizbang.Core.Config
{
    /// <summary>
    ///     配置
    /// </summary>
    public class Setting
    {
        /// <summary>
        ///     构造新的配置
        /// </summary>
        /// <param name="name">配置名称</param>
        /// <param name="value">配置对应的值</param>
        public Setting(string name, string value)
        {
            Name = name;
            Value = value;
            Comment = new Comment();
        }

        /// <summary>
        ///     配置名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        ///     配置对应的值
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        ///     配置注释
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
}