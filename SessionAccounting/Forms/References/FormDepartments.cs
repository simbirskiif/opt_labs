using SessionAccounting.Common;
using SessionAccounting.Repositories;
using Unity;

namespace SessionAccounting.Forms.References;

public class FormDepartments : ListFormBase
{
    private readonly IDepartmentRepository _departmentRepository;

    public FormDepartments(IUnityContainer container, IDepartmentRepository departmentRepository)
        : base(container, "Кафедры", true, true)
    {
        _departmentRepository = departmentRepository ?? throw new ArgumentNullException(nameof(departmentRepository));
    }

    protected override object LoadDataSource()
    {
        return _departmentRepository.ReadDepartments();
    }

    protected override void AddItem()
    {
        IocContainer.Resolve<FormDepartment>().ShowDialog();
    }

    protected override void EditItem(int id)
    {
        var form = IocContainer.Resolve<FormDepartment>();
        form.Id = id;
        form.ShowDialog();
    }

    protected override void DeleteItem(int id)
    {
        _departmentRepository.DeleteDepartment(id);
    }
}
