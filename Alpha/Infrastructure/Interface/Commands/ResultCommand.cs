namespace Taumis.Alpha.Infrastructure.Interface.Commands
{
    public abstract class ResultCommand<TResult> : IResultCommand<TResult>
    {
        public TResult Result { get; set; }
    }
}
