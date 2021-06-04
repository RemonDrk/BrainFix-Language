using System;
using System.Linq;
using System.IO;
using System.Reflection;

namespace BrainFix_Language
{
    class Program
    {

        static char[] numbers = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        static string src = "";

        static void Main(string[] args)
        {
            try
            {
                GetSource(args);
                if (src == "") WriteProgramInConsole();
                else
                {
                    ReverseConsoleColors();
                    Console.WriteLine("CODE:");
                    ReverseConsoleColors();
                    Console.WriteLine(src);
                    ReverseConsoleColors();
                    Console.WriteLine("\nOUTPUT:");
                    ReverseConsoleColors();
                    Execute(src);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong:\n{0}", e.Message);
                Console.ResetColor();
            }

            Console.WriteLine("\n\nPress any key to exit...");
            Console.ReadKey();
        }

        static void WriteProgramInConsole()
        {
            string descriptionTextP1 = "No valid source file (program.bfix or with arg) found. Check README.md for more detail.\n" +
                                     "You can dirrectly write BrainFuck/BrainFix code in console but your code will not be saved.\n";

            string descriptionTextP2 = "Type exit to exit or insert code to execute...";

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(descriptionTextP1);
            Console.ResetColor();
            Console.WriteLine(descriptionTextP2);

            while (true)
            {
                Console.WriteLine("\n");
                string input = Console.ReadLine();
                if (input.ToLower() == "exit") Environment.Exit(0);
                else
                {
                    Console.WriteLine("\n");
                    Execute(input);
                }
            }
        }

        static void GetSource(string[] args)
        {
            string pathArg = args.Length > 0 ? args[0] : "C:\\HOPETHISFILEDONTEXIST\\VHAJKDLH";
            string srcPath = "";
            string appPath = "";

            appPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\";

            if (File.Exists(pathArg)) srcPath = pathArg;
            else if (File.Exists(appPath + "program.bfix")) srcPath = appPath + "program.bfix";

            if (srcPath != "") src = File.ReadAllText(srcPath);
        }

        static void Execute(string source)
        {
            char[] memory = new char[30000];
            int ptr = 0, idx = 0, bracketCounter = 0, numToUseInOp = 0;
            bool perform = true;

            for(;idx < source.Length; idx++)
            {
                char op = source[idx];
                if(perform == true)
                {
                    switch (op)
                    {
                        case '.': Console.Write(memory[ptr]); break;
                        case ',': memory[ptr] = Console.ReadKey().KeyChar; break;
                        case '+': memory[ptr]++; break;
                        case '-': memory[ptr]--; break;
                        case 'O': memory[ptr] = Char.MinValue; break;
                        case 'M': memory[ptr] = Char.MaxValue; break;
                        case '>': ptr++; break;
                        case '<': ptr--; break;
                        case '[':
                            for (int i = idx + 1; i < source.Length; i++)
                            {
                                if (source[i] == '[') bracketCounter--;
                                if (source[i] == ']')
                                {
                                    if (bracketCounter == 0)
                                    {
                                        idx = i - 1;
                                        break;
                                    }
                                    else bracketCounter++;
                                }
                            } break;
                        case ']':
                            if (memory[ptr] <= 0) break;
                            for(int i = idx - 1; i >= 0; i--)
                            {
                                if (source[i] == ']') bracketCounter++;
                                if (source[i] == '[')
                                {
                                    if (bracketCounter == 0)
                                    {
                                        idx = i;
                                        break;
                                    }
                                    else bracketCounter--;
                                }
                            } break;
                        case '@':
                            idx++;
                            char code = source[idx];
                            switch (code)
                            {
                                case '*': Console.Write("'cell_no:{0}'", ptr); break;
                                case '.': Console.Write("'cell_value:{0}'", (int)memory[ptr]); break;
                                case 'Ğ': Console.Write("'Ğ? Ğ.'"); break;
                                default: idx--; break;
                            } break;
                        case '#':
                            for(int i = idx + 1; i < source.Length; i++)
                            {
                                if(source[i] == '#')
                                {
                                    idx = i;
                                    break;
                                }
                            } break;
                        case '=': case '^': case 'V':
                            string numberStr = source.Substring(idx + 1);
                            numberStr = numberStr.Substring(0, numberStr.IndexOf('?'));

                            int endIf = source.IndexOf('?', idx);
                            int compariseWith = int.Parse(numberStr);
                            int value = memory[ptr];

                            perform =
                                op == '=' ? value == compariseWith :
                                op == '^' ? value > compariseWith :
                                op == 'V' ? value < compariseWith : false;

                            idx = endIf;
                            break;
                        case ':':
                            int numOfDigits = 3;
                            for(int i = 1; i < 3; i++)
                            {
                                char nextChar = source[idx + i + 1];
                                if (!numbers.Contains(nextChar))
                                {
                                    numOfDigits = i;
                                    break;
                                }
                            }
                            numToUseInOp = int.Parse(source.Substring(idx + 1, numOfDigits));
                            idx = idx + 1 + numOfDigits;
                            switch (source[idx])
                            {
                                case '+': memory[ptr] += (char)numToUseInOp; break;
                                case '-': memory[ptr] -= (char)numToUseInOp; break;
                                case '>': ptr += numToUseInOp; break;
                                case '<': ptr -= numToUseInOp; break;
                            } break;
                    }
                }
                if (op == 'K') perform = true;
                else if (op == '|') perform = !perform;
            }
        }

        static void ReverseConsoleColors()
        {
            var previousForegroundColor = Console.ForegroundColor;
            Console.ForegroundColor = Console.BackgroundColor;
            Console.BackgroundColor = previousForegroundColor;
        }
    }
}
