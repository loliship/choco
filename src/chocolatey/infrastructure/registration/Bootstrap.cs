﻿using log4net.Config;

[assembly: XmlConfigurator(Watch = true)]


namespace chocolatey.infrastructure.registration
{
    using System;
    using log4net;
    using chocolatey.infrastructure;
    using logging;

    /// <summary>
    /// Application bootstrapping - sets up logging and errors for the app domain
    /// </summary>
    public class Bootstrap
    {
        private static readonly log4net.ILog _logger = LogManager.GetLogger(typeof(Bootstrap));

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public static void initialize()
        {
            //initialization code 
            _logger.Debug("XmlConfiguration is now operational");
        }

        /// <summary>
        /// Startups this instance.
        /// </summary>
        public static void startup()
        {
            AppDomain.CurrentDomain.UnhandledException += DomainUnhandledException;
            _logger.DebugFormat("Performing bootstrapping operations for '{0}'.", ApplicationParameters.name);
            
            Log.InitializeWith<Log4NetLog>();
        }

        /// <summary>
        /// Handles unhandled exception for the application domain.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.UnhandledExceptionEventArgs"/> instance containing the event data.</param>
        private static void DomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;
            var exceptionMessage = string.Empty;
            if (ex != null)
            {
                exceptionMessage = ex.ToString();
            }
            _logger.ErrorFormat("{0} had an error on {1} (with user {2}):{3}{4}",
                                ApplicationParameters.name,
                                Environment.MachineName,
                                Environment.UserName,
                                Environment.NewLine,
                                exceptionMessage
                );
        }

        /// <summary>
        /// Shutdowns this instance.
        /// </summary>
        public static void shutdown()
        {
            _logger.DebugFormat("Performing shutdown operations for '{0}'.", ApplicationParameters.name);
        }
    }
}