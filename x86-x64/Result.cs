using System;
using System.Collections.Generic;
using System.Text;
using Animals.Core.Utililties;

namespace Animals.Core
{
    /// <summary>
    /// Encapsulates information about the result of a request to the bot
    /// </summary>
    public class Result
    {
        /// <summary>
        /// The bot that is providing the answer
        /// </summary>
        public Aeon ThisAeon;
        /// <summary>
        /// The user for whom this is a result
        /// </summary>
        public User ThisUser;
        /// <summary>
        /// The request from the user
        /// </summary>
        public Request ThisRequest;
        /// <summary>
        /// The raw input from the user
        /// </summary>
        public string RawInput
        {
            get
            {
                return ThisRequest.RawInput;
            }
        }
        /// <summary>
        /// The normalized sentence(s) (paths) fed into the graphmaster
        /// </summary>
        public List<string> NormalizedPaths = new List<string>();
        /// <summary>
        /// The amount of time the request took to process
        /// </summary>
        public TimeSpan Duration;
        /// <summary>
        /// The result from the bot with logging and checking
        /// </summary>
        public string Output
        {
            get
            {
                if (OutputSentences.Count > 0)
                {
                    return RawOutput;
                }
                if (ThisRequest.HasTimedOut)
                {
                    return ThisAeon.TimeOutMessage;
                }
                StringBuilder paths = new StringBuilder();
                foreach (string pattern in NormalizedPaths)
                {
                    paths.Append(pattern + Environment.NewLine);
                }
                ThisAeon.WriteToLog("The bot could not find any response for the input: " + RawInput + " with the path(s): " + Environment.NewLine + paths + " from the user with an id: " + ThisUser.UserId);
                return string.Empty;
            }
        }
        /// <summary>
        /// Returns the raw sentences without any logging 
        /// </summary>
        public string RawOutput
        {
            get
            {
                StringBuilder result = new StringBuilder();
                foreach (string sentence in OutputSentences)
                {
                    string sentenceForOutput = sentence.Trim();
                    if (!CheckEndsAsSentence(sentenceForOutput))
                    {
                        sentenceForOutput += ".";
                    }
                    result.Append(sentenceForOutput + " ");
                }
                return result.ToString().Trim();
            }
        }
        /// <summary>
        /// The subQueries processed by the bot's graphmaster that contain the templates that 
        /// are to be converted into the collection of Sentences
        /// </summary>
        public List<SubQuery> SubQueries = new List<SubQuery>();
        /// <summary>
        /// The individual sentences produced by the bot that form the complete response
        /// </summary>
        public List<string> OutputSentences = new List<string>();
        /// <summary>
        /// The individual sentences that constitute the raw input from the user
        /// </summary>
        public List<string> InputSentences = new List<string>();
        /// <summary>
        /// Initializes a new instance of the <see cref="Result"/> class.
        /// </summary>
        /// <param name="thisUser">The user for whom this is a result</param>
        /// <param name="thisAeon">The bot providing the result</param>
        /// <param name="thisRequest">The request that originated this result</param>
        public Result(User thisUser, Aeon thisAeon, Request thisRequest)
        {
            ThisUser = thisUser;
            ThisAeon = thisAeon;
            ThisRequest = thisRequest;
            ThisRequest.ThisResult = this;
        }
        /// <summary>
        /// Returns the raw output from the bot
        /// </summary>
        /// <returns>The raw output from the bot</returns>
        public override string ToString()
        {
            return Output;
        }
        /// <summary>
        /// Checks that the provided sentence ends with a sentence splitter
        /// </summary>
        /// <param name="sentence">the sentence to check</param>
        /// <returns>True if ends with an appropriate sentence splitter</returns>
        private bool CheckEndsAsSentence(string sentence)
        {
            foreach (string splitter in ThisAeon.Splitters)
            {
                if (sentence.Trim().EndsWith(splitter))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
