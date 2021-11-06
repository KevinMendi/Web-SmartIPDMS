using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ipdms.Models
{
    public class ApplicationType
    {
        [Key]
        [JsonPropertyName("applicationTypeId")]
        public int application_type_id { get; set; }

        [JsonPropertyName("applicationTypeName")]
        public int application_type_name { get; set; }

        [JsonPropertyName("applicationTypeDesc")]
        public int application_type_desc { get; set; }
    }
}
