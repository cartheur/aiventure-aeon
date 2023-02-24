using System;
using System.Xml;
using Animals.Core.Utililties;

namespace Animals.Core.CoreTagHandlers
{
    /// <summary>
    /// The thatstar element tells the  interpreter that it should substitute the contents of a 
    /// wildcard from a pattern-side that element. 
    /// 
    /// The thatstar element has an optional integer index attribute that indicates which wildcard 
    /// to use; the minimum acceptable value for the index is "1" (the first wildcard). 
    /// 
    /// An  interpreter should raise an error if the index attribute of a star specifies a 
    /// wildcard that does not exist in the that element's pattern content. Not specifying the index 
    /// is the same as specifying an index of "1". 
    /// 
    /// The thatstar element does not have any content. 
    /// </summary>
    public class ThatStar : CoreTagHandler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ThatStar"/> class.
        /// </summary>
        /// <param name="thisAeon">The bot involved in this request</param>
        /// <param name="thisUser">The user making the request</param>
        /// <param name="query">The query that originated this node</param>
        /// <param name="thisRequest">The request inputted into the system</param>
        /// <param name="thisResult">The result to be passed to the user</param>
        /// <param name="templateNode">The node to be processed</param>
        public ThatStar(Aeon thisAeon,
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
            if (TemplateNode.Name.ToLower() == "thatstar")
            {
                if (TemplateNode.Attributes != null && TemplateNode.Attributes.Count == 0)
                {
                    if (Query.ThatStar.Count > 0)
                    {
                        return Query.ThatStar[0];
                    }
                    ThisAeon.WriteToLog("An out-of-bounds index to thatstar was encountered when processing the input: " + ThisRequest.RawInput);
                }
                else if (TemplateNode.Attributes != null && TemplateNode.Attributes.Count == 1)
                {
                    if (TemplateNode.Attributes[0].Name.ToLower() == "index")
                    {
                        if (TemplateNode.Attributes[0].Value.Length > 0)
                        {
                            try
                            {
                                int result = Convert.ToInt32(TemplateNode.Attributes[0].Value.Trim());
                                if (Query.ThatStar.Count > 0)
                                {
                                    if (result > 0)
                                    {
                                        return (string)Query.ThatStar[result - 1];
                                    }
                                    else
                                    {
                                        ThisAeon.WriteToLog("An input tag with a badly formed index (" + TemplateNode.Attributes[0].Value + ") was encountered processing the input: " + ThisRequest.RawInput);
                                    }
                                }
                                else
                                {
                                    ThisAeon.WriteToLog("An out-of-bounds index to thatstar was encountered when processing the input: " + ThisRequest.RawInput);
                                }
                            }
                            catch
                            {
                                ThisAeon.WriteToLog("A thatstar tag with a badly formed index (" + TemplateNode.Attributes[0].Value + ") was encountered processing the input: " + ThisRequest.RawInput);
                            }
                        }
                    }
                }
            }
            return string.Empty;
        }
    }
}
