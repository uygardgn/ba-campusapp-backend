using BACampusApp.Entities.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.StudentHomework
{
	public class StudentHomeworkStudentUpdateDto
	{
		public Guid Id { get; set; }
		public IFormFile? AttachedFile { get; set; }
        public bool IsHardDelete { get; set; }
    }
}
