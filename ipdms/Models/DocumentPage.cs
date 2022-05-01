using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ipdms.Models
{
    public class DocumentPage
    {
        [Key]
        [JsonPropertyName("documentPageId")]
        public int document_page_id { get; set; }

        [JsonPropertyName("documentId")]
        public int document_id { get; set; }

        [JsonPropertyName("path")]
        public string path { get; set; }
    }
}
