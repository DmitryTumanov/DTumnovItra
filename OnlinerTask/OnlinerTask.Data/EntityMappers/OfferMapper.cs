using OnlinerTask.Data.SearchModels;
using OnlinerTask.Data.DataBaseModels;
using System.ComponentModel.DataAnnotations.Schema;
using OnlinerTask.Data.EntityMappers.Interfaces;

namespace OnlinerTask.Data.EntityMappers
{
    public class OfferMapper :IDependentMapper<Offer, OffersModel>
    {
        public OfferMapper() { }

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
                Count = (int)model.Count
            };
        }
    }
}
