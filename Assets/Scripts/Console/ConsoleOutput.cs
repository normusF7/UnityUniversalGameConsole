using System.Collections.Generic;
using UnityEngine;

namespace GameConsole
{
    public class ConsoleOutput
    {
        private int currentLine = 0;
        private Console console;

        public IList<Message> output { get; private set; } = new List<Message>();

        public ConsoleOutput(Console console, bool logWelcomeMessage)
        {
            this.console = console;

            if (logWelcomeMessage)
                ShowWelcomeMessage();
        }

        public void Update()
        {
            CheckInput();
        }

        public void AddMessage(Message message)
        {
            output.Add(message);

            currentLine = output.Count - 1;

            if (output.Count > 0)
                UpdateOutput();
        }

        private void UpdateOutput()
        {
            if(output.Count <= 0)
            {
                console.unityConsole.UpdateOutput(string.Empty);
                return;
            }

            string outputString = string.Empty;
            int startPos = output.Count > 15 ? currentLine - 15 : 0;
            int endPos = output.Count >= 16 ? startPos + 15 : output.Count - 1;

            for (int i = startPos; i <= endPos; i++)
            {
                outputString += output[i].ToStringFormatted(console.unityConsole.ConsoleSettings);
                if (i != endPos)
                {
                    outputString += "\n";
                }
            }

            //TODO: probably should be handled by UnityConsoleOutput
            console.unityConsole.UpdateOutput(outputString);
        }

        private void CheckInput()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || console.unityConsole.OutputScroll.CumulativeChange > 21)
            {
                currentLine -= 1;
                currentLine = Mathf.Max(currentLine, 15);
                UpdateOutput();
                console.unityConsole.OutputScroll.Reset();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || console.unityConsole.OutputScroll.CumulativeChange < -21)
            {
                currentLine += 1;
                currentLine = Mathf.Min(currentLine, output.Count - 1);
                UpdateOutput();
                console.unityConsole.OutputScroll.Reset();
            }
        }

        private void ShowWelcomeMessage()
        {
            AddMessage(new Message(Console.CMD_INIT_MESSAGE, true));
        }
    }
}
