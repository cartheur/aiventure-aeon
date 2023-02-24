using System.Xml;
using Animals.Core.Utililties;

namespace Animals.Core.CoreTagHandlers
{
    /// <summary>
    /// The think element instructs the interpreter to perform all usual processing of its 
    /// contents, but to not return any value, regardless of whether the contents produce output.
    /// 
    /// The think element has no attributes. It may contain any template elements.
    /// </summary>
    public class Think : CoreTagHandler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Think"/> class.
        /// </summary>
        /// <param name="thisAeon">The bot involved in this request</param>
        /// <param name="thisUser">The user making the request</param>
        /// <param name="query">The query that originated this node</param>
        /// <param name="thisRequest">The request inputted into the system</param>
        /// <param name="thisResult">The result to be passed to the user</param>
        /// <param name="templateNode">The node to be processed</param>
        public Think(Aeon thisAeon,
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
            return string.Empty;
        }
    }
}
