using System.Diagnostics;
using Microsoft.Maui.Platform;

namespace MauiCrash
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public static string GetPathToEngineExec()
        {
            string currentDirectory = AppContext.BaseDirectory;
            string directoryFiveLevelsUp = Path.GetFullPath(Path.Combine(currentDirectory, "..", "..", "..", "..", "..", ".."));
#if DEBUG
            return directoryFiveLevelsUp + "\\x64\\Debug\\SecondaryWindow.exe";
#else
            return directoryFiveLevelsUp + "\\x64\\Release\\SecondaryWindow.exe";
#endif
        }

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
#if WINDOWS
            IntPtr handle = ((MauiWinUIWindow)App.Current.Windows[0].Handler.PlatformView).WindowHandle;

            string commandArgs = handle.ToString();

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = GetPathToEngineExec(),
                Arguments = commandArgs,
                UseShellExecute = false
            };

            for(int i = 0; i <2; i++)
            {
                try
                {
                    Process process = new Process
                    {
                        StartInfo = startInfo
                    };
                    process.Start();
                } 
                catch (Exception ex)
                {
                    Console.WriteLine($"Cannot run process: {ex.Message}");
                }
            }
#endif
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }

}
