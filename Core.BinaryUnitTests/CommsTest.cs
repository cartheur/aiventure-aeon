using System;
using System.IO;
using Cartheur.Animals.Core;
using Cartheur.Animals.Utilities;
using NUnit.Framework;

namespace Core.BinaryUnitTests
{
    [TestFixture]
    public class CommsTest
    {
        public bool Initialized;
        public string ServerPathConfigFiles { get; set; }
        public string PathUpdateFiles { get; set; }
        public string PathReductionFiles { get; set; }
        public string PathMindpixelFiles { get; set; }
        public string PathPersonalityFiles { get; set; }

        #region Fixture - using m.e. as user
        [TestFixtureSetUp]
        public void Intialize()
        {
            Presence = new Aeon {Name = "Aeon"};
            var loader = new AeonLoader(Presence);
            string path = Path.Combine(Environment.CurrentDirectory, Path.Combine("config", "Settings.xml"));
            ServerPathConfigFiles = Path.Combine(Environment.CurrentDirectory, "config");
            
            try
            {
                Presence.LoadSettings(path);
                PathPersonalityFiles = Path.Combine(Environment.CurrentDirectory, @"personality\lv\rhoda");
                PathMindpixelFiles = Path.Combine(Environment.CurrentDirectory, @"personality\lv\mindpixel");
                PathReductionFiles = Path.Combine(Environment.CurrentDirectory, @"personality\lv\reductions");
                PathUpdateFiles = Path.Combine(Environment.CurrentDirectory, @"personality\lv\update");
                loader.LoadAeon(PathPersonalityFiles);
                //loader.LoadAeon(PathMindpixelFiles);
                //loader.LoadAeon(PathReductionFiles);
                //loader.LoadAeon(PathUpdateFiles);
                Initialized = true;
            }
            catch (Exception ex)
            {
                Presence.WriteToLog(ex.Message, Logging.LogType.Error, Logging.LogCaller.TestFramework);
            }
        }
        [TestFixtureTearDown]
        public void SaveState()
        {
            
        }
        public Aeon Presence { get; set; }
        public Result ThisResult { get; set; }
        public Request ThisRequest { get; set; }
        public User ThisUser { get; set; }
        protected void Send(string rawInput)
        {
            ThisUser = new User(@"Dave", Presence);
            ThisRequest = new Request(rawInput, ThisUser, Presence);
            ThisResult = new Result(ThisUser, Presence, ThisRequest);
            if(Aeon.PersonalityLoaded)
            ThisResult = Presence.Chat(ThisRequest);
        }
        #endregion

        //[TestCase("A hello to you.")]
        //[TestCase("Do you know me?")]
        //[TestCase("My name is John.")]
        //[TestCase("What is my name?")]
        [TestCase("Hello")]
        //[TestCase("Franky is in Gateau.")]
        //[TestCase("No, I am not Franky.")]
        //[TestCase("Franky is a Gateau.")]
        public void HelloProto(string input)
        {
            Assert.IsTrue(Initialized);
            Send(input);
            Assert.IsNotNull(ThisResult);
            Console.WriteLine(ThisResult.ToString());
        }
        [TestCase, Description(@"Learning example from: http://alicebot.wikidot.com/learn-aiml")]
        [TestCase("What is a Gateau?")]
        //[TestCase("Learn that Gateau is a type of cake")]
        //[TestCase("What is a Gateau?")]
        public void ProtoLearning(string input)
        {
            Assert.IsTrue(Initialized);
            Send(input);
            Assert.IsNotNull(ThisResult);
            Console.WriteLine(ThisResult.ToString());
        }
    }
}
