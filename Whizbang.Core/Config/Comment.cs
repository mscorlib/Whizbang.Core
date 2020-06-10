using System.Collections.Generic;

namespace Whizbang.Core.Config
{
    /// <summary>
    ///     注释
    /// </summary>
    public class Comment
    {
        private List<string> _headComments;
        private string _lineComment = string.Empty;

        /// <summary>
        ///     获取顶部注释
        /// </summary>
        public IList<string> HeadComments
        {
            get { return _headComments; }
        }

        /// <summary>
        ///     获取行注释
        /// </summary>
        public string LineComment
        {
            get { return _lineComment; }
        }

        /// <summary>
        ///     添加顶部注释
        /// </summary>
        /// <param name="content"></param>
        public void AddHeadComments(string content)
        {
            if (null == _headComments)
                _headComments = new List<string>();

            _headComments.Add(content);
        }

        /// <summary>
        ///     创建行注释
        /// </summary>
        /// <param name="content"></param>
        public void CreateLineComment(string content)
        {
            if (string.IsNullOrWhiteSpace(_lineComment))
            {
                _lineComment = content;
            }
        }
    }
}