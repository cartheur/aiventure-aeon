using System;
using Animals.Core;
using Animals.Core.Utililties;
using NUnit.Framework;

namespace Core.UnitTests
{
    [TestFixture]
    public class ConversationalAeonTests
    {
        public bool IsAeonOnline;
        public Aeon.Language ThisLanguage { get; set; }
        public Aeon PresenceOne { get; set; }
        public Aeon PresenceTwo { get; set; }
        public Result ThisOneResult { get; set; }
        public Result ThisTwoResult { get; set; }
        public Request ThisOneRequest { get; set; }
        public Request ThisTwoRequest { get; set; }
        public User ThisUser { get; set; }
        protected void SendOne(string user, Aeon presence, string rawInput)
        {
            ThisUser = new User(user, presence);
            ThisOneRequest = new Request(rawInput, ThisUser, presence);
            ThisOneResult = new Result(ThisUser, presence, ThisOneRequest);
            ThisOneResult = presence.Chat(ThisOneRequest);
        }
        protected void SendTwo(string user, Aeon presence, string rawInput)
        {
            ThisUser = new User(user, presence);
            ThisTwoRequest = new Request(rawInput, ThisUser, presence);
            ThisTwoResult = new Result(ThisUser, presence, ThisTwoRequest);
            ThisTwoResult = presence.Chat(ThisTwoRequest);
        }
        protected void Intialize(string name, Aeon thisPresence)
        {
            var loader = new PersonalityLoader(thisPresence);
            thisPresence.LoadSettings();
            ThisLanguage = Aeon.Language.English;
            try
            {
                loader.LoadPersonality(ThisLanguage);
            }
            catch (Exception ex)
            {
                thisPresence.WriteToLog(ex.Message);
            }
            finally
            {
                IsAeonOnline = thisPresence.Initialized = true;
                thisPresence.Name = name;
            }
        }

        [TestCase(@"Hello.")]
        [TestCase(@"How are you?")]
        [TestCase(@"I think it is hot in here.")]
        [TestCase(@"My name is aeon.")]
        public void GetThemTalking(string input)
        {
            // This version runs backwards, e.g., the outputs are not from the flow you think it is the result of.
            PresenceOne = new Aeon();
            Intialize(@"AeonOne", PresenceOne);
            PresenceTwo = new Aeon();
            Intialize(@"AeonTwo", PresenceTwo);
            Assert.IsTrue(IsAeonOnline);
            SendOne(@"Dave", PresenceOne, input);
            SendTwo(@"Franky", PresenceTwo, ThisOneResult.ToString());
            SendOne(@"Dave", PresenceOne, ThisTwoResult.ToString());
            Assert.IsNotNull(ThisOneResult);
            Assert.IsNotNull(ThisTwoResult);
            Console.WriteLine(PresenceOne.Name + @" says: " + ThisOneResult);
            Console.WriteLine(PresenceTwo.Name + @" says: " + ThisTwoResult);
        }

        //
        // Value for creating sequential transcript files.
        const int FileNumber = 6;
        // How about creating lists of loaded values? How about reproducing what is found in the github repo from the paper? Bring the scripts and data here.

        [TestCase(@"Hello.", FileNumber)]
        [TestCase(@"How are you?", FileNumber)]
        [TestCase(@"I think it is hot in here.", FileNumber)]
        [TestCase(@"My name is aeon.", FileNumber)]
        public void GetThemTalkingLinear(string input, int fileNumber)
        {
            var iterations = 9;
            // This version runs in an incremental way.
            PresenceOne = new Aeon();
            Intialize(@"AeonOne", PresenceOne);
            PresenceTwo = new Aeon();
            Intialize(@"AeonTwo", PresenceTwo);
            Assert.IsTrue(IsAeonOnline);
            // Print the experimental parameters.
            Console.WriteLine(@"With a synthetic input of '" + input + @"'" + @" iterating " + iterations + @" times.");
            Logger.RecordTranscript(@"With a synthetic input of '" + input + @"'" + @" iterating " + iterations + @" times.", FileNumber);
            // Get a conversation started from stimulation inputs.
            SendOne(@"Dave", PresenceOne, input);
            SendTwo(@"Franky", PresenceTwo, ThisOneResult.ToString());
            Console.WriteLine(PresenceOne.Name + @" says: " + ThisOneResult);
            Logger.RecordTranscript(PresenceOne.Name + @" says: " + ThisOneResult, FileNumber);
            Console.WriteLine(PresenceTwo.Name + @" says: " + ThisTwoResult);
            Logger.RecordTranscript(PresenceTwo.Name + @" says: " + ThisTwoResult, FileNumber);
            // Let them banter for the number of prescribed iterations.
            for (var i = 0; i <= iterations; i++)
            {
                SendOne(@"Dave", PresenceOne, ThisTwoResult.ToString());
                SendTwo(@"Franky", PresenceTwo, ThisOneResult.ToString());
                Console.WriteLine(PresenceOne.Name + @" says: " + ThisOneResult);
                Logger.RecordTranscript(PresenceOne.Name + @" says: " + ThisOneResult, FileNumber);
                Console.WriteLine(PresenceTwo.Name + @" says: " + ThisTwoResult);
                Logger.RecordTranscript(PresenceTwo.Name + @" says: " + ThisTwoResult, FileNumber);
            }
            Logger.RecordTranscript(@"", FileNumber, true);
        }
    }
}
