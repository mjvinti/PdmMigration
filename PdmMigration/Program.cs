using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PdmMigration
{
    class Program
    {
        //CHT
        //public static string catalogFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\PDM-Catalog_2017-11-06.csv";
        //public static string inputFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\DE\2017-11-06\DE_2017-11-06.txt";
        //public static string batchFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\DE\2017-11-06\singlePdfCopy.bat";
        //public static string serverName = "pdm.cht.moog.com";
        //public static string outputFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\DE\2017-11-06\DE_import_2017-11-06.txt";
        //public static string misfitToys = @"P:\Architecture Group\Projects\PDM Migration\extracts\DE\2017-11-06\DE_importMisfits_2017-11-06.txt";
        //public static string jobTicketLocation = @"P:\Architecture Group\Projects\PDM Migration\extracts\DE\2017-11-06\jobTickets\";
        //public static string uncRawPrefix = @"\\CHTTCVOL01.cht.moog.com";
        //public static string uncPdfPrefix = @"\\CHTTCVOL01.cht.moog.com\tcpdf";
        //public static string adlibDTD = @"";
        //public static DateTime recentDateTime = DateTime.MinValue;
        //public static bool isWindows = true;
        //public static bool isLuDateTime = false;
        //public static bool isIeDateTime = false;

        //CN
        //public static string catalogFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\PDM-Catalog_2017-11-06.csv";
        //public static string inputFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\DE\2017-11-06\DE_2017-11-06.txt";
        //public static string batchFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\DE\2017-11-06\singlePdfCopy.bat";
        //public static string serverName = "pdm.cn.moog.com";
        //public static string outputFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\DE\2017-11-06\DE_import_2017-11-06.txt";
        //public static string misfitToys = @"P:\Architecture Group\Projects\PDM Migration\extracts\DE\2017-11-06\DE_importMisfits_2017-11-06.txt";
        //public static string jobTicketLocation = @"P:\Architecture Group\Projects\PDM Migration\extracts\DE\2017-11-06\jobTickets\";
        //public static string uncRawPrefix = @"\\cnpu1svwi003012.cn.moog.com";
        //public static string uncPdfPrefix = @"\\cnpu1svwi003012.cn.moog.com\tcpdf";
        //public static string adlibDTD = @"";
        //public static DateTime recentDateTime = DateTime.MinValue;
        //public static bool isWindows = true;
        //public static bool isLuDateTime = false;
        //public static bool isIeDateTime = false;

        //DE
        //public static string catalogFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\PDM-Catalog_2017-11-06.csv";
        //public static string inputFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\DE\2017-11-06\DE_2017-11-06.txt";
        //public static string batchFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\DE\2017-11-06\singlePdfCopy.bat";
        //public static string serverName = "pdm.de.moog.com";
        //public static string outputFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\DE\2017-11-06\DE_import_2017-11-06.txt";
        //public static string misfitToys = @"P:\Architecture Group\Projects\PDM Migration\extracts\DE\2017-11-06\DE_importMisfits_2017-11-06.txt";
        //public static string jobTicketLocation = @"P:\Architecture Group\Projects\PDM Migration\extracts\DE\2017-11-06\jobTickets\";
        //public static string uncRawPrefix = @"\\debo1svwi003010.de.moog.com";
        //public static string uncPdfPrefix = @"\\debo1svwi003010.de.moog.com\tcpdf";
        //public static string adlibDTD = @"";
        //public static DateTime recentDateTime = DateTime.MinValue;
        //public static bool isWindows = true;
        //public static bool isLuDateTime = false;
        //public static bool isIeDateTime = false;

        //EA
        public static string catalogFile = @"C:\Users\mvinti\Desktop\PDM\PdmMigration_Remote_2017-11-10\EA\PDM-Catalog_2017-11-06.csv";
        public static string inputFile = @"C:\Users\mvinti\Desktop\PDM\PdmMigration_Remote_2017-11-10\EA\xmlTest.txt";
        public static string batchFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\EA\2017-11-01\singlePdfCopy.bat";
        public static string serverName = "pdm.moog.com";
        public static string outputFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\EA\2017-11-01\EA_import_2017-11-06.txt";
        public static string misfitToys = @"P:\Architecture Group\Projects\PDM Migration\extracts\EA\2017-11-01\EA_importMisfits_2017-11-06.txt";
        public static string jobTicketLocation = @"C:\Users\mvinti\Desktop\PDM\PdmMigration_Remote_2017-11-10\EA\xmlTestJobTickets\";
        public static string uncRawPrefix = @"\\eacmpnas01.moog.com\Vol5_Data\PDM\EA";
        public static string uncPdfPrefix = @"\\eacmpnas01.moog.com\Vol5_Data\PDM\EA\tcpdf";
        public static string adlibDTD = @"C:\AdlibExpress.dtd";
        public static DateTime recentDateTime = DateTime.MinValue;
        public static bool isWindows = false;
        public static bool isLuDateTime = false;
        public static bool isIeDateTime = false;

        //IE
        //public static string catalogFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\PDM-Catalog_2017-11-06.csv";
        //public static string inputFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\DE\2017-11-06\DE_2017-11-06.txt";
        //public static string batchFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\DE\2017-11-06\singlePdfCopy.bat";
        //public static string serverName = "pdm.ie.moog.com";
        //public static string outputFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\DE\2017-11-06\DE_import_2017-11-06.txt";
        //public static string misfitToys = @"P:\Architecture Group\Projects\PDM Migration\extracts\DE\2017-11-06\DE_importMisfits_2017-11-06.txt";
        //public static string jobTicketLocation = @"P:\Architecture Group\Projects\PDM Migration\extracts\DE\2017-11-06\jobTickets\";
        //public static string uncRawPrefix = @"\\ieri3svwi003068.ie.moog.com";
        //public static string uncPdfPrefix = @"\\ieri3svwi003068.ie.moog.com\tcpdf";
        //public static string adlibDTD = @"";
        //public static DateTime recentDateTime = DateTime.MinValue;
        //public static bool isWindows = true;
        //public static bool isLuDateTime = false;
        //public static bool isIeDateTime = true;

        //IN
        //public static string catalogFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\PDM-Catalog_2017-11-06.csv";
        //public static string inputFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\DE\2017-11-06\DE_2017-11-06.txt";
        //public static string batchFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\DE\2017-11-06\singlePdfCopy.bat";
        //public static string serverName = "inpdm01.in.moog.com";
        //public static string outputFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\DE\2017-11-06\DE_import_2017-11-06.txt";
        //public static string misfitToys = @"P:\Architecture Group\Projects\PDM Migration\extracts\DE\2017-11-06\DE_importMisfits_2017-11-06.txt";
        //public static string jobTicketLocation = @"P:\Architecture Group\Projects\PDM Migration\extracts\DE\2017-11-06\jobTickets\";
        //public static string uncRawPrefix = @"\\inel2svwi003011.in.moog.com\IN";
        //public static string uncPdfPrefix = @"\\inel2svwi003011.in.moog.com\IN\tcpdf";
        //public static string adlibDTD = @"";
        //public static DateTime recentDateTime = DateTime.MinValue;
        //public static bool isWindows = true;
        //public static bool isLuDateTime = false;
        //public static bool isIeDateTime = false;


        //ITC
        //public static string catalogFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\PDM-Catalog_2017-11-06.csv";
        //public static string inputFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\DE\2017-11-06\DE_2017-11-06.txt";
        //public static string batchFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\DE\2017-11-06\singlePdfCopy.bat";
        //public static string serverName = "pdm.itc.moog.com";
        //public static string outputFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\DE\2017-11-06\DE_import_2017-11-06.txt";
        //public static string misfitToys = @"P:\Architecture Group\Projects\PDM Migration\extracts\DE\2017-11-06\DE_importMisfits_2017-11-06.txt";
        //public static string jobTicketLocation = @"P:\Architecture Group\Projects\PDM Migration\extracts\DE\2017-11-06\jobTickets\";
        //public static string uncRawPrefix = @"\\itca1svwi003006.it.moog.com";
        //public static string uncPdfPrefix = @"\\itca1svwi003006.it.moog.com\tcpdf";
        //public static string adlibDTD = @"";
        //public static DateTime recentDateTime = DateTime.MinValue;
        //public static bool isWindows = true;
        //public static bool isLuDateTime = false;
        //public static bool isIeDateTime = false;

        //JP
        //public static string catalogFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\PDM-Catalog_2017-11-06.csv";
        //public static string inputFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\DE\2017-11-06\DE_2017-11-06.txt";
        //public static string batchFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\DE\2017-11-06\singlePdfCopy.bat";
        //public static string serverName = "pdm.jp.moog.com";
        //public static string outputFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\DE\2017-11-06\DE_import_2017-11-06.txt";
        //public static string misfitToys = @"P:\Architecture Group\Projects\PDM Migration\extracts\DE\2017-11-06\DE_importMisfits_2017-11-06.txt";
        //public static string jobTicketLocation = @"P:\Architecture Group\Projects\PDM Migration\extracts\DE\2017-11-06\jobTickets\";
        //public static string uncRawPrefix = @"\\jphi1svwi003033.jp.moog.com";
        //public static string uncPdfPrefix = @"\\jphi1svwi003033.jp.moog.com\tcpdf";
        //public static string adlibDTD = @"";
        //public static DateTime recentDateTime = DateTime.MinValue;
        //public static bool isWindows = true;
        //public static bool isLuDateTime = false;
        //public static bool isIeDateTime = false;

        //LU
        //public static string catalogFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\PDM-Catalog_2017-11-06.csv";
        //public static string inputFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\DE\2017-11-06\DE_2017-11-06.txt";
        //public static string batchFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\DE\2017-11-06\singlePdfCopy.bat";
        //public static string serverName = "pdm.lu.moog.com";
        //public static string outputFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\DE\2017-11-06\DE_import_2017-11-06.txt";
        //public static string misfitToys = @"P:\Architecture Group\Projects\PDM Migration\extracts\DE\2017-11-06\DE_importMisfits_2017-11-06.txt";
        //public static string jobTicketLocation = @"P:\Architecture Group\Projects\PDM Migration\extracts\DE\2017-11-06\jobTickets\";
        //public static string uncRawPrefix = @"\\lujp1svwi003239.lu.moog.com";
        //public static string uncPdfPrefix = @"\\lujp1svwi003239.lu.moog.com\tcpdf";
        //public static string adlibDTD = @"";
        //public static DateTime recentDateTime = DateTime.MinValue;
        //public static bool isWindows = true;
        //public static bool isLuDateTime = true;
        //public static bool isIeDateTime = false;

        //MITC
        //public static string catalogFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\PDM-Catalog_2017-11-06.csv";
        //public static string inputFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\DE\2017-11-06\DE_2017-11-06.txt";
        //public static string batchFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\DE\2017-11-06\singlePdfCopy.bat";
        //public static string serverName = "pdm.in.moog.com";
        //public static string outputFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\DE\2017-11-06\DE_import_2017-11-06.txt";
        //public static string misfitToys = @"P:\Architecture Group\Projects\PDM Migration\extracts\DE\2017-11-06\DE_importMisfits_2017-11-06.txt";
        //public static string jobTicketLocation = @"P:\Architecture Group\Projects\PDM Migration\extracts\DE\2017-11-06\jobTickets\";
        //public static string uncRawPrefix = @"\\inel2svwi003011.in.moog.com\MITC";
        //public static string uncPdfPrefix = @"\\inel2svwi003011.in.moog.com\MITC\tcpdf";
        //public static string adlibDTD = @"";
        //public static DateTime recentDateTime = DateTime.MinValue;
        //public static bool isWindows = true;
        //public static bool isLuDateTime = false;
        //public static bool isIeDateTime = false;

        //PH
        //public static string catalogFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\PDM-Catalog_2017-11-06.csv";
        //public static string inputFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\PH\2017-11-06\PH_2017-11-06.txt";
        //public static string batchFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\PH\2017-11-06\singlePdfCopy.bat";
        //public static string serverName = "pdm.ph.moog.com";
        //public static string outputFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\PH\2017-11-06\PH_import_2017-11-06.txt";
        //public static string misfitToys = @"P:\Architecture Group\Projects\PDM Migration\extracts\PH\2017-11-06\PH_importMisfits_2017-11-06.txt";
        //public static string jobTicketLocation = @"P:\Architecture Group\Projects\PDM Migration\extracts\PH\2017-11-06\jobTickets\";
        //public static string uncRawPrefix = @"\\PHTCPVOL01.ph.moog.com";
        //public static string uncPdfPrefix = @"\\PHTCPVOL01.ph.moog.com\tcpdf";
        //public static string adlibDTD = @"";
        //public static DateTime recentDateTime = DateTime.MinValue;
        //public static bool isWindows = true;
        //public static bool isLuDateTime = false;
        //public static bool isIeDateTime = false;

        //SLC
        //public static string catalogFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\PDM-Catalog_2017-11-06.csv";
        //public static string inputFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\SLC\2017-11-06\SLC_2017-11-06.txt";
        //public static string batchFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\SLC\2017-11-06\singlePdfCopy.bat";
        //public static string serverName = "pdm.slc.moog.com";
        //public static string outputFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\SLC\2017-11-06\SLC_import_2017-11-06.txt";
        //public static string misfitToys = @"P:\Architecture Group\Projects\PDM Migration\extracts\SLC\2017-11-06\SLC_importMisfits_2017-11-06.txt";
        //public static string jobTicketLocation = @"P:\Architecture Group\Projects\PDM Migration\extracts\SLC\2017-11-06\jobTickets\";
        //public static string uncRawPrefix = @"\\SLCTCVOL01.slc.moog.com";
        //public static string uncPdfPrefix = @"\\SLCTCVOL01.slc.moog.com\tcpdf";
        //public static string adlibDTD = @"";
        //public static DateTime recentDateTime = DateTime.MinValue;
        //public static bool isWindows = true;
        //public static bool isLuDateTime = false;
        //public static bool isIeDateTime = false;

        //STL
        //public static string catalogFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\PDM-Catalog_2017-11-06.csv";
        //public static string inputFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\DE\2017-11-06\DE_2017-11-06.txt";
        //public static string batchFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\DE\2017-11-06\singlePdfCopy.bat";
        //public static string serverName = "pdm.stl.moog.com";
        //public static string outputFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\DE\2017-11-06\DE_import_2017-11-06.txt";
        //public static string misfitToys = @"P:\Architecture Group\Projects\PDM Migration\extracts\DE\2017-11-06\DE_importMisfits_2017-11-06.txt";
        //public static string jobTicketLocation = @"P:\Architecture Group\Projects\PDM Migration\extracts\DE\2017-11-06\jobTickets\";
        //public static string uncRawPrefix = @"\\debo1svwi003010.de.moog.com";
        //public static string uncPdfPrefix = @"\\debo1svwi003010.de.moog.com\tcpdf";
        //public static string adlibDTD = @"";
        //public static DateTime recentDateTime = DateTime.MinValue;
        //public static bool isWindows = true;
        //public static bool isLuDateTime = false;
        //public static bool isIeDateTime = false;

        //TOR
        //public static string catalogFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\PDM-Catalog_2017-11-06.csv";
        //public static string inputFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\TOR\2017-11-06\TOR_2017-11-06.txt";
        //public static string batchFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\TOR\2017-11-06\singlePdfCopy.bat";
        //public static string serverName = "pdm.tor.moog.com";
        //public static string outputFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\TOR\2017-11-06\TOR_import_2017-11-06.txt";
        //public static string misfitToys = @"P:\Architecture Group\Projects\PDM Migration\extracts\TOR\2017-11-06\TOR_importMisfits_2017-11-06.txt";
        //public static string jobTicketLocation = @"P:\Architecture Group\Projects\PDM Migration\extracts\TOR\2017-11-06\jobTickets\";
        //public static string uncRawPrefix = @"\\toriman.tor.moog.com";
        //public static string uncPdfPrefix = @"\\toriman.tor.moog.com\tcpdf";
        //public static string adlibDTD = @"";
        //public static DateTime recentDateTime = DateTime.MinValue;
        //public static bool isWindows = false;
        //public static bool isLuDateTime = false;
        //public static bool isIeDateTime = false;

        //UK
        //public static string catalogFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\PDM-Catalog_2017-11-06.csv";
        //public static string inputFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\UK\2017-11-06\UK_2017-11-06.txt";
        //public static string batchFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\UK\2017-11-06\singlePdfCopy.bat";
        //public static string serverName = "pdm.uk.moog.com";
        //public static string outputFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\UK\2017-11-06\UK_import_2017-11-06.txt";
        //public static string misfitToys = @"P:\Architecture Group\Projects\PDM Migration\extracts\UK\2017-11-06\UK_importMisfits_2017-11-06.txt";
        //public static string jobTicketLocation = @"P:\Architecture Group\Projects\PDM Migration\extracts\UK\2017-11-06\jobTickets\";
        //public static string uncRawPrefix = @"\\GBGL1SVTC01.uk.moog.com";
        //public static string uncPdfPrefix = @"\\GBGL1SVTC01.uk.moog.com\tcpdf";
        //public static string adlibDTD = @"";
        //public static DateTime recentDateTime = DateTime.MinValue;
        //public static bool isWindows = true;
        //public static bool isLuDateTime = false;
        //public static bool isIeDateTime = false;

        public static bool IsExt(string token)
        {
            switch (token.ToLower())
            {
                case "7z":
                case "cad":
                case "cg4":
                case "csh":
                case "csv":
                case "db":
                case "dis":
                case "dll_crea":
                case "dos":
                case "dot":
                case "dwg":
                case "dxf":
                case "edt":
                case "gif":
                case "gp4":
                case "gwk":
                case "hp":
                case "hpdf":
                case "hpg":
                case "hpp":
                case "htm":
                case "html":
                case "ini":
                case "jpg":
                case "js":
                case "mdb":
                case "mht":
                case "mil":
                case "msg":
                case "obd":
                case "oft":
                case "pcx":
                case "pdf":
                case "plt":
                case "png":
                case "ppt":
                case "pptx":
                case "pra":
                case "prt":
                case "reg":
                case "rss":
                case "rst":
                case "rtf":
                case "smf":
                case "ss":
                case "ss_old":
                case "tif":
                case "txt":
                case "url":
                case "vsd":
                case "wdf":
                case "wrl":
                case "xls":
                case "xlsx":
                case "xlt":
                case "xps":
                case "xs":
                case "xst":
                case "z":
                case "z_old":
                case "zip":
                case "flv":
                case "mpg":
                case "doc":
                case "docx":
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsPdfAble(string fileName)
        {
            if (fileName.EndsWith(".Z"))
            {
                return false;
            }

            if (fileName.EndsWith("._"))
            {
                return false;
            }

            if (fileName.ToLower().EndsWith(".zip"))
            {
                return false;
            }

            if (fileName.ToLower().EndsWith(".doc"))
            {
                return false;
            }

            if (fileName.ToLower().EndsWith(".docx"))
            {
                return false;
            }

            if (fileName.ToLower().EndsWith(".mpg"))
            {
                return false;
            }

            if (fileName.ToLower().EndsWith(".flv"))
            {
                return false;
            }

            return true;
        }

        public static void JobTicketGenerator(Dictionary<string, List<PdmItem>> dictionary, List<string> batchLines)
        {
            int counter = 0;

            foreach (KeyValuePair<string, List<PdmItem>> kvp in dictionary)
            {
                counter++;
                //if (kvp.Key != "-98081.AF")
                //{
                //    Console.WriteLine("     SKIPPING: " + kvp.Key);
                //    continue;
                //}

                //Console.WriteLine("NOT SKIPPING: " + kvp.Key);

                //if there is only one kvp, then we already have a pdf somewhere in theory
                if (kvp.Value.Count < 2)
                {
                    StringBuilder sourcePdfBuilder = new StringBuilder();
                    
                    //find pdf and copy to correct folder; build batch file
                    if(kvp.Value[0].UncRaw.EndsWith(".pdf"))
                    {
                        sourcePdfBuilder.Append(kvp.Value[0].UncRaw.Replace("web\\", "web\\pdf\\"));
                    }
                    else
                    {
                        sourcePdfBuilder.Append(kvp.Value[0].UncRaw.Replace("web\\", "web\\pdf\\") + ".pdf");
                    }

                    if(File.Exists(sourcePdfBuilder.ToString()))
                    {
                        batchLines.Add("Copy " + sourcePdfBuilder.ToString() + " " + uncPdfPrefix + "\\" + kvp.Key + ".pdf");
                        continue;
                    }
                    else
                    {
                        //do nothing and build the job ticket
                        batchLines.Add("REM FILE DOES NOT EXIST: " + sourcePdfBuilder.ToString());
                    }
                }

                StringBuilder jobTicket = new StringBuilder();

                jobTicket.AppendLine("<?xml version=\"1.0\" encoding=\"ISO-8859-1\" ?>");
                jobTicket.AppendLine("<?AdlibExpress applanguage = \"USA\" appversion = \"4.11.0\" dtdversion = \"2.6\" ?>");
                jobTicket.AppendLine("<!DOCTYPE JOBS SYSTEM \"" + adlibDTD + "\">");
                jobTicket.AppendLine("<JOBS xmlns:JOBS=\"http://www.adlibsoftware.com\" xmlns:JOB=\"http://www.adlibsoftware.com\">");
                jobTicket.AppendLine("<JOB>");
                jobTicket.AppendLine("<JOB:DOCINPUTS>");

                DateTime mostRecentDate = DateTime.MinValue;
                foreach(var i in kvp.Value)
                {
                    //Find most recent date in list; ignore time
                    if(i.FileDateTime.Date > mostRecentDate)
                    {
                        mostRecentDate = i.FileDateTime.Date;
                    }
                }

                foreach(var i in kvp.Value)
                {
                    string filename = i.FileName;

                    if (i.FileName.EndsWith(".Z") || i.FileName.EndsWith("._"))
                    {
                        filename = i.FileName.Remove(i.FileName.Length - 2, 2);
                    }

                    if (i.FileName.EndsWith(".pra"))
                    {
                        filename += ".plt";
                    }
                    
                    if(i.PdfAble)
                    {
                        if (i.FileDateTime.Date == mostRecentDate)
                        {
                            jobTicket.AppendLine("<JOB:DOCINPUT FILENAME=\"" + filename + "\" FOLDER=\"" + uncRawPrefix + i.FilePath.Replace("/", "\\") + "\"/>");
                        }
                        else
                        {
                            jobTicket.AppendLine("<!-- SKIPPING(OLDER DATE): " + filename + ", " + i.FilePath.Replace("/", "\\") + " -->");
                        }
                    }
                    else
                    {
                        jobTicket.AppendLine("<!-- SKIPPING(NOT PDF-ABLE): " + filename + ", " + i.FilePath.Replace("/", "\\") + " -->");
                        Console.WriteLine("THIS IS NOT PDF-ABLE: " + filename + ", " + i.FilePath);
                    }
                }

                jobTicket.AppendLine("</JOB:DOCINPUTS>");
                jobTicket.AppendLine("<JOB:DOCOUTPUTS>");
                jobTicket.AppendLine("<JOB:DOCOUTPUT FILENAME=\"" + kvp.Key + ".pdf\" FOLDER=\"" + uncPdfPrefix + "\\\" DOCTYPE=\"PDF\" />");
                jobTicket.AppendLine("</JOB:DOCOUTPUTS>");
                jobTicket.AppendLine("<JOB:SETTINGS>");
                jobTicket.AppendLine("<JOB:PDFSETTINGS JPEGCOMPRESSIONLEVEL=\"5\" MONOIMAGECOMPRESSION=\"Default\" GRAYSCALE=\"No\" PAGECOMPRESSION=\"Yes\" DOWNSAMPLEIMAGES=\"No\" RESOLUTION=\"1200\" PDFVERSION=\"PDFVersion15\" PDFVERSIONINHERIT=\"No\" PAGES=\"All\" />");
                jobTicket.AppendLine("</JOB:SETTINGS>");
                jobTicket.AppendLine("</JOB>");
                jobTicket.AppendLine("</JOBS>");

                Console.WriteLine(counter);
                File.WriteAllText(jobTicketLocation + mostRecentDate.ToString("yyyy-MM-dd") + "_" +  kvp.Key + ".xml", jobTicket.ToString());
            }
            //File.WriteAllLines(batchFile, batchLines);
        }

        public static Hashtable LoadPdmCatalog()
        {
            Hashtable pdmHashTable = new Hashtable();

            //load Pdm Catalog File
            StreamReader sr = new StreamReader(catalogFile);
            string headerLine = sr.ReadLine();
            string catalogLine;

            while ((catalogLine = sr.ReadLine()) != null)
            {
                var pdmCatalogItem = new PdmItem();
                List<string> pdmCatalog = catalogLine.Split(',').ToList();

                pdmCatalogItem.Server = pdmCatalog[0];
                pdmCatalogItem.FileName = pdmCatalog[2];

                if (!pdmHashTable.ContainsKey(pdmCatalogItem.FileName))
                {
                    pdmHashTable.Add(pdmCatalogItem.FileName, null);
                }
            }
            return pdmHashTable;
        }

        static void Main(string[] args)
        {
            Dictionary<string, List<PdmItem>> dictionary = new Dictionary<string, List<PdmItem>>();
            Hashtable pdmCatalogTable = LoadPdmCatalog();
            List<string> delimitedDataField = new List<string>{ "FILE_SIZE,LAST_ACCESSED,ITEM,REV,SHEET,SERVER,UNC_RAW,UNC_PDF" };
            List<string> islandOfMisfitToys = new List<string>();
            List<string> batchLines = new List<string>();

            //parse extract file
            StreamReader file = new StreamReader(inputFile);
            string inputLine;
            
            while ((inputLine = file.ReadLine()) != null)
            {
                Console.WriteLine(inputLine);
                PdmItem pdmItem = new PdmItem();
                pdmItem.ParseInputLine(inputLine);

                if(pdmItem.FileDateTime < recentDateTime)
                {
                    continue;
                }

                if (!pdmCatalogTable.ContainsKey(pdmItem.FileName))
                {
                    pdmItem.IsMisfit = true;
                    islandOfMisfitToys.Add("Not in catalog: " + inputLine);
                    continue;
                }

                if (pdmItem.IsMisfit)
                {
                    islandOfMisfitToys.Add("Misfit: " + inputLine);
                }
                else
                {
                    delimitedDataField.Add(pdmItem.GetOutputLine());
                }

                //logic to handle no revs
                string uID;
                if (String.IsNullOrEmpty(pdmItem.ItemRev))
                {
                    uID = pdmItem.ItemName;
                }
                else
                {
                    uID = pdmItem.ItemName + "." + pdmItem.ItemRev;
                }

                if (!dictionary.Keys.Contains(uID))
                {
                    List<PdmItem> pdmItems = new List<PdmItem>();
                    pdmItems.Add(pdmItem);
                    dictionary.Add(uID, pdmItems);
                }
                else
                {
                    dictionary[uID].Add(pdmItem);
                }
            }
            
            //output all misfits to file
            //File.WriteAllLines(misfitToys, islandOfMisfitToys);

            //Comment this next code until misfits are reviewed and corrected in source extract file
            //generate file for Graig
            //File.WriteAllLines(outputFile, delimitedDataField);

            //generate XML job tickets
            JobTicketGenerator(dictionary, batchLines);
        }
    }
}
