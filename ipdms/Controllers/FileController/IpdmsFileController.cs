using Google.Cloud.Vision.V1;
using ipdms.Models;
using ipdms.Models.AppDbContext;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using System.Text;
using System.IO;
using System.Globalization;
using System.Threading;


namespace ipdms.Controllers.FileController
{
    [ApiController]
    [Route("[controller]")]
    public class IpdmsFileController : ControllerBase
    {
        private readonly IpdmsDbContext _context;

        public IpdmsFileController(IpdmsDbContext context)
        {
            _context = context;
        }

        //POST: api/IpdmsFile/register/project
        [HttpPost]
        public async Task<ActionResult<string>> SaveProject([FromBody] string data)
        {
            try
            {
                var result = JsonConvert.DeserializeObject<JToken>(data);
                var mailDate = new DateTime();

                if (string.IsNullOrEmpty(result["mailingDate"].ToString()))
                {
                    var pdefBase64 = new IpdmsFile()
                    {
                        image64 = result["pdfBase64"].ToString()
                    };

                    var projectIdentifier = new ProjectIdentifier();
                    projectIdentifier = AnalyzeImage(pdefBase64);
                    mailDate = DateTime.Parse(projectIdentifier.MailDate);
                }
                else
                {
                    mailDate = DateTime.ParseExact(result["mailingDate"].ToString(), "dd/MM/yyyy", null);
                }

                var project = new Project()
                {
                    ipdms_user_id = (int)result["agentName"],
                    applicant_name = result["applicantName"].ToString(),
                    application_no = result["applicationNo"].ToString(),
                    application_type_id = (int)result["applicationTypeId"],
                    project_title = result["projectTitle"].ToString(),
                    project_path = $"{Constants.Constants.projectBase}/{result["applicationTypeId"]}-{result["applicationNo"]}",
                    CREATE_USER_ID = (int)result["createUserId"],
                    CREATE_USER_DATE = DateTime.Now,
                    LAST_UPDATE_USER_ID = (int)result["lastUpdateUserId"],
                    LAST_UPDATE_USER_DATE = DateTime.Now
                };

                _context.Project.Add(project);
                await _context.SaveChangesAsync();
                int projectId = project.project_id;

                var document = new Document()
                {
                    office_action_id = (int)result["officeActionId"],
                    project_id = projectId,
                    mail_date = mailDate,
                    filling_date = DateTime.ParseExact(result["fillingDate"].ToString(), "dd/MM/yyyy", null),
                    pdf_name = result["fileName"].ToString(),
                    CREATE_USER_ID = (int)result["createUserId"],
                    CREATE_USER_DATE = DateTime.Now,
                    LAST_UPDATE_USER_ID = (int)result["lastUpdateUserId"],
                    LAST_UPDATE_USER_DATE = DateTime.Now
                };

                _context.Document.Add(document);
                await _context.SaveChangesAsync();


                var folderPath = $"{project.application_type_id}-{project.application_no}/";
                System.IO.Directory.CreateDirectory($"{Constants.Constants.projectBase}{folderPath}");
                using (FileStream stream = System.IO.File.Create($"{Constants.Constants.projectBase}{folderPath}{result["fileName"]}"))
                {
                    byte[] byteArray = Convert.FromBase64String((result["pdfBase64"].ToString()).Remove(0, 28));
                    stream.Write(byteArray, 0, byteArray.Length);
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
          
            return "Successfully Saved Project!";
        }

        //POST: api/IpdmsFile/analyze-file
        [HttpPost("analyze/image")]
        public ProjectIdentifier AnalyzeImage(IpdmsFile file)
        {
            var fileContent = "";
            var fileContentList = new List<string>();
            int n = 22;
            file.image64 = file.image64.Remove(0, n);
            byte[] bytes = Convert.FromBase64String(file.image64);
            var imageAnalysisResult = new ProjectIdentifier();
            Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);
            }

            System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS",
                "C:/kmendi/smart-ipdms/ipdms/ClientApp/CSharp_SA.json");

            var client = ImageAnnotatorClient.Create();
            var response = client.DetectText(image);

            if (response[0].Description != null)
            {
                fileContent = response[0].Description;
                fileContentList = fileContent.Split('\n').ToList();
                imageAnalysisResult = ExtractProjectIdentifier(fileContentList);

            }
            return imageAnalysisResult;
        }

