using SessionAccounting.Common;
using SessionAccounting.Repositories;
using Unity;

namespace SessionAccounting.Forms.References;

public class FormGroups : ListFormBase
{
    private readonly IStudentGroupRepository _groupRepository;

    public FormGroups(IUnityContainer container, IStudentGroupRepository groupRepository)
        : base(container, "Группы", true, true)
    {
        _groupRepository = groupRepository ?? throw new ArgumentNullException(nameof(groupRepository));
    }

    protected override object LoadDataSource()
    {
        return _groupRepository.ReadStudentGroups();
    }

    protected override void AddItem()
    {
        IocContainer.Resolve<FormGroup>().ShowDialog();
    }

    protected override void EditItem(int id)
    {
        var form = IocContainer.Resolve<FormGroup>();
        form.Id = id;
        form.ShowDialog();
    }

    protected override void DeleteItem(int id)
    {
        _groupRepository.DeleteStudentGroup(id);
    }
}
