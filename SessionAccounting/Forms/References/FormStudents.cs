using SessionAccounting.Common;
using SessionAccounting.Repositories;
using Unity;

namespace SessionAccounting.Forms.References;

public class FormStudents : ListFormBase
{
    private readonly IStudentRepository _studentRepository;

    public FormStudents(IUnityContainer container, IStudentRepository studentRepository)
        : base(container, "Студенты", true, true)
    {
        _studentRepository = studentRepository ?? throw new ArgumentNullException(nameof(studentRepository));
    }

    protected override object LoadDataSource()
    {
        return _studentRepository.ReadStudents();
    }

    protected override void AddItem()
    {
        IocContainer.Resolve<FormStudent>().ShowDialog();
    }

    protected override void EditItem(int id)
    {
        var form = IocContainer.Resolve<FormStudent>();
        form.Id = id;
        form.ShowDialog();
    }

    protected override void DeleteItem(int id)
    {
        _studentRepository.DeleteStudent(id);
    }
}
