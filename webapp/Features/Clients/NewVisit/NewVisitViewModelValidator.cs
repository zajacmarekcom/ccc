using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using webapp.Models.ViewModels;

namespace webapp.Features.Clients.NewVisit
{
    public class NewVisitViewModelValidator : AbstractValidator<NewVisitViewModel>
    {
        public NewVisitViewModelValidator()
        {
            RuleFor(x => x).Must(x => SumIsCorrect(x)).WithMessage("Całkowita liczba procent musi wynosić 100%");
            RuleFor(x => x).Must(x => MainMarketSegmentIsCorrect(x)).WithMessage("Wiodący segment rynku jest niż procentowy wybór sementów rynku");
        }

        private bool SumIsCorrect(NewVisitViewModel data)
        {
            return data.ClientStatus != 1 || data.Model.MarketSegments.Where(s => s.Checked).Sum(s => s.Percent) == 100;
        }

        private bool MainMarketSegmentIsCorrect(NewVisitViewModel data)
        {
            var max = data.Model.MarketSegments.Where(s => s.Checked).Select(s => s.Percent).DefaultIfEmpty().Max();
            var maxId = data.Model.MarketSegments.Where(s => s.Checked && s.Percent == max).Select(s => s.Id).FirstOrDefault();

            return data.ClientStatus != 1 || maxId == data.Model.Visit.MainMarketSegmentId;
        }
    }
}