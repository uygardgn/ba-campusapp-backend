using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.Categorys
{
    public class CategoryListBySubCategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        // Parent Category FK
        public Guid? ParentCategoryId { get; set; }
        public Guid? SubCategoryId{ get; set; }
    }
}
