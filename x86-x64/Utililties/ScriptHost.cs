using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using Animals.Core.Addins;

namespace Animals.Core.Utililties
{
    public class ScriptHost
    {
        private Personality.Mood.Moods _currentMood;
        public void MessageWindow(string message)
        {
            MessageBox.Show(message, @"Message via script", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        // how about something "moody" on this Tuesday afternoon?
        public void ChangeMood(string mood)
        {
            //MessageWindow(@"The current mood on this planet is: " + mood + ".");
            Console.WriteLine(@"The current mood on this planet is: " + mood + ".");
            // Get the mood as an integer.
            switch (mood)
            {
                case "happy":
                    _currentMood = Personality.Mood.Moods.happy;
                    break;
                case "sad":
                    _currentMood = Personality.Mood.Moods.sad;
                    break;
                case "angry":
                    _currentMood = Personality.Mood.Moods.angry;
                    break;
                case "depressed":
                    _currentMood = Personality.Mood.Moods.depressed;
                    break;
                case "mellow":
                    _currentMood = Personality.Mood.Moods.mellow;
                    break;
            }
            // Make a random change to the mood.
            var sortie = new Random(Convert.ToInt32(_currentMood));
            var store = sortie.Next(0, 4);
            Personality.Mood.SetMood(store);
            //MessageWindow(@"Now the mood is: " + Personality.Mood.CurrentMood + ".");
            Console.WriteLine(@"Now the mood is: " + Personality.Mood.CurrentMood + ".");
        }

        public void CallTorch(string command)
        {
            var parameters = "-ltorch";
            ExecuteCommand(command + parameters);
            //Create process
            System.Diagnostics.Process process = new System.Diagnostics.Process
            {
                StartInfo =
                {
                    FileName = "C:\\Users\\tuckchr01\\Documents\\myne\\animals\\core\\Core.UnitTests\\torchy.bat",
                    Arguments = parameters,
                    UseShellExecute = false,
                    RedirectStandardOutput = true
                }
            };
            //strCommand is path and file name of command to run
            //strCommandParameters are parameters to pass to program
            //Set output of program to be written to process output stream
            string strCmdText;
            strCmdText = "lua -ltorch";
            var herer = System.Diagnostics.Process.Start("CMD.exe", strCmdText);
            var here = System.Diagnostics.Process.Start("C:\\Users\\tuckchr01\\Documents\\myne\\animals\\core\\Core.UnitTests\\torchy.bat");
            try
            {
                var outy = herer.StandardOutput.ReadToEnd();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            //Start the process
            //process.Start();

            //if (process != null)
            //{
            //    process.StandardInput.WriteLine("require torch");
            //    process.StandardInput.WriteLine("x = torch.Tensor()");
            //    process.StandardInput.WriteLine("print(x)");
            //    //process.StandardInput.WriteLine("yourCommand.exe arg1 arg2");

            //    process.StandardInput.Close(); // line added to stop process from hanging on ReadToEnd()

            //    string outputString = process.StandardOutput.ReadToEnd();
            //}

            //Get program output
            string strOutput = process.StandardOutput.ReadToEnd();

            //Wait for process to finish
            process.WaitForExit();


            //var process = new System.Diagnostics.Process
            //{
            //    StartInfo =
            //    {
            //        WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal,
            //        FileName = "cmd.exe",
            //        Arguments = command
            //    }
            //};
            //process.Start();
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
    }
}
