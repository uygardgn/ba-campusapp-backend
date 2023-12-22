using BACampusApp.Authentication.Options;

namespace BACampusApp.DataAccess.EFCore.Extensions;
public static class DependencyInjection
{
    public static IServiceCollection AddEFCoreServices(this IServiceCollection services)
    {
        services.AddScoped<ISupplementaryResourceRepository, SupplementaryResourceRepository>();
        services.AddScoped<ITagRepository, TagRepository>();
        services.AddScoped<ISupplementaryResourceTagRepository, SupplementaryResourceTagRepository>();
        services.AddScoped<IEducationSubjectRepository, EducationSubjectRepository>();
        services.AddScoped<ISubjectRepository, SubjectRepository>();
        services.AddScoped<IEducationRepository, EducationRepository>();
        services.AddScoped<IHomeWorkRepository, HomeWorkRepository>();
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<IAdminRepository,AdminRepository>();
        services.AddScoped<ITrainerRepository,TrainerRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<IClassroomRepository, ClassroomRepository>();
        services.AddScoped<IStudentHomeworkRepository, StudentHomeworkRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
        services.AddScoped<IClassroomStudentRepository, ClassroomStudentRepository>();
        services.AddScoped<ITechnicalUnitsRepository,TechnicalUnitsRepository>();
        services.AddScoped<ISupplementaryResourceTagRepository, SupplementaryResourceTagRepository>();
        services.AddScoped<IClassroomTrainersRepository, ClassroomTrainersRepository>();
        services.AddScoped<IActivityStateLogRepository, ActivityStateLogRepository>();
        services.AddScoped<IRoleLogRepository, RoleLogRepository>();
        services.AddScoped<IBranchRepository, BranchRepository>();
        services.AddScoped<ITrainingTypeRepository, TrainingTypeRepository>();
        services.AddScoped<ITrainerLogTableRepository,TrainerLogTableRepository>();
        services.AddScoped<IStudentLogTableRepository , StudentLogTableRepository>();
        services.AddScoped<IUserPasswordsRepository , UserPasswordsRepository>();

        services.AddScoped<IGroupTypeRepository, GroupTypeRepository>();

        services.AddScoped<IGroupTypeRepository, GroupTypeRepository>();

        services.AddScoped<ITokenBlackListRepository, TokenBlackListRepository>();

        services.AddScoped<ISupplementaryResourceEducationSubjectRepository, SupplementaryResourceEducationSubjectRepository>();

        services.AddScoped<JwtOptions>();
        return services;

    }
}
