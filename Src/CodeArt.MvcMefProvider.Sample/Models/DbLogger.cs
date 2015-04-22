using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CodeArt.MvcMefProvider.Sample.Models
{
    public class DbLogger : ILogger
    {
        private readonly ProductDbContext _context;

        public DbLogger(ProductDbContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            _context = context;
        }
        
        public void WriteEntry(string logEntry)
        {
            var entry = new LogEntry();
            entry.EntryTime = DateTime.Now;
            entry.Message = logEntry;

            _context.Entry(entry).State = EntityState.Added;
        }
    }
}