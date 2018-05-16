namespace Taumis.Alpha.Infrastructure.Interface.Commands
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private ICommandHandlerAdapter[] _handlers;

        public CommandDispatcher(params ICommandHandlerAdapter[] handlers)
        {
            _handlers = handlers;
        }

        public void Execute<TCommand>(TCommand command) where TCommand : ICommand
        {
            foreach (var _h in _handlers)
            {
                _h.Execute(command);
            }
        }
    }
}
