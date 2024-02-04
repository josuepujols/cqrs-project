using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Infrastructure.Commands
{
    public record DeleteTaskCommand(int Id) : IRequest<bool>;
}
