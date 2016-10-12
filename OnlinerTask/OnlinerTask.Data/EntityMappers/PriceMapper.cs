using OnlinerTask.Data.SearchModels;
using OnlinerTask.Data.DataBaseModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlinerTask.Data.EntityMappers
{
    public class PriceMapper
    {
        public PriceMapper() { }

        public PriceModel ConvertToModel(Price dbmodel)
        {
            return new PriceModel()
            {
                HtmlUrl = dbmodel.HtmlUrl,
                Offers = new OfferMapper().ConvertToModel(dbmodel.Offer),
                PriceMax = new PriceAmmountMapper().ConvertToModel(dbmodel.PriceMaxAmmount),
                PriceMin = new PriceAmmountMapper().ConvertToModel(dbmodel.PriceMinAmmount)
            };
        }

        public Price ConvertToModel(PriceModel model, int pricemaxid, int priceminid)
        {
            return new Price()
            {
                HtmlUrl = model.HtmlUrl,
                Offer = new OfferMapper().ConvertToModel(model.Offers),
                PriceMaxAmmount = new PriceAmmountMapper().ConvertToModel(model.PriceMax, pricemaxid),
                PriceMinAmmount = new PriceAmmountMapper().ConvertToModel(model.PriceMin, priceminid)
            };
        }
    }
}
