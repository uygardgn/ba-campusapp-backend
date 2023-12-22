using BACampusApp.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.StudentHomework
{
    public class StudentHomeworkPointDto
    {
        public Guid Id { get; set; }

        //Yalnızca Id ile çekme işlemi yapıldığından satırlar kapatılmıştır, nihai sonuç sonrası duruma göre kaldırılacaklardır.
        //public Guid HomeWorkId { get; set; }
        //public Guid StudentId { get; set; }
        public double Point { get; set; }
    }
}