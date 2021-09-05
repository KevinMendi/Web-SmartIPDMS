using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ipdms.Models
{
    public class IpdmsUser
    {
        [Key]
        public int user_id { get; set; }

        public string first_name { get; set; }

        public string last_name { get; set; }

        public string middle_name { get; set; }

        public int user_role_id { get; set; }

        public string email { get; set; }

        public string password { get; set; }

        public string CREATE_USER_ID { get; set; }

        public DateTime? CREATE_USER_DATE { get; set; }

        public string LAST_UPDATE_USER_ID { get; set; }

        public DateTime? LAST_UPDATE_USER_DATE { get; set; }

        //[Key]
        //public int UserId { get; set; }

        //public string FirstName { get; set; }

        //public string LastName { get; set; }

        //public string MidleInitial { get; set; }

        //public int UserRoleId { get; set; }

        //public string Email { get; set; }

        //public string Password { get; set; }
    }
}
