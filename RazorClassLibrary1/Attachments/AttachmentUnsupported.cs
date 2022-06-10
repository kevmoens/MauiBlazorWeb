using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorClassLibrary1.Attachments
{
    public class AttachmentUnsupported : IAttachment
    {
        public string? FileName { get; set; }

        public Task<int> Open()
        {
            return Task.FromResult(-1);
        }

        public Task<int> Print()
        {
            return Task.FromResult(-1);
        }

        public Task<int> PrintTo(string Printer)
        {
            return Task.FromResult(-1);
        }
    }
}
