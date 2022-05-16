namespace GameConsole.Commands
{
    [ConsoleCommand]
    public class AddTwoIntsCommand : BaseCommand
    {
        public AddTwoIntsCommand() : base("add", "Adds two ints.", "Int to add", "Int to add")
        {
        }

        public Message ExecuteCmd(int value0, int value1)
        {
            return new Message((value0 + value1).ToString(), true);
        }
    }
}
