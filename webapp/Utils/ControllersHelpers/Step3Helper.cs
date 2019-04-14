using webapp.Models;
using webapp.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace webapp.Utils.ControllersHelpers
{
    public class Step3Helper
    {
        public static VisitSupplier CreateSupplier(SelectedSuppliers data)
        {
            var supplier = new VisitSupplier
            {
                SupplierId = data.Id,
                Text = data.Text
            };

            return supplier;
        }

        public static VisitManufacturers CreateManufacturer(SelectedManufacturers data)
        {
            var manufacturer = new VisitManufacturers
            {
                ManufacturerId = data.Id,
                GroupId = data.GroupId,
                Percent = data.Percent
            };

            return manufacturer;
        }

        public static VisitManufacturersGroup CreateManufacturersGroup(SelectedManufacturersGroups data)
        {
            var group = new VisitManufacturersGroup();
            group.Percent = data.Percent;
            group.ManufacturersGroupId = data.Id;
            group.SelectedManufacturers = data.SelectedManufacturers;

            return group;
        }

        public static IEnumerable<VisitSupplier> GetSuppliers(NewVisitStep3 data)
        {
            var suppliers = new List<VisitSupplier>();

            if (data.Suppliers == null) return suppliers;

            suppliers.AddRange(data.Suppliers.Select(CreateSupplier));

            return suppliers;
        }

        public static IEnumerable<VisitManufacturers> GetManufacturers(NewVisitStep3 data, ref int percentSum)
        {
            var manufacturers = new List<VisitManufacturers>();

            if (data.Manufacturers == null) return manufacturers;

            foreach (var m in data.Manufacturers.Where(manufacturer => manufacturer.Checked))
            {
                percentSum += m.Percent;
                var manufacturer = CreateManufacturer(m);
                manufacturers.Add(manufacturer);
            }

            return manufacturers;
        }

        public static IEnumerable<VisitManufacturersGroup> GetManufacturersGroups(NewVisitStep3 data, ref int percentSum)
        {
            var groups = new List<VisitManufacturersGroup>();

            if (data.ManufacturersGroups == null) return groups;

            foreach (var g in data.ManufacturersGroups.Where(group => group.Checked))
            {
                if (!data.Manufacturers.Any(m => m.GroupId == g.Id && m.Checked && m.Percent > 0))
                {
                    percentSum += g.Percent;
                    g.SelectedManufacturers = false;
                }
                else
                {
                    g.Percent = data.Manufacturers.Where(m => m.GroupId == g.Id && m.Checked && m.Percent > 0).Sum(m => m.Percent);
                    g.SelectedManufacturers = true;
                }
                var group = CreateManufacturersGroup(g);
                groups.Add(group);
            }

            return groups;
        }

        public static ClientStep3 AddDataToStep(ClientStep3 step, bool cooperation, IEnumerable<VisitManufacturers> manufacturers, IEnumerable<VisitManufacturersGroup> groups, IEnumerable<VisitSupplier> suppliers, int visitId)
        {
            step.Cooperation = cooperation;
            step.Manufacturers = (ICollection<VisitManufacturers>)manufacturers;
            step.ManufacturersGroups = (ICollection<VisitManufacturersGroup>)groups;
            step.Suppliers = (ICollection<VisitSupplier>)suppliers;
            if(cooperation == false && step.Suppliers != null)
            {
                step.Suppliers.Clear();
            }
            step.VisitId = visitId;

            return step;
        }

        public static ClientStep3 GetAndClearStep(int visitId, WebappContext context)
        {
            var step = context.ClientSteps3.AsEnumerable().Last(s => s.VisitId == visitId);
            step.Manufacturers.Clear();
            step.ManufacturersGroups.Clear();
            step.Suppliers.Clear();
            context.Entry(step).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
            return step;
        }
    }
}