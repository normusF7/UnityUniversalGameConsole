namespace GameConsole.Commands
{
    [ConsoleCommand]
    public class EchoTestCommand : BaseCommand
    {
        public EchoTestCommand() : base("echoTest", 
            "Logs provided string to console output window. Second command for testing.", "Message to print") { }

        private Message ExecuteCmd(string message)
        {
            return new Message(message, true);
        }
    }
}
