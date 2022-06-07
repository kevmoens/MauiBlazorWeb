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
    }
}
