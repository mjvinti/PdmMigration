using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdmMigration
{
    public class PdmItem
    {
        public Int64 FileSize { get; set; }
        public string FileMonth { get; set; }
        public string FileDay { get; set; }
        public string FileYear { get; set; }
        public string FileTime { get; set; }
        public DateTime FileDateTime { get; set; }
        public string ItemName { get; set; }
        public string ItemRev { get; set; }
        public string ItemExt { get; set; }
        public string ItemSht { get; set; }
        public bool HasRev { get; set; }
        public bool HasSht { get; set; }
        public bool HasExt { get; set; }
        public string FileName { get; set; }     // ex: A12345.0.01.plt.Z
        public string FilePath { get; set; }     // ex: F:\archive\pdm\web\hpgl\A\
        public string FilePathName { get; set; } // ex: F:\archive\pdm\web\hpgl\A\A12345.0.01.plt.Z
        public string Server { get; set; }       // ex: pdm.moog.com
        public string UncRaw { get; set; }       // ex: \\eacmpnas01.moog.com\Vol5_Data\PDM\EA\archive\pdm\web\hpgl\A\A12345.0.01.plt
        public string UncPdf { get; set; }       // ex: \\eacmpnas01.moog.com\Vol5_Data\PDM\EA\tcpdf\A12345.0.pdf

        public string getOutputLine()
        {
            //build and return full output line string using the data members above (for Graig's file)
            
        }
    }
}
