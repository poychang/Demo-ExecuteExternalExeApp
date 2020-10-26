using System;
using System.Diagnostics;
using System.IO;

namespace ExecuteExternalExeApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var RootDirectory = AppDomain.CurrentDomain.BaseDirectory ?? System.Reflection.Assembly.GetExecutingAssembly().Location;
            var pathToExe = Path.Combine(RootDirectory, "HelloTime.exe");

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = pathToExe,
                    Arguments = "--name Poy --lastName Chang",
                    UseShellExecute = false,
                    CreateNoWindow = false,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true
                },
            };

            //* Set your output and error (asynchronous) handlers
            process.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);
            process.ErrorDataReceived += new DataReceivedEventHandler(OutputHandler);
            //* Start process and handlers
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();
        }

        static void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            // Write to console
            Console.WriteLine(outLine.Data);
        }
    }
}
