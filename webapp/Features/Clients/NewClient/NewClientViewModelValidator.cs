using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapp.Features.Clients.NewClient
{
    public class NewClientViewModelValidator : AbstractValidator<NewClientViewModel>
    {
        public NewClientViewModelValidator(NewClientDao dao)
        {
            RuleFor(x => x.Model.NIP).Must(x => !dao.NipExists(x)).WithMessage("Podany NIP już istnieje");
        }
    }
}