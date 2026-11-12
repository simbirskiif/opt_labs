using Microsoft.Extensions.Configuration;
using Serilog;
using SessionAccounting.Data;
using SessionAccounting.Repositories;
using SessionAccounting.Repositories.Implementations;
using Unity;
using Unity.Lifetime;

namespace SessionAccounting;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

        try
        {
            ApplicationConfiguration.Initialize();
            Application.Run(CreateContainer().Resolve<FormMain>());
        }
        catch (Exception exception)
        {
            Log.Fatal(exception, "Ошибка при запуске приложения");
            throw;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    private static IUnityContainer CreateContainer()
    {
        var container = new UnityContainer();
        container.RegisterInstance<IUnityContainer>(container);
        container.RegisterInstance<ILogger>(Log.Logger);

        container.RegisterType<DataContext>(new TransientLifetimeManager());

        container.RegisterType<IFacultyRepository, FacultyRepository>(new TransientLifetimeManager());
        container.RegisterType<IDepartmentRepository, DepartmentRepository>(new TransientLifetimeManager());
        container.RegisterType<IStudentGroupRepository, StudentGroupRepository>(new TransientLifetimeManager());
        container.RegisterType<ITeacherRepository, TeacherRepository>(new TransientLifetimeManager());
        container.RegisterType<IDisciplineRepository, DisciplineRepository>(new TransientLifetimeManager());
        container.RegisterType<IStudentRepository, StudentRepository>(new TransientLifetimeManager());
        container.RegisterType<ISessionRepository, SessionRepository>(new TransientLifetimeManager());
        container.RegisterType<IScholarshipPaymentRepository, ScholarshipPaymentRepository>(new TransientLifetimeManager());

        return container;
    }
}
