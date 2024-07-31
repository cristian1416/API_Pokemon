using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon.Domain.Interfaces
{
    public interface ILogger1
    {
        void Information(string message);
        void Error(Exception ex, string message);
        void Debug(string message);
        void Warning(string message);
    }
}
