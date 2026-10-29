using SessionAccounting.Common;
using SessionAccounting.Repositories;
using Unity;

namespace SessionAccounting.Forms.References;

public class FormTeachers : ListFormBase
{
    private readonly ITeacherRepository _teacherRepository;

    public FormTeachers(IUnityContainer container, ITeacherRepository teacherRepository)
        : base(container, "Преподаватели", true, true)
    {
        _teacherRepository = teacherRepository ?? throw new ArgumentNullException(nameof(teacherRepository));
    }

    protected override object LoadDataSource()
    {
        return _teacherRepository.ReadTeachers();
    }

    protected override void AddItem()
    {
        IocContainer.Resolve<FormTeacher>().ShowDialog();
    }

    protected override void EditItem(int id)
    {
        var form = IocContainer.Resolve<FormTeacher>();
        form.Id = id;
        form.ShowDialog();
    }

    protected override void DeleteItem(int id)
    {
        _teacherRepository.DeleteTeacher(id);
    }
}
