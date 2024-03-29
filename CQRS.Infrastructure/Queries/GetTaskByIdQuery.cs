﻿using CQRS.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Infrastructure.Queries
{
    public record GetTaskByIdQuery(int Id) : IRequest<TaskItemDto>;
}
