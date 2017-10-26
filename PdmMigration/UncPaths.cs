using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdmMigration
{
    public class UncPaths
    {
        public static string BuildUncRawPath(string uncRawPrefix, string filePath)
        {
            StringBuilder uncRawPathName = new StringBuilder();

            if (filePath.EndsWith("._") || filePath.EndsWith(".Z"))
            {
                filePath = filePath.Remove(filePath.Length - 2);
            }

            uncRawPathName.Append(uncRawPrefix);
            uncRawPathName.Append(filePath.Remove(0, 2));
            uncRawPathName.Replace('/', '\\');
            string uncRaw = uncRawPathName.ToString();

            return uncRaw;
        }

        public static string BuildUncPdfPath(string uncPdfPrefix, string itemName, string itemRev)
        {
            StringBuilder uncPdfPathName = new StringBuilder();

            uncPdfPathName.Append(uncPdfPrefix + "\\" + itemName);

            if (!String.IsNullOrEmpty(itemRev))
            {
                uncPdfPathName.Append("." + itemRev);
            }

            uncPdfPathName.Append(".pdf");
            uncPdfPathName.Replace('/', '\\');
            string uncPdf = uncPdfPathName.ToString();

            return uncPdf;
        }
    }
}
