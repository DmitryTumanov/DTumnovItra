using OnlinerTask.DAL.SearchModels;
using OnlinerTask.Data.DBModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlinerTask.Data.EntityMappers
{
    [NotMapped]
    public class OfferMapper : Offer
    {
        public OfferMapper() : base() { }
        public OfferMapper(OffersModel model) : base()
        {
            Count = model.Count;
        }

        public OffersModel ConvertToModel(Offer dbmodel)
        {
            return new OffersModel()
            {
                Count = (int)dbmodel.Count
            };
        }
    }
}
