using FluentValidation;
using ToDoApp.Services.Dtos;

namespace ToDoApp.Api.Validators
{
    public class CreateTaskValidator : AbstractValidator<CreateTaskDto>
    {
        public CreateTaskValidator() 
        {
            RuleFor(x => x.Title)
                .MaximumLength(50);

            RuleFor(x => x.Description)
                .MaximumLength(150);
        }
    }
}
