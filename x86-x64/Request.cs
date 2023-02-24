using System;

namespace Animals.Core
{
    /// <summary>
    /// Encapsulates all sorts of information about a request to the bot for processing
    /// </summary>
    public class Request
    {
        #region Attributes
        /// <summary>
        /// The raw input from the user
        /// </summary>
        public string RawInput;

        /// <summary>
        /// The time at which this request was created within the system
        /// </summary>
        public DateTime StartedOn;

        /// <summary>
        /// The user who made this request
        /// </summary>
        public User ThisUser;

        /// <summary>
        /// The bot to which the request is being made
        /// </summary>
        public Aeon ThisAeon;

        /// <summary>
        /// The final result produced by this request
        /// </summary>
        public Result ThisResult;

        /// <summary>
        /// Flag to show that the request has timed out
        /// </summary>
        public bool HasTimedOut = false;

        #endregion

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="rawInput">The raw input from the user</param>
        /// <param name="thisUser">The user who made the request</param>
        /// <param name="thisAeon">The bot to which this is a request</param>
        public Request(string rawInput, User thisUser, Aeon thisAeon)
        {
            RawInput = rawInput;
            ThisUser = thisUser;
            ThisAeon = thisAeon;
            StartedOn = DateTime.Now;
        }
    }
}
