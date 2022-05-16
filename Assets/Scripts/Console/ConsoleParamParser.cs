

namespace GameConsole
{
    public abstract class ConsoleParamParser
    {
        public enum ParseErrorType { IncorrectParse, IncorrectFormat}

        public abstract object Parse(string value);

        protected void LogIncorrectParsing(ParseErrorType errorType)
        {
            //TODO: Make new exception type: InvalidParse
            if(errorType == ParseErrorType.IncorrectParse)
            {
                throw new System.InvalidCastException();
            }
            else if(errorType == ParseErrorType.IncorrectFormat)
            {
                throw new System.FormatException();
            }
        }
    }
}
