using System;
using UnityEngine;

namespace GameConsole
{
    public class Message
    {
        public enum MessageType { Info, Warning, Error };

        public readonly bool isOutput;
        public readonly string message;
        public readonly DateTime timestamp;
        public readonly MessageType messageType;

        public Message(string message, bool isOutput, MessageType messageType = MessageType.Info)
        {
            this.isOutput = isOutput;
            this.message = message;
            this.messageType = messageType;
            timestamp = DateTime.Now;
        }

        //TODO: Make struct designed to carry message format settings and pass it here instead of whole ConsoleSettings
        public string ToStringFormatted(ConsoleSettings consoleSettings)
        {
            return string.Format("{0}{1}", GetPrefix(consoleSettings), consoleSettings.typeColoredMessags ? 
                GetColoredMessage(consoleSettings) : message);
        }

        private string GetPrefix(ConsoleSettings consoleSettings)
        {
            string prefix = string.Format("{0}{1}", consoleSettings.showTimestamp ? string.Format("{0} ", 
                GetTimestampAsString()) : string.Empty, isOutput ? string.Empty : "> ");
            return prefix;
        }

        private string GetColoredMessage(ConsoleSettings consoleSettings)
        {
            string color = string.Empty;

            switch (messageType)
            {
                case MessageType.Info:
                    {
                        color = ColorUtility.ToHtmlStringRGB(consoleSettings.defaultColor);
                        break;
                    }
                case MessageType.Warning:
                    {
                        color = ColorUtility.ToHtmlStringRGB(consoleSettings.warningColor);
                        break;
                    }
                case MessageType.Error:
                    {
                        color = ColorUtility.ToHtmlStringRGB(consoleSettings.errorColor);
                        break;
                    }
            }

            return string.Format("<color=#{0}>{1}</color>", color, message);
        }

        public string GetTimestampAsString()
        {
            return string.Format("[{0}]", timestamp.ToString("HH:mm:ss"));
        }
    }
}