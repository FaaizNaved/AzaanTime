using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamazTimeApp.Core
{
    public class EntityBase
    {
        public int CREATED_ID { get; set; }
        public DateTime CREATED_DATE { get; set; }

        public int? UPDATED_ID { get; set; }
        public DateTime? UPDATED_DATE { get; set; }

        public bool IS_ACTIVE { get; set; }

        public DateTime? DELETED_AT { get; set; }
        public int? DELETED_BY { get; set; }

        public string? RECORD_SOURCE_NAME { get; set; }
    }
}
