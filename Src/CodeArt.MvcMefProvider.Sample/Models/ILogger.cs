using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CodeArt.MvcMefProvider.Sample.Models
{
    public interface ILogger
    {
        void WriteEntry(string logEntry);
    }
}