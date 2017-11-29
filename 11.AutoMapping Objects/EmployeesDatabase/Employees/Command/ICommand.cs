namespace Employees.Client.Command
{
    interface ICommand
    {
        string Execute(params string[] data);
    }
}
