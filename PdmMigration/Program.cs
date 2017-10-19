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
        static string catalogFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\PDM-Catalog.csv";
        static string inputFile = @"P:\Architecture Group\Projects\PDM Migration\extracts\TOR\TOR_full_201710081815.txt";
        static string serverName = "pdm.tor.moog.com";
        static string outputFile = @"C:\Users\mvinti\Desktop\PDM\TestDataExtracts\TOR\test_torpdm4.txt";
        static string misfitToys = @"C:\Users\mvinti\Desktop\PDM\TestDataExtracts\TOR\test_torpdm4_misfits.txt";
        static string uncRawPrefix = @"\\toriman.tor.moog.com";
        static string uncPdfPrefix = @"\\toriman.tor.moog.com\tcpdf";

        static bool IsExt(string token)
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

        public static PdmItem ParseSourceLine(string line)
        {
            PdmItem pdmItem = new PdmItem();
            pdmItem.HasRev = false;
            pdmItem.HasSht = false;
            pdmItem.HasExt = false;
            int idx1;
            int idx2;
            string pathOnly;
            string fileName;

            if (line.EndsWith(".Z") || line.StartsWith("-r") || !line.Contains("\\"))
            {
                //populate all data in PDMItem by parsing line with Linux rules
                List<string> linuxData = line.Split(' ').ToList();
                linuxData.RemoveAll(String.IsNullOrEmpty);
                linuxData.RemoveRange(0, 4);

                pdmItem.FileSize = Convert.ToInt64(linuxData[0]);

                pdmItem.FileMonth = linuxData[1];
                pdmItem.FileDay = linuxData[2];

                if (linuxData[3].Contains(":"))
                {
                    pdmItem.FileYear = "2017";
                    pdmItem.FileTime = linuxData[3];
                }
                else
                {
                    pdmItem.FileYear = linuxData[3];
                    pdmItem.FileTime = "00:00";
                }

                pdmItem.FileDateTime = Convert.ToDateTime(pdmItem.FileMonth + ' ' + pdmItem.FileDay + ' ' + pdmItem.FileYear + ' ' + pdmItem.FileTime);

                pdmItem.FilePath = linuxData[4];

                idx1 = pdmItem.FilePath.LastIndexOf('/');
                pathOnly = pdmItem.FilePath.Substring(0, idx1 + 1);
                fileName = pdmItem.FilePath.Substring(idx1 + 1);

                string[] linuxDataFileSplit = pdmItem.FilePath.Split('.');

                idx2 = linuxDataFileSplit[0].LastIndexOf('/');
                pdmItem.ItemName = linuxDataFileSplit[0].Substring(idx2 + 1);

                if (linuxDataFileSplit.Length == 2)
                {
                    pdmItem.HasExt = true;
                    pdmItem.ItemExt = linuxDataFileSplit[1];
                    //string uncRawPath = BuildUncRawPath(item.FilePath);
                    //string uncPdfPath = BuildUncPdfPath(item.ItemName, item.ItemRev);
                }

                else if (linuxDataFileSplit.Length > 2)
                {
                    if (!IsExt(linuxDataFileSplit[1]))
                    {
                        pdmItem.HasRev = true;
                        pdmItem.ItemRev = linuxDataFileSplit[1];
                    }
                    else
                    {
                        pdmItem.HasExt = true;
                        pdmItem.ItemExt = linuxDataFileSplit[1];
                    }

                    if (!IsExt(linuxDataFileSplit[2]))
                    {
                        pdmItem.HasSht = true;
                        pdmItem.ItemSht = linuxDataFileSplit[2];
                    }
                    else
                    {
                        pdmItem.HasExt = true;
                        pdmItem.ItemExt = linuxDataFileSplit[2];
                    }

                    if (linuxDataFileSplit.Length > 3 && IsExt(linuxDataFileSplit[3]))
                    {
                        pdmItem.HasExt = true;
                        pdmItem.ItemExt = linuxDataFileSplit[3];
                    }

                    if (nonStandardData)
                    {
                        misfit_InvalidFormat
                    }
                }

                else if (line.EndsWith("._") || line.Contains("\\"))
                {
                    //popluate all data in PDMItem by parsing line with Windows rules
                    List<string> windowsData = line.Split(' ').ToList();
                    windowsData.RemoveAll(String.IsNullOrEmpty);

                    pdmItem.FileSize = Convert.ToInt64(windowsData[0]);

                    pdmItem.FileDateTime = Convert.ToDateTime(windowsData[1] + ' ' + windowsData[2] + ' ' + windowsData[3]);

                    //item.FileDateTime = data[1] is month/day/year; data[2] is HH:MM:SS; data[3] is AM/PM 
                    //item.FileDateTime = Convert.ToDateTime(item.FileMonth + ' ' + item.FileDay + ' ' + item.FileYear + ' ' + item.FileTime);

                    pdmItem.FilePath = windowsData[4];

                    idx1 = pdmItem.FilePath.LastIndexOf('\\');
                    pathOnly = pdmItem.FilePath.Substring(2, idx1 + 1);
                    fileName = pdmItem.FilePath.Substring(idx1 + 1);

                    string[] windowsDataFileSplit = pdmItem.FilePath.Split('.');

                    idx2 = windowsDataFileSplit[0].LastIndexOf('\\');
                    pdmItem.ItemName = windowsDataFileSplit[0].Substring(idx2 + 1);

                    if (windowsDataFileSplit.Length == 2)
                    {
                        pdmItem.HasExt = true;
                        pdmItem.ItemExt = windowsDataFileSplit[1];
                        //string uncRawPath = BuildUncRawPath(item.FilePath);
                        //string uncPdfPath = BuildUncPdfPath(item.ItemName, item.ItemRev);
                    }

                    else if (windowsDataFileSplit.Length > 2)
                    {
                        if (!IsExt(windowsDataFileSplit[1]))
                        {
                            pdmItem.HasRev = true;
                            pdmItem.ItemRev = windowsDataFileSplit[1];
                        }
                        else
                        {
                            pdmItem.HasExt = true;
                            pdmItem.ItemExt = windowsDataFileSplit[1];
                        }

                        if (!IsExt(windowsDataFileSplit[2]))
                        {
                            pdmItem.HasSht = true;
                            pdmItem.ItemSht = windowsDataFileSplit[2];
                        }
                        else
                        {
                            pdmItem.HasExt = true;
                            pdmItem.ItemExt = windowsDataFileSplit[2];
                        }

                        if (windowsDataFileSplit.Length > 3 && IsExt(windowsDataFileSplit[3]))
                        {
                            pdmItem.HasExt = true;
                            pdmItem.ItemExt = windowsDataFileSplit[3];
                        }

                        if (nonStandardData)
                        {
                            misfit_InvalidFormat
                        }
                    }

                    else
                    {
                        misfit_InvalidFormat
                    }

                    return pdmItem;
                }

            }

        }

        public static void JobTicketGenerator(Dictionary<string, List<string>> dictionary)
        {
            foreach (KeyValuePair<string, List<string>> kvp in dictionary)
            {
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

                //File.WriteAllText(@"\\eacmpnas01.moog.com\Vol3_Data\PDM\migration\pdm.moog.com\jobs\" + kvp.Key + ".xml", jobTicket.ToString());
                File.WriteAllText(@"C:\Users\mvinti\Desktop\PDM\XmlChallenge\jobTickets\" + kvp.Key + ".xml", jobTicket.ToString());
            }
        }

        static void Main(string[] args)
        {
            List<PdmItem> pdmItems = new List<PdmItem>();
            Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
            Hashtable hashtable = new Hashtable();

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
                
                if(!hashtable.ContainsKey(pdmCatalogItem.FileName))
                {
                    hashtable.Add(pdmCatalogItem.FileName, null);
                }
            }

            //parse extract file
            StreamReader file = new StreamReader(inputFile);
            int counter = 0;
            int counterSS = 0;
            string inputLine;
            
            while ((inputLine = file.ReadLine()) != null)
            {
                if (inputLine.EndsWith(".ss"))
                {
                    counterSS++;
                    continue;
                }
                
                PdmItem item = ParseSourceLine(inputLine);

                if(hashtable.ContainsKey(pdmItem.FileName))
                {
                    pdmItems.Add(item);
                    List<string> valueFilePath = new List<string>();
                    valueFilePath.Add(pdmItem.FileName);
                    dictionary.Add(pdmItem.ItemName + "." + pdmItem.ItemRev, valueFilePath);
                }
        
                else
                {
                    misfit_NotInCatalog
                }

                counter++;
            }

            //Inspect Dictionary for possible invalid sheets 
            foreach(KeyValuePair<string, List<string>> dictionaryKeyValuePair in dictionary)
            {
                foreach(var value in dictionaryKeyValuePair.Value)
                {
                    if(pdmItem.ItemSht.Length == value.Length)
                    {
                        misfit_InvalidSheets
                    }
                }
                    
            }
            
            //output all misfits to file
            File.WriteAllLines(misfitToys, islandOfMisfitToys);

            //Comment this next code until misfits are reviewed and corrected in source extract file
            // generate file for Graig
            //File.WriteAllLines(outputFile, delimitedDataField);

            // generate XML job tickets
            //JobTicketGenerator(dictionary);

        }
    }
}
