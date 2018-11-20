namespace Taumis.Alpha.Infrastructure.Interface.Commands
{
    public interface ICommandHandlerAdapter
    {
        bool Execute(object command);
    }
}
