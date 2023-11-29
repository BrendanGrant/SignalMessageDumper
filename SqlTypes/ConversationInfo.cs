using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalMessageDumper.SqlTypes
{
    [DebuggerDisplay("{name}")]
    [Table("conversations")]
    public class ConversationInfo
    {
        [Column("Id")]
        public string Id
        { get; set; }

        [Column("json")]
        public string JSON
        { get; set; }

        [Column("active_at")]
        public int active_at
        { get; set; }

        [Column("type")]
        public string type
        { get; set; }

        [Column("members")]
        public string members
        { get; set; }

        [Column("name")]
        public string name
        { get; set; }

        [Column("profileName")]
        public string profileName
        { get; set; }

        [Column("profileFamilyName")]
        public string profileFamilyName
        { get; set; }

        [Column("profileFullName")]
        public string profileFullName
        { get; set; }

        [Column("e164")]
        public string e164
        { get; set; }

        [Column("uuid")]
        public string uuid
        { get; set; }

        [Column("groupId")]
        public string groupId
        { get; set; }
    }
}
