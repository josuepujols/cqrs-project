using CQRS.Infrastructure.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Infrastructure.Handlers
{
    public class DeleteTaskHanlder : IRequestHandler<DeleteTaskCommand, bool>
    {
        private readonly ApplicationDbContext _context;
        public DeleteTaskHanlder(ApplicationDbContext context)
            => _context = context;

        public async Task<bool> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _context.TaskItems.FindAsync(new object[] { request.Id }, cancellationToken);
            if (task is null)
                return false;

            _context.TaskItems.Remove(task);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
