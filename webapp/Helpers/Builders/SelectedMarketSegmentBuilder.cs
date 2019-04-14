using webapp.Models;

namespace webapp.Helpers.Builders
{
    public class SelectedMarketSegmentBuilder
    {
        public static SelectedMarketSegment CreateSelectedMarketSegmentFromMarketSegment(MarketSegment segment)
        {
            var model = new SelectedMarketSegment()
            {
                Id = segment.Id,
                Name = segment.Name,
                Checked = false,
                Percent = 0,
                EId = 0
            };

            return model;
        }
    }
}