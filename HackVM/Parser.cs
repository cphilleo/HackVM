using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using HackVM.Commands;

namespace HackVM
{
    public class Parser
    {
        private string _fileName;

        public Parser(string fileName)
        {
            _fileName = fileName;
        }

        public List<ICommand> GetCommands()
        {
            var commands = new List<ICommand>();

            var lines = Parse();

            foreach (var line in lines)
            {
                switch(line[0])
                {
                    //Arithmetic commands
                    case "add":
                        commands.Add(new BinaryCommand("+"));
                        break;
                    case "sub":
                        commands.Add(new BinaryCommand("-"));
                        break;
                    case "and":
                        commands.Add(new BinaryCommand("&"));
                        break;
                    case "or":
                        commands.Add(new BinaryCommand("|"));
                        break;
                    case "neg":
                        commands.Add(new UnaryCommand("-"));
                        break;
                    case "not":
                        commands.Add(new UnaryCommand("!"));
                        break;
                    case "eq":
                        commands.Add(new EqualityCommand("EQ"));
                        break;
                    case "gt":
                        commands.Add(new EqualityCommand("GT"));
                        break;
                    case "lt":
                        commands.Add(new EqualityCommand("LT"));
                        break;

                    //Memory commands
                    case "push":
                        commands.Add(new PushCommand(line[1], line[2]));
                        break;
                    case "pop":
                        commands.Add(new PopCommand(line[1], line[2]));
                        break;

                    //branching commands
                    case "label":
                        commands.Add(new LabelCommand(line[1]));
                        break;
                    case "goto":
                        commands.Add(new GotoCommand(line[1], false));
                        break;
                    case "if-goto":
                        commands.Add(new GotoCommand(line[1], true));
                        break;

                    //function commands
                    case "function":
                        commands.Add(new FunctionCommand(line[1], line[2]));
                        break;
                    case "return":
                        commands.Add(new ReturnCommand());
                        break;
                    case "call":
                        commands.Add(new CallCommand(line[1], line[2]));
                        break;
                }
            }
            
            return commands;
        }

        private List<string[]> Parse()
        {
            var output = new List<string[]>();

            //get all input
            Console.WriteLine("read lines from: " + _fileName);
            var lines = File.ReadAllLines(_fileName);

            //clean input
            foreach (var line in lines)
            {
                //trim whitespace
                var clean = line.Trim();

                //remove comments
                clean = Regex.Replace(line, "//.*", string.Empty);

                //consolidate whitespace
                clean = Regex.Replace(clean, @"\s+", " ");

                //if not empty after cleaning, add to ouput list
                if (clean.Length > 0)
                {
                    //split command on spaces
                    output.Add(clean.Split(' '));
                }
            }

            return output;
        }
    }
}