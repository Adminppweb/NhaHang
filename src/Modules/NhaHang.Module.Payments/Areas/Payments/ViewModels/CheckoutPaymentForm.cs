using System.Collections.Generic;

namespace NhaHang.Module.Payments.Areas.Payments.ViewModels
{
    public class CheckoutPaymentForm
    {
        public IList<PaymentProviderVm> PaymentProviders { get; set; } = new List<PaymentProviderVm>();
    }
}
