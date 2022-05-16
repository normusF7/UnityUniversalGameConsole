using UnityEngine;

namespace GameConsole
{
    public class ConsoleSettings : ScriptableObject
    {
        public bool showWelcomeMessage = true;
        [Tooltip("Whether to write Unity Editor console messages to Game Console output.")]
        public bool logEditorConsoleMessages = false;
        [Tooltip("Show timestamp as prefix to message in console output window.\nWARNING:\nThis bool doesn't affect 'dump' command, timestamp will allways be visible in dump file created by this command.")]
        public bool showTimestamp = false;
        [Tooltip("Color messages by type.")]
        public bool typeColoredMessags = true;
        [Tooltip("Max number of rows of console output that can be shown.")]
        public int consoleRowCount = 10;
        [Tooltip("Limit of hints that can be displayed while writing command.")]
        public int hintCount = 10;

        [Header("Colors")]
        public Color defaultColor = Color.white;
        public Color warningColor = Color.yellow;
        public Color errorColor = new Color(222, 33, 33);
    }
}
