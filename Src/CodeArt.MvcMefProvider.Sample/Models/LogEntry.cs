using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeArt.MvcMefProvider.Sample.Models
{
    public class LogEntry
    {
        public int Id { get; set; }

        public DateTime EntryTime { get; set; }

        public string Message { get; set; }
    }
}