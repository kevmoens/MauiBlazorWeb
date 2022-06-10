using RazorClassLibrary1.Attachments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MauiBlazorApp1.Attachments
{
    public class AttachmentWindows : IAttachment
    {

        [DllImport("shell32", EntryPoint = "ShellExecuteA")]
        private static extern int ShellExecute(int hwnd, string lpOperation, string lpFile, string lpParameters, string lpDirectory, int nShowCmd);

        public string? FileName { get; set; }

        public Task<int> Open()
        {


            if ((FileName?.ToUpper()?.StartsWith("HTTP") ?? false) || File.Exists(FileName))
            {
                string arglpOperation = "Open";
                string arglpParameters = "";
                var lngReturn = ShellExecute(0, arglpOperation, FileName, arglpParameters, System.Environment.CurrentDirectory, 1);
                if (lngReturn == 2L | lngReturn == 31L)
                {
                    string arglpOperation1 = "";
                    string arglpParameters1 = "";
                    lngReturn = ShellExecute(0, arglpOperation1, FileName, arglpParameters1, System.Environment.CurrentDirectory, 1);
                }
            }

            return Task.FromResult(0);
        }

        public Task<int> Print()
        {           
            if ((FileName?.ToUpper()?.StartsWith("HTTP") ?? false) || File.Exists(FileName))
            {
                string arglpOperation = "print";
                string arglpParameters = "";
                var lngReturn = ShellExecute(0, arglpOperation, FileName, arglpParameters, System.Environment.CurrentDirectory, 1);
                if (lngReturn == 2L | lngReturn == 31L)
                {
                    string arglpOperation1 = "";
                    string arglpParameters1 = "";
                    lngReturn = ShellExecute(0, arglpOperation1, FileName, arglpParameters1, System.Environment.CurrentDirectory, 1);
                }
            }
            return Task.FromResult(0);
        }
        public async Task<int> PrintTo(string Printer)
        {
            var printers = new List<string>();
            await Task.Delay(0);
            if (Printer == null)
            {
                return -1;
            }
#if WINDOWS
            printers = await GetPrinters();

            if (!printers.Contains(Printer, StringComparer.InvariantCultureIgnoreCase))
            {
                return -2;
            }

            var file = new System.IO.FileInfo(FileName);
            if (file.Extension.ToUpperInvariant() == ".XLSX")
            {
                PrintExcelFile(Printer);
                return 0;
            }
#endif

            //NON EXCEL FILES
            string arglpOperation = "printto";
            string arglpParameters = "\"" + Printer + "\"";
            var lngReturn = ShellExecute(0, arglpOperation, FileName, arglpParameters, System.Environment.CurrentDirectory, 1);


            return 0;
        }
#if WINDOWS
        private async Task<List<string>> GetPrinters()
        {

            var printers = new List<string>();
            // This selection filter will filter for printer device interfaces.
            string printerInterfaceClass = "{0ecef634-6ef0-472a-8085-5ad023ecbccd}";
            string selectorFilter = "System.Devices.InterfaceClassGuid:=\"" + printerInterfaceClass + "\"";

            // Look for the Container Id in each device interface.
            string containerIdPropertyName = "System.Devices.ContainerId";
            string[] propertiesToRetrieve = new string[] { containerIdPropertyName };

            // Asynchronously find all printer device interfaces.
            Windows.Devices.Enumeration.DeviceInformationCollection printerInterfaceCollection =
                await Windows.Devices.Enumeration.DeviceInformation.FindAllAsync(selectorFilter, propertiesToRetrieve);

            // For each printer device returned, retrieve the device container, which contains
            // the association information for the Windows Store Device Apps.
            foreach (Windows.Devices.Enumeration.DeviceInformation printerInterfaceInformation in printerInterfaceCollection)
            {
                printers.Add((string)printerInterfaceInformation.Properties["System.ItemNameDisplay"]);
            }
        }
        private void PrintExcelFile(string Printer) {
         Type excelType = Type.GetTypeFromProgID("Excel.Application");
                dynamic excelApp = Activator.CreateInstance(excelType);
                // Open the Workbook:
                var wb = excelApp.Workbooks.Open(
                    FileName,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                // Get the first worksheet.
                // (Excel uses base 1 indexing, not base 0.)
                var ws = wb.Worksheets[1];

                // Print out 1 copy to the default printer:
                ws.PrintOut(
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Printer, Type.Missing, Type.Missing, Type.Missing);

                // Cleanup:
                GC.Collect();
                GC.WaitForPendingFinalizers();

                Marshal.FinalReleaseComObject(ws);

                wb.Close(false, Type.Missing, Type.Missing);
                Marshal.FinalReleaseComObject(wb);

                excelApp.Quit();
                Marshal.FinalReleaseComObject(excelApp);
        }
#endif 
    }
}
