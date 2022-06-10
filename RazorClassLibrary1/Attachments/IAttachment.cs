using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorClassLibrary1.Attachments
{
    public interface IAttachment
    {
        string? FileName { get; set; }
        Task<int> Open();
        Task<int> Print();
        Task<int> PrintTo(string Printer);
    }
}
