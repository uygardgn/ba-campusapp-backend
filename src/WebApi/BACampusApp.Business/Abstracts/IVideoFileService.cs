using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Business.Abstracts
{
    public interface IVideoFileService
    {
        Task<bool> CheckVideoDurationAsync(IFormFile videoFile);
        Task<IFormFile> ConvertTo480pAsync(IFormFile videoFile);
        Task<byte[]> ConvertTo360pAsync(byte[] videoBytes);
        Task<bool> CheckVideoSizeAsync(IFormFile videoFile);
    }
}
