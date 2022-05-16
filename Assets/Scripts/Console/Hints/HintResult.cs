using System;

namespace GameConsole.Hints
{
    public class HintResult : IComparable
    {
        public readonly BaseCommand command;

        public bool IsValid { get => command != null; }

        public HintResult(BaseCommand command)
        {
            this.command = command;
        }

        public int CompareTo(object obj)
        {
            if (obj is HintResult)
            {
                return command.name.CompareTo((obj as HintResult).command.name);  // compare user names
            }

            throw new ArgumentException("Object is not a HintResult.");
        }

        public override string ToString()
        {
            if (command == null)
            {
                return string.Empty;
            }

            string parameters = string.Empty;

            for (int i = 0; i < command.method.parmaterers.Length; i++)
            {
                parameters += string.Format("{0} <i>{1}</i>", command.method.parmaterers[i].type.Name, command.method.parmaterers[i].name);
            }

            return string.Format("{0} ({1}) <i>{2}</i>", command.name, parameters, command.description);
        }
    }
}
