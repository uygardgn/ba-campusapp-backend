using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Core.Utilities.Helpers
{
    public class FileInformations
    {
        public string FileName { get; set; }// dosyanın adı
        public FileType? FileType { get; set; }// dosyanın tipi
        public byte[]? FileByteArrayFormat { get; set; }// byte dizisi formatı
        public string Message { get; set; }
    }
}
