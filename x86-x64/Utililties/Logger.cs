using System;
using System.IO;
using System.Text;

namespace Animals.Core.Utililties
{
    public class Logger
    {
        private static bool _fileCreated = false;
        /// <summary>
        /// The file path for executing assemblies.
        /// </summary>
        public static string FilePath()
        {
            return Environment.CurrentDirectory;
        }
        /// <summary>
        /// The type of log to write.
        /// </summary>
        public enum LogType { Information, Error, Gossip, Temporal, Warning };
        /// <summary>
        /// The classes within the interpreter calling the log.
        /// </summary>
        public enum LogCaller { Aeon, AeonGui, AeonLoader, Bot, Cognizance, Condition, Cryptography, Get, Gossip, Input, Interaction, Learn, LearningThread, Me, MonoRuntime, Mood, Node, Result, Script, Set, SharedFunction, SpeechRecognizer, Star, TestFramework, That, ThatStar, Think, TopicStar }
        /// <summary>
        /// The last message passed to logging.
        /// </summary>
        public static string LastMessage = "";
        /// <summary>
        /// The delegate for returning the last log message to the calling application.
        /// </summary>
        public delegate void LoggingDelegate();
        /// <summary>
        /// Occurs when [returned to console] is called.
        /// </summary>
        public static event LoggingDelegate ReturnedToConsole;
        /// <summary>
        /// Logs a message sent from the calling application to a file.
        /// </summary>
        /// <param name="message">The message to log. Space between the message and log type enumeration provided.</param>
        /// <param name="logType">Type of the log.</param>
        /// <param name="caller">The class creating the log entry.</param>
        public static void WriteLog(string message, LogType logType, LogCaller caller)
        {
            LastMessage = message;
            StreamWriter stream = new StreamWriter(FilePath() + @"\logs\logfile.txt", true);
            switch (logType)
            {
                case LogType.Error:
                    stream.WriteLine(DateTime.Now + " - " + " ERROR " + " - " + message + " from class " + caller + ".");
                    break;
                case LogType.Warning:
                    stream.WriteLine(DateTime.Now + " - " + " WARNING " + " - " + message + " from class " + caller + ".");
                    break;
                case LogType.Information:
                    stream.WriteLine(DateTime.Now + " - " + message);
                    break;
                case LogType.Temporal:
                    stream.WriteLine(DateTime.Now + " - " + message + ".");
                    break;
                case LogType.Gossip:
                    stream.WriteLine(DateTime.Now + " - " + message + ".");
                    break;
            }
            stream.Close();
            if (!Equals(null, ReturnedToConsole))
            {
                ReturnedToConsole();
            }
        }

        /// <summary>
        /// Records a transcript of the conversation.
        /// </summary>
        /// <param name="message">The message to save in transcript format.</param>
        /// <param name="insertNewLine">Inserts a new line, if required.</param>
        /// <param name="fileNumber">Use for saving iterative transcript files.</param>
        public static void RecordTranscript(string message, int fileNumber, bool insertNewLine = false)
        {
            try
            {
                StreamWriter stream = new StreamWriter(FilePath() + @"\logs\transcript.txt", true);
                if (!_fileCreated && fileNumber == 0)
                {
                    // File has not been created previously, write the header to the file.
                    stream.WriteLine(@"August 2017" + Environment.NewLine + @"A transcript log for an interative conversation between two aeons, in pursuit of validation / critique of a paper as well as outlining an example of ethical application.");
                    stream.WriteLine(Environment.NewLine);
                    _fileCreated = true;
                }
                if (fileNumber != 0)
                {
                    stream.Dispose();
                    stream = new StreamWriter(FilePath() + @"\logs\transcript" + fileNumber + ".txt", true);
                    if (!_fileCreated)
                    {
                        stream.WriteLine(@"August 2017" + Environment.NewLine + @"A transcript log for an interative conversation between two aeons, in pursuit of validation / critique of a paper as well as outlining an example of ethical application.");
                        stream.WriteLine(Environment.NewLine);
                        _fileCreated = true;
                    }
                }
                if (insertNewLine)
                    stream.WriteLine(Environment.NewLine);
                stream.WriteLine(message);
                stream.Close();
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message, LogType.Error, LogCaller.Me);
            }

        }
        /// <summary>
        /// Saves the last result to support analysis of the algorithm.
        /// </summary>
        /// <param name="output">The output from the conversation.</param>
        public static void SaveAnalyticBit(string output)
        {
            try
            {
                StreamWriter stream = new StreamWriter(FilePath() + @"\logs\analytics.txt", true);
                stream.WriteLine(DateTime.Now + ": " + output);
                stream.Close();
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message, LogType.Error, LogCaller.Me);
            }

        }
        /// <summary>
        /// Saves the last result to support analysis of the algorithm to storage.
        /// </summary>
        /// <param name="output">The output from the conversation.</param>
        public static void SaveToResultStorage(StringBuilder output)
        {
            try
            {
                StreamWriter stream = new StreamWriter(FilePath() + @"\logs\resultstorage.txt", true);
                stream.WriteLine("#" + DateTime.Now + ";" + output);
                stream.Close();
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message, LogType.Error, LogCaller.Me);
            }

        }
        /// <summary>
        /// Writes a debug log with object parameters.
        /// </summary>
        /// <param name="objects">The objects.</param>
        public static void Debug(params object[] objects)
        {
            StreamWriter stream = new StreamWriter(FilePath() + @"\logs\logfile.txt", true);
            foreach (object obj in objects)
            {
                stream.WriteLine(obj);
            }
            stream.WriteLine("--");
            stream.Close();
        }
    }
}
