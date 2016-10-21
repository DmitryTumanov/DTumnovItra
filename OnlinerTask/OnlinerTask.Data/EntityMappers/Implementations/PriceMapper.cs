using OnlinerTask.Data.DataBaseModels;
using OnlinerTask.Data.SearchModels;

namespace OnlinerTask.Data.EntityMappers.Implementations
{
    public class PriceMapper: IPriceMapper<Price, PriceModel>
    {
        private readonly IDependentMapper<Offer, OffersModel> offerMapper;
        private readonly IPriceAmmountMapper<PriceAmmount, PriceAmmountModel> priceAmmountMapper;

        public PriceMapper(IDependentMapper<Offer, OffersModel> offerMapper, IPriceAmmountMapper<PriceAmmount, PriceAmmountModel> priceAmmountMapper)
        {
            this.offerMapper = offerMapper;
            this.priceAmmountMapper = priceAmmountMapper;
        }

        public PriceModel ConvertToModel(Price dbmodel)
        {
            return new PriceModel()
            {
                HtmlUrl = dbmodel.HtmlUrl,
                Offers = offerMapper.ConvertToModel(dbmodel.Offer),
                PriceMax = priceAmmountMapper.ConvertToModel(dbmodel.PriceMaxAmmount),
                PriceMin = priceAmmountMapper.ConvertToModel(dbmodel.PriceMinAmmount)
            };
        }

        public Price ConvertToModel(PriceModel model, int pricemaxid, int priceminid)
        {
            return new Price()
            {
                HtmlUrl = model.HtmlUrl,
                Offer = offerMapper.ConvertToModel(model.Offers),
                PriceMaxId = pricemaxid,
                PriceMinId = priceminid
            };
        }
    }
}
