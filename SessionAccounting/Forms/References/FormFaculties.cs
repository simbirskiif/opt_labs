using SessionAccounting.Common;
using SessionAccounting.Repositories;
using Unity;

namespace SessionAccounting.Forms.References;

public class FormFaculties : ListFormBase
{
    private readonly IFacultyRepository _facultyRepository;

    public FormFaculties(IUnityContainer container, IFacultyRepository facultyRepository)
        : base(container, "Факультеты", true, true)
    {
        _facultyRepository = facultyRepository ?? throw new ArgumentNullException(nameof(facultyRepository));
    }

    protected override object LoadDataSource()
    {
        return _facultyRepository.ReadFaculties();
    }

    protected override void AddItem()
    {
        IocContainer.Resolve<FormFaculty>().ShowDialog();
    }

    protected override void EditItem(int id)
    {
        var form = IocContainer.Resolve<FormFaculty>();
        form.Id = id;
        form.ShowDialog();
    }

    protected override void DeleteItem(int id)
    {
        _facultyRepository.DeleteFaculty(id);
    }
}
