using Google.Cloud.Vision.V1;
using ipdms.Dtos;
using ipdms.Models;
using ipdms.Models.AppDbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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

        [HttpPost("save-pages")]
        public async Task<ActionResult<string>> SavePages([FromBody] object data)
        {
            var message = "";
            try
            {
                var result = JsonConvert.DeserializeObject<JToken>(data.ToString());

                var documentDetails = await (from p in _context.Project
                                       join d in _context.Document on p.project_id equals d.project_id
                                        where d.document_id == (int)result["documentId"] && p.is_deleted == false
                                        select new
                                        {
                                            applicationTypeId = p.application_type_id,
                                            applicationNo = p.application_no,
                                            documentFolder = d.pdf_name
                                        }).FirstOrDefaultAsync();

                var folderBaseName = documentDetails.applicationTypeId == 1 ? "Invention" : "Utility Model";
                var folderName = documentDetails.applicationNo.Replace("/", "_");

                var folderPath = $"{folderBaseName}_{folderName}/";

                var img64 = (result["image64"].ToString()).Remove(0, 23);

                byte[] byteArray = Convert.FromBase64String(img64);
                var filename = $"{"Page"}_{result["currentPage"]}{".png"}";

                using (FileStream stream = System.IO.File.Create($"{Constants.Constants.projectBase}{folderPath}{documentDetails.documentFolder}/{filename}"))
                {
                    stream.Write(byteArray, 0, byteArray.Length);
                    int counter = 0;
                    decimal number = (decimal)byteArray.Length;
                    while (Math.Round(number / 1024) >= 1)
                    {
                        number = number / 1024;
                        counter++;
                    }
                    //fileSize = (int)Math.Ceiling(number);
                }

                var documentPage = new DocumentPage()
                {
                    document_id = (int)result["documentId"],
                    path = $"{Constants.Constants.projectPath}{folderPath}{documentDetails.documentFolder}/{filename}"
                };

                _context.DocumentPage.Add(documentPage);
                await _context.SaveChangesAsync();

                message = "Successfully Saved!";
            }
            catch(Exception ex)
            {
                message = ex.ToString();
            }
            return message;
        }

        //POST: api/IpdmsFile/register/project
        [HttpPost]
        public async Task<ActionResult<string>> SaveProject([FromBody] object data)
        {
            try
            {
                var result = JsonConvert.DeserializeObject<JToken>(data.ToString());
                var mailDate = new DateTime?();

                // 1 - Register Project
                // 2 - Upload File
                if (result["saveType"].ToString() == "1")
                {
                    mailDate = DateTime.ParseExact(result["mailingDate"].ToString(), "dd/MM/yyyy", null);
                }
                else if(result["saveType"].ToString() == "2" || result["saveType"].ToString() == "3")
                {
                    try
                    {
                        mailDate = DateTime.ParseExact(result["mailingDate"].ToString(), "dd/MM/yyyy", null);
                    }catch{
                        if(string.IsNullOrEmpty(result["mailingDate"].ToString()))
                        {
                            mailDate = null;
                        }
                        else
                        {
                            mailDate = DateTime.Parse(result["mailingDate"].ToString()); ;
                        }
                    }
                }

                var folderBaseName = result["applicationTypeId"].ToString() == "1" ? "Invention" : "Utility Model";
               // result["applicationNo"] = "1/2014/000317";
                var folderName = (result["applicationNo"].ToString()).Replace("/", "_");
                var appNUm = result["applicationNo"].ToString();

                //For Upload File when saving documents
                if (string.IsNullOrEmpty(result["projectTitle"].ToString()))
                {
                    var documentDetails = new Project();

                    if (string.IsNullOrEmpty(result["projectId"].ToString()))
                    {
                        documentDetails = (from p in _context.Project
                                           where p.application_no == result["applicationNo"].ToString().Trim() && p.is_deleted == false
                                           select p).FirstOrDefault();
                    }
                    else
                    {
                        documentDetails = (from p in _context.Project
                                           where p.project_id == (int)result["projectId"] && p.is_deleted == false
                                           select p).FirstOrDefault();
                    }

                    if (documentDetails != null)
                    {
                        result["projectTitle"] = documentDetails.project_title;
                        result["applicantName"] = documentDetails.applicant_name;
                        result["agentName"] = documentDetails.ipdms_user_id;

                        if (string.IsNullOrEmpty(result["projectId"].ToString()))
                        {
                            result["projectId"] = documentDetails.project_id;
                        }
                    }
                    else
                    {
                        return "Application number does not exist! Please register the Project!";
                    }
                }

                //Create Folder for saving PDF in drive
                var folderPath = $"{folderBaseName}_{folderName}/";
                var fileSize = 0;
                var fileNameGlobal = "";
               
                if (result["saveType"].ToString() == "1" || result["saveType"].ToString() == "2" || result["saveType"].ToString() == "3")
                {
                    var pdf64 = "";
                    var fileName = "";
                    var scannedDocumentFolder = "";
                    if (result["saveType"].ToString() == "3")
                    {
                        scannedDocumentFolder = result["fileName"].ToString().Replace(" ", "_");
                        
                        pdf64 = (result["pdfBase64"].ToString()).Remove(0, 23);
                        fileName = $"{scannedDocumentFolder}_{DateTime.Now.ToString("hmmss").Trim()}{".png"}";
                        fileNameGlobal = fileName.Replace(".png", "");

                        System.IO.Directory.CreateDirectory($"{Constants.Constants.projectBase}{folderPath}{fileNameGlobal}/");

                        byte[] byteArray = Convert.FromBase64String(pdf64);
                        using (FileStream stream = System.IO.File.Create($"{Constants.Constants.projectBase}{folderPath}{fileNameGlobal}/{fileName}"))
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
                    }
                    else
                    {
                        System.IO.Directory.CreateDirectory($"{Constants.Constants.projectBase}{folderPath}");
                        pdf64 = (result["pdfBase64"].ToString()).Remove(0, 28);
                        var tempName = (result["fileName"].ToString()).Replace(".pdf", "");
                        fileName = $"{tempName}{DateTime.Now.ToString("hmmss").Trim()}{".pdf"}";
                        fileNameGlobal = fileName;

                        byte[] byteArray = Convert.FromBase64String(pdf64);
                        using (FileStream stream = System.IO.File.Create($"{Constants.Constants.projectBase}{folderPath}{fileName}"))
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
                    }
                   
                }

                //Check if Project alreaady exist
                var ifProjectExist = _context.Project.Any(x => x.application_no == result["applicationNo"].ToString().Trim() && x.is_deleted != true);

                var documentId = 0;

                if (!ifProjectExist)
                {
                    //New Project or Application Type/No
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
                        LAST_UPDATE_USER_DATE = DateTime.Now,
                        ref_project_id = result["saveType"].ToString() == "3" ? int.Parse(result["refProjectId"].ToString()) : null
                    };

                    _context.Project.Add(project);
                    await _context.SaveChangesAsync();
                    if (result["saveType"].ToString() == "1")
                    {
                        int projectId = project.project_id;

                        var document = new Document()
                        {
                            office_action_id = (int)result["officeActionId"],
                            project_id = projectId,
                            mail_date = mailDate,
                            filling_date = string.IsNullOrEmpty(result["fillingDate"].ToString()) == true ? DateTime.Now : DateTime.ParseExact(result["fillingDate"].ToString(), "dd/MM/yyyy", null),
                            pdf_name = fileNameGlobal,
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
                    else if (result["saveType"].ToString() == "3")
                    {
                        //update is converted
                        var p = await _context.Project.Where(x => x.project_id == int.Parse(result["refProjectId"].ToString())).FirstOrDefaultAsync();
                        p.is_converted = true;
                        p.LAST_UPDATE_USER_DATE = DateTime.Now;
                       // _context.Project.Update(p);
                        await _context.SaveChangesAsync();

                    }


                }
                else
                {
                    // If Project or Application Type/No already exist
                    var document = new Document()
                    {
                        office_action_id = (int)result["officeActionId"],
                        project_id = (int)result["projectId"],
                        mail_date = mailDate,
                        filling_date = string.IsNullOrEmpty(result["fillingDate"].ToString()) == true ? DateTime.Now : DateTime.ParseExact(result["fillingDate"].ToString(), "dd/MM/yyyy", null),
                        pdf_name = fileNameGlobal,
                        //pdf_content = result["pdfBase64"].ToString(),
                        pdf_file_size = fileSize,
                        CREATE_USER_ID = (int)result["createUserId"],
                        CREATE_USER_DATE = DateTime.Now,
                        LAST_UPDATE_USER_ID = (int)result["lastUpdateUserId"],
                        LAST_UPDATE_USER_DATE = DateTime.Now
                    };

                    _context.Document.Add(document);
                    await _context.SaveChangesAsync();

                    documentId = document.document_id;
                    if (result["saveType"].ToString() == "3")
                    {
                        return documentId.ToString();
                    }
                        
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
            var bytes = new byte[] { };
            if (file.type == 3)
            {
                bytes = Convert.FromBase64String((file.image64).Remove(0, 23));
            }
            else
            {
                bytes = Convert.FromBase64String((file.image64).Remove(0, 28));
            }
            

            string pngBase64 = Convert.ToBase64String(bytes);

            byte[] pngBytes = Convert.FromBase64String((pngBase64));

            if (file.type != 3)
            {
               pngBytes = Freeware.Pdf2Png.Convert(bytes, 1);
            }
                //convert to image
                //byte[] pngByte = Freeware.Pdf2Png.Convert(bytes, 1);

            var imageAnalysisResult = new ProjectIdentifier();
            Image image;
            using (MemoryStream ms = new MemoryStream(pngBytes))
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
               // imageAnalysisResult = ExtractProjectIdentifier(fileContentList);


                //Get Office Action
                var officeAction = GetOfficeAction(fileContentList);
                imageAnalysisResult.OfficeActionId = officeAction.OfficeActionId;
                imageAnalysisResult.OfficeActionName = officeAction.OfficeActionName;

                //Get Application Type
                var applicationType = GetApplicationType(fileContentList);
                imageAnalysisResult.ApplicationTypeId = applicationType.ApplicationTypeId;
                imageAnalysisResult.ApplicationTypeName = applicationType.ApplicationTypeName;

                //Get Application No
                var applicationNo = GetApplicationNo(fileContentList);
                imageAnalysisResult.ApplicationNo = applicationNo.ApplicationNo;

                //Get Mail Date
                var mailDate = GetMailDate(fileContentList);
                //imageAnalysisResult.MailDate = mailDate != null ? mailDate.MailDate.Length < 10 ? "" : mailDate.MailDate : "";
                if (mailDate.MailDate != null)
                {
                    if (mailDate.MailDate.Length < 10)
                    {
                        imageAnalysisResult.MailDate = "";
                    }
                    else
                    {
                        imageAnalysisResult.MailDate = mailDate.MailDate;
                    }
                }
                else
                {
                    imageAnalysisResult.MailDate = "";
                }
            }
    
            return imageAnalysisResult;
        }

        public OfficeActionDto GetOfficeAction(List<string> extractedText)
        {
            var officeActionDto = new OfficeActionDto();
            var officeActionList = _context.OfficeAction.ToList();

            var officeAction = (from o in officeActionList
                                where extractedText.Contains(o.office_action_name1)
                                select o).FirstOrDefault();

            if (officeAction != null)
            {
                officeActionDto.OfficeActionId = officeAction.office_action_id;
                officeActionDto.OfficeActionName = officeAction.office_action_name1;
            }
            else
            {
                officeActionDto.OfficeActionId = null;
                officeActionDto.OfficeActionName = null;
            }

            return officeActionDto;
        }

        public ApplicationTypeDto GetApplicationType(List<string> extractedText)
        {
            var applicationTypeDto = new ApplicationTypeDto(); 
            var applicationTypeList = _context.ApplicationType.ToList();

           extractedText.RemoveAll(x => x.Contains("Inventions (PUBREG)") || x.Contains("Inventions (PUBREG)") || x.StartsWith("Inventions"));

            var applicationType = applicationTypeList.Where(p => extractedText.Any(p2 => p2.Contains(p.application_type_name))).FirstOrDefault();

            if (applicationType != null)
            {
                applicationTypeDto.ApplicationTypeId = applicationType.application_type_id;
                applicationTypeDto.ApplicationTypeName = applicationType.application_type_name;
            }
            else
            {
                applicationTypeDto.ApplicationTypeId = null;
                applicationTypeDto.ApplicationTypeName = null;
            }

            return applicationTypeDto;
        }

        public ApplicationNoDto GetApplicationNo(List<string> extractedText)
        {
            var applicationNoDto = new ApplicationNoDto();
            applicationNoDto.ApplicationNo = null;
            var applicationTypeList = _context.ApplicationType.ToList();

            var applicationTypeNo = applicationTypeList.Where(p => extractedText.Any(p2 => p2.Contains(p.application_type_name))).FirstOrDefault();
            if (applicationTypeNo != null)
            {
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
                    applicationNoDto.ApplicationNo = appNumber.Remove(0, toTrim);
                    toTrim = 0;
                }
                else
                {
                    applicationNoDto.ApplicationNo = appNumber;
                }
            }
          

            return applicationNoDto;

        }

        public MailDateDto GetMailDate(List<string> extractedText)
        {
            var mailDateDto = new MailDateDto();

            try
            {
                mailDateDto.MailDate = null;
                var monthsList = new List<string>(){
                "JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC"};

                var monthsListFull = new List<string>(){
                "JANUARY", "FEBRUARY", "MARCH", "APRIL", "MAY", "JUNE", "JULY", "AUGUST", "SEPTEMBER", "OCTOBER", "NOVEMBER", "DECEMBER"};

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
                            mailDateDto.MailDate = mdate;
                            break;
                        }
                    }

                    if (mailDateDto.MailDate != null)
                    {
                        break;
                    }
                }

                if(mailDateDto.MailDate == null)
                {
                    foreach (var m in monthsListFull)
                    {
                        mailDates = extractedText.Where(s => s.Contains(m)).ToList();
                        foreach (var mdate in mailDates)
                        {
                            if (mdate.Contains("SUMMARY") || mdate.Contains("USEP"))
                            {

                            }
                            else
                            {
                                mailDateDto.MailDate = mdate;
                                break;
                            }
                        }

                        if (mailDateDto.MailDate != null)
                        {
                            break;
                        }
                    }
                }

                var result = Regex.Replace(mailDateDto.MailDate, @"[^A-Za-z0-9]+", "");
                //if printed
                if (result.StartsWith("MAILED"))
                {
                    var date = result.Replace("MAILED", "");

                    var month = Regex.Replace(date, @"[\d-]", string.Empty);

                    var dayAndYear = date.Replace(month, "");

                    mailDateDto.MailDate = MailDateFormat1($"{month.Substring(0, 3)}{dayAndYear}");
          
                }
                else
                {
                    //Determine Mail date Format
                    


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
                        mailDateDto.MailDate = MailDateFormat1(result);
                    }
                }
            }
            catch
            {
                return mailDateDto;
            }
           

            return mailDateDto;
        }
        public string MailDateFormat1(string dateToCheck)
        {
            var mailDateStr = "";
            try
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
                 mailDateStr = $"{dayStr}/{monthNumber.ToString().PadLeft(2, '0')}/{yearStr}";
                Console.WriteLine($"Mail Date: {mailDateStr}");
            }
            catch 
            {
                return mailDateStr;
            }
           

            return mailDateStr;
        }


        //[HttpGet("projects")]
        //public async Task<ActionResult<IEnumerable<Project>>> GetProjectList()
        //{
        //    var test = await _context.Project.ToListAsync();
        //    return test;
        //}
        [HttpGet("projects/user/{userId}/role/{roleId}/year/{year}")]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetProjectList(int userId, int roleId, int year)
        {
            var result = await (from p in _context.Project
                         join a in _context.ApplicationType on p.application_type_id equals a.application_type_id
                         join i in _context.IpdmsUser on p.ipdms_user_id equals i.ipdms_user_id
                         where p.ipdms_user_id == userId && p.is_deleted == false 
                         && (_context.Document.Where(d => d.project_id == p.project_id).Count() > 0)
                         select new
                         {
                             IsActive = false,
                             ProjectId = p.project_id,
                             Application = new { icon =  "pe-7s-folder", projectId = p.project_id, type =  a.application_type_name , number = p.application_no },
                             Project = new { pname = p.project_title, createDate = p.CREATE_USER_DATE },
                             Agent = new { first = i.first_name, last = i.last_name },
                             NumberOfFiles = _context.Document.Where(d => d.project_id == p.project_id).Count(),
                             Status = new { projectStatusId = p.project_status_id == 0 && p.is_converted == true ? 1 : p.project_status_id == 0 && p.is_converted == false ? 2 : 3, projectStatus = p.project_status_id == 0 && p.is_converted == true ? "Converted to UM" : p.project_status_id == 0 && p.is_converted == false ? "In Progress" : "Finished" },
                             Actions = new { projectId = p.project_id}
                         }).OrderByDescending(o => o.Project.createDate).ToListAsync();

            if (roleId == 1)
            {
                result = await (from p in _context.Project
                                join a in _context.ApplicationType on p.application_type_id equals a.application_type_id
                                join i in _context.IpdmsUser on p.ipdms_user_id equals i.ipdms_user_id
                                where  p.is_deleted == false 
                                select new
                                {
                                    IsActive = false,
                                    ProjectId = p.project_id,
                                    Application = new { icon = "pe-7s-folder", projectId = p.project_id, type = a.application_type_name, number = p.application_no },
                                    Project = new { pname = p.project_title, createDate = p.CREATE_USER_DATE },
                                    Agent = new { first = i.first_name, last = i.last_name },
                                    NumberOfFiles = _context.Document.Where(d => d.project_id == p.project_id).Count(),
                                    Status = new { projectStatusId = p.project_status_id == 0 && p.is_converted == true ? 1 : p.project_status_id == 0 && p.is_converted == false ? 2 : 3, projectStatus = p.project_status_id == 0 && p.is_converted == true ? "Converted to UM" : p.project_status_id == 0 && p.is_converted == false ? "In Progress" : "Finished" },
                                    Actions = new { projectId = p.project_id }
                                }).OrderByDescending(o => o.Project.createDate).ToListAsync();
            }

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
                                       project = new { projectId = p.project_id, appType = p.application_type_id == 1 ? "Invention" : "Utility Model", appNumber = p.application_no, projectTitle = p.project_title, applicantName = p.applicant_name, projectStatusId = p.project_status_id, isConverted = p.is_converted },
                                       
        }).ToListAsync();

            return documents;
        }

        [HttpGet("project-converted/document/{projectId}")]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetConvertedProjectDocumentListByProjectId(int projectId)
        {
            var documents = await (from p in _context.Project
                                   join d in _context.Document on p.project_id equals d.project_id
                                   join oa in _context.OfficeAction on d.office_action_id equals oa.office_action_id
                                   where p.ref_project_id == projectId && d.is_deleted == false 
                                   select new
                                   {
                                       IsActive = false,
                                       OfficeAction = new { icon = "pe-7s-file", type = oa.office_action_name1, id = oa.office_action_id },
                                       File = new { fname = d.pdf_name },
                                       fileSize = d.pdf_file_size,
                                       created_on = d.CREATE_USER_DATE == null ? null : d.CREATE_USER_DATE.Value.ToShortDateString(),
                                       response_date = d.response_date == null ? null : d.response_date.Value.ToShortDateString(),
                                       mail_date = d.mail_date == null ? null : d.mail_date.Value.ToShortDateString(),
                                       due = d.mail_date != null ? d.mail_date.Value.AddDays(oa.office_action_due.Value).ToShortDateString() : null,
                                       Actions = new { documentId = d.document_id, folder = p.project_path, fname = d.pdf_name },
                                       project = new { projectId = p.project_id, appType = p.application_type_id == 1 ? "Invention" : "Utility Model", appNumber = p.application_no, projectTitle = p.project_title, applicantName = p.applicant_name, projectStatusId = p.project_status_id, isConverted = p.is_converted, dateConverted = p.LAST_UPDATE_USER_DATE },

                                   }).ToListAsync();

            return documents;
        }

        [HttpGet("project-converted/{projectId}")]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetConvertedProjectDetailsById(int projectId)
        {
            var project = await (from p in _context.Project
                                   where p.ref_project_id == projectId && p.is_deleted == false
                                   select new { 
                                    project = new { projectId = p.project_id, convertedTime = p.CREATE_USER_DATE == null ? null : p.CREATE_USER_DATE.Value.ToShortDateString(), refProjectId = p.ref_project_id, convertedItemsCount =  _context.Document.Where(x => x.project_id == p.project_id).ToList().Count }
                                   }).ToListAsync();

            return project;
        }


        [HttpGet("document-page/document/{documentId}")]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetDocumentPageById(int documentId)
        {
            var documents = await (from d in _context.DocumentPage
                                 where d.document_id == documentId
                                 select d).ToListAsync();

            return documents;
        }

        ////////////////////////DASHBOARD
        [HttpGet("count/projects/user/{userId}")]
        public int GetProjectsCount(int userId)
        {
            var result = _context.Project.Count(n => n.ipdms_user_id == userId);

            return result;
        }

        [HttpGet("count/invention/user/{userId}/role/{roleId}")]
        public  int GetInventionCount(int userId, int roleId)
        {
            var result = 0;
            if (roleId == 1)
            {
                result = _context.Project.Count(n => n.application_type_id == 1 && n.project_status_id == 0 && n.is_converted == false && n.is_deleted == false);
            }
            else
            {
                result = _context.Project.Count(n => n.ipdms_user_id == userId && n.application_type_id == 1 && n.project_status_id == 0 && n.is_converted == false  && n.is_deleted == false);
            }
            
                                
            return result;
        }

        [HttpGet("count/utility-model/user/{userId}/role/{roleId}")]
        public int GetUtilityModelCount(int userId, int roleId)
        {
            var result = 0;
            if (roleId == 1)
            {
                result = _context.Project.Count(n =>  n.application_type_id == 2 && n.project_status_id == 0 && n.is_deleted == false);
            }
            else
            {
                result = _context.Project.Count(n => n.ipdms_user_id == userId && n.application_type_id == 2 && n.project_status_id == 0 && n.is_deleted == false);
            }

            return result;
        }

        //[HttpGet("count/invention/finished/user/{userId}/role/{roleId}")]
        //public int GetInventionFinishedCount(int userId, int roleId)
        //{
        //    var result = 0;
        //    var appType = new int[] { 1, 2 };
        //    if (roleId == 1)
        //    {
        //        result = _context.Project.Count(n => appType.Contains(n.application_type_id) && n.project_status_id == 1 && n.is_deleted == false);
        //    }
        //    else
        //    {
        //        result = _context.Project.Count(n => n.ipdms_user_id == userId && appType.Contains(n.application_type_id) && n.project_status_id == 1 && n.is_deleted == false);
        //    }

        //    return result;
        //}

        //[HttpGet("count/utility-model/finished/user/{userId}/role/{roleId}")]
        //public int GetUtilityModelFinishedCount(int userId, int roleId)
        //{
        //    var result = _context.Project.Count(n => n.ipdms_user_id == userId && n.application_type_id == 1 && n.project_status_id == 1);

        //    return result;
        //}

        [HttpGet("count/projects/finished/user/{userId}/role/{roleId}")]
        public int GetFinishedProjectsCount(int userId, int roleId)
        {
            var result = 0;
            var appType = new int[] { 1, 2 };
            if (roleId == 1)
            {
                result = _context.Project.Count(n => appType.Contains(n.application_type_id) && n.project_status_id == 1 && n.is_deleted == false);
            }
            else
            {
                result = _context.Project.Count(n => n.ipdms_user_id == userId && appType.Contains(n.application_type_id) && n.project_status_id == 1 && n.is_deleted == false);
            }

            return result;
        }

        [HttpGet("count/projects/converted/user/{userId}/role/{roleId}")]
        public int GetConvertedProjectsCount(int userId, int roleId)
        {
            var result = 0;

            if (roleId == 1)
            {
                result = _context.Project.Count(n => n.application_type_id == 1 && n.is_converted == true && n.is_deleted == false);
            }
            else
            {
                result = _context.Project.Count(n => n.ipdms_user_id == userId && n.application_type_id == 1 && n.is_converted == true && n.is_deleted == false);
            }

            return result;
        }

        //
        [HttpGet("invention/user/{userId}/role/{roleId}")]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetInventionInProgress(int userId, int roleId)
        {
            var result = new List<Project>();
            if (roleId == 1)
            {
                result =  await _context.Project.Where(n => n.application_type_id == 1 && n.project_status_id == 0 && n.is_converted == false && n.is_deleted == false).ToListAsync();
            }
            else
            {
                result =  await _context.Project.Where(n => n.ipdms_user_id == userId && n.application_type_id == 1 && n.project_status_id == 0 && n.is_converted == false && n.is_deleted == false).ToListAsync();
            }


            return result;
        }

        [HttpGet("utility-model/user/{userId}/role/{roleId}")]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetUtilityModelInProgress(int userId, int roleId)
        {
            var result = new List<Project>();
            if (roleId == 1)
            {
                result = await _context.Project.Where(n => n.application_type_id == 2 && n.project_status_id == 0 && n.is_deleted == false).ToListAsync();
            }
            else
            {
                result = await _context.Project.Where(n => n.ipdms_user_id == userId && n.application_type_id == 2 && n.project_status_id == 0 && n.is_deleted == false).ToListAsync();
            }

            return result;
        }

        [HttpGet("projects/finished/user/{userId}/role/{roleId}")]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetFinishedProjects(int userId, int roleId)
        {
            var result = new List<Project>();
            var appType = new int[] { 1, 2 };
            if (roleId == 1)
            {
                result = await _context.Project.Where(n => appType.Contains(n.application_type_id) && n.project_status_id == 1 && n.is_deleted == false).ToListAsync();
            }
            else
            {
                result = await _context.Project.Where(n => n.ipdms_user_id == userId && appType.Contains(n.application_type_id) && n.project_status_id == 1 && n.is_deleted == false).ToListAsync();
            }

            return result;
        }

        [HttpGet("projects/converted/user/{userId}/role/{roleId}")]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetConvertedProjects(int userId, int roleId)
        {
            var result = new List<Project>();

            if (roleId == 1)
            {
                result = await _context.Project.Where(n => n.application_type_id == 1 && n.is_converted == true && n.is_deleted == false).ToListAsync();
            }
            else
            {
                result = await _context.Project.Where(n => n.ipdms_user_id == userId && n.application_type_id == 1 && n.is_converted == true && n.is_deleted == false).ToListAsync();
            }

            return result;
        }


        [HttpGet("dashboard/uploaded/office-action/user/{userId}/role/{roleId}")]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetOfficeActionUpdateList(int userId, int roleId)
        {
            var result = await (from p in _context.Project
                                join d in _context.Document on p.project_id equals d.project_id
                                join oa in _context.OfficeAction on d.office_action_id equals oa.office_action_id
                                where p.ipdms_user_id == userId && d.is_deleted == false
                                select new
                                {
                                    Project = new {projectId = p.project_id, appType = p.application_type_id == 1 ? "Invention" : "Utility Model", appNumber = p.application_no, projectTitle = p.project_title },
                                    Document = new { documentId = d.document_id, officeAction = oa.office_action_name1, createDate = d.CREATE_USER_DATE }
                                    
                                }).OrderByDescending(o => o.Document.createDate).ToListAsync();

            if (roleId == 1)
            {
                result = await (from p in _context.Project
                                join d in _context.Document on p.project_id equals d.project_id
                                join oa in _context.OfficeAction on d.office_action_id equals oa.office_action_id
                                where d.is_deleted == false
                                select new
                                {
                                    Project = new { projectId = p.project_id, appType = p.application_type_id == 1 ? "Invention" : "Utility Model", appNumber = p.application_no, projectTitle = p.project_title },
                                    Document = new { documentId = d.document_id, officeAction = oa.office_action_name1, createDate = d.CREATE_USER_DATE }

                                }).OrderByDescending(o => o.Document.createDate).ToListAsync();
            }


            return result;
        }

        [HttpGet("dashboard/project/recent/user/{userId}/role/{roleId}")]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetRecentProjects(int userId, int roleId)
        {
            var result = await _context.Project
                .Where(p => p.ipdms_user_id == userId)
                .OrderByDescending(p => p.CREATE_USER_DATE)
                .ToListAsync();

            if (roleId == 1)
            {
                result = await _context.Project
                .OrderByDescending(p => p.CREATE_USER_DATE)
                .ToListAsync();
            }

            return result;
        }

        [HttpGet("dashboard/agents")]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetAgents(int userId)
        {
            var result = await _context.IpdmsUser.Where(a => a.user_role_id == 3).OrderByDescending(b => b.CREATE_USER_DATE).ToListAsync();

            return result;
        }

        [HttpGet("project/due/user/{userId}/role/{roleId}")]
        public async Task<OfficeActionDueDto> GetProjectWithDueCount(int userId, int roleId)
        {
            //var officeActionsWithNoDue = new int[] { 1, 6, 7, 10, 11 };
            var todaysDate = DateTime.Today;
            var officeActionDue = new OfficeActionDueDto()
            {
                officeActionDueToday  = 0,
                officeActionDueThisWeek = 0,
                officeActionDueThisMonth = 0
            };

            var result = await (from p in _context.Project
                                join d in _context.Document on p.project_id equals d.project_id
                                join oa in _context.OfficeAction on d.office_action_id equals oa.office_action_id
                                where p.is_deleted == false
                                && d.is_deleted == false
                                && d.office_action_id != 1
                                && p.ipdms_user_id == userId
                                && d.mail_date != null
                                && d.response_date == null
                                select new
                                {
                                    Project = new { projectId = p.project_id, appType = p.application_type_id == 1 ? "Invention" : "Utility Model", appNumber = p.application_no, projectTitle = p.project_title },
                                    Document = new { documentId = d.document_id, officeAction = oa.office_action_name1, createDate = d.CREATE_USER_DATE, due = d.mail_date.Value.AddDays(oa.office_action_due.GetValueOrDefault()) }
                                }).ToListAsync();

            if (roleId == 1)
            {
                result = await (from p in _context.Project
                                join d in _context.Document on p.project_id equals d.project_id
                                join oa in _context.OfficeAction on d.office_action_id equals oa.office_action_id
                                where p.is_deleted == false
                                && d.is_deleted == false
                                && d.office_action_id != 1
                                && d.mail_date != null
                                && d.response_date == null
                                select new
                                {
                                    Project = new { projectId = p.project_id, appType = p.application_type_id == 1 ? "Invention" : "Utility Model", appNumber = p.application_no, projectTitle = p.project_title },
                                    Document = new { documentId = d.document_id, officeAction = oa.office_action_name1, createDate = d.CREATE_USER_DATE, due = d.mail_date.Value.AddDays(oa.office_action_due.GetValueOrDefault()) }
                                }).ToListAsync();
            }

            foreach (var o in result)
            {
                //var officeDueDate = DateTime.ParseExact(o.Document.due.ToString(), "MM/dd/yyyy", CultureInfo.InvariantCulture);

                if (o.Document.due.Year == DateTime.Now.Year && o.Document.due.Month == DateTime.Now.Month && o.Document.due.Day == DateTime.Now.Day)
                {
                    officeActionDue.officeActionDueToday += 1;
                }


                if (o.Document.due.Year == DateTime.Now.Year && o.Document.due.Month == DateTime.Now.Month)
                {
                    officeActionDue.officeActionDueThisMonth += 1;

                    var cal = System.Globalization.DateTimeFormatInfo.CurrentInfo.Calendar;
                    var d1 = DateTime.Now.Date.AddDays(-1 * (int)cal.GetDayOfWeek(DateTime.Now));
                    var d2 = o.Document.due.Date.AddDays(-1 * (int)cal.GetDayOfWeek(o.Document.due));

                    if (d1 == d2)
                    {
                        officeActionDue.officeActionDueThisWeek += 1;
                    }
                }
            }
            return officeActionDue;
        }

        [HttpGet("project/due/month/user/{userId}/role/{roleId}")]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetProjectWithDueMonth(int userId, int roleId)
        {
            //var officeActionsWithNoDue = new int[] { 1, 6, 7, 10, 11 };
            var officeActionDueThisMonth = new List<Project>();
            var todaysDate = DateTime.Today;


            var result = await (from p in _context.Project
                                join d in _context.Document on p.project_id equals d.project_id
                                join oa in _context.OfficeAction on d.office_action_id equals oa.office_action_id
                                where p.is_deleted == false
                                && d.is_deleted == false
                                && d.office_action_id != 1
                                && p.ipdms_user_id == userId
                                && d.mail_date != null
                                && d.response_date == null
                                select new
                                {
                                    Project = p,
                                    Document = new { documentId = d.document_id, officeAction = oa.office_action_name1, createDate = d.CREATE_USER_DATE, due = d.mail_date.Value.AddDays(oa.office_action_due.GetValueOrDefault()) }
                                }).ToListAsync();

            if (roleId == 1)
            {
                result = await (from p in _context.Project
                                join d in _context.Document on p.project_id equals d.project_id
                                join oa in _context.OfficeAction on d.office_action_id equals oa.office_action_id
                                where p.is_deleted == false
                                && d.is_deleted == false
                                && d.office_action_id != 1
                                && d.mail_date != null
                                && d.response_date == null
                                select new
                                {
                                    Project = p,
                                    Document = new { documentId = d.document_id, officeAction = oa.office_action_name1, createDate = d.CREATE_USER_DATE, due = d.mail_date.Value.AddDays(oa.office_action_due.GetValueOrDefault()) }
                                }).ToListAsync();
            }

            foreach (var o in result)
            {


                if (o.Document.due.Year == DateTime.Now.Year && o.Document.due.Month == DateTime.Now.Month)
                {
                    officeActionDueThisMonth.Add(o.Project);

                }
            }
            return officeActionDueThisMonth;
        }

        [HttpGet("project/due/week/user/{userId}/role/{roleId}")]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetProjectWithDueWeek(int userId, int roleId)
        {
            //var officeActionsWithNoDue = new int[] { 1, 6, 7, 10, 11 };
            var officeActionDueThisWeek = new List<Project>();
            var todaysDate = DateTime.Today;


            var result = await (from p in _context.Project
                                join d in _context.Document on p.project_id equals d.project_id
                                join oa in _context.OfficeAction on d.office_action_id equals oa.office_action_id
                                where p.is_deleted == false
                                && d.is_deleted == false
                                && d.office_action_id != 1
                                && p.ipdms_user_id == userId
                                && d.mail_date != null
                                && d.response_date == null
                                select new
                                {
                                    Project = p,
                                    Document = new { documentId = d.document_id, officeAction = oa.office_action_name1, createDate = d.CREATE_USER_DATE, due = d.mail_date.Value.AddDays(oa.office_action_due.GetValueOrDefault()) }
                                }).ToListAsync();

            if (roleId == 1)
            {
                result = await (from p in _context.Project
                                join d in _context.Document on p.project_id equals d.project_id
                                join oa in _context.OfficeAction on d.office_action_id equals oa.office_action_id
                                where p.is_deleted == false
                                && d.is_deleted == false
                                && d.office_action_id != 1
                                && d.mail_date != null
                                && d.response_date == null
                                select new
                                {
                                    Project = p,
                                    Document = new { documentId = d.document_id, officeAction = oa.office_action_name1, createDate = d.CREATE_USER_DATE, due = d.mail_date.Value.AddDays(oa.office_action_due.GetValueOrDefault()) }
                                }).ToListAsync();
            }

            foreach (var o in result)
            {


                if (o.Document.due.Year == DateTime.Now.Year && o.Document.due.Month == DateTime.Now.Month)
                {
                    var cal = System.Globalization.DateTimeFormatInfo.CurrentInfo.Calendar;
                    var d1 = DateTime.Now.Date.AddDays(-1 * (int)cal.GetDayOfWeek(DateTime.Now));
                    var d2 = o.Document.due.Date.AddDays(-1 * (int)cal.GetDayOfWeek(o.Document.due));

                    if (d1 == d2)
                    {
                        officeActionDueThisWeek.Add(o.Project);
                    }

                }
            }
            return officeActionDueThisWeek;
        }

        [HttpGet("project/due/today/user/{userId}/role/{roleId}")]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetProjectWithDueToday(int userId, int roleId)
        {
            //var officeActionsWithNoDue = new int[] { 1, 6, 7, 10, 11 };
            var officeActionDueThisToday = new List<Project>();
            var todaysDate = DateTime.Today;


            var result = await (from p in _context.Project
                                join d in _context.Document on p.project_id equals d.project_id
                                join oa in _context.OfficeAction on d.office_action_id equals oa.office_action_id
                                where p.is_deleted == false
                                && d.is_deleted == false
                                && d.office_action_id != 1
                                && p.ipdms_user_id == userId
                                && d.mail_date != null
                                && d.response_date == null
                                select new
                                {
                                    Project = p,
                                    Document = new { documentId = d.document_id, officeAction = oa.office_action_name1, createDate = d.CREATE_USER_DATE, due = d.mail_date.Value.AddDays(oa.office_action_due.GetValueOrDefault()) }
                                }).ToListAsync();

            if (roleId == 1)
            {
                result = await (from p in _context.Project
                                join d in _context.Document on p.project_id equals d.project_id
                                join oa in _context.OfficeAction on d.office_action_id equals oa.office_action_id
                                where p.is_deleted == false
                                && d.is_deleted == false
                                && d.office_action_id != 1
                                && d.mail_date != null
                                && d.response_date == null
                                select new
                                {
                                    Project = p,
                                    Document = new { documentId = d.document_id, officeAction = oa.office_action_name1, createDate = d.CREATE_USER_DATE, due = d.mail_date.Value.AddDays(oa.office_action_due.GetValueOrDefault()) }
                                }).ToListAsync();
            }

            foreach (var o in result)
            {


                if (o.Document.due.Year == DateTime.Now.Year && o.Document.due.Month == DateTime.Now.Month && o.Document.due.Day == DateTime.Now.Day)
                {
                    officeActionDueThisToday.Add(o.Project);
                }
            }
            return officeActionDueThisToday;
        }

        [HttpGet("project/user/{userId}/role/{roleId}/year/{year}")]
        public async Task<int[]> GetProjectSummaryData(int userId, int roleId, int year)
        {
            var data = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

         

            var result = await (from p in _context.Project
                                where p.CREATE_USER_DATE.Value.Year == year && p.ipdms_user_id == userId
                                select new
                                {
                                    Project = new { projectId = p.project_id, projectMonthCreated = p.CREATE_USER_DATE.Value.Month }
                                }).ToListAsync();

            if (roleId == 1)
            {
                result = await (from p in _context.Project
                                where p.CREATE_USER_DATE.Value.Year == year 
                                select new
                                {
                                    Project = new { projectId = p.project_id, projectMonthCreated = p.CREATE_USER_DATE.Value.Month }
                                }).ToListAsync();
            }


            foreach (var i in result)
            {
                data[i.Project.projectMonthCreated - 1] += 1;
            }

            return data;
        }

        //Admin
        [HttpGet("project/{projectId}")]
        public async Task<ActionResult<dynamic>> GetProjectById(int projectId)
        {
            var projectById = await (from p in _context.Project
                                   where p.project_id == projectId && p.is_deleted == false
                                   select new
                                   {
                                       project = new { appTypeId = p.application_type_id, appNumber = p.application_no, projectTitle = p.project_title, applicantName = p.applicant_name, projectStatusId = p.project_status_id, agentId = p.ipdms_user_id, createUserId = p.CREATE_USER_ID, lastUpdateUserId = p.LAST_UPDATE_USER_ID }
                                   }).FirstOrDefaultAsync();

            return projectById;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, Project project)
        {
            if (id != project.project_id)
            {
                return BadRequest();
            }

            _context.Entry(project).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _context.Project.AnyAsync(x => x.project_id == id) == false)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
    }
}
