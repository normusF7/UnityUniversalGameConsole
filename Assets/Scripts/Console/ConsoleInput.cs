using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace GameConsole
{
    public class ConsoleInput
    {
        public static char STRING_SPECIAL_CHAR = '\"';

        public Action onCommandExecuted;

        private Console console;
        private UnityConsoleInput unityConsoleInput;

        public ConsoleInput(Console console, UnityConsoleInput unityConsoleInput)
        {
            this.console = console;
            this.unityConsoleInput = unityConsoleInput;
            console.onConsoleActivated += ActivateInput;
            console.onConsoleDeactivated += DeactivateInput;
        }

        ~ConsoleInput()
        {
            console.onConsoleActivated -= ActivateInput;
            console.onConsoleDeactivated -= DeactivateInput;
        }

        public void InputChanged(string value)
        {
            InputCommand result = FilterInput(value);

            if(result != null)
            {
                console.hintSystem.UpdateHints(result.commandName, result.commandInputEnded);
            }
            else
            {
                console.hintSystem.UpdateHints(string.Empty, false);
            }
        }

        public void ExecuteInput(string value)
        {
            InputCommand result = FilterInput(value);

            if (result == null)
                return;

            console.ExecuteCommand(result);
            onCommandExecuted?.Invoke();
        }

        //TODO: we should probably get rid of unityConsoleInput reference from this class
        public void SetInput(string value)
        {
            unityConsoleInput.inputField.text = value;
            unityConsoleInput.inputField.caretPosition = unityConsoleInput.inputField.text.Length; 
        }

        //Try to make sense out of input command, find command name, arguments, special cases eg. strings that begin with "
        private InputCommand FilterInput(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            string commandName = string.Empty;
            string[] args = null;
            int whitespaceIndex = value.IndexOf(' ');

            if (whitespaceIndex != -1)
            {
                commandName = value.Substring(0, whitespaceIndex);

                if (whitespaceIndex + 1 < value.Length)
                {
                    args = ProcessArguments(value.Substring(whitespaceIndex + 1, value.Length - whitespaceIndex - 1));
                }
            }
            else
            {
                //No arguments, whole input is a command name
                commandName = value;
            }

            return new InputCommand(value, whitespaceIndex != -1, commandName, args);
        }

        //Normally arguments are separated by white spaces, but when user want to input string with spaces he can use "
        //to let console know length of the string argument
        private string PreprocessStrings(string value)
        {
            int startIndex = value.IndexOf('"', 0);
            int endIndex = value.IndexOf('"', startIndex);

            string substring = value.Substring(startIndex, endIndex - startIndex);

            return substring;
        }

        private string[] ProcessArguments(string argRaw)
        {
            int currentArgStartIndex = 0;
            int currentArgEndIndex = 0;

            ICollection<string> args = new Collection<string>();

            do
            {
                if (currentArgStartIndex >= argRaw.Length)
                {
                    //End of arguments input
                    break;
                }

                int nextIndex = currentArgStartIndex + 1;

                if (argRaw[currentArgStartIndex] == ' ')
                {
                    //Debug.Log(argRaw + " " + currentArgStartIndex);
                    currentArgStartIndex = nextIndex;
                    continue;
                }

                if (IsStringSpecialChar(argRaw[currentArgStartIndex]))
                {
                    if (nextIndex >= argRaw.Length)
                    {
                        //End of arguments input
                        break;
                    }

                    currentArgEndIndex = argRaw.IndexOf('\"', nextIndex);

                    if (currentArgEndIndex == -1)
                    {
                        break;
                    }

                    args.Add(argRaw.Substring(currentArgStartIndex + 1, currentArgEndIndex - currentArgStartIndex - 1));
                    currentArgStartIndex = currentArgEndIndex + 2;
                }
                else
                {
                    currentArgEndIndex = argRaw.IndexOf(' ', currentArgStartIndex);

                    if(currentArgEndIndex == -1)
                    {
                        args.Add(argRaw.Substring(currentArgStartIndex, argRaw.Length - currentArgStartIndex));
                        break;
                    }

                    args.Add(argRaw.Substring(currentArgStartIndex, currentArgEndIndex - currentArgStartIndex));
                    currentArgStartIndex = currentArgEndIndex + 1;
                }

            } while (true);

            return args.Count > 0 ? args.ToArray() : null;
        }

        private bool IsStringSpecialChar(char value)
        {
            return value == STRING_SPECIAL_CHAR;
        }

        private void ActivateInput()
        {
            unityConsoleInput.inputField.ActivateInputField();
            unityConsoleInput.inputField.text = string.Empty;
        }

        private void DeactivateInput()
        {
            unityConsoleInput.inputField.DeactivateInputField();
        }

        public class InputCommand
        {
            //Command name end is determined by spacebar, when we know that user ended writing command name by pressing space
            //we can give him hint about it's input arguments
            //TODO: there should be better way to handle this
            public readonly bool commandInputEnded;
            public readonly string rawInput;
            public readonly string commandName;
            public readonly string[] args;

            public InputCommand(string rawInput, bool commandInputEnded, string commandName, string[] args)
            {
                this.rawInput = rawInput;
                this.commandInputEnded = commandInputEnded;
                this.commandName = commandName;
                this.args = args;
            }
        }
    }
}
