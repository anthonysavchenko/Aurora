namespace Taumis.Alpha.Infrastructure.Interface.Commands
{
    public interface ICommandDispatcher
    {
        void Execute<TCommand>(TCommand command) where TCommand : ICommand;
    }
}
