using application.DTOs;
using FluentValidation;

namespace application.Validation
{
    public class CreateEmployeeDtoValidator : AbstractValidator<CreateEmployeeDto>
    {
        public CreateEmployeeDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("El nombre es obligatorio")
                .MaximumLength(50);

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("El apellido es obligatorio")
                .MaximumLength(50);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El correo es obligatorio")
                .EmailAddress().WithMessage("Formato de correo inválido")
                .MaximumLength(100);

            RuleFor(x => x.DateHired)
                .LessThanOrEqualTo(DateTime.Today)
                .WithMessage("La fecha de contratación no puede ser futura");

            RuleFor(x => x.DepartmentID)
                .GreaterThan(0).WithMessage("Debe seleccionar un departamento válido");
        }
    }
}
