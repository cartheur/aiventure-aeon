using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Forms;
using Boagaphish.Core;
using Boagaphish.Writer;

namespace Core.Display
{
    public partial class InteractionForm : Form
    {
        public Assemble Init = new Assemble();
        readonly Stopwatch _now = new Stopwatch();
        public double AliveTime { get; set; }

        public InteractionForm()
        {
            InitializeComponent();
            //Mood mood = new Mood();
            petsMood.Image = Properties.Resources.image_smile;
            emotionTypeComboBox.SelectedItem = "none";
            usernameTextBox.Text = @"me";
            Interaction.Username = usernameTextBox.Text;

            foreach (var line in Init.Emotions)
            {
                emotionTypeComboBox.Items.Add(line);
            }
            // Retrieve the stored value, if there is one.
            AliveTime = Convert.ToDouble(Logging.ActiveTime.LogTime(null, true));
            aliveTimeLabel.Text = AliveTime.ToString(CultureInfo.InvariantCulture);

        }

        private void touchButton_Click(object sender, EventArgs e)
        {
            petsResponseTextBox.Text = @"I've been touched " + emotionTypeComboBox.SelectedItem + @", " + Interaction.Username;
            switch (emotionTypeComboBox.SelectedItem.ToString())
            {
                case "happy":
                    ChangeMood(Mood.Happy);
                    break;
                case "angry":
                    ChangeMood(Mood.Sad);
                    break;
                case "hyper":
                    ChangeMood(Mood.Sad);
                    break;
                case "hard":
                    ChangeMood(Mood.Sad);
                    break;
                case "sad":
                    ChangeMood(Mood.Sad);
                    break;
                case "soft":
                    ChangeMood(Mood.Content);
                    break;
                case "nice":
                    ChangeMood(Mood.Happy);
                    break;
                case "none\0":
                    ChangeMood(Mood.None);
                    break;
            }
            Refresh();

        }
        private void talkButton_Click(object sender, EventArgs e)
        {
            petsResponseTextBox.Text = @"You are talking to me " + emotionTypeComboBox.SelectedItem + @", " + Interaction.Username;
            switch (emotionTypeComboBox.SelectedItem.ToString())
            {
                case "happy":
                    ChangeMood(Mood.Happy);
                    break;
                case "angry":
                    ChangeMood(Mood.Angry);
                    break;
                case "hyper":
                    ChangeMood(Mood.Hyper);
                    break;
                case "hard":
                    ChangeMood(Mood.Sad);
                    break;
                case "sad":
                    ChangeMood(Mood.Sad);
                    break;
                case "soft":
                    ChangeMood(Mood.Content);
                    break;
                case "nice":
                    ChangeMood(Mood.Happy);
                    break;
                case "none\0":
                    ChangeMood(Mood.None);
                    break;
            }
            Refresh();
        }
        private void clearButton_Click(object sender, EventArgs e)
        {
            petsResponseTextBox.Text = "";
        }
        private void saveDataButton_Click(object sender, EventArgs e)
        {
            Interaction.Username = usernameTextBox.Text;
        }
        protected void ChangeMood(Mood mood)
        {
            switch (mood)
            {
                case Mood.Sad:
                    //mood = mood;
                    petsMood.Image = Properties.Resources.image_frown;
                    Interaction.SpeakText("I am " + mood + ", " + Interaction.Username + ".");
                    Refresh();
                    break;
                case Mood.None:
                    petsMood.Image = Properties.Resources.image_noemotion;
                    Interaction.SpeakText("I am feeling normal, " + Interaction.Username + ".");
                    Refresh();
                    break;
                case Mood.Happy:
                    petsMood.Image = Properties.Resources.image_smile;
                    Interaction.SpeakText("I am " + mood + ", " + Interaction.Username + ".");
                    Refresh();
                    break;
                case Mood.Angry:
                    petsMood.Image = Properties.Resources.image_frown;
                    Interaction.SpeakText("I am " + mood + ", " + Interaction.Username + ".");
                    Refresh();
                    break;
                case Mood.Content:
                    petsMood.Image = Properties.Resources.image_smile;
                    Interaction.SpeakText("I am " + mood + ", " + Interaction.Username + ".");
                    Refresh();
                    break;
                case Mood.Hyper:
                    petsMood.Image = Properties.Resources.image_noemotion;
                    Interaction.SpeakText("I am " + mood + ", " + Interaction.Username + ".");
                    Refresh();
                    break;
                default:
                    petsMood.Image = Properties.Resources.image_smile;
                    Interaction.SpeakText("I am " + mood + ", " + Interaction.Username + ".");
                    Refresh();
                    break;
            }

        }
        private void sendMessageButton_Click(object sender, EventArgs e)
        {
            Interaction.SendMessage(teletypeInputBox.Text);
            petsResponseTextBox.Text = Interaction.ParseMessage(teletypeInputBox.Text, Interaction.NlpMethods.NlpMethodType.Tokenize).ToString();
        }
        private void InteractionForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            // Compute and store the alive time for the session.
            _now.Stop();
            // Compute total activity time.
            double span = _now.Elapsed.Seconds;
            var totalTime = span + AliveTime;
            // Store the total time into non-volatile memory.
            //ActiveTime.LogTime(totalTime, timeFilePath, false);
            Logging.ActiveTime.LogTime(totalTime, false);
            // Store the total time into the grid location.
            //timeOne.CellNames.Add(totalTime);
        }
        private void InteractionForm_Load(object sender, EventArgs e)
        {
            _now.Start();
        }
    }
}
