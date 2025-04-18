using System;
using System.Diagnostics;

namespace CIProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting CI Pipeline...");

            try
            {
                // Step 1: Build the Project
                Console.WriteLine("Building the project...");
                RunCommand("mvn", "clean install");

                // Step 2: Run Tests
                Console.WriteLine("Running tests...");
                RunCommand("mvn", "test");

                Console.WriteLine("CI Pipeline completed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Pipeline failed: {ex.Message}");
                Environment.Exit(1); // Exit with error code
            }
        }

        static void RunCommand(string command, string arguments)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = command,
                    Arguments = arguments,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();
            process.WaitForExit();

            Console.WriteLine(output);

            if (!string.IsNullOrEmpty(error))
            {
                Console.WriteLine($"Error: {error}");
                throw new Exception(error);
            }
        }
    }
}
