using System.Collections;
using System.Collections.Generic;
using GameConsole.Hints;
using UnityEngine;
using TMPro;

namespace GameConsole
{
    public class UnityConsoleInput : MonoBehaviour
    {
        public UnityConsole unityConsole;
        public TMP_InputField inputField;

        public Console console { get; private set; }
        public ConsoleInput consoleInput { get; private set; }

        public ConsoleInput Initialize(Console console)
        {
            this.console = console;
            consoleInput = new ConsoleInput(console, this);
            consoleInput.onCommandExecuted += ClearInput;
            inputField.onSubmit.AddListener(consoleInput.ExecuteInput);
            inputField.onSubmit.AddListener(TextSubmitted);
            inputField.onValueChanged.AddListener(consoleInput.InputChanged);
            //TODO: this is not optimal solution, UpdateHints should be private so subscription shuld be on hintSystem side
            //inputField.onValueChanged.AddListener(console.hintSystem.UpdateHints);
            return consoleInput;
        }

        private void OnDestroy()
        {
            inputField.onSubmit.RemoveListener(consoleInput.ExecuteInput);
            inputField.onSubmit.RemoveListener(TextSubmitted);
            inputField.onValueChanged.RemoveListener(consoleInput.InputChanged);
            //inputField.onValueChanged.RemoveListener(console.hintSystem.UpdateHints);
        }

        private void TextSubmitted(string value)
        {
            inputField.ActivateInputField();
            inputField.Select();
        }

        private void ClearInput()
        {
            inputField.text = string.Empty;
        }
    }
}
