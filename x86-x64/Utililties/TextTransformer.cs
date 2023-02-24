namespace Animals.Core.Utililties
{
    /// <summary>
    /// Encapsulates all the required methods and attributes for any text transformation.
    /// 
    /// An input string is provided and various methods and attributes can be used to grab
    /// a transformed string.
    /// 
    /// The protected ProcessChange() method is abstract and should be overridden to contain 
    /// the code for transforming the input text into the output text.
    /// </summary>
    public abstract class TextTransformer
    {
        /// <summary>
        /// The bot that this transformation is connected with
        /// </summary>
        public Aeon ThisAeon;
        /// <summary>
        /// The input string to be transformed in some way
        /// </summary>
        public string InputString { get; set; }
        /// <summary>
        /// The transformed string
        /// </summary>
        public string OutputString
        {
            get { return Transform(); }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="TextTransformer"/> class.
        /// </summary>
        /// <param name="thisAeon">The bot this transformer is a part of</param>
        /// <param name="inputString">The input string to be transformed</param>
        protected TextTransformer(Aeon thisAeon, string inputString)
        {
            ThisAeon = thisAeon;
            InputString = inputString;
        }
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="thisAeon">The bot this transformer is a part of</param>
        protected TextTransformer(Aeon thisAeon)
        {
            ThisAeon = thisAeon;
            InputString = string.Empty;
        }
        /// <summary>
        /// Default ctor for used as part of late binding mechanism
        /// </summary>
        protected TextTransformer()
        {
            ThisAeon = null;
            InputString = string.Empty;
        }
        /// <summary>
        /// Do a transformation on the supplied input string
        /// </summary>
        /// <param name="input">The string to be transformed</param>
        /// <returns>The resulting output</returns>
        public string Transform(string input)
        {
            InputString = input;
            return Transform();
        }
        /// <summary>
        /// Do a transformation on the string found in the InputString attribute
        /// </summary>
        /// <returns>The resulting transformed string</returns>
        public string Transform()
        {
            if (InputString.Length > 0)
            {
                return ProcessChange();
            }
            return string.Empty;
        }
        /// <summary>
        /// The method that does the actual processing of the text.
        /// </summary>
        /// <returns>The resulting processed text</returns>
        protected abstract string ProcessChange();
    }
}
