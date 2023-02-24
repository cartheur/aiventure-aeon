using System.Xml;
using Animals.Core.Utililties;

namespace Animals.Core.CoreTagHandlers
{
    /// <summary>
    /// The version element tells the  interpreter that it should substitute the version number of the interpreter.
    /// 
    /// The version element does not have any content. 
    /// </summary>
    public class VersionElement : CoreTagHandler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VersionElement"/> class.
        /// </summary>
        /// <param name="thisAeon">The bot involved in this request</param>
        /// <param name="thisUser">The user making the request</param>
        /// <param name="query">The query that originated this node</param>
        /// <param name="thisRequest">The request inputted into the system</param>
        /// <param name="thisResult">The result to be passed to the user</param>
        /// <param name="templateNode">The node to be processed</param>
        public VersionElement(Aeon thisAeon,
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
            if (TemplateNode.Name.ToLower() == "version")
            {
                return ThisAeon.GlobalSettings.GrabSetting("version");
            }
            return string.Empty;
        }
    }
}
