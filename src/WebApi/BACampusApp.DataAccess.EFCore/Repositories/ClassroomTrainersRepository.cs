namespace BACampusApp.DataAccess.EFCore.Repositories
{
    public class ClassroomTrainersRepository : EFBaseRepository<ClassroomTrainer>, IClassroomTrainersRepository
    {
        public ClassroomTrainersRepository(BACampusAppDbContext context) : base(context)
        { 
        }
    }
}
