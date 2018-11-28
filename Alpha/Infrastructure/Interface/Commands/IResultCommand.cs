namespace Taumis.Alpha.Infrastructure.Interface.Commands
{
    public interface IResultCommand<TResult> : ICommand
    {
        TResult Result { get; }
    }
}
