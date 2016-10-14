using OnlinerTask.Data.SearchModels;
using OnlinerTask.Data.DataBaseModels;
using System.ComponentModel.DataAnnotations.Schema;
using OnlinerTask.Data.EntityMappers.Interfaces;

namespace OnlinerTask.Data.EntityMappers
{
    public class PriceAmmountMapper: IPriceAmmountMapper<PriceAmmount, PriceAmmountModel>
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
