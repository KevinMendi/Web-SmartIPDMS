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
        [JsonPropertyName("documentId")]
        public int document_id { get; set; }

        [JsonPropertyName("officeActionId")]
        public int office_action_id { get; set; }

        [JsonPropertyName("projectId")]
        public int project_id { get; set; }

        [JsonPropertyName("pdfName")]
        public string pdf_name { get; set; }

        [JsonPropertyName("mailDate")]
        public DateTime? mail_date { get; set; }

        [JsonPropertyName("fillingDate")]
        public DateTime? filling_date { get; set; }

        [JsonPropertyName("createUserId")]
        public int CREATE_USER_ID { get; set; }

        [JsonPropertyName("createUserDate")]
        public DateTime? CREATE_USER_DATE { get; set; }

        [JsonPropertyName("lastUpdateUserId")]
        public int LAST_UPDATE_USER_ID { get; set; }

        [JsonPropertyName("lastUpdateUserDate")]
        public DateTime? LAST_UPDATE_USER_DATE { get; set; }
    }
}
