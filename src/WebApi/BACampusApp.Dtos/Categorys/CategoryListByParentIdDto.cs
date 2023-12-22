using BACampusApp.Entities.DbSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.Categorys
{
    public class CategoryListByParentIdDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid TechnicalUnitId { get; set; }


        // Parent Category FK
        public Guid? ParentCategoryId { get; set; }
        public List<CategoryListByParentIdDto> SubCategories { get; set; }


    }
}
