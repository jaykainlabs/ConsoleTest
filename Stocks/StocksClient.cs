using System;
using System.Collections.Generic;
using System.Text;
using NLog;

namespace Stocks
{
    public class StocksClient : IDisposable
    {
        private static ILogger _logger;
        private readonly SecConfig secConfig;
        public StocksClient(SecConfig secConfig, ILogger logger)
        {
            _logger = logger;
            _logger.Trace("Begin");

            _logger.Trace("End");
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
