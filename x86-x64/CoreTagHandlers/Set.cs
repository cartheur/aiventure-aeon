using System.Xml;
using Animals.Core.Utililties;

namespace Animals.Core.CoreTagHandlers
{
    /// <summary>
    /// The set element instructs the interpreter to set the value of a predicate to the result 
    /// of processing the contents of the set element. The set element has a required attribute name, 
    /// which must be a valid  predicate name. If the predicate has not yet been defined, the  
    /// interpreter should define it in memory. 
    /// 
    /// The interpreter should, generically, return the result of processing the contents of the 
    /// set element. The set element must not perform any text formatting or other "normalization" on 
    /// the predicate contents when returning them. 
    /// 
    /// The interpreter implementation may optionally provide a mechanism that allows the  
    /// author to designate certain predicates as "return-name-when-set", which means that a set 
    /// operation using such a predicate will return the name of the predicate, rather than its 
    /// captured value. (See [9.2].) 
    /// 
    /// A set element may contain any template elements.
    /// </summary>
    public class Set : CoreTagHandler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Set"/> class.
        /// </summary>
        /// <param name="thisAeon">The bot involved in this request</param>
        /// <param name="thisUser">The user making the request</param>
        /// <param name="query">The query that originated this node</param>
        /// <param name="thisRequest">The request inputted into the system</param>
        /// <param name="thisResult">The result to be passed to the user</param>
        /// <param name="templateNode">The node to be processed</param>
        public Set(Aeon thisAeon,
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
            if (TemplateNode.Name.ToLower() == "set")
            {
                if (ThisAeon.GlobalSettings.Count > 0)
                {
                    if (TemplateNode.Attributes != null && TemplateNode.Attributes.Count == 1)
                    {
                        if (TemplateNode.Attributes[0].Name.ToLower() == "name")
                        {
                            if (TemplateNode.InnerText.Length > 0)
                            {
                                ThisUser.Predicates.AddSetting(TemplateNode.Attributes[0].Value, TemplateNode.InnerText);
                                return ThisUser.Predicates.GrabSetting(TemplateNode.Attributes[0].Value);
                            }
                            // remove the predicate
                            ThisUser.Predicates.RemoveSetting(TemplateNode.Attributes[0].Value);
                            return string.Empty;
                        }
                    }
                }
            }
            return string.Empty;
        }
    }
}
