using GameConsole.Utils;
using System;

namespace GameConsole
{
    public class Parameter
    {
        public readonly string name;
        public readonly string description;
        public readonly object defaultValue;
        public readonly Type type;

        public Parameter( string name, string description, Type type, object defaultValue = null)
        {
            this.name = name;
            this.description = description;
            this.type = type;
            this.defaultValue = defaultValue;
        }

        public override string ToString()
        {
            if(defaultValue != null)
            {
                return string.Format("{0} {1} = {2}", type, name, defaultValue);
            }
            else
            {
                return string.Format("{0} {1}", type, name);
            }
        }

        public object Parse(Type type, string value)
        {
            return ConsoleParametersParser.Parse(type, value);
        }
    }
}
