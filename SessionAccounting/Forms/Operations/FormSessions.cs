using SessionAccounting.Common;
using SessionAccounting.Repositories;
using Unity;

namespace SessionAccounting.Forms.Operations;

public class FormSessions : ListFormBase
{
    private readonly ISessionRepository _sessionRepository;

    public FormSessions(IUnityContainer container, ISessionRepository sessionRepository)
        : base(container, "Сессии", false, false)
    {
        _sessionRepository = sessionRepository ?? throw new ArgumentNullException(nameof(sessionRepository));
    }

    protected override object LoadDataSource()
    {
        return _sessionRepository.ReadSessions();
    }

    protected override void AddItem()
    {
        IocContainer.Resolve<FormSession>().ShowDialog();
    }
}
