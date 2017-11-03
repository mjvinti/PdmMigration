﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PdmMigration
{
    class Program
    {
        public static string catalogFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\PDM-Catalog_2017-11-01.csv";
        public static string inputFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\EA\2017-11-01\EA_2017-11-01.txt";
        public static string batchFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\EA\2017-11-01\singlePdfCopy.bat";
        public static string serverName = "pdm.moog.com";
        public static string outputFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\EA\2017-11-01\EA_import_2017-11-01.txt";
        public static string misfitToys = @"P:\Architecture Group\Projects\PDM Migration\extracts\EA\2017-11-01\EA_importMisfits_2017-11-01.txt";
        public static string jobTicketLocation = @"P:\Architecture Group\Projects\PDM Migration\extracts\EA\2017-11-01\jobtickets\";
        public static string uncRawPrefix = @"\\eacmpnas01.moog.com\Vol5_Data\PDM\EA";
        public static string uncPdfPrefix = @"\\eacmpnas01.moog.com\Vol5_Data\PDM\EA\tcpdf";
        public static DateTime recentDateTime = DateTime.MinValue;
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
            foreach (KeyValuePair<string, List<PdmItem>> kvp in dictionary)
            {
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
                jobTicket.AppendLine("<!DOCTYPE JOBS SYSTEM \"X:\\PDM\\AdlibExpress.dtd\">");
                jobTicket.AppendLine("<JOBS xmlns:JOBS=\"http://www.adlibsoftware.com\" xmlns:JOB=\"http://www.adlibsoftware.com\">");
                jobTicket.AppendLine("<JOB>");
                jobTicket.AppendLine("<JOB:DOCINPUTS>");

                DateTime mostRecentDate = DateTime.MinValue;
                foreach(var i in kvp.Value)
                {
                    //Find most recent date in list
                    if(i.FileDateTime > mostRecentDate)
                    {
                        mostRecentDate = i.FileDateTime;
                    }
                }

                foreach(var i in kvp.Value)
                {
                    string filename = i.FileName;

                    if (i.FileName.EndsWith(".pra"))
                    {
                        filename += ".plt";
                    }
                    
                    if(i.PdfAble)
                    {
                        if (i.FileDateTime == mostRecentDate)
                        {
                            jobTicket.AppendLine("<JOB:DOCINPUT FILENAME=\"" + filename + "\" FOLDER =\"" + uncRawPrefix + i.FilePath.Replace("/", "\\") + "\"/>");
                        }
                        else
                        {
                            jobTicket.AppendLine("<!-- SKIPPING: " + filename + ", " + i.FilePath.Replace("/", "\\") + " -->");
                        }
                    }
                    else
                    {
                        jobTicket.AppendLine("<!-- SKIPPING: " + filename + ", " + i.FilePath.Replace("/", "\\") + " -->");
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

                //Console.WriteLine(jobTicket.ToString());
                File.WriteAllText(jobTicketLocation + mostRecentDate.ToString("yyyy-MM-dd") + "_" +  kvp.Key + ".xml", jobTicket.ToString());
            }
            File.WriteAllLines(batchFile, batchLines);
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

                //NEW CODE HERE
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
            File.WriteAllLines(misfitToys, islandOfMisfitToys);

            //Comment this next code until misfits are reviewed and corrected in source extract file
            //generate file for Graig
            File.WriteAllLines(outputFile, delimitedDataField);

            //generate XML job tickets
            JobTicketGenerator(dictionary, batchLines);
        }
    }
}
