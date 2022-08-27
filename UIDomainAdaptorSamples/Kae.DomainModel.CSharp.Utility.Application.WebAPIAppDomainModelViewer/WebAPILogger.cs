// Copyright (c) Knowledge & Experience. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using Kae.DomainModel.CSharp.Utility.Application.WebAPIAppDomainModelViewer.Controllers;
using Kae.Utility.Logging;

namespace Kae.DomainModel.CSharp.Utility.Application.WebAPIAppDomainModelViewer
{
    public class WebAPILogger : Logger
    {
        ILogger<DomainModelController> logger;

        public WebAPILogger(ILogger<DomainModelController> logger)
        {
            this.logger = logger;
        }

        protected override async Task LogInternal(Level level, string log, string timestamp)
        {
            switch (level)
            {
                case Level.Info:
                    logger.LogInformation($"{timestamp}: {log}");
                    break;
                case Level.Warn:
                    logger.LogWarning($"{timestamp}: {log}");
                    break;
                case Level.Erro:
                    logger.LogError($"{timestamp}: {log}");
                    break;
            }
        }
    }
}
