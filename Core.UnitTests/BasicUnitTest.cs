using System;
using System.Diagnostics;
using Animals.Core;
using Animals.Core.Addins;
using Animals.Core.Reason;
using Animals.Core.Utililties;
using NUnit.Framework;
using NLua;

namespace Core.UnitTests
{
    [TestFixture]
    public class BasicUnitTest
    {
        public bool Initialized;
        public bool PureReasonActive;
        private Lua _lua;
        private ScriptHost _scriptHost;
        public bool Platform;

        #region Fixture - using m.e. as user
        [SetUp]
        public void Intialize()
        {
            Platform = false;
            _scriptHost = new ScriptHost();
            _lua = new Lua();
            Presence = new Aeon();
            PureReasonActive = true;
            ThisLanguage = Aeon.Language.English;
            ThisPlatform = Aeon.Platform.Sum;
            // Todo: Presence.LoadFromBinaryFile(Path.Combine(Environment.CurrentDirectory, "bins"), "newt.bph");
            if (!Presence.BinaryFileLoadedIntoMemory && ThisPlatform != Aeon.Platform.WinMobile && ThisPlatform != Aeon.Platform.Liva && ThisPlatform != Aeon.Platform.Sum && ThisPlatform != Aeon.Platform.Summy)
            {
                var loader = new PersonalityLoader(Presence);
                Presence.LoadSettings();
                try
                {
                    loader.LoadPersonality(ThisLanguage);
                    Initialized = true;
                }
                catch (Exception ex)
                {
                    Presence.WriteToLog(ex.Message);
                }
            }
            else switch (ThisPlatform)
                {
                    case Aeon.Platform.WinMobile:
                        {
                            var loader = new PersonalityLoader(Presence);
                            Presence.LoadSettings();
                            try
                            {
                                loader.LoadPlatform(ThisPlatform);
                                Initialized = true;
                            }
                            catch (Exception ex)
                            {
                                Presence.WriteToLog(ex.Message);
                            }
                        }
                        break;
                    case Aeon.Platform.Liva:
                        {
                            var loader = new PersonalityLoader(Presence);
                            Presence.LoadSettings();
                            try
                            {
                                loader.LoadPlatform(ThisPlatform);
                                Initialized = true;
                            }
                            catch (Exception ex)
                            {
                                Presence.WriteToLog(ex.Message);
                            }
                        }
                        break;
                    case Aeon.Platform.Sum:
                        {
                            var loader = new PersonalityLoader(Presence);
                            Presence.LoadSettings();
                            try
                            {
                                loader.LoadPlatform(ThisPlatform);
                                Initialized = true;
                            }
                            catch (Exception ex)
                            {
                                Presence.WriteToLog(ex.Message);
                            }
                        }
                        break;
                        case Aeon.Platform.Summy:
                    {
                            var loader = new PersonalityLoader(Presence);
                            Presence.LoadSettings();
                            try
                            {
                                loader.LoadPlatform(ThisPlatform);
                                Initialized = true;
                            }
                            catch (Exception ex)
                            {
                                Presence.WriteToLog(ex.Message);
                            }
                        }
                        break;
                }

        }

        [TearDown]
        public void SaveState()
        {
            // Todo: Presence.SaveToBinaryFile(Path.Combine(Presence.ServerPathConfigFiles, "bins"), "newt.bph");
        }
        public Aeon.Language ThisLanguage { get; set; }
        public Aeon.Platform ThisPlatform { get; set; }
        public Aeon Presence { get; set; }
        public Result ThisResult { get; set; }
        public Request ThisRequest { get; set; }
        public User ThisUser { get; set; }
        protected void Send(string rawInput)
        {
            ThisUser = new User(@"m.e.", Presence);
            ThisRequest = new Request(rawInput, ThisUser, Presence);
            ThisResult = new Result(ThisUser, Presence, ThisRequest);
            ThisResult = Presence.Chat(ThisRequest);
        }
        #endregion

