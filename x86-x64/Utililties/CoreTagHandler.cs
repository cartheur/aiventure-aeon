using System.Xml;

namespace Animals.Core.Utililties
{
    /// <summary>
    /// The template for all classes that handle the tags found within template nodes of a category.
    /// </summary>
    public abstract class CoreTagHandler : TextTransformer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CoreTagHandler"/> class.
        /// </summary>
        /// <param name="thisAeon">The bot involved in this request</param>
        /// <param name="thisUser">The user making the request</param>
        /// <param name="query">The query that originated this node</param>
        /// <param name="thisRequest">The request itself</param>
        /// <param name="thisResult">The result to be passed back to the user</param>
        /// <param name="templateNode">The node to be processed</param>
        protected CoreTagHandler(Aeon thisAeon,
                                    User thisUser,
                                    SubQuery query,
                                    Request thisRequest,
                                    Result thisResult,
                                    XmlNode templateNode)
            : base(thisAeon, templateNode.OuterXml)
        {
            ThisUser = thisUser;
            Query = query;
            Logger.SaveAnalyticBit("Presence: " + thisAeon.Name + " || " + query.FullPath + " - " + query.Template);
            ThisRequest = thisRequest;
            ThisResult = thisResult;
            TemplateNode = templateNode;
            if (TemplateNode.Attributes != null) TemplateNode.Attributes.RemoveNamedItem("xmlns");
        }

        /// <summary>
        /// Default ctor to use when late binding
        /// </summary>
        protected CoreTagHandler() { }
        /// <summary>
        /// A flag to denote if inner tags are to be processed recursively before processing this tag
        /// </summary>
        public bool IsRecursive = true;
        /// <summary>
        /// A representation of the user who made the request
        /// </summary>
        public User ThisUser;
        /// <summary>
        /// The query that produced this node containing the wildcard matches
        /// </summary>
        public SubQuery Query;
        /// <summary>
        /// A representation of the input into the bot made by the user
        /// </summary>
        public Request ThisRequest;
        /// <summary>
        /// A representation of the result to be returned to the user
        /// </summary>
        public Result ThisResult;
        /// <summary>
        /// The template node to be processed by the class
        /// </summary>
        public XmlNode TemplateNode;
        /// <summary>
        /// Helper method that turns the passed string into an XML node
        /// </summary>
        /// <param name="outerXml">the string to XMLize</param>
        /// <returns>The XML node</returns>
        public static XmlNode GetNode(string outerXml)
        {
            XmlDocument temp = new XmlDocument();
            temp.LoadXml(outerXml);
            return temp.FirstChild;
        }
    }
}
