using System;
using System.Collections.Generic;

namespace GameConsole.Hints
{
    public class HintSystem
    {
        public Console console { get; private set; }
        public HintPanel hintPanel { get; private set; }
        public HintResult[] currentHintResults { get; private set; } = new HintResult[0];

        public Selector selector { get; private set; }

        public HintSystem(Console console, HintPanel hintPanel)
        {
            this.console = console;
            this.hintPanel = hintPanel;
            this.hintPanel.Initialize(this);
            selector = new Selector(this, hintPanel.selectorRect);
        }

        public void UpdateHints(string input, bool commandTerminated)
        {
            if(commandTerminated)
            {
                HintResult hint = RetrieveHintForCommand(input);

                if(hint.IsValid)
                    hintPanel.ShowDetailedDescription(hint);

                return;
            }

            currentHintResults = input.Length > 1 ? RetrieveHintsForInput(input) : null;

            if (currentHintResults == null)
            {
                hintPanel.ClearHintResults();
                selector.SetActive(false);
            }
            else
            {
                hintPanel.UpdateHintResultsText(currentHintResults);
            }
        }

        //Get hint for specific command
        private HintResult RetrieveHintForCommand(string commandName)
        {
            BaseCommand command = CommandsProvider.commands.Find(x => x.name.Contains(commandName));
            HintResult result = new HintResult(command);
            currentHintResults = new HintResult[1] { result };
            return result;
        }

        private HintResult[] RetrieveHintsForInput(string input)
        {
            string inputLower = input.ToLowerInvariant();
            List<BaseCommand> matchingResults = CommandsProvider.commands.FindAll(x => x.nameComparable.Contains(inputLower));

            if (matchingResults == null)
                return null;

            HintResult[] hintResults = new HintResult[matchingResults.Count];

            int hintCount = matchingResults.Count;

            if (hintCount > console.unityConsole.ConsoleSettings.hintCount)
            {
                hintCount = console.unityConsole.ConsoleSettings.hintCount;
            }

            for (int i = 0; i < hintCount; i++)
            {
                hintResults[i] = new HintResult(matchingResults[i]);
            }

            Array.Sort(hintResults);

            return hintResults;
        }

        public void TryCompleteInput()
        {
            if (!selector.isActive)
            {
                return;
            }

            console.consoleInput.SetInput(GetCurrentSelectedHint().command.name);
            selector.SetActive(false);
        }

        private HintResult GetCurrentSelectedHint()
        {
            if (!selector.isActive)
            {
                return null;
            }

            return currentHintResults[selector.currentIndex];
        }
    }
}
