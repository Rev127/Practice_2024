using FluentValidation;
using ToDoApp.Services.Dtos;

namespace ToDoApp.Api.Validators
{
    public class CreateBoardValidator : AbstractValidator<CreateBoardDto>
    {
        public CreateBoardValidator() 
        {
            RuleFor(x => x.Name)
                .MaximumLength(50);
        }
    }
}
