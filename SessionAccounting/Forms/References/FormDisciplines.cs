using SessionAccounting.Common;
using SessionAccounting.Repositories;
using Unity;

namespace SessionAccounting.Forms.References;

public class FormDisciplines : ListFormBase
{
    private readonly IDisciplineRepository _disciplineRepository;

    public FormDisciplines(IUnityContainer container, IDisciplineRepository disciplineRepository)
        : base(container, "Дисциплины", true, true)
    {
        _disciplineRepository = disciplineRepository ?? throw new ArgumentNullException(nameof(disciplineRepository));
    }

    protected override object LoadDataSource()
    {
        return _disciplineRepository.ReadDisciplines();
    }

    protected override void AddItem()
    {
        IocContainer.Resolve<FormDiscipline>().ShowDialog();
    }

    protected override void EditItem(int id)
    {
        var form = IocContainer.Resolve<FormDiscipline>();
        form.Id = id;
        form.ShowDialog();
    }

    protected override void DeleteItem(int id)
    {
        _disciplineRepository.DeleteDiscipline(id);
    }
}
