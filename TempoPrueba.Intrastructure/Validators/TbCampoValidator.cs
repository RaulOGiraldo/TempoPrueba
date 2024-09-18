using FluentValidation;
using InsttanttFlujos.Core.Entities;

namespace InsttanttFlujos.Intrastructure.Validators
{
    public class TbCampoValidator : AbstractValidator<TbCampo>
    {
        public TbCampoValidator()
        {
            RuleFor(x => x.IdCampo)
                .NotNull()
                .NotEmpty()
                .Length(1, 7);

            RuleFor(x => x.Nombre)
                .NotNull()
                .NotEmpty()
                .Length(1, 100);

            RuleFor(x => x.Tipo)
                .NotNull()
                .NotEmpty()
                .Length(1, 50);

            RuleFor(x => x.Longitud)
                .NotNull()
                .GreaterThan(0);
        }
    }
}
