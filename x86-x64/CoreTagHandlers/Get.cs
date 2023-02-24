using System.Xml;
using Animals.Core.Utililties;

namespace Animals.Core.CoreTagHandlers
{
    /// <summary>
    /// The get element tells the interpreter that it should substitute the contents of a 
    /// predicate, if that predicate has a value defined. If the predicate has no value defined, 
    /// the interpreter should substitute the empty string "". 
    /// 
    /// The interpreter implementation may optionally provide a mechanism that allows the 
    /// author to designate default values for certain predicates (see [9.3.]). 
    /// 
    /// The get element must not perform any text formatting or other "normalization" on the predicate
    /// contents when returning them. 
    /// 
    /// The get element has a required name attribute that identifies the predicate with an  
    /// predicate name. 
    /// 
    /// The get element does not have any content.
    /// </summary>
    public class Get : CoreTagHandler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Get"/> class.
        /// </summary>
        /// <param name="thisAeon">The bot involved in this request</param>
        /// <param name="thisUser">The user making the request</param>
        /// <param name="query">The query that originated this node</param>
        /// <param name="thisRequest">The request inputted into the system</param>
        /// <param name="thisResult">The result to be passed to the user</param>
        /// <param name="templateNode">The node to be processed</param>
        public Get(Aeon thisAeon,
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
            if (TemplateNode.Name.ToLower() == "get")
            {
                if (ThisAeon.GlobalSettings.Count > 0)
                {
                    if (TemplateNode.Attributes != null && TemplateNode.Attributes.Count == 1)
                    {
                        if (TemplateNode.Attributes[0].Name.ToLower() == "name")
                        {
                            return ThisUser.Predicates.GrabSetting(TemplateNode.Attributes[0].Value);
                        }
                    }
                }
            }
            return string.Empty;
        }
    }
}
