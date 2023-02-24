using Animals.Core.Utililties;

namespace Animals.Core.Normalize
{
    /// <summary>
    /// Strips any illegal characters found within the input string. Illegal characters are referenced from
    /// the bot's Strippers regex that is defined in the setup XML file.
    /// </summary>
    public class StripIllegalCharacters : TextTransformer
    {
        public StripIllegalCharacters(Aeon thisAeon, string inputString) : base(thisAeon, inputString)
        { }

        public StripIllegalCharacters(Aeon thisAeon)
            : base(thisAeon) 
        { }

        protected override string ProcessChange()
        {
            return ThisAeon.Strippers.Replace(InputString, " ");
        }
    }
}
