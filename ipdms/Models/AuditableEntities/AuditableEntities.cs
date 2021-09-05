using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ipdms.Models.AuditableEntities
{
    public abstract class AuditableEntities
    {
        public string CreateUserId { get; set; }

        public DateTime? CreateUserDate { get; set; }

        public string LastUpdateUserId { get; set; }

        public DateTime? LastUpdateUserDate { get; set; }
    }
}
