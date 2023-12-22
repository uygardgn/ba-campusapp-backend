using BACampusApp.Entities.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.StudentHomework
{
    public class StudentHomeworkSubmitDto
    {
        public Guid Id { get; set; } // Öğrenci ödev yanıtının benzersiz kimliği
        public Guid HomeWorkId { get; set; } // İlgili ödevin kimliği
        public Guid StudentId { get; set; } // Öğrencinin kimliği
        public IFormFile? ReferanceFile { get; set; }
        public string AttachedFile { get; set; } // Öğrenci tarafından yüklenen dosyanın adı
        public DateTime? SubmitDate { get; set; } // Yanıtın gönderildiği tarih
        public double? Point { get; set; } // Ödev puanı
        public HomeworkState? HomeworkState { get; set; } // Ödevin durumu (atandı, teslim edildi, teslim edilmedi, vs.)
    }
}
