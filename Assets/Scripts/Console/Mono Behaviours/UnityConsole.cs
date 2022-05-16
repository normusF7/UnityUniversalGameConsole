using System.Collections;
using UnityEngine;
using TMPro;
using GameConsole.Hints;
using GameConsole.UI;

namespace GameConsole
{
    public class UnityConsole : MonoBehaviour
    {
        public bool logWelomeMessage = true;

        public UnityConsoleInput unityConsoleInput;
        [SerializeField] private HintPanel hintPanel;
        [SerializeField] private ConsoleSettings consoleSettings;
        [SerializeField] private TextMeshProUGUI outputField;
        [SerializeField] private OutputScroll outputScroll;

        public ConsoleSettings ConsoleSettings => consoleSettings;
        public TextMeshProUGUI OutputField => outputField;
        public OutputScroll OutputScroll => outputScroll;

        private Coroutine animateConsoleRoutine;
        [SerializeField] private RectTransform consoleWindow;

        public Console console { get; private set; }

        private void Awake()
        {
            CheckConsoleSettings();
            console = new Console(this, hintPanel, logWelomeMessage);

            //Set console to be turned off at the start of the game
            ActivateConsoleImmidiate(false);
        }

        private void Update()
        {
            console.Update();
            ConsoleActivationCheck();
        }

        public void UpdateOutput(string message)
        {
            outputField.text = string.Format("{0}", message);
        }

        private void ConsoleActivationCheck()
        {
            if(Input.GetKeyDown(KeyCode.BackQuote))
            {
                ActivateConsole(!console.isActive);
            }
        }

        public void ActivateConsole(bool activate)
        {
            if(animateConsoleRoutine != null)
            {
                StopCoroutine(animateConsoleRoutine);
            }

            animateConsoleRoutine = StartCoroutine(AnimateConsoleRoutine(activate));
        }

        private void ActivateConsoleImmidiate(bool activate)
        {
            if (animateConsoleRoutine != null)
            {
                StopCoroutine(animateConsoleRoutine);
            }

            console.SetActive(activate);
            consoleWindow.gameObject.SetActive(activate);
            consoleWindow.anchoredPosition = activate ? GetConsoleEndScreenPos() : Vector3.zero;
        }

        private Vector3 GetConsoleEndScreenPos()
        {
            //TODO: Optimize this
            return new Vector3(0, -consoleWindow.GetComponentInParent<Canvas>().pixelRect.height / 3, 0);
        }

        private void CheckConsoleSettings()
        {
            if(consoleSettings == null)
            {
                Debug.LogWarning("ConsoleSettings is null, console will use default settings.");
                consoleSettings = new ConsoleSettings();
            }
        }

        private IEnumerator AnimateConsoleRoutine(bool show)
        {
            if(show)
            {
                consoleWindow.gameObject.SetActive(show);
            }

            console.SetActive(show);

            Vector3 startPos = consoleWindow.anchoredPosition;
            Vector3 endPos = show ? GetConsoleEndScreenPos() : Vector3.zero;
            float t = 0;

            do
            {
                t += Time.deltaTime * 8;
                consoleWindow.anchoredPosition = Vector3.Lerp(startPos, endPos, t);
                yield return null;
            } while (t < 1);

            if (!show)
                consoleWindow.gameObject.SetActive(false);
        }
    }
}
