namespace GameConsole.Commands
{
    [ConsoleCommand]
    public class EchoCommand : BaseCommand
    {
        public EchoCommand() : base("echo", "Logs provided string to console output window.") { }

        private Message ExecuteCmd(string message)
        {
            return new Message(message, true);
        }
    }
}
