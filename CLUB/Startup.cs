using DinkToPdf;
using DinkToPdf.Contracts;
using CLUB.Data;
using CLUB.Data.Entity;
using CLUB.Models;
using CLUB.Services.Auth;
using CLUB.Services.Auth.Interfaces;
using CLUB.Services.Employee;

using CLUB.Services.jwt;
using CLUB.Services.jwt.Interfaces;
using CLUB.Services.MasterData;
using CLUB.Services.MasterData.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using CLUB.Services.Bulk.Interface;
using CLUB.Services.Bulk;
using CLUB.Services.Channel.Interfaces;
using CLUB.Services.Channel;
using Club.Services.MasterData.Interfaces;
using Club.Services.MasterData;
using CLUB.Services.Navbar.Interface;
using CLUB.Services.Navbar;
using CLUB.Services.Employee.Interfaces;

namespace CLUB
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            #region MasterData DI
            services.AddScoped<ITypeService, EmployeeTypeService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<ILevelofEducationService, LevelofEducationService>();
            services.AddScoped<IResultService, ResultService>();
            services.AddScoped<ISubjectService, SubjectService>();
            services.AddScoped<IDegreeService, DegreeService>();
            services.AddScoped<IOrganizationService, OrganizationService>();
            services.AddScoped<ISalaryGradeService, SalaryGradeService>();
            services.AddScoped<ITrainingService, TrainingService>();
            services.AddScoped<ITravelService, TravelVehicleService>();
            services.AddScoped<IRelationService, RelationService>();
            services.AddScoped<IMembershipLanguageService, MembershipLanguageService>();
            services.AddScoped<IOccupationCadreService, OccupationCadreService>();
            services.AddScoped<IBookAwardService, BookAwardService>();
            services.AddScoped<IDesignationDepartmentService, DesignationDepartmentService>();
            services.AddScoped<IReligionMunicipalityService, ReligionMunicipalityService>();
            services.AddScoped<IStatusService, StatusService>();
            services.AddScoped<IHolidayService, HolidayService>();
            services.AddScoped<IRelDegreeSubjectService, RelDegreeSubjectService>();
            services.AddScoped<IYearCourseTitleService, YearCourseTitleService>();
            services.AddScoped<ISpacialSkilService, SpacialSkilService>();
            services.AddScoped<INoticeTypeService, NoticeTypeService>();
            services.AddScoped<IEventCategoryService, EventCategoryService>();
            services.AddScoped<IParticipantTypeService, ParticipantTypeService>();
            services.AddScoped<IParticipantHeadService, ParticipantHeadService>();
            #endregion

            #region Employee DI
            services.AddScoped<ISpouseChildrenService, SpouseChildrenService>();
            services.AddScoped<IPersonalInfoService, PersonalInfoService>();
            services.AddScoped<IDrivingLicenseService, DrivingLicenseInfoService>();
            services.AddScoped<IPassportInfoService, PassportInfoService>();
            services.AddScoped<IAwardPublicationLanguageService, AwardPublicationLanguageService>();
            services.AddScoped<IEmployeeInfoService, AddressEducationPhotoService>();
            services.AddScoped<IMembershipService, MembershipService>();
            services.AddScoped<IServiceHistoryService, ServiceHistoryService>();
            services.AddScoped<ITraningHistoryService, TraningHistoryService>();
            services.AddScoped<IPhotographService, PhotographService>();
            #endregion
            
            #region Bulk DI
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IRlnGroupMemberService, RlnGroupMemberService>();
            #endregion


            #region masterData
            services.AddScoped<IERPModuleService, EPRModuleService>();
            services.AddScoped<IModuleAssaign, ModuleAssaignService>();
            services.AddScoped<INavbar, NavbarService>();
            services.AddScoped<IPageAssaign, PageAssaignService>();
            services.AddScoped<IdbChangeHistory, dbChangeHistoryService>();
            services.AddScoped<IModuleAssignService, ModuleAssignService>();
            services.AddScoped<INavbarService, NavbarServices>();
            services.AddScoped<IPageAssignService, PageAssignService>();
            services.AddScoped<IUserInfos, UserInfoService>();
            #endregion

            #region Database Settings
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            #endregion

            #region Auth JWT
            services.AddSingleton<IJwtFactoryService, JwtFactoryService>();
            var jwtAppsettingsOptions = Configuration.GetSection(nameof(JwtIssuerOptions));

            SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtAppsettingsOptions["SecreatKey"]));

            services.Configure<JwtIssuerOptions>(Options =>
            {
                Options.Issuer = jwtAppsettingsOptions[nameof(JwtIssuerOptions.Issuer)];
                Options.Audience = jwtAppsettingsOptions[nameof(JwtIssuerOptions.Audience)];
                Options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppsettingsOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppsettingsOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
            #endregion

            #region Auth Related Settings
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 2;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromHours(1);

                options.LoginPath = "/Auth/Account/Login";
                options.AccessDeniedPath = "/Auth/Account/AccessDenied";
                options.SlidingExpiration = true;
            });


            services.AddAuthentication().AddJwtBearer(configureOptions =>
                {
                    configureOptions.ClaimsIssuer = jwtAppsettingsOptions[nameof(JwtIssuerOptions.Issuer)];
                    configureOptions.TokenValidationParameters = tokenValidationParameters;
                    configureOptions.SaveToken = true;
                });

            services.AddAuthorization(Options =>
            {
                Options.AddPolicy("ApiUserSimple", policy => policy.RequireClaim("rol", "user"));
                Options.AddPolicy("SiteAdmin", policy => policy.RequireClaim("rol", "admin"));
            });

            #endregion

            #region custtomAuthSUpport
            services.AddScoped<ILoggedinUser, LoggedinUser>();
            #endregion

            #region SMSEMAIL DI
            services.AddScoped<ISMSMailService, SMSMailService>();
            #endregion
            
            #region PDF DI
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            #endregion

            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.AreaViewLocationFormats.Clear();
                options.AreaViewLocationFormats.Add("/areas/{2}/Views/{1}/{0}.cshtml");
                options.AreaViewLocationFormats.Add("/areas/{2}/Views/Shared/{0}.cshtml");
                options.AreaViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddHttpContextAccessor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                      name: "areas",
                      template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