        public ProjectIdentifier ExtractProjectIdentifier(List<string> extractedText)
        {
            var projectIdentifier = new ProjectIdentifier();

            var officeActionList = new List<string>(){
                "SUBSTANTIVE EXAMINATION REPORT",
                "FORMALITY EXAMINATION REPORT",
                "NOTICE OF WITHDRAWN APPLICATION",
                "NOTICE OF PUBLICATION",
                "NOTICE OF ISSUANCE OF CERTIFICATE",
                "Revival Order",
                "Certificate of Registration",
                "Acknowledgement" 
            };

            var applicationTypeList = new List<string>(){
                "Industrial Design",
                "Invention",
                "Utility Model"
            };

            var monthsList = new List<string>(){
                "JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC"
            };

            var officeAction = officeActionList.Intersect(extractedText).ToList();
            if (officeAction != null)
            {
                projectIdentifier.OfficeAction = officeAction[0];
            }

            string applicationType = "";
            foreach (var a in applicationTypeList)
            {
                applicationType = extractedText.FirstOrDefault(s => s.Contains(a));

                if(applicationType != null)
                {
                    projectIdentifier.ApplicationType = applicationType;
                    break;
                }
            }

            string mailDate = "";
            foreach (var m in monthsList)
            {
                mailDate = extractedText.FirstOrDefault(s => s.Contains(m));

                if (mailDate != null)
                {
                    projectIdentifier.MailDate = mailDate.Trim();
                    break;
                }
            }

            //Determine Mail date Format
            var result = Regex.Replace(projectIdentifier.MailDate, @"[^A-Za-z0-9]+", "");

            var mailDateFormat = result.Substring(0, 3);
            var format1 = false;
            foreach (var ch in mailDateFormat)
            {
                //Check if it is digit
                if (!Char.IsDigit(ch))
                {
                    format1 = true; 
                }
                else
                {
                    format1 = false;
                }
            }

            if (format1)
            {
                projectIdentifier.MailDate = MailDateFormat1(result);
            }
            else{

            }

            return projectIdentifier;
        }

        public string MailDateFormat1(string dateToCheck)
        {
            var result = Regex.Replace(dateToCheck, @"[^A-Za-z0-9]+", "");

            //var month = Regex.Replace(result, @"[^A-Z]+", String.Empty);
            var monthCounter = 1;
            var monthInList = new List<char>();
            var monthStr = "";
            foreach (var ch in result)
            {
                if (monthCounter <= 3)
                {
                    if (!Char.IsDigit(ch))//if character
                    {
                        monthInList.Add(ch);
                        ++monthCounter;
                    }
                    else
                    {
                        //Call a function that handles different mail date format
                        Console.WriteLine("Call another function to handle this");
                    }
                }
                else
                {
                    monthCounter = 0;
                    //convert month in list type to string
                    monthStr = string.Join("", monthInList);
                    break;
                }

            }
            //Console.WriteLine(result);

            // mothStr contains the month in string type
            Console.WriteLine(monthStr);
            int monthNumber = 0;
            if (!String.IsNullOrEmpty(monthStr))
            {
                monthNumber = DateTime.ParseExact(monthStr, "MMM", CultureInfo.CurrentCulture).Month;
                Console.WriteLine(monthNumber);
            }


            //////////////day
            var dateFilter = result.Replace(monthStr, "");
            var dateInList = new List<char>();
            var dayStr = "";


            if (dateFilter != null)
            {
                dayStr = dateFilter.Substring(0, 2);

                foreach (var ch in dayStr)
                {
                    //Check if it is digit
                    if (Char.IsDigit(ch))
                    {
                        dateInList.Add(ch);
                    }
                    else
                    {
                        if (ch == 'O' || ch == 'o')
                        {
                            dateInList.Add('0');
                        }
                    }
                }
            }

            dayStr = string.Join("", dateInList);
            Console.WriteLine(dayStr);

            //////////////
            //Get Year

            var yearStr = "";

            if (dateFilter != null)
            {
                if (dateFilter.Length == 6)
                {
                    yearStr = dateFilter.Substring(dateFilter.Length - 4);
                }
                else if (dateFilter.Length == 4)
                {
                    yearStr = dateFilter.Substring(dateFilter.Length - 2);
                }
            }

            Console.WriteLine(yearStr);
            var mailDateStr = $"{dayStr}/{monthNumber.ToString().PadLeft(2, '0')}/{yearStr}";
            Console.WriteLine($"Mail Date: {mailDateStr}");

            return mailDateStr;
        }
    }
}
