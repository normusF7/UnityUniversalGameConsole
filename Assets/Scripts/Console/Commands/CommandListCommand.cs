namespace GameConsole.Commands
{
    [ConsoleCommand]
    public class CommandListCommand : BaseCommand
    {
        public CommandListCommand() : base("cmdlist", "Provides list of available commands") { }

        private Message ExecuteCmd()
        {
            string commands = string.Empty;

            for (int i = 0; i < CommandsProvider.commands.Count; i++)
            {
                commands += CommandsProvider.commands[i].name + " " + CommandsProvider.commands[i].description;
                if(i < CommandsProvider.commands.Count - 1)
                {
                    commands += "\n";
                }
            }

            return new Message(commands, true);
        }
    }
}
