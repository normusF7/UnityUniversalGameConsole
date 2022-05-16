using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameConsole.Utils
{
    public static class ConsoleParametersParser
    {
        private static Dictionary<Type, ConsoleParamParser> parsers = new Dictionary<Type, ConsoleParamParser>()
        {
            {typeof(bool), new BoolParser()},
            {typeof(int), new IntParser()},
            {typeof(Vector3), new Vector3Parser()},
            {typeof(String), new StringParser()}
        };

        public static object Parse(Type type, string value)
        {
            ConsoleParamParser parser;
            parsers.TryGetValue(type, out parser);

            if (parser != null)
            {
                return parser.Parse(value);
            }

            //TODO: ConsoleParamParser has method to throv incorrect parsing exceptions
            string errorMsg = String.Format("Couldn't parse parameter of type: '{0}' (value: '{1}'), no matching parser found.", type, value);
            Debug.LogError(errorMsg);

            return null;
        }
    }

    public sealed class BoolParser : ConsoleParamParser
    {
        public override object Parse(string value)
        {
            bool parsedValue;
            if (bool.TryParse(value, out parsedValue))
            {
                return parsedValue;
            }

            if (string.Compare(value, "0", System.StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                return false;
            }
            else if (string.Compare(value, "1", System.StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                return true;
            }

            LogIncorrectParsing(ParseErrorType.IncorrectParse);
            return null;
        }
    }

    public sealed class IntParser : ConsoleParamParser
    {
        public override object Parse(string value)
        {
            int parsedValue;

            if (int.TryParse(value, out parsedValue))
            {
                return parsedValue;
            }

            LogIncorrectParsing(ParseErrorType.IncorrectParse);
            return null;
        }
    }

    public sealed class Vector3Parser : ConsoleParamParser
    {
        public override object Parse(string value)
        {
            string[] subValues = value.Split(',');

            if (subValues.Length != 3)
            {
                LogIncorrectParsing(ParseErrorType.IncorrectFormat);
                return Vector3.zero;
            }

            float[] parsedValues = new float[3];

            for (int i = 0; i < parsedValues.Length; i++)
            {
                float.TryParse(subValues[i], out parsedValues[i]);
            }

            return new Vector3(parsedValues[0], parsedValues[1], parsedValues[2]);
        }
    }

    public sealed class StringParser : ConsoleParamParser
    {
        public override object Parse(string value)
        {
            return value;
        }
    }
}
