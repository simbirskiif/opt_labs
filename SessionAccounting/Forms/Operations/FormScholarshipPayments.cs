using SessionAccounting.Common;
using SessionAccounting.Repositories;
using Unity;

namespace SessionAccounting.Forms.Operations;

public class FormScholarshipPayments : ListFormBase
{
    private readonly IScholarshipPaymentRepository _paymentRepository;

    public FormScholarshipPayments(IUnityContainer container, IScholarshipPaymentRepository paymentRepository)
        : base(container, "Выплаты стипендий", false, true)
    {
        _paymentRepository = paymentRepository ?? throw new ArgumentNullException(nameof(paymentRepository));
    }

    protected override object LoadDataSource()
    {
        return _paymentRepository.ReadScholarshipPayments();
    }

    protected override void AddItem()
    {
        IocContainer.Resolve<FormScholarshipPayment>().ShowDialog();
    }

    protected override void DeleteItem(int id)
    {
        _paymentRepository.DeleteScholarshipPayment(id);
    }
}
