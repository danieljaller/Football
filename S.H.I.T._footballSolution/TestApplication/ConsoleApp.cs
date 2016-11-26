using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApplication
{
    // Created by Shahram Pourmakhdomi
    // Greatly improved by Martin Lindstedt
    public abstract class ConsoleApp
    {
        const int defaultMaxInputAttempts = 5;
        protected static int DefaultMaxInputAttempts { get { return defaultMaxInputAttempts; } }

        bool keepRunning;
        Dictionary<ConsoleKey, Action> commandMapper;
        Dictionary<ConsoleKey, string> commandLabelMapper;

        protected ConsoleApp()
        {
            commandMapper = new Dictionary<ConsoleKey, Action>();
            commandLabelMapper = new Dictionary<ConsoleKey, string>();
            Initialize();
            RunLoop();
        }

        protected abstract void Initialize();
        protected void RunLoop()
        {
            keepRunning = true;
            do
            {
                PrintMenu();
                var command = GetCommand();
                ExecuteCommand(command);
            } while (keepRunning);
        }
        protected void EndLoop()
        {
            keepRunning = false;
        }
        protected ConsoleKey GetCommand()
        {
            var command = Console.ReadKey().Key;
            Console.WriteLine();
            return command;
        }

        protected void ExecuteCommand(ConsoleKey command)
        {
            if (commandMapper.ContainsKey(command))
            {
                var action = commandMapper[command];
                action();
            }
            else
            {
                Console.WriteLine("Unknown command!");
            }
        }
        protected void AddCommand(ConsoleKey command, string commandLabel, Action commandAction)
        {
            commandLabelMapper[command] = commandLabel;
            commandMapper[command] = commandAction;
        }

        protected void PrintMenu()
        {
            PrintMenuBorder();
            foreach (var command in commandLabelMapper.Keys)
            {
                var label = commandLabelMapper[command];
                Console.WriteLine($"[{Convert.ToChar(command)}] {label}");
            }
            PrintMenuBorder();
            Console.Write("Enter one of the above commands: ");
        }

        private void PrintMenuBorder()
        {
            int borderLength = 0;
            foreach (var command in commandLabelMapper.Keys)
            {
                borderLength = ((commandLabelMapper[command].Length + 4) > borderLength) ? commandLabelMapper[command].Length + 4 : borderLength;
            }
            for (int i = 0; i < borderLength; i++)
            {
                Console.Write("=");
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Returns 0 if maximum input attempts is reached.
        /// </summary>
        /// <param name="answer"></param>
        /// <param name="prompt"></param>
        /// <param name="errorMessage"></param>
        /// <param name="maxInputAttempts"></param>
        protected void AskFor(out uint answer, string prompt, string errorMessage = "", int maxInputAttempts = defaultMaxInputAttempts)
        {
            int counter = 0;
            Console.Write(prompt);
            var line = Console.ReadLine();
            while (counter < maxInputAttempts)
            {
                if (uint.TryParse(line, out answer))
                    return;

                Console.Write("Please enter a unsigned integer: ");
                line = Console.ReadLine();

                counter++;
            }

            PrintMaxInputAttemptsMessage(maxInputAttempts, errorMessage);
            answer = 0;
        }

        /// <summary>
        /// Returns 0 if maximum input attempts is reached.
        /// </summary>
        /// <param name="answer"></param>
        /// <param name="prompt"></param>
        /// <param name="errorMessage"></param>
        /// <param name="maxInputAttempts"></param>
        protected void AskFor(out int answer, string prompt, string errorMessage = "", int maxInputAttempts = defaultMaxInputAttempts)
        {
            int counter = 0;
            Console.Write(prompt);
            var line = Console.ReadLine();
            while (counter < maxInputAttempts)
            {
                if (int.TryParse(line, out answer))
                    return;

                Console.Write("Please enter a integer: ");
                line = Console.ReadLine();

                counter++;
            }

            PrintMaxInputAttemptsMessage(maxInputAttempts, errorMessage);
            answer = 0;
        }

        /// <summary>
        /// Returns 0 if maximum input attempts is reached.
        /// </summary>
        /// <param name="answer"></param>
        /// <param name="prompt"></param>
        /// <param name="errorMessage"></param>
        /// <param name="maxInputAttempts"></param>
        protected void AskFor(out double answer, string prompt, string errorMessage = "", int maxInputAttempts = defaultMaxInputAttempts)
        {
            int counter = 0;
            Console.Write(prompt);
            var line = Console.ReadLine();
            while (counter < maxInputAttempts)
            {
                if (double.TryParse(line, out answer))
                    return;

                Console.Write("Please enter a decimal value: ");
                line = Console.ReadLine();

                counter++;
            }

            PrintMaxInputAttemptsMessage(maxInputAttempts, errorMessage);
            answer = 0;
        }

        /// <summary>
        /// Returns DateTime.MinValue if maximum input attempts is reached.
        /// </summary>
        /// <param name="answer"></param>
        /// <param name="prompt"></param>
        /// <param name="errorMessage"></param>
        /// <param name="maxInputAttempts"></param>
        protected void AskFor(out DateTime answer, string prompt, string errorMessage = "", int maxInputAttempts = defaultMaxInputAttempts)
        {
            int counter = 0;
            Console.Write(prompt);
            var line = Console.ReadLine();
            while (counter < maxInputAttempts)
            {
                if (DateTime.TryParse(line, out answer))
                    return;

                if (line.Length == 8)
                {
                    int tmp;
                    if (int.TryParse(line, out tmp))
                    {
                        line = line.Insert(6, "-");
                        line = line.Insert(4, "-");
                    }
                }
                
                Console.Write("Please enter a date value (yyyy-mm-dd) or (yyyymmdd): ");
                line = Console.ReadLine();

                counter++;
            }

            PrintMaxInputAttemptsMessage(maxInputAttempts, errorMessage);
            answer = DateTime.MinValue;
        }
        /// <summary>
        /// Returns a string.
        /// </summary>
        /// <param name="answer"></param>
        /// <param name="prompt"></param>
        protected void AskFor(out string answer, string prompt)
        {
            Console.Write(prompt);
            answer = Console.ReadLine();
        }

        /// <summary>
        /// Returns false if maximum input attempts is reached.
        /// </summary>
        /// <param name="answer"></param>
        /// <param name="prompt"></param>
        /// <param name="errorMessage"></param>
        /// <param name="maxInputAttempts"></param>
        protected void AskFor(out bool answer, string prompt, string errorMessage = "", int maxInputAttempts = defaultMaxInputAttempts)
        {
            int counter = 0;
            
            while (counter < maxInputAttempts)
            {
                Console.Write($"{prompt} (Y/n): ");
                ConsoleKey inputKey = Console.ReadKey().Key;
                Console.WriteLine();
                if (inputKey == ConsoleKey.Y || inputKey == ConsoleKey.Enter)
                {
                    answer = true;
                    return;
                }
                if (inputKey == ConsoleKey.N)
                {
                    answer = false;
                    return;
                }
                counter++;
            }

            answer = false;
            PrintMaxInputAttemptsMessage(maxInputAttempts, errorMessage);
        }

        private void PrintMaxInputAttemptsMessage(int maxInputAttempts, string errorMessage = "")
        {
            Console.WriteLine($"Maximum input attempts ({maxInputAttempts}) reached.");
            if (!string.IsNullOrEmpty(errorMessage))
                Console.WriteLine(errorMessage);
        }
    }
}
