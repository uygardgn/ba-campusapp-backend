using BACampusApp.Business.Concretes;
using BACampusApp.Business.TypedHttpClients;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Reflection;

namespace BACampusApp.Business.Extensions
{
    public static class DependencyInjection
    {
        private const string TurkishLanguageCode = "tr-TR";
        private const string EnglishLanguageCode = "en-US";

        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            // AutoMapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            //Her gelen request için oluşturulacak instance oluşturuyor
            services.AddScoped<ISupplementaryResourceService, SupplementaryResourceManager>();
            services.AddScoped<IEducationService, EducationManager>();
            services.AddScoped<IStudentService, StudentManager>();
            services.AddScoped<IAdminService, AdminManager>();
            services.AddScoped<ISubjectService, SubjectManager>();
            services.AddScoped<IJwtService, JwtManager>();
            services.AddScoped<IEmailService, EmailManager>();
            services.AddScoped<ITrainerService, TrainerManager>();
            services.AddScoped<ICommentService, CommentManager>();
            services.AddScoped<IHomeWorkService, HomeWorkManager>();
            services.AddScoped<IAccountService, AccountManager>();
            services.AddScoped<IClassroomService, ClassroomManager>();
            services.AddScoped<IStudentHomeworkService, StudentHomeworkManager>();
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<ISubCategoryService, SubCategoryManager>();
            services.AddScoped<ITagService, TagManager>();
            services.AddScoped<IPhoneNumberService, PhoneNumberManager>();
            services.AddScoped<IActivityStateLogSevices, ActivityStateLogManager>();         
            services.AddScoped<IClassroomStudentService, ClassroomStudentManager>();
            services.AddScoped<ITechnicalUnitsService, TechnicalUnitsManager>();
            services.AddScoped<ISupplementaryResourceTagService, SupplementaryResourceTagManager>();
            services.AddScoped<IEducationSubjectService, EducationSubjectManager>();
            services.AddScoped<IClassroomTrainersService, ClassroomTrainerManager>();
            services.AddScoped<IRoleLogService, RoleLogManager>();
            services.AddScoped<IBranchService, BranchManager>();
            services.AddScoped<ITrainingTypeService, TrainingTypeManager>();
            services.AddScoped<IUserPasswordsService, UserPasswordsManager>();
            services.AddScoped<IStudentLogTableService, StudentLogTableManager>();
            services.AddScoped<ITrainerLogTableService, TrainerLogTableManager>();

            services.AddScoped<IGroupTypeService, GroupTypeManager>();


            services.AddScoped<IGroupTypeService, GroupTypeManager>();

            services.AddScoped<ITokenBlackListService, TokenBlackListManager>();

            services.AddScoped<IReportService, ReportManager>();

            services.AddScoped<ISupplementaryResourceEducationSubjectService, SupplementaryResourceEducationSubjectManager>();

            services.AddHttpClient<IpApiService>();

            services.AddScoped<IVideoFileService, VideoFileManager>();

            //Bu servisler error ve success messagge'lar için kullanılacak Localization extension'ının sisteme entegre edilmesini içerir.
            services.AddLocalization();
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo(TurkishLanguageCode),
                    new CultureInfo(EnglishLanguageCode),
                };
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;

                options.DefaultRequestCulture = new RequestCulture(EnglishLanguageCode);

                options.RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(async context =>
                {
                    string defaultLanguage = EnglishLanguageCode;
                    var languages = context.Request.Headers["Accept-Language"].ToString().Split(',');
                    if (languages.Any(a => a.Contains("tr")))                    
                        defaultLanguage = TurkishLanguageCode;
                    
                    return await Task.FromResult(new ProviderCultureResult(defaultLanguage, defaultLanguage));
                }));
            });

            return services;
        }
    }
}
