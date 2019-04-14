using System;
using webapp.Enums;
using webapp.Models.ViewModels;

namespace webapp.Helpers.Builders
{
    public class BusinessItemBuilder
    {
        public static BusinessItem CreateBusinessItem(int businessId, string name, DateTime visitDate, int status,
            DateTime addDate)
        {
            var model = new BusinessItem
            {
                Id = businessId,
                Name = name,
                Status = (BusinessStatus)status,
                VisitDate = visitDate,
                AddDate = addDate
            };

            return model;
        }
    }
}