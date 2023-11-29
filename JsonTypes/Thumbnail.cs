using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalMessageDumper.JsonTypes
{
    public class Thumbnail
    {
        public string contentType { get; set; }
        public ulong width { get; set; }
        public ulong height { get; set; }
        public string path { get; set; }
    }
}
