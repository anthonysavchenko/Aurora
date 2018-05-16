namespace Taumis.Alpha.Infrastructure.Interface.Commands
{
    public class CommandHandlerAdapter<TCommand> : ICommandHandlerAdapter
        where TCommand : ICommand
    {
        private readonly ICommandHandler<TCommand> _handler;

        public CommandHandlerAdapter(ICommandHandler<TCommand> handler)
        {
            _handler = handler;
        }

        public void Execute(object command)
        {
            if (command is TCommand)
            {
                _handler.Execute((TCommand)command);
            }
        }
    }
}
