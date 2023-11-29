using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalMessageDumper.JsonTypes
{
    public class MessageAttachment
    {
        public string contentType { get; set; }
        public ulong size { get; set; }

        public string fileName { get; set; }
        public ulong flags { get; set; }

        public ulong width { get; set; }

        public ulong height { get; set; }

        public ulong uploadTimestamp { get; set; }

        public ulong cdnNumber { get; set; }

        public string cdnKey { get; set; }
        public string path { get; set; }
        public Thumbnail thumbnail { get; set; }
    }

}
