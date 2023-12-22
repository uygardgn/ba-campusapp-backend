using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.EducationSubject
{
	public class EducationSubjectListCreateDto
	{
		public Guid EducationId { get; set; }
		public List<Guid> SubjectIds { get; set; }
	}
}
