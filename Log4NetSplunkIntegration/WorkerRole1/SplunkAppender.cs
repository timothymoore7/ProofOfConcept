using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using log4net.Appender;
using log4net.Core;
using log4net.Util;
using log4net.Layout;
using Microsoft.WindowsAzure.Diagnostics;
using Splunk.Logging;

namespace WorkerRole1
{
    public class SplunkAppender : AppenderSkeleton
    {
        protected override void Append(LoggingEvent loggingEvent)
        {
            LogLevel logLevel = LogLevel.Information;
            switch (loggingEvent.Level.Name)
            {
                case "DEBUG":
                    logLevel = LogLevel.Verbose;
                    break;
                case "WARN":
                    logLevel = LogLevel.Warning;
                    break;
                case "INFO":
                    logLevel = LogLevel.Information;
                    break;
                case "ERROR":
                    logLevel = LogLevel.Error;
                    break;
                case "FATAL":
                    logLevel = LogLevel.Critical;
                    break;
            }

            Log(loggingEvent.MessageObject.ToString(), logLevel);
        }

        public SplunkAppender()
        {
            //Put connection string here ending with :8088 - Exclude tail of "/services/collector/event/1.0" as it will be appended
            _connectionString = new Uri("https://input-IAmYourEndPoint.cloud.splunk.com:8088");
            _connectionToken = "I-R-SecretKey";

            _middleware = new HttpEventCollectorResendMiddleware(100);
            _collectorSender = new HttpEventCollectorSender(
                _connectionString,
                _connectionToken,
                null,
                HttpEventCollectorSender.SendMode.Sequential,
                0,
                0,
                0,
                _middleware.Plugin);
            _collectorSender.OnError += o => Trace.WriteLine(o.Message);
        }

        private readonly HttpEventCollectorResendMiddleware _middleware;
        private readonly HttpEventCollectorSender _collectorSender;
        private readonly Uri _connectionString;
        private readonly string _connectionToken;

        private void Log(string message, LogLevel logLevel)
        {
            try
            {
                //TODO: Remove once we get a full production instance.  This will bypass SSL certificate verification
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                ServicePointManager.ServerCertificateValidationCallback = AcceptAllCertifications;

                _collectorSender.Send(Guid.NewGuid().ToString(), logLevel.ToString(), message, null);

                //Flushing takes place automatically behind the scenes
                //_collectorSender.FlushSync();
                //_collectorSender.FlushAsync().Wait();

            }
            catch (Exception error)
            {
                Trace.WriteLine(error);
            }
        }

        private static bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}
