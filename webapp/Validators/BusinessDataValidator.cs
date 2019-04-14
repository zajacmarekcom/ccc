using webapp.Models;
using FluentValidation;

namespace webapp.Validators
{
    public class BusinessDataValidator : AbstractValidator<BusinessData>
    {
        public BusinessDataValidator()
        {
            RuleFor(d => d.RecipientName).NotEmpty();
            RuleFor(d => d.Status).NotEmpty();
            RuleFor(d => d.Sap).NotEmpty().When(d => d.CooperationTypeId == 1 && d.Status == 1);
            RuleFor(d => d.CooperationTypeId).NotNull().When(d => d.Status == 1);
            
            RuleFor(d => d.ContactPerson).NotEmpty().When(d => d.Status == 1);
            RuleFor(d => d.ContactPersonPosition).NotEmpty().When(d => d.Status == 1);
            RuleFor(d => d.LegalFormId).NotNull().When(d => d.Status == 1);
            
            RuleFor(d => d.AgentId).NotNull().When(d => d.Status == 1);
            RuleFor(d => d.PhoneNumber).NotEmpty().When(d => d.Status == 1);
            RuleFor(d => d.Owner).NotEmpty().When(d => d.Status == 1);
            RuleFor(d => d.StartYear).NotEmpty().When(d => d.Status == 1);

            When(d => d.Status != 2, () =>
            {
                RuleFor(d => d.Street).NotEmpty();
                RuleFor(d => d.BuildingNumber).NotEmpty();
                RuleFor(d => d.City).NotEmpty();
                RuleFor(d => d.PostCode).NotEmpty();
                RuleFor(d => d.ProvinceId).NotNull();
                RuleFor(d => d.DistrictId).NotNull();
                RuleFor(d => d.NIP)
                .Matches("^((\\d{3}-\\d{2}-\\d{2}-\\d{3})|(\\d{3}-\\d{3}-\\d{2}-\\d{2}))$")
                .WithMessage("Nieprawidłowy NIP");
                RuleFor(d => d.NIP).NotEmpty();
            });
        }
    }
}