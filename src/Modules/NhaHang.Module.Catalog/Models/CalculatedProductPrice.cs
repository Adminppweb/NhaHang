using NhaHang.Module.Core.Services;

namespace NhaHang.Module.Catalog.Models
{
    public class CalculatedProductPrice
    {
        private ICurrencyService _currencyService;
        public CalculatedProductPrice()
        {
        }

        public CalculatedProductPrice(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        public decimal Price { get; set; }

        public decimal? OldPrice { get; set; }

        public int PercentOfSaving { get; set; } = 0;

        public string PriceString => _currencyService.FormatCurrency(Price);

        public string OldPriceString => OldPrice.HasValue ? _currencyService.FormatCurrency(OldPrice.Value) : string.Empty;

    }
}
