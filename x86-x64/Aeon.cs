using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Mail;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using Animals.Core.CoreTagHandlers;
using Animals.Core.Utililties;

namespace Animals.Core
{
    public class Aeon
    {
        public bool Initialized { get; set; }
        public string Name { get; set; }
        public enum Platform
        {
            WinMobile, Liva, Laptop, Basic, Variant
        }
        public enum Language
        {
            English, German, French, Spanish, Italian, Sal
        }
        /// <summary>
        /// A dictionary object that looks after all the settings associated with this bot
        /// </summary>
        public SettingsDictionary GlobalSettings;
        /// <summary>
        /// A dictionary of all the gender based substitutions used by this bot
        /// </summary>
        public SettingsDictionary GenderSubstitutions;
        /// <summary>
        /// A dictionary of all the first person to second person (and back) substitutions
        /// </summary>
        public SettingsDictionary Person2Substitutions;
        /// <summary>
        /// A dictionary of first / third person substitutions
        /// </summary>
        public SettingsDictionary PersonSubstitutions;
        /// <summary>
        /// Generic substitutions that take place during the normalization process
        /// </summary>
        public SettingsDictionary Substitutions;
        /// <summary>
        /// The default predicates to set up for a user
        /// </summary>
        public SettingsDictionary DefaultPredicates;
        /// <summary>
        /// Holds information about the available custom tag handling classes (if loaded)
        /// Key = class name
        /// Value = TagHandler class that provides information about the class
        /// </summary>
        private readonly Dictionary<string, TagHandler> _customTags;
        /// <summary>
        /// Holds references to the assemblies that hold the custom tag handling code.
        /// </summary>
        private readonly Dictionary<string, Assembly> _lateBindingAssemblies = new Dictionary<string, Assembly>();
        /// <summary>
        /// A list containing the tokens used to split the input into sentences during the normalization process.
        /// </summary>
        public List<string> Splitters = new List<string>();
        /// <summary>
        /// A buffer to hold log messages to be written out to the log file when a max size is reached
        /// </summary>
        private readonly List<string> _logBuffer = new List<string>();
        /// <summary>
        /// The loaded aeon in memory from binary file.
        /// </summary>
        public bool BinaryFileLoadedIntoMemory;
        /// <summary>
        /// How big to let the log buffer get before writing to disk
        /// </summary>
        private int MaxLogBufferSize
        {
            get
            {
                return Convert.ToInt32(GlobalSettings.GrabSetting("maxlogbuffersize"));
            }
        }
        /// <summary>
        /// Flag to show if the bot is willing to accept user input
        /// </summary>
        public bool IsAcceptingUserInput = true;
        /// <summary>
        /// The message to show if a user tries to use the bot whilst it is set to not process user input
        /// </summary>
        private string NotAcceptingUserInputMessage
        {
            get
            {
                return GlobalSettings.GrabSetting("notacceptinguserinputmessage");
            }
        }
        /// <summary>
        /// The maximum amount of time a request should take (in milliseconds)
        /// </summary>
        public double TimeOut
        {
            get
            {
                return Convert.ToDouble(GlobalSettings.GrabSetting("timeout"));
            }
        }
        /// <summary>
        /// The message to display in the event of a timeout
        /// </summary>
        public string TimeOutMessage
        {
            get
            {
                return GlobalSettings.GrabSetting("timeoutmessage");
            }
        }
        /// <summary>
        /// The locale of the bot as a CultureInfo object
        /// </summary>
        public CultureInfo Locale
        {
            get
            {
                return new CultureInfo(GlobalSettings.GrabSetting("culture"));
            }
        }
        /// <summary>
        /// Will match all the illegal characters that might be inputted by the user
        /// </summary>
        public Regex Strippers
        {
            get
            {
                return new Regex(GlobalSettings.GrabSetting("stripperregex"), RegexOptions.IgnorePatternWhitespace);
            }
        }
        /// <summary>
        /// The email address of the botmaster to be used if WillCallHome is set to true
        /// </summary>
        public string AdminEmail
        {
            get
            {
                return GlobalSettings.GrabSetting("adminemail");
            }
            set
            {
                if (value.Length > 0)
                {
                    // check that the email is valid
                    string patternStrict = @"^(([^<>()[\]\\.,;:\s@\""]+"
                    + @"(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@"
                    + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}"
                    + @"\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+"
                    + @"[a-zA-Z]{2,}))$";
                    Regex reStrict = new Regex(patternStrict);

                    if (reStrict.IsMatch(value))
                    {
                        // update the settings
                        GlobalSettings.AddSetting("adminemail", value);
                    }
                    else
                    {
                        WriteToLog("The AdminEmail is not a valid email address");
                    }
                }
                else
                {
                    GlobalSettings.AddSetting("adminemail", "");
                }
            }
        }
        /// <summary>
        /// Flag to denote if the bot is writing messages to its logs
        /// </summary>
        public bool IsLogging
        {
            get
            {
                string islogging = GlobalSettings.GrabSetting("islogging");
                if (islogging.ToLower() == "true")
                {
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// Flag to denote if the bot will email the botmaster using the AdminEmail setting should an error
        /// occur
        /// </summary>
        public bool WillCallHome
        {
            get
            {
                string willcallhome = GlobalSettings.GrabSetting("willcallhome");
                if (willcallhome.ToLower() == "true")
                {
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// When the Bot was initialised
        /// </summary>
        public DateTime StartedOn = DateTime.Now;
        /// <summary>
        /// The supposed sex of the bot
        /// </summary>
        public Gender Sex
        {
            get
            {
                int sex = Convert.ToInt32(GlobalSettings.GrabSetting("gender"));
                Gender result;
                switch (sex)
                {
                    case -1:
                        result = Gender.Unknown;
                        break;
                    case 0:
                        result = Gender.Female;
                        break;
                    case 1:
                        result = Gender.Male;
                        break;
                    default:
                        result = Gender.Unknown;
                        break;
                }
                return result;
            }
        }
        /// <summary>
        /// The directory to look in for the files
        /// </summary>
        public string PathToPersonalityFiles
        {
            get
            {
                return Path.Combine(Environment.CurrentDirectory, GlobalSettings.GrabSetting("personalitydirectory"));
            }
        }
        /// <summary>
        /// Gets or sets the server path configuration files.
        /// </summary>
        public string ServerPathConfigFiles { get; set; }
        /// <summary>
        /// The directory to look in for the various XML configuration files
        /// </summary>
        public string PathToConfigFiles
        {
            get
            {
                return Path.Combine(Environment.CurrentDirectory, GlobalSettings.GrabSetting("configdirectory"));
            }
        }
        /// <summary>
        /// The directory into which the various log files will be written
        /// </summary>
        public string PathToLogs
        {
            get
            {
                return Path.Combine(Environment.CurrentDirectory, GlobalSettings.GrabSetting("logdirectory"));
            }
        }
        /// <summary>
        /// The number of categories this bot has in its graphmaster "brain"
        /// </summary>
        public int Size;
        /// <summary>
        /// The "brain" of the bot
        /// </summary>
        public Node Presence;
        /// <summary>
        /// If set to false the input from personality files will undergo the same normalization process that
        /// user input goes through. If true the bot will assume the format is correct. Defaults to true.
        /// </summary>
        public bool TrustPersonalityFiles = true;
        /// <summary>
        /// The maximum number of characters a "that" element of a path is allowed to be. Anything above
        /// this length will cause "that" to be "*". This is to avoid having the graphmaster process
        /// huge "that" elements in the path that might have been caused by the bot reporting third party
        /// data.
        /// </summary>
        public int MaxThatSize = 256;
        public delegate void LogMessageDelegate();
        public event LogMessageDelegate WrittenToLog;
        /// <summary>
        /// Initializes a new instance of the <see cref="Aeon"/> class.
        /// </summary>
        public Aeon()
        {
            GlobalSettings = new SettingsDictionary(this);
            GenderSubstitutions = new SettingsDictionary(this);
            Person2Substitutions = new SettingsDictionary(this);
            PersonSubstitutions = new SettingsDictionary(this);
            Substitutions = new SettingsDictionary(this);
            DefaultPredicates = new SettingsDictionary(this);
            _customTags = new Dictionary<string, TagHandler>();
            Presence = new Node();
        }
        public void LoadPersonalityFromFiles(Language language)
        {
            PersonalityLoader loader = new PersonalityLoader(this);
            loader.LoadPersonality(language);
        }
        public void LoadPersonalityFromXml(XmlDocument newFiles, string filename)
        {
            PersonalityLoader loader = new PersonalityLoader(this);
            loader.LoadXml(newFiles, filename);
        }
        public void LoadSettings()
        {
            string path = Path.Combine(Environment.CurrentDirectory, Path.Combine("config", "Settings.xml"));
            ServerPathConfigFiles = Path.Combine(Environment.CurrentDirectory, "config");
            LoadSettings(path);
            ServerPathConfigFiles = Environment.CurrentDirectory;
        }
        public void LoadSettings(string pathToSettings)
        {
            GlobalSettings.LoadSettings(pathToSettings);
            // Check if any default settings are missing.
            if (!GlobalSettings.ContainsSettingCalled("version"))
            {
                GlobalSettings.AddSetting("version", Environment.Version.ToString());
            }
            if (!GlobalSettings.ContainsSettingCalled("name"))
            {
                GlobalSettings.AddSetting("name", "Unknown");
            }
            if (!GlobalSettings.ContainsSettingCalled("botmaster"))
            {
                GlobalSettings.AddSetting("botmaster", "Unknown");
            }
            if (!GlobalSettings.ContainsSettingCalled("master"))
            {
                GlobalSettings.AddSetting("botmaster", "Unknown");
            }
            if (!GlobalSettings.ContainsSettingCalled("author"))
            {
                GlobalSettings.AddSetting("author", "Nicholas H.Tollervey");
            }
            if (!GlobalSettings.ContainsSettingCalled("location"))
            {
                GlobalSettings.AddSetting("location", "Unknown");
            }
            if (!GlobalSettings.ContainsSettingCalled("gender"))
            {
                GlobalSettings.AddSetting("gender", "-1");
            }
            if (!GlobalSettings.ContainsSettingCalled("birthday"))
            {
                GlobalSettings.AddSetting("birthday", "2006/11/08");
            }
            if (!GlobalSettings.ContainsSettingCalled("birthplace"))
            {
                GlobalSettings.AddSetting("birthplace", "Towcester, Northamptonshire, UK");
            }
            if (!GlobalSettings.ContainsSettingCalled("website"))
            {
                GlobalSettings.AddSetting("website", "http://summy.com");
            }
            if (GlobalSettings.ContainsSettingCalled("adminemail"))
            {
                string emailToCheck = GlobalSettings.GrabSetting("adminemail");
                AdminEmail = emailToCheck;
            }
            else
            {
                GlobalSettings.AddSetting("adminemail", "");
            }
            if (!GlobalSettings.ContainsSettingCalled("islogging"))
            {
                GlobalSettings.AddSetting("islogging", "False");
            }
            if (!GlobalSettings.ContainsSettingCalled("willcallhome"))
            {
                GlobalSettings.AddSetting("willcallhome", "False");
            }
            if (!GlobalSettings.ContainsSettingCalled("timeout"))
            {
                GlobalSettings.AddSetting("timeout", "2000");
            }
            if (!GlobalSettings.ContainsSettingCalled("timeoutmessage"))
            {
                GlobalSettings.AddSetting("timeoutmessage", "The request has timed out.");
            }
            if (!GlobalSettings.ContainsSettingCalled("culture"))
            {
                GlobalSettings.AddSetting("culture", "en-US");
            }
            if (!GlobalSettings.ContainsSettingCalled("splittersfile"))
            {
                GlobalSettings.AddSetting("splittersfile", "Splitters.xml");
            }
            if (!GlobalSettings.ContainsSettingCalled("person2substitutionsfile"))
            {
                GlobalSettings.AddSetting("person2substitutionsfile", "Person2Substitutions.xml");
            }
            if (!GlobalSettings.ContainsSettingCalled("personsubstitutionsfile"))
            {
                GlobalSettings.AddSetting("personsubstitutionsfile", "PersonSubstitutions.xml");
            }
            if (!GlobalSettings.ContainsSettingCalled("gendersubstitutionsfile"))
            {
                GlobalSettings.AddSetting("gendersubstitutionsfile", "GenderSubstitutions.xml");
            }
            if (!GlobalSettings.ContainsSettingCalled("defaultpredicates"))
            {
                GlobalSettings.AddSetting("defaultpredicates", "DefaultPredicates.xml");
            }
            if (!GlobalSettings.ContainsSettingCalled("substitutionsfile"))
            {
                GlobalSettings.AddSetting("substitutionsfile", "Substitutions.xml");
            }
            if (!GlobalSettings.ContainsSettingCalled("personalitydirectory"))
            {
                GlobalSettings.AddSetting("personalitydirectory", "aeon");
            }
            if (!GlobalSettings.ContainsSettingCalled("configdirectory"))
            {
                GlobalSettings.AddSetting("configdirectory", "config");
            }
            if (!GlobalSettings.ContainsSettingCalled("logdirectory"))
            {
                GlobalSettings.AddSetting("logdirectory", "logs");
            }
            if (!GlobalSettings.ContainsSettingCalled("maxlogbuffersize"))
            {
                GlobalSettings.AddSetting("maxlogbuffersize", "64");
            }
            if (!GlobalSettings.ContainsSettingCalled("notacceptinguserinputmessage"))
            {
                GlobalSettings.AddSetting("notacceptinguserinputmessage", "This bot is currently set to not accept user input.");
            }
            if (!GlobalSettings.ContainsSettingCalled("stripperregex"))
            {
                GlobalSettings.AddSetting("stripperregex", "[^0-9a-zA-Z]");
            }
            // Load the dictionaries and formatter for this aeon from the various configuration files.
            Person2Substitutions.LoadSettings(Path.Combine(ServerPathConfigFiles, GlobalSettings.GrabSetting("person2substitutionsfile")));
            PersonSubstitutions.LoadSettings(Path.Combine(ServerPathConfigFiles, GlobalSettings.GrabSetting("personsubstitutionsfile")));
            GenderSubstitutions.LoadSettings(Path.Combine(ServerPathConfigFiles, GlobalSettings.GrabSetting("gendersubstitutionsfile")));
            DefaultPredicates.LoadSettings(Path.Combine(ServerPathConfigFiles, GlobalSettings.GrabSetting("defaultpredicates")));
            Substitutions.LoadSettings(Path.Combine(ServerPathConfigFiles, GlobalSettings.GrabSetting("substitutionsfile")));
            LoadSplitters(Path.Combine(ServerPathConfigFiles, GlobalSettings.GrabSetting("splittersfile")));
        }
        private void LoadSplitters(string pathToSplitters)
        {
            FileInfo splittersFile = new FileInfo(pathToSplitters);
            if (splittersFile.Exists)
            {
                XmlDocument splittersXmlDoc = new XmlDocument();
                splittersXmlDoc.Load(pathToSplitters);
                // the XML should have an XML declaration like this:
                // <?xml version="1.0" encoding="utf-8" ?> 
                // followed by a <root> tag with children of the form:
                // <item value="value"/>
                if (splittersXmlDoc.ChildNodes.Count == 2)
                {
                    if (splittersXmlDoc.LastChild.HasChildNodes)
                    {
                        foreach (XmlNode myNode in splittersXmlDoc.LastChild.ChildNodes)
                        {
                            if (myNode.Attributes != null && (myNode.Name == "item") && (myNode.Attributes.Count == 1))
                            {
                                string value = myNode.Attributes["value"].Value;
                                Splitters.Add(value);
                            }
                        }
                    }
                }
            }
            if (Splitters.Count == 0)
            {
                // No splitters, some defaults.
                Splitters.Add(".");
                Splitters.Add("!");
                Splitters.Add("?");
                Splitters.Add(";");
            }
        }
        /// <summary>
        /// The last message to be entered into the log (for testing purposes)
        /// </summary>
        public string LastLogMessage = string.Empty;
        /// <summary>
        /// Writes a (timestamped) message to the bot's log.
        /// 
        /// Log files have the form of yyyyMMdd.log.
        /// </summary>
        /// <param name="message">The message to log</param>
        public void WriteToLog(string message)
        {
            LastLogMessage = message;
            if (IsLogging)
            {
                _logBuffer.Add(DateTime.Now.ToString(CultureInfo.InvariantCulture) + ": " + message + Environment.NewLine);
                if (MaxLogBufferSize - 1 >_logBuffer.Count)
                {
                    DirectoryInfo logDirectory = new DirectoryInfo(PathToLogs);
                    if (!logDirectory.Exists)
                    {
                        logDirectory.Create();
                    }
                    string logFileName = DateTime.Now.ToString("yyyyMMdd") + ".log";
                    FileInfo logFile = new FileInfo(Path.Combine(PathToLogs, logFileName));
                    StreamWriter writer;
                    if (!logFile.Exists)
                    {
                        writer = logFile.CreateText();
                    }
                    else
                    {
                        writer = logFile.AppendText();
                    }
                    foreach (string msg in _logBuffer)
                    {
                        writer.WriteLine(msg);
                    }
                    writer.Close();
                    _logBuffer.Clear();
                }
            }
            if (!Equals(null, WrittenToLog))
            {
                WrittenToLog();
            }
        }
        public Result Chat(string rawInput, string userGuid)
        {
            Request request = new Request(rawInput, new User(userGuid, this), this);
            return Chat(request);
        }
        public Result Chat(Request request)
        {
            Result result = new Result(request.ThisUser, this, request);

            if (IsAcceptingUserInput)
            {
                //ToDo: here we are, scripting as we go...the one that you script, scripts you in so many ways.
                // Normalize the input.
                PersonalityLoader loader = new PersonalityLoader(this);
                Normalize.SplitIntoSentences splitter = new Normalize.SplitIntoSentences(this);
                string[] rawSentences = splitter.Transform(request.RawInput);
                foreach (string sentence in rawSentences)
                {
                    result.InputSentences.Add(sentence);
                    string path = loader.GeneratePath(sentence, request.ThisUser.GetLastOutput(), request.ThisUser.Topic, true);
                    result.NormalizedPaths.Add(path);
                }
                // Grab the templates for the various sentences.
                foreach (string path in result.NormalizedPaths)
                {
                    SubQuery query = new SubQuery(path);
                    query.Template = Presence.Evaluate(path, query, request, MatchState.UserInput, new StringBuilder());
                    result.SubQueries.Add(query);
                }
                // Process the templates into appropriate output.
                foreach (SubQuery query in result.SubQueries)
                {
                    if (query.Template.Length > 0)
                    {
                        try
                        {
                            XmlNode templateNode = CoreTagHandler.GetNode(query.Template);
                            string outputSentence = ProcessNode(templateNode, query, request, result, request.ThisUser);
                            if (outputSentence.Length > 0)
                            {
                                result.OutputSentences.Add(outputSentence);
                            }
                        }
                        catch (Exception e)
                        {
                            if (WillCallHome)
                            {
                                PhoneHome(e.Message, request);
                            }
                            WriteToLog("A problem was encountered when trying to process the input: " + request.RawInput + " with the template: \"" + query.Template + "\"");
                        }
                    }
                }
            }
            else
            {
                result.OutputSentences.Add(NotAcceptingUserInputMessage);
            }
            // Populate the result.
            result.Duration = DateTime.Now - request.StartedOn;
            request.ThisUser.AddResult(result);

            return result;
        }
        private string ProcessNode(XmlNode node, SubQuery query, Request request, Result result, User user)
        {
            // Check for timeout (to avoid infinite loops).
            if (request.StartedOn.AddMilliseconds(request.ThisAeon.TimeOut) < DateTime.Now)
            {
                request.ThisAeon.WriteToLog("Request timeout. User: " + request.ThisUser.UserId + " raw input: \"" + request.RawInput + "\" processing template: \"" + query.Template + "\"");
                request.HasTimedOut = true;
                return string.Empty;
            }
            // Process the node.
            string tagName = node.Name.ToLower();
            if (tagName == "template")
            {
                StringBuilder templateResult = new StringBuilder();
                if (node.HasChildNodes)
                {
                    // Recursively check.
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        templateResult.Append(ProcessNode(childNode, query, request, result, user));
                    }
                }
                return templateResult.ToString();
            }
            var tagHandler = GetBespokeTags(user, query, request, result, node);
            if (Equals(null, tagHandler))
            {
                switch (tagName)
                {
                    case "bot":
                        tagHandler = new BotElement(this, user, query, request, result, node);
                        break;
                    case "condition":
                        tagHandler = new Condition(this, user, query, request, result, node);
                        break;
                    case "date":
                        tagHandler = new Date(this, user, query, request, result, node);
                        break;
                    case "formal":
                        tagHandler = new Formal(this, user, query, request, result, node);
                        break;
                    case "gender":
                        tagHandler = new GenderElement(this, user, query, request, result, node);
                        break;
                    case "get":
                        tagHandler = new Get(this, user, query, request, result, node);
                        break;
                    case "gossip":
                        tagHandler = new Gossip(this, user, query, request, result, node);
                        break;
                    case "id":
                        tagHandler = new Id(this, user, query, request, result, node);
                        break;
                    case "input":
                        tagHandler = new Input(this, user, query, request, result, node);
                        break;
                    case "learn":
                        tagHandler = new Learn(this, user, query, request, result, node);
                        break;
                    case "lowercase":
                        tagHandler = new Lowercase(this, user, query, request, result, node);
                        break;
                    case "person":
                        tagHandler = new Person(this, user, query, request, result, node);
                        break;
                    case "person2":
                        tagHandler = new Person2(this, user, query, request, result, node);
                        break;
                    case "random":
                        tagHandler = new RandomElement(this, user, query, request, result, node);
                        break;
                    case "sentence":
                        tagHandler = new Sentence(this, user, query, request, result, node);
                        break;
                    case "set":
                        tagHandler = new Set(this, user, query, request, result, node);
                        break;
                    case "size":
                        tagHandler = new Size(this, user, query, request, result, node);
                        break;
                    case "sr":
                        tagHandler = new Sr(this, user, query, request, result, node);
                        break;
                    case "srai":
                        tagHandler = new Srai(this, user, query, request, result, node);
                        break;
                    case "star":
                        tagHandler = new Star(this, user, query, request, result, node);
                        break;
                    case "that":
                        tagHandler = new That(this, user, query, request, result, node);
                        break;
                    case "thatstar":
                        tagHandler = new ThatStar(this, user, query, request, result, node);
                        break;
                    case "think":
                        tagHandler = new Think(this, user, query, request, result, node);
                        break;
                    case "topicstar":
                        tagHandler = new TopicStar(this, user, query, request, result, node);
                        break;
                    case "uppercase":
                        tagHandler = new Uppercase(this, user, query, request, result, node);
                        break;
                    case "version":
                        tagHandler = new VersionElement(this, user, query, request, result, node);
                        break;
                }
            }
            if (Equals(null, tagHandler))
            {
                return node.InnerText;
            }
            if (tagHandler.IsRecursive)
            {
                if (node.HasChildNodes)
                {
                    // Recursively check.
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        if (childNode.NodeType != XmlNodeType.Text)
                        {
                            childNode.InnerXml = ProcessNode(childNode, query, request, result, user);
                        }
                    }
                }
                return tagHandler.Transform();
            }
            string resultNodeInnerXml = tagHandler.Transform();
            XmlNode resultNode = CoreTagHandler.GetNode("<node>" + resultNodeInnerXml + "</node>");
            if (resultNode.HasChildNodes)
            {
                StringBuilder recursiveResult = new StringBuilder();
                // Recursively check.
                foreach (XmlNode childNode in resultNode.ChildNodes)
                {
                    recursiveResult.Append(ProcessNode(childNode, query, request, result, user));
                }
                return recursiveResult.ToString();
            }
            return resultNode.InnerXml;
        }
        public CoreTagHandler GetBespokeTags(User user, SubQuery query, Request request, Result result, XmlNode node)
        {
            if (_customTags.ContainsKey(node.Name.ToLower()))
            {
                TagHandler customTagHandler = _customTags[node.Name.ToLower()];
                CoreTagHandler newCustomTag = customTagHandler.Instantiate(_lateBindingAssemblies);
                if (Equals(null, newCustomTag))
                {
                    return null;
                }
                newCustomTag.ThisUser = user;
                newCustomTag.Query = query;
                newCustomTag.ThisRequest = request;
                newCustomTag.ThisResult = result;
                newCustomTag.TemplateNode = node;
                newCustomTag.ThisAeon = this;

                return newCustomTag;
            }
            return null;
        }

        #region Serialization

        /// <summary>
        /// Saves the aeon node (and children) to a binary file.
        /// </summary>
        /// <param name="path">The path to the file for saving</param>
        /// <param name="fileName">The name of the binary file to save</param>
        public void SaveToBinaryFile(string path, string fileName)
        {
            // Check to delete an existing version of the file.
            FileInfo fi = new FileInfo(path);
            if (fi.Exists)
            {
                fi.Delete();
            }
            try
            {
                FileStream saveFile = File.Create(path + @"\" + fileName);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(saveFile, Presence);
                saveFile.Close();
            }
            catch (Exception ex)
            {
                WriteToLog(ex.Message);
            }
            finally
            {
                BinaryFileLoadedIntoMemory = true;
            }
        }
        /// <summary>
        /// Loads a dump of the aeon into memory.
        /// </summary>
        /// <param name="path">The path to the dump file</param>
        /// <param name="fileName">The file to retrieve</param>
        public Node LoadFromBinaryFile(string path, string fileName)
        {
            FileStream loadFile = File.OpenRead(path + @"\" + fileName);
            BinaryFormatter bf = new BinaryFormatter();
            try
            {
                Presence = (Node)bf.Deserialize(loadFile);
                loadFile.Close();
            }
            catch (Exception ex)
            {
                WriteToLog(ex.Message);
            }
            
            return Presence;
        }

        #endregion

        #region Latebinding custom-tag library handlers

        /// <summary>
        /// Loads any custom tag handlers found in the library referenced in the argument.
        /// </summary>
        /// <param name="pathToLibrary">the path to the library containing the custom tag handling code</param>
        public void LoadCustomTagHandlers(string pathToLibrary)
        {
            Assembly tagAssembly = Assembly.LoadFrom(pathToLibrary);
            Type[] tagAssemblyTypes = tagAssembly.GetTypes();
            for (int i = 0; i < tagAssemblyTypes.Length; i++)
            {
                object[] typeCustomAttributes = tagAssemblyTypes[i].GetCustomAttributes(false);
                for (int j = 0; j < typeCustomAttributes.Length; j++)
                {
                    if (typeCustomAttributes[j] is CustomTagAttribute)
                    {
                        // We've found a custom tag handling class, so store the assembly and store it away in the Dictionary<,> as a TagHandler class for later usage.
                        // Store the assembly.
                        if (!_lateBindingAssemblies.ContainsKey(tagAssembly.FullName))
                        {
                            _lateBindingAssemblies.Add(tagAssembly.FullName, tagAssembly);
                        }
                        // Create the new tag-handler representation.
                        TagHandler newTagHandler = new TagHandler
                        {
                            AssemblyName = tagAssembly.FullName,
                            ClassName = tagAssemblyTypes[i].FullName,
                            TagName = tagAssemblyTypes[i].Name.ToLower()
                        };
                        if (_customTags.ContainsKey(newTagHandler.TagName))
                        {
                            WriteToLog("Unable to add the custom tag: <" + newTagHandler.TagName + ">, found in: " + pathToLibrary + " as a handler for this tag already exists.");
                        }
                        _customTags.Add(newTagHandler.TagName, newTagHandler);
                    }
                }
            }
        }
        #endregion

        #region Phone Home
        /// <summary>
        /// Attempts to send an email to the caretaker at the AdminEmail address setting with error messages resulting from an interaction.
        /// </summary>
        /// <param name="errorMessage">The resulting error message</param>
        /// <param name="request">The request object that encapsulates all sorts of useful information</param>
        public void PhoneHome(string errorMessage, Request request)
        {
            MailMessage msg = new MailMessage("donotreply@summy.com", AdminEmail)
            {
                Subject = "The program has encountered a problem."
            };
            string message = @"Dear Maestro,

This is an automatically generated email to report errors with your aeon.

At *TIME* the aeon encountered the following error:

""*MESSAGE*""

whilst processing the following input:

""*RAWINPUT*""

from the user with an id of: *USER*

The normalized paths generated by the raw input were as follows:

*PATHS*

Personality malfunction!

Regards,

The M.E.
";
            message = message.Replace("*TIME*", DateTime.Now.ToString(CultureInfo.InvariantCulture));
            message = message.Replace("*MESSAGE*", errorMessage);
            message = message.Replace("*RAWINPUT*", request.RawInput);
            message = message.Replace("*USER*", request.ThisUser.UserId);
            StringBuilder paths = new StringBuilder();
            foreach (string path in request.ThisResult.NormalizedPaths)
            {
                paths.Append(path + Environment.NewLine);
            }
            message = message.Replace("*PATHS*", paths.ToString());
            msg.Body = message;
            msg.IsBodyHtml = false;
            try
            {
                if (msg.To.Count > 0)
                {
                    SmtpClient client = new SmtpClient();
                    client.Send(msg);
                }
            }
            catch
            {
                // if we get here then we can't really do much more
            }
        }
        #endregion
    }
}
