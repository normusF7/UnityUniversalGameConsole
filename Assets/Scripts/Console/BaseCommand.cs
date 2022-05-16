using System;
using System.Reflection;

namespace GameConsole
{
    public abstract class BaseCommand
    {
        public static string CMD_EXEC_METHOD_NAME = "ExecuteCmd";

        public readonly string name;

        /// <summary>
        /// name with lower case for comparsions
        /// </summary>
        public readonly string nameComparable;
        public readonly string description;

        public Method method { get; private set; }

        protected BaseCommand(string name, string description, params string[] argumentDescriptions)
        {
            this.name = name;
            this.nameComparable = name.ToLowerInvariant();
            this.description = description;
            SetMethod(argumentDescriptions);
        }

        public Message Execute(params string[] args)
        {
            Message message = null;

            Type type = GetType();
            MethodInfo methodInfo = type.GetMethod(CMD_EXEC_METHOD_NAME, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);

            if(methodInfo !=  null)
            {
                message = methodInfo.Invoke(this, ParseParameters(args)) as Message;
            }

            return message;
        }

        private void SetMethod(params string[] argumentDescriptions)
        {
            Type type = GetType();
            MethodInfo methodInfo = type.GetMethod(CMD_EXEC_METHOD_NAME, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
            ParameterInfo[] parametersInfo = methodInfo.GetParameters();
            Parameter[] parameters = new Parameter[parametersInfo.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                parameters[i] = new Parameter(parametersInfo[i].Name, argumentDescriptions.Length > i ? argumentDescriptions[i] : string.Empty, parametersInfo[i].ParameterType);
            }

            method = new Method(CMD_EXEC_METHOD_NAME, parameters);
        }

        private object[] ParseParameters(string[] values)
        {
            if (values == null)
            {
                return null;
            }

            object[] objectValues = new object[method.GetParametersCount()];

            for (int i = 0; i < objectValues.Length; i++)
            {
                objectValues[i] = method.parmaterers[i].Parse(method.parmaterers[i].type, values[i]);
            }

            return objectValues;
        }
    }
}
