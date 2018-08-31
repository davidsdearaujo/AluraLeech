using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluraBot.Browser.Entidades
{
    public class OnVideoDownloadedEventArgs
    {
        public int Count { get; internal set; }
        public int Downloaded { get; internal set; }
        public string Path { get; internal set; }
    }
}
