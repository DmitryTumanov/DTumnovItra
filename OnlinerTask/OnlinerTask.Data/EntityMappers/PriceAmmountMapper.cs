using OnlinerTask.DAL.SearchModels;
using OnlinerTask.Data.DBModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlinerTask.Data.EntityMappers
{
    [NotMapped]
    public class PriceAmmountMapper: PriceAmmount
    {
        public PriceAmmountMapper() : base() { }
        public PriceAmmountMapper(PriceAmmountModel model, int priceid):base()
        {
            this.Currency = model.Currency;
            this.Amount = model.Amount;
            this.Id = priceid;
        }

        public static PriceAmmountModel ConvertToModel(PriceAmmount dbmodel)
        {
            return new PriceAmmountModel()
            {
                Amount = (double)dbmodel.Amount,
                Currency = dbmodel.Currency
            };
        }
    }
}
