using System.Text;
using System.Xml;
using Animals.Core.Utililties;

namespace Animals.Core.CoreTagHandlers
{
    /// <summary>
    /// The formal element tells the interpreter to render the contents of the element 
    /// such that the first letter of each word is in uppercase, as defined (if defined) by 
    /// the locale indicated by the specified language (if specified). This is similar to methods 
    /// that are sometimes called "Title Case". 
    /// 
    /// If no character in this string has a different uppercase version, based on the Unicode 
    /// standard, then the original string is returned.
    /// </summary>
    public class Formal : CoreTagHandler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Formal"/> class.
        /// </summary>
        /// <param name="thisAeon">The bot involved in this request</param>
        /// <param name="thisUser">The user making the request</param>
        /// <param name="query">The query that originated this node</param>
        /// <param name="thisRequest">The request inputted into the system</param>
        /// <param name="thisResult">The result to be passed to the user</param>
        /// <param name="templateNode">The node to be processed</param>
        public Formal(Aeon thisAeon,
                        User thisUser,
                        SubQuery query,
                        Request thisRequest,
                        Result thisResult,
                        XmlNode templateNode)
            : base(thisAeon, thisUser, query, thisRequest, thisResult, templateNode)
        {
        }

        protected override string ProcessChange()
        {
            if (TemplateNode.Name.ToLower() == "formal")
            {
                StringBuilder result = new StringBuilder();
                if (TemplateNode.InnerText.Length > 0)
                {
                    string[] words = TemplateNode.InnerText.ToLower().Split();
                    foreach (string word in words)
                    {
                        string newWord = word.Substring(0, 1);
                        newWord = newWord.ToUpper();
                        if (word.Length > 1)
                        {
                            newWord += word.Substring(1);
                        }
                        result.Append(newWord + " ");
                    }
                }
                return result.ToString().Trim();
            }
            return string.Empty;
        }
    }
}
