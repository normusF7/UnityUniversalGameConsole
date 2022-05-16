using UnityEngine;

namespace GameConsole
{
    public class Method
    {
        private static string PARAM_INDEX_OUT_RANGE = "Parameter index is out of range.";

        public readonly string name;
        public readonly Parameter[] parmaterers;

        public string realName;

        public Method(string name, Parameter[] parameters)
        {
            this.name = name;
            this.parmaterers = parameters;
        }

        public int GetParametersCount()
        {
            return parmaterers.Length;
        }
    }
}
