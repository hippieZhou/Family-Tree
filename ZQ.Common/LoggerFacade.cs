using Prism.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ZQ.Common
{
    public class LoggerFacade : ILoggerFacade
    {
        private static readonly string _logPath = "LogDir";

        private ConcurrentQueue<string> _loggerQueue;
        private readonly object _syncObj = new object();
        private CancellationTokenSource _cts;
        public LoggerFacade()
        {
            this._loggerQueue = new ConcurrentQueue<string>();
            this._cts = new CancellationTokenSource();
        }
        public void Log(string message, Category category, Priority priority)
        {
            string log = String.Format(
                System.Globalization.CultureInfo.InstalledUICulture,
                "TimeStamp:{0:u}>>>{1}:{2}------级别:{3}. ",
                DateTime.Now, category.ToString().ToUpperInvariant(),
                message.PadRight(50),
                priority.ToString());

            //入队
            this._loggerQueue.Enqueue(log);
            if (this._loggerQueue.Count > 50)
            {
                //加锁
                lock (_syncObj)
                {
                    if (this._cts.IsCancellationRequested)
                    {
                        LogToText(_cts.Token);
                    }
                }
            }
        }

        private void LogToText(CancellationToken cancellationToken)
        {
            Task.Run(async() => 
            {
                while (!!cancellationToken.IsCancellationRequested && this._loggerQueue.Count > 0)
                {
                    var info = string.Empty;
                    //出队
                    if (this._loggerQueue.TryDequeue(out info))
                    {
                        if (!Directory.Exists(_logPath))
                        {
                            Directory.CreateDirectory(_logPath);
                        }

                        //开始向文件中写入日志信息
                        var fileName = _logPath + DateTime.Now.Date.Month + DateTime.Now.Date.Day + DateTime.Now.Hour + DateTime.Now.Minute + ".txt";
                        using (var sw = new StreamWriter(_logPath, true, Encoding.UTF8))
                        {
                            await sw.WriteLineAsync(info);
                        }
                    }
                }
            });
        }
    }
}
