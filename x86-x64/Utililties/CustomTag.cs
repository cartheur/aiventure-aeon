using System;

namespace Animals.Core.Utililties
{
    /// <summary>
    /// A custom attribute to be applied to all custom tags in external "late bound" assemblies.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class CustomTagAttribute : Attribute { }
}
