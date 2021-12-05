﻿using Google.Cloud.Vision.V1;
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
using Microsoft.EntityFrameworkCore;


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

                var folderBaseName = result["applicationTypeId"].ToString() == "1" ? "Invention" : "Utility Model";
                result["applicationNo"] = "1/2014/000317";
                var folderName = (result["applicationNo"].ToString()).Replace("/", "_");

                if (string.IsNullOrEmpty(result["projectTitle"].ToString()))
                {
                    var documentDetails = (from p in _context.Project
                                           where p.project_id == (int)result["projectId"]
                                           select p).FirstOrDefault();


                    result["projectTitle"] = documentDetails.project_title;
                    result["applicantName"] = documentDetails.applicant_name;
                    result["agentName"] = documentDetails.ipdms_user_id;


                }

                //save PDF
                var folderPath = $"{folderBaseName}_{folderName}/";
                var fileSize = 0;
                byte[] byteArray = Convert.FromBase64String((result["pdfBase64"].ToString()).Remove(0, 28));
                System.IO.Directory.CreateDirectory($"{Constants.Constants.projectBase}{folderPath}");
                using (FileStream stream = System.IO.File.Create($"{Constants.Constants.projectBase}{folderPath}{result["fileName"]}"))
                {
                    
                    stream.Write(byteArray, 0, byteArray.Length);
                    int counter = 0;
                    decimal number = (decimal)byteArray.Length;
                    while (Math.Round(number / 1024) >= 1)
                    {
                        number = number / 1024;
                        counter++;
                    }
                    fileSize = (int)Math.Ceiling(number);                
                }

                var ifProjectExist = _context.Project.Any(x => x.application_no == result["applicationNo"].ToString().Trim());

                if (!ifProjectExist)
                {
                    var project = new Project()
                    {
                        ipdms_user_id = (int)result["agentName"],
                        applicant_name = result["applicantName"].ToString(),
                        application_no = result["applicationNo"].ToString(),
                        application_type_id = (int)result["applicationTypeId"],
                        project_title = result["projectTitle"].ToString(),
                        project_path = $"{Constants.Constants.projectPath}{folderBaseName}_{folderName}",
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
                        filling_date = string.IsNullOrEmpty(result["fillingDate"].ToString()) == true ? DateTime.Now : DateTime.ParseExact(result["fillingDate"].ToString(), "dd/MM/yyyy", null),
                        pdf_name = result["fileName"].ToString(),
                        //pdf_content = result["pdfBase64"].ToString(),
                        pdf_file_size = fileSize,
                        CREATE_USER_ID = (int)result["createUserId"],
                        CREATE_USER_DATE = DateTime.Now,
                        LAST_UPDATE_USER_ID = (int)result["lastUpdateUserId"],
                        LAST_UPDATE_USER_DATE = DateTime.Now
                    };

                    _context.Document.Add(document);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var document = new Document()
                    {
                        office_action_id = (int)result["officeActionId"],
                        project_id = (int)result["projectId"],
                        mail_date = mailDate,
                        filling_date = string.IsNullOrEmpty(result["fillingDate"].ToString()) == true ? DateTime.Now : DateTime.ParseExact(result["fillingDate"].ToString(), "dd/MM/yyyy", null),
                        pdf_name = result["fileName"].ToString(),
                        //pdf_content = result["pdfBase64"].ToString(),
                        pdf_file_size = fileSize,
                        CREATE_USER_ID = (int)result["createUserId"],
                        CREATE_USER_DATE = DateTime.Now,
                        LAST_UPDATE_USER_ID = (int)result["lastUpdateUserId"],
                        LAST_UPDATE_USER_DATE = DateTime.Now
                    };

                    _context.Document.Add(document);
                    await _context.SaveChangesAsync();
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
            //int n = 22;
            //file.image64 = file.image64.Remove(0, n);
            //byte[] bytes = Convert.FromBase64String(file.image64);

            byte[] bytes = Convert.FromBase64String((file.image64).Remove(0, 28));

            string pngBase64 = Convert.ToBase64String(bytes);

            byte[] pngBytes = Convert.FromBase64String((pngBase64));
            //convert to image
            byte[] pngByte = Freeware.Pdf2Png.Convert(bytes, 1);

            var imageAnalysisResult = new ProjectIdentifier();
            Image image;
            using (MemoryStream ms = new MemoryStream(pngByte))
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
            

            //var officeActionList = new List<string>(){
            //    //"SUBSTANTIVE EXAMINATION REPORT",
            //    //"FORMALITY EXAMINATION REPORT",
            //    //"NOTICE OF WITHDRAWN APPLICATION",
            //    //"NOTICE OF PUBLICATION",
            //    //"NOTICE OF ISSUANCE OF CERTIFICATE",
            //    //"Revival Order",
            //    //"Certificate of Registration",
            //    //"Acknowledgement",

            //    "Acknowledgement",
            //    "FORMALITY EXAMINATION REPORT",
            //    "Subsequent Formality Examination Report",//same with Formality Examination Report
            //    "Notice of Publication",
            //    "SUBSTANTIVE EXAMINATION REPORT",
            //    "SUBSEQUENT SUBSTANTIVE EXAMINATION REPORT",//same with Substantive Examination Report
            //    "Completion of Final Requirements",
            //    "Notice of Allowance",
            //    "Certificate",
            //    "NOTICE OF ISSUANCE OF CERTIFICATE",
            //    "NOTICE OF WITHDRAWN APPLICATION",
            //    "Revival Order",
            //    "Notice of Forfeiture of Application",

            //};
            //var officeActionList = new List<string>();

            var officeActionList = _context.OfficeAction.ToList();



            //var applicationTypeList = new List<string>(){
            //    "Industrial Design",
            //    "Invention",
            //    "Utility Model"
            //};

            var applicationTypeList = _context.ApplicationType.ToList();

            var projectIdentifier = new ProjectIdentifier();

            var monthsList = new List<string>(){
                "JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC"
            };

            var officeAction = (from o in officeActionList
                               where extractedText.Contains(o.office_action_name1)
                               select o).FirstOrDefault();

            if (officeAction != null)
            {
                projectIdentifier.OfficeActionId = officeAction.office_action_id;
                projectIdentifier.OfficeActionName = officeAction.office_action_name1;
            }

            //string applicationTypeNo = "";
            //foreach (var a in applicationTypeList)
            //{
            //applicationTypeNo = extractedText.FirstOrDefault(s => s.Contains(a.application_type_name));

            //var applicationTypeNo = (from a in applicationTypeList
            //                         where a.application_type_name.IndexOf(extractedText) > 0
            //                         select a).FirstOrDefault();

            var applicationTypeNo = applicationTypeList.Where(p => extractedText.Any(p2 => p2.Contains(p.application_type_name))).FirstOrDefault();

            if (applicationTypeNo != null)
                {
                    projectIdentifier.ApplicationTypeId = applicationTypeNo.application_type_id;
                    projectIdentifier.ApplicationTypeName = applicationTypeNo.application_type_name;

                    var appNumberTemp = extractedText.FirstOrDefault(x => x.Contains(applicationTypeNo.application_type_name));

                    var appNumber = (Regex.Replace(appNumberTemp, @"[A-Za-z]+", "")).Trim();

                    var toTrim = 0;
                    foreach (char c in appNumber)
                    {
                        if (!Char.IsLetterOrDigit(c))//if character
                        {
                            ++toTrim;
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (toTrim > 0)
                    {
                        projectIdentifier.ApplicationNo = appNumber.Remove(0, toTrim);
                        toTrim = 0;
                    }
                    else
                    {
                        projectIdentifier.ApplicationNo = appNumber;
                    }

                
                }
            //}

            var mailDates = new List<string>();
            var mailDate = "";
            foreach (var m in monthsList)
            {
                mailDates = extractedText.Where(s => s.Contains(m)).ToList();
                foreach (var mdate in mailDates)
                {
                    if (mdate.Contains("SUMMARY") || mdate.Contains("USEP"))
                    {
                        
                    }
                    else
                    {
                        projectIdentifier.MailDate = mdate;
                        break;
                    }
                }

                if (projectIdentifier.MailDate != null)
                {
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
            else
            {

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


        //[HttpGet("projects")]
        //public async Task<ActionResult<IEnumerable<Project>>> GetProjectList()
        //{
        //    var test = await _context.Project.ToListAsync();
        //    return test;
        //}
        [HttpGet("projects/{userId}")]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetProjectList(int userId)
        {
            var result = await (from p in _context.Project
                         join a in _context.ApplicationType on p.application_type_id equals a.application_type_id
                         join i in _context.IpdmsUser on p.ipdms_user_id equals i.ipdms_user_id
                         where p.ipdms_user_id == userId
                         select new
                         {
                             IsActive = false,
                             ProjectId = p.project_id,
                             Application = new { icon =  "pe-7s-folder", projectId = p.project_id, type =  a.application_type_name , number = p.application_no },
                             Project = new { pname = p.project_title },
                             Agent = new { first = i.first_name, last = i.last_name },
                             NumberOfFiles = _context.Document.Where(d => d.project_id == p.project_id).Count()
                         }).ToListAsync();

            return result;
        }
        [HttpGet("project/document/{projectId}")]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetDocumentListByProjectId(int projectId)
        {
            var documents = await (from p in _context.Project
                                   join d in _context.Document on p.project_id equals d.project_id
                                   join oa in _context.OfficeAction on d.office_action_id equals oa.office_action_id
                                   where d.project_id == projectId && d.is_deleted == false
                                   select new
                                   {
                                       IsActive = false,
                                       OfficeAction = new { icon = "pe-7s-file", type = oa.office_action_name1, id = oa.office_action_id},
                                       File = new { fname = d.pdf_name }, 
                                       fileSize = d.pdf_file_size, 
                                       created_on =  d.CREATE_USER_DATE == null ? null: d.CREATE_USER_DATE.Value.ToShortDateString(),
                                       response_date = d.response_date == null ? null : d.response_date.Value.ToShortDateString(),
                                       mail_date = d.mail_date == null ? null : d.mail_date.Value.ToShortDateString(),
                                       due = d.mail_date != null ? d.mail_date.Value.AddDays(oa.office_action_due.Value).ToShortDateString() : null,
                                       Actions = new { documentId = d.document_id, folder = p.project_path, fname = d.pdf_name }, 
        }).ToListAsync();

            return documents;
        }

        
    }
}
