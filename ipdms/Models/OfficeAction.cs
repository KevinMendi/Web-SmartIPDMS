using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ipdms.Models
{
    public class OfficeAction
    {
        [Key]
        [JsonPropertyName("officeActionId")]
        public int office_action_id { get; set; }

        [JsonPropertyName("officeActionName")]
        public string office_action_name { get; set; }

        [JsonPropertyName("officeActionDesc")]
        public string office_action_desc { get; set; }

        [JsonPropertyName("officeActionDue")]
        public int? office_action_due { get; set; }
    }
}
