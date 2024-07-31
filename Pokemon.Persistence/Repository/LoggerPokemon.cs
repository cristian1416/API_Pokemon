using Pokemon.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon.Persistence.Repository
{
    public class LoggerPokemon : ILogger1
    {
        private readonly ILogger1 _logger;

        public LoggerPokemon(ILogger1 logger)
        {
            _logger = logger;
        }

        public void Information(string message)
        {
            _logger.Information(message);
        }

        public void Error(Exception ex, string message)
        {
            _logger.Error(ex, message);
        }

        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Warning(string message)
        {
            _logger.Warning(message);
        }
    }
}
