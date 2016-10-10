using OnlinerTask.DAL.SearchModels;
using OnlinerTask.Data.DBModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlinerTask.Data.EntityMappers
{
    [NotMapped]
    public class PriceMapper : Price
    {
        public PriceMapper() : base() { }
        public PriceMapper(PriceModel model, int pricemaxid, int priceminid) : base()
        {
            this.HtmlUrl = model.HtmlUrl;
            this.Offer = new OfferMapper(model.Offers);
            this.PriceMinAmmount = new PriceAmmountMapper(model.PriceMin, priceminid);
            this.PriceMaxAmmount = new PriceAmmountMapper(model.PriceMax, pricemaxid);
        }
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
    }
}
