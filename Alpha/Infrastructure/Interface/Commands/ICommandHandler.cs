namespace Taumis.Alpha.Infrastructure.Interface.Commands
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        void Execute(TCommand command);
    }
}
