using System;
using System.Collections.Generic;
using Animals.Core.Utililties;

namespace Animals.Core
{
    /// <summary>
    /// Encapsulates information and history of a user who has interacted with the bot
    /// </summary>
    public class User
    {
        /// <summary>
        /// The bot this user is using
        /// </summary>
        public Aeon ThisAeon;
        /// <summary>
        /// The GUID that identifies this user to the bot.
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// A collection of all the result objects returned to the user in this session
        /// </summary>
        private readonly List<Result> _results = new List<Result>();
		/// <summary>
		/// the value of the "topic" predicate
		/// </summary>
        public string Topic
        {
            get
            {
                return Predicates.GrabSetting("topic");
            }
        }
		/// <summary>
		/// the predicates associated with this particular user
		/// </summary>
        public SettingsDictionary Predicates;
        /// <summary>
        /// The most recent result to be returned by the bot
        /// </summary>
        public Result LastResult
        {
            get
            {
                if (_results.Count > 0)
                {
                    return (Result)_results[0];
                }
                else
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="userId">The GUID of the user</param>
        /// <param name="aeon">the bot the user is connected to</param>
        /// <exception cref="Exception">The UserID cannot be empty</exception>
		public User(string userId, Aeon aeon)
		{
            if (userId.Length > 0)
            {
                UserId = userId;
                ThisAeon = aeon;
                Predicates = new SettingsDictionary(ThisAeon);
                ThisAeon.DefaultPredicates.Clone(Predicates);
                Predicates.AddSetting("topic", "*");
            }
            else
            {
                ThisAeon.WriteToLog("The UserID cannot be empty");
            }
		}
        /// <summary>
        /// Returns the string to use for the next that part of a subsequent path
        /// </summary>
        /// <returns>the string to use for that</returns>
        public string GetLastOutput()
        {
            if (_results.Count > 0)
            {
                return ((Result)_results[0]).RawOutput;
            }
            return "*";
        }
        /// <summary>
        /// Returns the first sentence of the last output from the bot
        /// </summary>
        /// <returns>the first sentence of the last output from the bot</returns>
        public string GetThat()
        {
            return GetThat(0,0);
        }
        /// <summary>
        /// Returns the first sentence of the output "n" steps ago from the bot
        /// </summary>
        /// <param name="n">the number of steps back to go</param>
        /// <returns>the first sentence of the output "n" steps ago from the bot</returns>
        public string GetThat(int n)
        {
            return GetThat(n, 0);
        }
        /// <summary>
        /// Returns the sentence numbered by "sentence" of the output "n" steps ago from the bot
        /// </summary>
        /// <param name="n">the number of steps back to go</param>
        /// <param name="sentence">the sentence number to get</param>
        /// <returns>the sentence numbered by "sentence" of the output "n" steps ago from the bot</returns>
        public string GetThat(int n, int sentence)
        {
            if ((n >= 0) && (n < _results.Count))
            {
                Result historicResult = (Result)_results[n];
                if ((sentence >= 0) && (sentence < historicResult.OutputSentences.Count))
                {
                    return historicResult.OutputSentences[sentence];
                }
            }
            return string.Empty;
        }
        /// <summary>
        /// Returns the first sentence of the last output from the bot
        /// </summary>
        /// <returns>the first sentence of the last output from the bot</returns>
        public string GetResultSentence()
        {
            return GetResultSentence(0, 0);
        }
        /// <summary>
        /// Returns the first sentence from the output from the bot "n" steps ago
        /// </summary>
        /// <param name="n">the number of steps back to go</param>
        /// <returns>the first sentence from the output from the bot "n" steps ago</returns>
        public string GetResultSentence(int n)
        {
            return GetResultSentence(n, 0);
        }
        /// <summary>
        /// Returns the identified sentence number from the output from the bot "n" steps ago
        /// </summary>
        /// <param name="n">the number of steps back to go</param>
        /// <param name="sentence">the sentence number to return</param>
        /// <returns>the identified sentence number from the output from the bot "n" steps ago</returns>
        public string GetResultSentence(int n, int sentence)
        {
            if ((n >= 0) && (n < _results.Count))
            {
                Result historicResult = (Result)_results[n];
                if ((sentence >= 0) && (sentence < historicResult.InputSentences.Count))
                {
                    return historicResult.InputSentences[sentence];
                }
            }
            return string.Empty;
        }
        /// <summary>
        /// Adds the latest result from the bot to the Results collection
        /// </summary>
        /// <param name="latestResult">the latest result from the bot</param>
        public void AddResult(Result latestResult)
        {
            _results.Insert(0, latestResult);
        }
    }
}
