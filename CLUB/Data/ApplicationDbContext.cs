using CLUB.Areas.Auth.Models;
using CLUB.Areas.Employee.Models;
using CLUB.Data.Entity;
using CLUB.Data.Entity.Auth;
using CLUB.Data.Entity.Bulk;
using CLUB.Data.Entity.Employee;
using CLUB.Data.Entity.Master;
using CLUB.Data.Entity.Navbar;
using CLUB.Models.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor _httpContextAccessor)
            : base(options)
        {
            this._httpContextAccessor = _httpContextAccessor;
        }

        #region Master Data

        //Main Entity
        public DbSet<ActivityStatus> activityStatuses { get; set; }
        public DbSet<Award> awards { get; set; }
        public DbSet<BookCategory> bookCategories { get; set; }
        public DbSet<Cadre> cadres { get; set; }
        public DbSet<Country> countries { get; set; }
        public DbSet<Department> departments { get; set; }
        public DbSet<Designation> designations { get; set; }
        public DbSet<Division> divisions { get; set; }
        public DbSet<District> districts { get; set; }
        public DbSet<EmployeeType> employeeTypes { get; set; }
        public DbSet<Language> languages { get; set; }
        public DbSet<Membership> memberships { get; set; }
        public DbSet<MunicipilityLocation> municipilityLocations { get; set; }
        public DbSet<Occupation> occupations { get; set; }
        public DbSet<Relation> relations { get; set; }
        public DbSet<Religion> religions { get; set; }
        public DbSet<SalaryGrade> salaryGrades { get; set; }
        public DbSet<ServiceStatus> serviceStatuses { get; set; }
        public DbSet<Vehicle> vehicles { get; set; }
        public DbSet<Thana> thanas { get; set; }
        public DbSet<TrainingCategory> trainingCategories { get; set; }
        public DbSet<TrainingInstitute> trainingInstitutes { get; set; }
        public DbSet<TravelPurpose> travelPurposes { get; set; }
        public DbSet<Result> results { get; set; }
        public DbSet<Organization> organizations { get; set; }
        public DbSet<LevelofEducation> levelofEducations { get; set; }
        public DbSet<Subject> subjects { get; set; }
        public DbSet<Degree> degrees { get; set; }
        public DbSet<RelDegreeSubject> relDegreeSubjects { get; set; }
        public DbSet<Holiday> holidays { get; set; }
        public DbSet<NoticeType> noticeTypes { get; set; }
        public DbSet<EventCategory> eventCategories { get; set; }

        public DbSet<CourseTitle> courseTitles { get; set; }
        public DbSet<Year> years { get; set; }
        public DbSet<SpacialSkill> spacialSkills { get; set; }
        
        public DbSet<ParticipantType> participantTypes { get; set; } // For Participant Type
        public DbSet<ParticipantHead> participantHeads { get; set; } // For Pricing Participant Head
                                                                     //--------------------------------------------------------------------------
                                                                     //From SP Or SQl

        #endregion

        #region Employee Data
        //Main Entity

        public DbSet<EmployeeInfo> employeeInfos { get; set; }
        public DbSet<Address> addresses { get; set; }
        public DbSet<EmployeeAward> employeeAwards { get; set; }
        public DbSet<Children> childrens { get; set; }
        public DbSet<DrivingLicense> drivingLicenses { get; set; }
        public DbSet<EducationalQualification> educationalQualifications { get; set; }
        public DbSet<EmployeeLanguage> employeeLanguages { get; set; }
        public DbSet<EmployeeMembership> employeeMemberships { get; set; }
        public DbSet<PassportDetails> passportDetails { get; set; }
        public DbSet<Photograph> photographs { get; set; }
        public DbSet<Publication> publications { get; set; }
        public DbSet<Spouse> spouses { get; set; }
        public DbSet<TransferLog> transferLogs { get; set; }
        public DbSet<TraningLog> traningLogs { get; set; }

        //--------------------------------------------------------------------------
        //From SP Or SQl
        public DbQuery<EmployeeWithDesignationVM> employeeWithDesignations { get; set; }

        #endregion
        
        #region Bulk
        public DbSet<MemberGroup> memberGroups { get; set; }
        public DbSet<RlnMemberGroup> rlnMemberGroups { get; set; }
        public DbSet<BulkHistory> bulkHistories { get; set; }

        public DbSet<Receiver> receivers { get; set; }
        public DbSet<ReceiverGroup> receiverGroups { get; set; }
        public DbSet<RlnReceiverGroup> rlnreceiverGroups { get; set; }
        public DbSet<SendHistory> sendHistorys { get; set; }
        #endregion

        #region Settings Configs
        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            AddTimestamps();
            return await base.SaveChangesAsync();
        }

        private void AddTimestamps()
        {

            var entities = ChangeTracker.Entries().Where(x => x.Entity is Base && (x.State == EntityState.Added || x.State == EntityState.Modified));

            var currentUsername = !string.IsNullOrEmpty(_httpContextAccessor?.HttpContext?.User?.Identity?.Name)
                ? _httpContextAccessor.HttpContext.User.Identity.Name
                : "Anonymous";

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((Base)entity.Entity).createdAt = DateTime.Now;
                    ((Base)entity.Entity).createdBy = currentUsername;
                }
                else
                {
                    entity.Property("createdAt").IsModified = false;
                    entity.Property("createdBy").IsModified = false;
                    ((Base)entity.Entity).updatedAt = DateTime.Now;
                    ((Base)entity.Entity).updatedBy = currentUsername;
                }
            }
        }
        #endregion
        
        #region proxy
        public DbQuery<GetAllUserViewModel> getAllUserViewModels { get; set; }
        public DbQuery<GetAllUserViewNewModel> getAllUserViewNewModels { get; set; }

        #endregion

        #region settings
        public DbSet<UserOTPLog> userOTPLogs { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<UserBackup> userBackups { get; set; }
        public DbSet<Navbars> navbars { get; set; }
        public DbSet<UserAccessPage> userAccessPages { get; set; }
        public DbSet<ModuleAccessPage> moduleAccessPages { get; set; }
        public DbSet<UserLogHistory> userLogHistories { get; set; }
        public DbSet<DbChangeHistory> DbChangeHistories { get; set; }
        public DbSet<Module> modules { get; set; }

        public DbQuery<UserAccessPageViewModel> userAccessPageViewModels { get; set; }
        public DbQuery<NavbarViewModel> navbarViewModels { get; set; }
        public DbQuery<NavbarWithRoleVm> navbarWithRoleVms { get; set; }
        #endregion

    }
}
