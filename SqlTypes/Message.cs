using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalMessageDumper.SqlTypes
{
    [Table("message")]
    public class Message
    {
        public long rowid { get; set; }
        public string id { get; set; }
        public string json { get; set; }
        public long readStatus { get; set; }
        public long expires_at { get; set; }
        public long sent_at { get; set; }
        public long schemaVersion { get; set; }
        public string conversationId { get; set; }
        public long received_at { get; set; }
        public string source { get; set; }
        public long hasAttachments { get; set; }
        public long hasFileAttachments { get; set; }
        public long hasVisualMediaAttachments { get; set; }
        public long expireTimer { get; set; }
        public long expirationStartTimestamp { get; set; }
        public string type { get; set; }
        public string body { get; set; }
        public long messageTimer { get; set; }
        public long messageTimerStart { get; set; }
        public long messageTimerExpiresAt { get; set; }
        public long isErased { get; set; }
        public long isViewOnce { get; set; }
        public string sourceServiceId { get; set; }
        public string serverGuid { get; set; }
        public long sourceDevice { get; set; }
        public string storyId { get; set; }
        public long isStory { get; set; }
    }

}
