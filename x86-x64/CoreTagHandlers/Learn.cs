using System.IO;
using System.Xml;
using Animals.Core.Utililties;

namespace Animals.Core.CoreTagHandlers
{
    /// <summary>
    /// The learn element instructs the interpreter to retrieve a resource specified by a URI and to process its contents.
    /// </summary>
    public class Learn : CoreTagHandler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Learn"/> class.
        /// </summary>
        /// <param name="thisAeon">The bot involved in this request</param>
        /// <param name="thisUser">The user making the request</param>
        /// <param name="query">The query that originated this node</param>
        /// <param name="thisRequest">The request inputted into the system</param>
        /// <param name="thisResult">The result to be passed to the user</param>
        /// <param name="templateNode">The node to be processed</param>
        public Learn(Aeon thisAeon,
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
            if (TemplateNode.Name.ToLower() == "learn")
            {
                // currently only files in the local filesystem can be referenced
                // ToDo: Network HTTP and web service based learning
                if (TemplateNode.InnerText.Length > 0)
                {
                    string path = TemplateNode.InnerText;
                    FileInfo fi = new FileInfo(path);
                    if (fi.Exists)
                    {
                        XmlDocument doc = new XmlDocument();
                        try
                        {
                            doc.Load(path);
                            ThisAeon.LoadPersonalityFromXml(doc, path);
                        }
                        catch
                        {
                            ThisAeon.WriteToLog("Attempted to <learn> some new personality elements from the following URI: " + path);
                        }
                    }
                }
            }
            return string.Empty;
        }
    }
}
