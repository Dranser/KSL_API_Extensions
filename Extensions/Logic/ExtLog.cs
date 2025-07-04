using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace KSL.API.Extensions
{
    public static class ExtLog
    {
        private class LogEntry
        {
            public string Message;
            public string Tag;
            public int Count;

            public override string ToString()
            {
                return Count > 1 ? Tag + ": " + Message + " (x" + Count + ")" : Tag + ": " + Message;
            }
        }

        private static readonly ConcurrentDictionary<string, LogEntry> _logBuffer = new ConcurrentDictionary<string, LogEntry>();
        private const int Threshold = 128;

        public static void Info(string message, bool spamProtected = false, [CallerMemberName] string caller = "", [CallerFilePath] string file = "")
        {
            Log(message, Kino.Log.Info, caller, file, spamProtected);
        }

        public static void Warning(string message, bool spamProtected = false, [CallerMemberName] string caller = "", [CallerFilePath] string file = "")
        {
            Log(message, Kino.Log.Warning, caller, file, spamProtected);
        }

        public static void Error(string message, bool spamProtected = false, [CallerMemberName] string caller = "", [CallerFilePath] string file = "")
        {
            Log(message, Kino.Log.Error, caller, file, spamProtected);
        }

        public static void Error(Exception ex, bool spamProtected = false, [CallerMemberName] string caller = "", [CallerFilePath] string file = "")
        {
            Log(ex != null ? ex.ToString() : "Unknown exception", Kino.Log.Error, caller, file, spamProtected);
        }

        private static void Log(string message, Action<string> target, string caller, string file, bool spamProtected)
        {
            string origin = "[" + System.IO.Path.GetFileNameWithoutExtension(file) + "." + caller + "]";
            string key = origin + message;

            if (!spamProtected)
            {
                target(origin + ": " + message);
                return;
            }

            var entry = _logBuffer.AddOrUpdate(
                key,
                k => new LogEntry { Message = message, Tag = origin, Count = 1 },
                (k, existing) =>
                {
                    existing.Count++;
                    return existing;
                });

            if (entry.Count >= Threshold)
            {
                target(entry.ToString());
                _logBuffer.TryRemove(key, out _);
            }
        }
    }
}