        //[TestCase("Hello.")]
        [TestCase("Do you know me?")]
        [TestCase("My name is John.")]
        [TestCase("What is my name?")]
        //[TestCase("Franky is in Gateau.")]
        //[TestCase("No, I am not Franky.")]
        //[TestCase("Franky is a Gateau.")]
        public void HelloProto(string input)
        {
            //ThisLanguage = Aeon.Language.English;
            //ThisPlatform = Aeon.Platform.WinMobile;
            Assert.IsTrue(Initialized);
            Send(input);
            Assert.IsNotNull(ThisResult);
            Console.WriteLine(ThisResult.ToString());
        }
        [Test, Description(@"Learning example from: http://alicebot.wikidot.com/learn-aiml")]
        [TestCase("What is a Gateau?")]
        //[TestCase("Learn that Gateau is a type of cake")]
        //[TestCase("What is a Gateau?")]
        public void ProtoLearning(string input)
        {
            ThisLanguage = Aeon.Language.English;
            ThisPlatform = Aeon.Platform.WinMobile;
            Assert.IsTrue(Initialized);
            Send(input);
            Assert.IsNotNull(ThisResult);
            Console.WriteLine(ThisResult.ToString());
        }

        #region Other knock-on tests
        //[TestCase("Hello")]
        //[TestCase("How are you?")]
        //[TestCase("How is it where you are?")]
        //[Reason("Pure", true)]
        //[Kantian(Kant.Gauge.TranscendentalAthestetic, 7, Vetted = true)]
        [TestCase("A hello to you.")]
        [TestCase("Do you know me?")]
        [TestCase("My name is John.")]
        [TestCase("What is my name?")]
        public void Hello(string input)
        {
            // Conversation with all files loaded not working.
            Assert.IsTrue(Initialized);
            Send(input);
            Assert.IsNotNull(ThisResult);
            Console.WriteLine(ThisResult.ToString());
            //Console.WriteLine(KantianAttribute.Value);
            //Console.WriteLine(KantianAttribute.Correction);
        }
        [TestCase("bonjour")]
        [TestCase("Comment allez-vous?")]
        [TestCase("Comment est-il là où vous êtes?")]
        public void Bonjour(string input)
        {
            ThisLanguage = Aeon.Language.French;
            Assert.IsTrue(Initialized);
            Send(input);
            Assert.IsNotNull(ThisResult);
            Console.WriteLine(ThisResult.ToString());
        }
        [Test, Description(@"Learning example from: http://alicebot.wikidot.com/learn-aiml")]
        [TestCase("What is Gateau?")]
        [TestCase("Learn that Gateau is a type of cake")]
        [TestCase("What is a Gateau?")]
        public void LearningExample(string input)
        {
            ThisLanguage = Aeon.Language.English;
            ThisPlatform = Aeon.Platform.Laptop;
            Assert.IsTrue(Initialized);
            Send(input);
            Assert.IsNotNull(ThisResult);
            Console.WriteLine(ThisResult.ToString());
        }
        [Test, Description(@"Script sanity test")]
        public void ScriptWindowingTest()
        {
            const string message = "A message passed through the script engine shown in a window.";
            _lua.RegisterFunction(@"MessageWindow", _scriptHost, _scriptHost.GetType().GetMethod("MessageWindow"));
            _lua.DoString(@"MessageWindow('" + message + "');");
        }
        [Test, Description(@"Script variable manipulations: basic")]
        [TestCase(10)]
        [TestCase(24)]
        [TestCase(55)]
        [TestCase(89)]
        public void ScriptMood(int input)
        {
            var moody = new Personality.Mood(input);
            var moodValue = Personality.Mood.CurrentMood;
            _lua.RegisterFunction(@"ChangeMood", _scriptHost, _scriptHost.GetType().GetMethod("ChangeMood"));
            _lua.DoString(@"ChangeMood('" + moodValue + "');");
        }
        //[Test]
        //[TestCase("lua")]
        //[TestCase("require \"torch\" ")]
        //[TestCase("x=torch.Tensor()")]
        //[TestCase("print(x)")]
        public void CallTorchTest(string command)
        {
            _lua.RegisterFunction(@"CallTorch", _scriptHost, _scriptHost.GetType().GetMethod("CallTorch"));
            // _lua.DoString(@"CallTorch('" + command + "');");
            ExecuteCommand(command);

        }
        public static void ExecuteCommand(string command)
        {
            var processInfo = new ProcessStartInfo("cmd.exe", "/c " + command);
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;
            processInfo.RedirectStandardError = true;
            processInfo.RedirectStandardOutput = true;

            var process = Process.Start(processInfo);

            process.OutputDataReceived += (object sender, DataReceivedEventArgs e) =>
                Console.WriteLine("output>>" + e.Data);
            process.BeginOutputReadLine();

            process.ErrorDataReceived += (object sender, DataReceivedEventArgs e) =>
                Console.WriteLine("error>>" + e.Data);
            process.BeginErrorReadLine();

            process.WaitForExit();

            Console.WriteLine("ExitCode: {0}", process.ExitCode);
            process.Close();
        }
        #endregion
    }
}
