using Application.Features.Cliente.Commands.Create;
using Application.Features.Poliza.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Poliza.Validator
{
    public class PolizaValidator : AbstractValidator<PvHeaderCreateCommand>
    {
        public PolizaValidator()
        {
            RuleFor(x => x.TxtDescription)
                .NotEmpty().WithMessage("La descripción es obligatoria")
                .MaximumLength(100).WithMessage("la descripción no puede superar los 100 caracteres");
            RuleFor(x => x.CodRamo)
                .NotEqual(0).WithMessage("Ramo no puede ser cero");
            RuleFor(x => x.CodSuc)
                .NotEqual(0).WithMessage("Sucursal no puede ser cero");
            RuleFor(x => x.NroPol)
                .NotEqual(0).WithMessage("NroPol no puede ser cero");

        }
    }
}
