using CQRS.Application.DTOs;
using CQRS.Infrastructure.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Infrastructure.Handlers
{
    public class UpdateTaskHanlder : IRequestHandler<UpdateTaskCommand, TaskItemDto>
    {
        private readonly ApplicationDbContext _context;
        public UpdateTaskHanlder(ApplicationDbContext context)
            => _context = context;
        public async Task<TaskItemDto> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _context.TaskItems.FindAsync(new object[] { request.Id }, cancellationToken);
            if (task is null)
                return null;

            task.Title = request.Title;
            task.Description = request.Description;
            task.IsCompleted = request.IsCompleted;

            await _context.SaveChangesAsync(cancellationToken);

            return new TaskItemDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                IsCompleted = task.IsCompleted
            };
        }
    }
}
