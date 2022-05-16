using System.Collections.Generic;
using GameConsole.Hints;
using System;

namespace GameConsole
{
    public class Console
    {
        //TODO: Move static strings like those to separate class
        public static string ERR_CMD_NOT_RECOGNIZED = "Command '{0}' is not recognized by console.";
        public static string CMD_INIT_MESSAGE = "Game Console by Norbert Musial, " +
            "github: https://github.com/normusF7/UnityGameConsole";
        public static List<BaseCommand> commandList = new List<BaseCommand>();

        public Action onConsoleActivated;
        public Action onConsoleDeactivated;

        public readonly UnityConsole unityConsole;

        public bool isActive { get; private set; }
        public HintSystem hintSystem { get; private set; }
        public ConsoleInput consoleInput { get; private set; }
        public  ConsoleOutput consoleOutput { get; private set; }

        public Console(UnityConsole unityConsole, HintPanel hintPanel, bool logWelcomeMessage)
        {
            this.unityConsole = unityConsole;
            hintSystem = new HintSystem(this, hintPanel);
            consoleInput = this.unityConsole.unityConsoleInput.Initialize(this);
            consoleOutput = new ConsoleOutput(this, logWelcomeMessage);
        }

        public void ExecuteCommand(ConsoleInput.InputCommand inputCommand)
        {
            BaseCommand consoleCommand = CommandsProvider.commands.Find(x => x.name == inputCommand.commandName);
            Message returnMessage = null;

            consoleOutput.AddMessage(new Message(inputCommand.rawInput, false));

            if (consoleCommand != null)
            {
                returnMessage = consoleCommand.Execute(inputCommand.args);
            }
            else
            {
                returnMessage = new Message(string.Format(ERR_CMD_NOT_RECOGNIZED, inputCommand.rawInput), true, 
                    Message.MessageType.Error);
            }

            if (returnMessage != null)
            {
                consoleOutput.AddMessage(returnMessage);
            }
        }

        public void Update()
        {
            consoleOutput.Update();
        }

        public void SetActive(bool set)
        {
            //TODO: make this property
            isActive = set;

            if(isActive)
            {
                onConsoleActivated?.Invoke();
            }
            else
            {
                onConsoleDeactivated?.Invoke();
            }
        }

        public void ShowMessage(Message message)
        {
            consoleOutput.AddMessage(message);
        }

        public void ShowError(string content)
        {
            Message message = new Message(content, true, Message.MessageType.Error);
            consoleOutput.AddMessage(message);
        }
    }
}
