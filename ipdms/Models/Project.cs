using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ipdms.Models
{
    public class Project
    {
        [Key]
        [JsonPropertyName("project_id")]
        public int projectId { get; set; }

        [JsonPropertyName("application_type_id")]
        public int applicationTypeId { get; set; }

        [JsonPropertyName("project_status_id")]
        public int projectStatusId { get; set; }

        [JsonPropertyName("ipdms_user_id")]
        public int ipdmsUserId { get; set; }

        [JsonPropertyName("application_no")]
        public string applicationNo { get; set; }

        [JsonPropertyName("project_title")]
        public string projectTitle { get; set; }

        [JsonPropertyName("applicant_name")]
        public string applicantName { get; set; }

        [JsonPropertyName("project_path")]
        public string projectPath { get; set; }

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
