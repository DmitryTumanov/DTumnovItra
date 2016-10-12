using OnlinerTask.Data.SearchModels;
using OnlinerTask.Data.DataBaseModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlinerTask.Data.EntityMappers
{
    public class PriceAmmountMapper
    {
        public PriceAmmountMapper() { }

        public PriceAmmountModel ConvertToModel(PriceAmmount dbmodel)
        {
            return new PriceAmmountModel()
            {
                Amount = (double)dbmodel.Amount,
                Currency = dbmodel.Currency
            };
        }

        public PriceAmmount ConvertToModel(PriceAmmountModel model, int priceid)
        {
            return new PriceAmmount()
            {
                Id = priceid,
                Amount = (double)model.Amount,
                Currency = model.Currency
            };
        }
    }
}
