using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ipdms.Models
{
    public class Document
    {
        [Key]
        [JsonPropertyName("document_id")]
        public int documentId { get; set; }

        [JsonPropertyName("office_action_id")]
        public int officeActionId { get; set; }

        [JsonPropertyName("project_id")]
        public int projectId { get; set; }

        [JsonPropertyName("mail_date")]
        public DateTime? mailDate { get; set; }

        [JsonPropertyName("CREATE_USER_ID")]
        public string createUserId { get; set; }

        [JsonPropertyName("CREATE_USER_DATE")]
        public DateTime? createUserDate { get; set; }

        [JsonPropertyName("LAST_UPDATE_USER_ID")]
        public string lastUpdateUserId { get; set; }

        [JsonPropertyName("LAST_UPDATE_USER_DATE")]
        public DateTime? lastUpdateUserDate { get; set; }
    }
}
