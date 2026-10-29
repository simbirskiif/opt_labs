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
        ApplicationConfiguration.Initialize();
        Application.Run(CreateContainer().Resolve<FormMain>());
    }

    private static IUnityContainer CreateContainer()
    {
        var container = new UnityContainer();
        container.RegisterInstance<IUnityContainer>(container);

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
