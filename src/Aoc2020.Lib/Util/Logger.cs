using System;
using System.IO;
using System.Threading.Tasks;

namespace Aoc2020.Lib.Util
{
    public class Logger : IDisposable
    {
        private readonly string filename;
        private readonly StreamWriter file;

        private Logger(string filename)
        {
            this.filename = filename;
            this.file = new StreamWriter(filename, true);
        }

        public static Logger Create(string filename, bool overwrite = true)
        {
            if (overwrite && File.Exists(filename))
            {
                File.Delete(filename);
            }
            return new Logger(filename);
        }

        public async Task LogAsync(string message)
        {
            await this.file.WriteLineAsync($"{DateTime.UtcNow.ToString("HH:mm:ffff")}: {message}");
        }

        public void Log(string message)
        {
            this.file.WriteLine($"{DateTime.UtcNow.ToString("HH:mm:ffff")}: {message}");
        }

        public void Dispose()
        {
            if (this.file != null)
            {
                file.Dispose();
            }
        }
    }
}
