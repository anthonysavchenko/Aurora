using System.Collections.Generic;
using System.Linq;

namespace Taumis.Alpha.Infrastructure.Interface.Commands
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private List<ICommandHandlerAdapter> _handlers;

        public CommandDispatcher()
        {
            _handlers = new List<ICommandHandlerAdapter>();
        }

        public CommandDispatcher(params ICommandHandlerAdapter[] handlers)
        {
            _handlers = handlers.ToList();
        }

        public void AddHandler(ICommandHandlerAdapter handler)
        {
            _handlers.Add(handler);
        }

        public void AddHandlers(params ICommandHandlerAdapter[] handlers)
        {
            _handlers.AddRange(handlers);
        }

        public void Execute<TCommand>(TCommand command) where TCommand : ICommand
        {
            foreach (var _h in _handlers)
            {
                if (_h.Execute(command))
                    break;
            }
        }
    }
}
