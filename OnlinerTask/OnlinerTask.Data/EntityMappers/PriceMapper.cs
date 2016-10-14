using OnlinerTask.Data.SearchModels;
using OnlinerTask.Data.DataBaseModels;
using OnlinerTask.Data.EntityMappers.Interfaces;

namespace OnlinerTask.Data.EntityMappers
{
    public class PriceMapper: IPriceMapper<Price, PriceModel>
    {
        private readonly IDependentMapper<Offer, OffersModel> _offerMapper;
        private readonly IPriceAmmountMapper<PriceAmmount, PriceAmmountModel> _priceAmmountMapper;

        public PriceMapper(IDependentMapper<Offer, OffersModel> offerMapper, IPriceAmmountMapper<PriceAmmount, PriceAmmountModel> priceAmmountMapper)
        {
            _offerMapper = offerMapper;
            _priceAmmountMapper = priceAmmountMapper;
        }

        public PriceModel ConvertToModel(Price dbmodel)
        {
            return new PriceModel()
            {
                HtmlUrl = dbmodel.HtmlUrl,
                Offers = _offerMapper.ConvertToModel(dbmodel.Offer),
                PriceMax = _priceAmmountMapper.ConvertToModel(dbmodel.PriceMaxAmmount),
                PriceMin = _priceAmmountMapper.ConvertToModel(dbmodel.PriceMinAmmount)
            };
        }

        public Price ConvertToModel(PriceModel model, int pricemaxid, int priceminid)
        {
            return new Price()
            {
                HtmlUrl = model.HtmlUrl,
                Offer = _offerMapper.ConvertToModel(model.Offers),
                PriceMaxAmmount = _priceAmmountMapper.ConvertToModel(model.PriceMax, pricemaxid),
                PriceMinAmmount = _priceAmmountMapper.ConvertToModel(model.PriceMin, priceminid)
            };
        }
    }
}
