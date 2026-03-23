using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProjectTasks.Commands.CreateProjectTask
{
    public class CreateProjectTaskCommandValidator : AbstractValidator<CreateProjectTaskCommand>
    {
        public CreateProjectTaskCommandValidator()
        {
            RuleFor(v => v.Title)
                .MaximumLength(200)
                .NotEmpty().WithMessage("Title is required.");

            RuleFor(v => v.ProjectId)
                .NotEmpty().WithMessage("Project Id is required.");

            RuleFor(v => v.PriorityLevel)
                .NotEmpty().WithMessage("Priority is required.");

        }
    }
}
