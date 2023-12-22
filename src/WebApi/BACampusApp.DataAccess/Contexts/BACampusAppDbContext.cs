using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BACampusApp.DataAccess.Contexts;
public class BACampusAppDbContext : IdentityDbContext<IdentityUser,IdentityRole,string>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    internal const string ConnectionName = "BACampusApp";
    public BACampusAppDbContext(DbContextOptions<BACampusAppDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options) 
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public virtual DbSet<SupplementaryResource> SupplementaryResources { get; set; }
    public virtual DbSet<SupplementaryResourceTag> SupplementaryResourceTags { get; set; }
    public virtual DbSet<Tag> Tags { get; set; }
    public virtual DbSet<Education> Educations { get; set; }
    public virtual DbSet<EducationSubject> EducationSubjects { get; set; }
    public virtual DbSet<Student> Students { get; set; }
    public virtual DbSet<Admin> Admins { get; set; }
    public virtual DbSet<Subject> Subjects { get; set; }
    public virtual DbSet<Trainer> Trainers { get; set; }
    public virtual DbSet<Comment> Comments { get; set; }
    public virtual DbSet<HomeWork> HomeWorks { get; set; }
    public virtual DbSet<StudentHomework> StudentHomeworks { get; set; }
    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Classroom> Classrooms { get; set; }
    public virtual DbSet<ClassroomStudent> ClassroomStudents { get; set; }
    public virtual DbSet<ClassroomTrainer> ClassroomTrainers { get; set; }
    public virtual DbSet<TechnicalUnits> TechnicalUnits { get; set; }
    public virtual DbSet<ActivityStateLog> ActivityStateLogs { get; set; }
    public virtual DbSet<RoleLog> RoleLogs { get; set; }
    public virtual DbSet<Branch> Branches { get; set; }
    public virtual DbSet<TrainingType> TrainingTypes { get; set; }
    public virtual DbSet<TrainerLogTable> TrainerLogTable { get; set; }
    public virtual DbSet<StudentLogTable> StudentLogTable { get; set; }
    public virtual DbSet<UserPasswords> UserPasswords { get; set; }

    public virtual DbSet<GroupType> GroupTypes { get; set; }

    public virtual DbSet<TokenBlackList> TokenBlackLists { get; set; }

    public virtual DbSet<SupplementaryResourceEducationSubject> SupplementaryResourceEducationSubjects { get; set; }




    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IEntityTypeConfiguration).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public override int SaveChanges()
    {
        SetBaseProperties();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SetBaseProperties();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void SetBaseProperties()
    {
        var entries = ChangeTracker.Entries<BaseEntity>();

        var user = _httpContextAccessor.HttpContext!.User;
        var userId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        foreach (var entry in entries)
        {
            SetIfAdded(entry, userId);
            SetIfModified(entry, userId);
            SetIfDeleted(entry, userId);
        }
    }

    private void SetIfDeleted(EntityEntry<BaseEntity> entry, string userId)
    {
        if (entry.State is not EntityState.Deleted)
        {
            return;
        }

        if (entry.Entity is not AuditableEntity entity)
        {
            return;
        }

        entry.State = EntityState.Modified;

        entity.Status = Status.Deleted;
        entity.DeletedDate = DateTime.Now;
        entity.DeletedBy = userId;
    }

    private void SetIfModified(EntityEntry<BaseEntity> entry, string userId)
    {
        if (entry.State == EntityState.Modified)
        {
            entry.Entity.Status = Status.Active;
        }

        entry.Entity.ModifiedBy = userId;
        entry.Entity.ModifiedDate = DateTime.Now;
    }

    private async void SetIfAdded(EntityEntry<BaseEntity> entry, string userId)
    {
        if (entry.State != EntityState.Added)
        {
            return;
        }
     
        entry.Entity.Status = Status.Active;
        entry.Entity.CreatedBy = userId;
        entry.Entity.CreatedDate = DateTime.Now;
    }
}