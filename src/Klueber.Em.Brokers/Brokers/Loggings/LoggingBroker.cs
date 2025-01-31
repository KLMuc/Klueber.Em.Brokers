﻿using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;

namespace Klueber.Em.Brokers.Brokers.Loggings
{
    [ExcludeFromCodeCoverage]
    public class LoggingBroker : ILoggingBroker
    {
        private readonly ILogger logger;

        public LoggingBroker(ILogger logger) => this.logger = logger;

        public void LogCritical(Exception exception) =>
            this.logger.LogCritical(exception, exception.Message);

        public void LogDebug(string message) => this.logger.LogDebug(message);

        public void LogError(Exception exception) =>
            this.logger.LogError(exception, exception.Message);

        public void LogInformation(string message) => this.logger.LogInformation(message);

        public void LogTrace(string message) => this.logger.LogTrace(message);

        public void LogWarning(string message) => this.logger.LogWarning(message);
    }
}