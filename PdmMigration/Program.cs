using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdmMigration
{
    class Program
    {
        public static string catalogFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\PDM-Catalog_2017-10-26.csv";
        public static string inputFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\EA\eapdm1_full_2017-10-24-0120.txt";
        public static string batchFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\EA\singlePdfCopy.bat";
        public static string serverName = "pdm.moog.com";
        public static string outputFile = @"C:\Users\mvinti\Desktop\PDM\PdmMigration_Remote_10.25.2017\EA\eapdm_extracts3_2017-10-26.txt";
        public static string misfitToys = @"C:\Users\mvinti\Desktop\PDM\PdmMigration_Remote_10.25.2017\EA\eapdm_misfits3_2017-10-26.txt";
        public static string uncRawPrefix = @"\\eacmpnas01.moog.com\Vol5_Data\PDM\EA";
        public static string uncPdfPrefix = @"\\eacmpnas01.moog.com\Vol5_Data\PDM\EA\tcpdf";
        public static bool isWindows = false;
        public static bool isLuDateTime = false;
        public static bool isIeDateTime = false;

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
                case "doc":
                case "docx":
                case "dos":
                case "dot":
                case "dwg":
                case "dxf":
                case "edt":
                case "flv":
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
                case "mpg":
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
                    return true;
                default:
                    return false;
            }
        }

        public static void JobTicketGenerator(Dictionary<string, List<PdmItem>> dictionary)
        {
            foreach(KeyValuePair<string, List<PdmItem>> kvp in dictionary)
            {
                //if there is only one kvp, then we already have a pdf somewhere in theory
                if(kvp.Value.Count < 2)
                {
                    //find pdf and copy to correct folder; build batch file

                    continue;
                }

                StringBuilder jobTicket = new StringBuilder();

                jobTicket.AppendLine("<?xml version=\"1.0\" encoding=\"ISO-8859-1\" ?>");
                jobTicket.AppendLine("<?AdlibExpress applanguage = \"USA\" appversion = \"4.11.0\" dtdversion = \"2.6\" ?>");
                jobTicket.AppendLine("<!DOCTYPE JOBS SYSTEM \"X:\\PDM\\AdlibExpress.dtd\">");
                jobTicket.AppendLine("<JOBS xmlns:JOBS=\"http://www.adlibsoftware.com\" xmlns:JOB=\"http://www.adlibsoftware.com\">");
                jobTicket.AppendLine("<JOB>");
                jobTicket.AppendLine("<JOB:DOCINPUTS>");

                foreach (var i in kvp.Value)
                {
                    jobTicket.AppendLine("<JOB:DOCINPUT FILENAME=\"" + i + "\" FOLDER =\"X:\\PDM\\in\\\" />");
                }

                jobTicket.AppendLine("</JOB:DOCINPUTS>");
                jobTicket.AppendLine("<JOB:DOCOUTPUTS>");
                jobTicket.AppendLine("<JOB:DOCOUTPUT FILENAME = \"" + kvp.Key + ".pdf\" FOLDER = \"X:\\PDM\\out\\\" DOCTYPE=\"PDF\" />");
                jobTicket.AppendLine("</JOB:DOCOUTPUTS>");
                jobTicket.AppendLine("<JOB:SETTINGS>");
                jobTicket.AppendLine("<JOB:PDFSETTINGS JPEGCOMPRESSIONLEVEL=\"5\" MONOIMAGECOMPRESSION=\"Default\" GRAYSCALE=\"No\" PAGECOMPRESSION=\"Yes\" DOWNSAMPLEIMAGES=\"No\" RESOLUTION=\"1200\" PDFVERSION=\"PDFVersion15\" PDFVERSIONINHERIT=\"No\" PAGES=\"All\" />");
                jobTicket.AppendLine("</JOB:SETTINGS>");
                jobTicket.AppendLine("</JOB>");
                jobTicket.AppendLine("</JOBS>");

                Console.WriteLine(jobTicket.ToString());
                //File.WriteAllText(@"\\eacmpnas01.moog.com\Vol3_Data\PDM\migration\pdm.moog.com\jobs\" + kvp.Key + ".xml", jobTicket.ToString());
                File.WriteAllText(@"C:\Users\mvinti\Desktop\PDM\XmlChallenge\jobTickets\" + kvp.Key + ".xml", jobTicket.ToString());
            }
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

            //parse extract file
            StreamReader file = new StreamReader(inputFile);
            string inputLine;
            
            while ((inputLine = file.ReadLine()) != null)
            {
                Console.WriteLine(inputLine);
                PdmItem pdmItem = new PdmItem();
                pdmItem.ParseInputLine(inputLine);

                if(!pdmCatalogTable.ContainsKey(pdmItem.FileName))
                {
                    pdmItem.IsMisfit = true;
                    islandOfMisfitToys.Add("Not in catalog: " + inputLine);
                    continue;
                }

                if(pdmItem.IsMisfit)
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

            //Inspect Dictionary for possible invalid sheets 
            //foreach(KeyValuePair<string, List<string>> dictionaryKeyValuePair in dictionary)
            //{
            //    foreach(var value in dictionaryKeyValuePair.Value)
            //    {
            //        if(pdmItem.ItemSht.Length == value.Length)
            //        {
            //            misfit_InvalidSheets
            //        }
            //    }
                    
            //}
            
            //output all misfits to file
            //File.WriteAllLines(misfitToys, islandOfMisfitToys);

            //Comment this next code until misfits are reviewed and corrected in source extract file
            // generate file for Graig
            //File.WriteAllLines(outputFile, delimitedDataField);

            // generate XML job tickets
            JobTicketGenerator(dictionary);

        }
    }
}
