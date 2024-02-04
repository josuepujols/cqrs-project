using CQRS.Application.DTOs;
using CQRS.Domain;
using CQRS.Infrastructure.Commands;
using MediatR;

namespace CQRS.Infrastructure.Handlers
{
    public class CreateTaskHandler
        : IRequestHandler<CreateTaskCommand, TaskItemDto>
    {
        private readonly ApplicationDbContext _context;
        public CreateTaskHandler(ApplicationDbContext context)
            => _context = context;
        public async Task<TaskItemDto> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var taskItem = new TaskItem
            {
                Title = request.Title,
                Description = request.Description
            };

            await _context.TaskItems.AddAsync(taskItem, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return new TaskItemDto 
            {
                Id = taskItem.Id,
                Title = taskItem.Title, 
                Description = taskItem.Description,
                IsCompleted = taskItem.IsCompleted
            };
        }
    }
}
