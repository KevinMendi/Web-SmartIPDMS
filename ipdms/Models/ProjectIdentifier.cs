using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ipdms.Models
{
    public class ProjectIdentifier
    {
        public int? OfficeActionId { get; set; }

        public string OfficeActionName { get; set; }

        public int ProjectId { get; set; }

        public string PdfName { get; set; }

        public int PdfFileSize { get; set; }

        public int? ApplicationTypeId { get; set; }

        public string ApplicationTypeName { get; set; }

        public string ApplicationNo { get; set; }

        public string MailDate { get; set; }
    }
}
