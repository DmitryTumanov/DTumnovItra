using OnlinerTask.Data.SearchModels;
using OnlinerTask.Data.DataBaseModels;
using OnlinerTask.Data.EntityMappers.Interfaces;

namespace OnlinerTask.Data.EntityMappers
{
    public class PriceAmmountMapper: IPriceAmmountMapper<PriceAmmount, PriceAmmountModel>
    {
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
                Amount = model.Amount,
                Currency = model.Currency
            };
        }
    }
}
