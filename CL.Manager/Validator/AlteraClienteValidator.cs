using CL.Core.Shared.ModelView;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CL.Manager.Validator
{
    public class AlteraClienteValidator : AbstractValidator<AlteraCliente>
    {
        public AlteraClienteValidator()
        {
            RuleFor(p => p.Id).NotEmpty().NotNull().GreaterThan(0); //Seja maior do que zero
            Include(new NovoClienteValidator());
        }
    }
}
