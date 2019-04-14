using System.Data.Entity;
using webapp.Models;

namespace webapp.Repository
{
    public class SettingsRepository
    {
        private readonly WebappContext _context;

        public SettingsRepository(WebappContext context)
        {
            _context = context;
        }

        public string Get(string key)
        {
            var set = _context.Settings.Find(key);
            return set != null ? set.Value : string.Empty;
        }

        public void Set(string key, string value)
        {
            var set = _context.Settings.Find(key);
            if (set == null)
            {
                var newSet = new Settings
                {
                    Key = key,
                    Value = value
                };

                _context.Settings.Add(newSet);
                _context.SaveChanges();
            }
            else
            {
                set.Value = value;
                _context.Entry(set).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }
    }
}