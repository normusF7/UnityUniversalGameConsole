namespace GameConsole.Commands
{
    [ConsoleCommand]
    public class HelpCommand : BaseCommand
    {
        public HelpCommand() : base("help", "") { }

        private Message ExecuteCmd()
        {
            return new Message("Type 'cmdlist' to get list of all available commands.", true);
        }
    }
}
