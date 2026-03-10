using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Projects.Commands.UpdateProject
{
    public class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
    {
        public UpdateProjectCommandValidator()
        {
            RuleFor(v => v.Name)
                 .MaximumLength(200)
                 .NotEmpty().WithMessage("Project name is required.");

            RuleFor(v => v.Deadline)
                .GreaterThan(DateTime.UtcNow)
                .When(v => v.Deadline.HasValue)
                .WithMessage("The deadline must be in the future.");
        }
    }
}
