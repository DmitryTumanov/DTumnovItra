using OnlinerTask.Data.SearchModels;
using OnlinerTask.Data.DataBaseModels;
using OnlinerTask.Data.EntityMappers.Interfaces;

namespace OnlinerTask.Data.EntityMappers
{
    public class OfferMapper :IDependentMapper<Offer, OffersModel>
    {
        public OffersModel ConvertToModel(Offer dbmodel)
        {
            return new OffersModel()
            {
                Count = (int)dbmodel.Count
            };
        }

        public Offer ConvertToModel(OffersModel model)
        {
            return new Offer()
            {
                Count = model.Count
            };
        }
    }
}
