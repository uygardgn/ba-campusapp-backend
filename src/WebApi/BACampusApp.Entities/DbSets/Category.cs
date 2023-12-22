namespace BACampusApp.Entities.DbSets
{
    public class Category : AuditableEntity
    {
        public Category() 
        {
            SubCategories = new HashSet<Category>();
            Educations = new HashSet<Education>();
        }

        public string Name { get; set; }
        // TechnicalUnit FK
        public Guid TechnicalUnitId { get; set; }
        // TechnicalUnit Navigation Property
        public virtual TechnicalUnits? TechnicalUnit { get; set; }
        public virtual ICollection<Education>? Educations { get; set; }



        // Parent Category FK
        public Guid? ParentCategoryId { get; set; }

        // Parent Category Navigation Property
        public virtual Category? ParentCategory { get; set; }

        // SubCategories (Child Categories)
        public virtual ICollection<Category> SubCategories { get; set; }
    }
}
