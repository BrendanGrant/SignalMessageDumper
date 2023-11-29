using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalMessageDumper.SqlTypes
{
    [DebuggerDisplay("{Name}")]
    [Table("tables")]
    public class table
    {
        //[PrimaryKey]
        //[Column("id")]
        //public Guid Id
        //{ get; set; }

        [Column("type")]
        public string Type
        { get; set; }

        [Column("name")]
        public string Name
        { get; set; }


        [Column("tbl_name")]
        public string tbl_name
        { get; set; }


        [Column("rootpage")]
        public int RootPage
        { get; set; }


        [Column("sql")]
        public string SQL
        { get; set; }
    }
}
