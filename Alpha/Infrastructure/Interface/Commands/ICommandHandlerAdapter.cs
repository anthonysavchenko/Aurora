namespace Taumis.Alpha.Infrastructure.Interface.Commands
{
    public interface ICommandHandlerAdapter
    {
        void Execute(object command);
    }
}
