using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using Spire.DataExport;
using Spire.DataExport.RTF;

namespace Scin_Beauty.Class
{
    class Export
    { 
        public void ExportToWord(DataTable dataTable)
        {
            RTFExport rtfExport = new RTFExport();
            rtfExport.DataSource = Spire.DataExport.Common.ExportSource.DataTable;
            rtfExport.DataTable = dataTable;
            rtfExport.ActionAfterExport = Spire.DataExport.Common.ActionType.OpenView;
            RTFStyle rtfStyle = new RTFStyle();
            rtfStyle.FontColor = Color.Black;
            rtfExport.RTFOptions.DataStyle = rtfStyle;
            if (OpenFileDialog()) 
            {
                rtfExport.FileName = FullFilePath;
                rtfExport.SaveToFile();
            }
        }

        public string FilePath { get; set; }
        public string FullFilePath { get; set; }
        public string DestinationPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
        public bool OpenFileDialog()
        {
            SaveFileDialog openFileDialog = new SaveFileDialog();
            openFileDialog.Filter = "Your extension here (*.docx)|*.docx|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.SafeFileName;
                FullFilePath = openFileDialog.FileName;
                return true;
            }
            return false;
        }
    }
}
