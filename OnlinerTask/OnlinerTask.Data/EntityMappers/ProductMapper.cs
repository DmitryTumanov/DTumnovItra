using OnlinerTask.DAL.SearchModels;
using OnlinerTask.Data.DBModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlinerTask.Data.EntityMappers
{
    [NotMapped]
    public class ProductMapper : Product
    {
        public ProductMapper() : base() { }
        public ProductMapper(ProductModel model, string UserEmail, int pricemaxid, int priceminid) : base()
        {
            this.UserEmail = UserEmail;
            this.ProductId = model.Id;
            this.ProductKey = model.Key;
            this.Name = model.Name;
            this.FullName = model.FullName;
            this.Description = model.Description;
            this.HtmlUrl = model.HtmlUrl;
            this.ReviewUrl = model.ReviewUrl;
            this.Review = new ReviewMapper(model.Reviews);
            this.Image = new ImageMapper(model.Images);
            this.Price = new PriceMapper(model.Prices, pricemaxid, priceminid);
        }

        public static ProductModel ConvertToModel(Product dbmodel)
        {
            return new ProductModel()
            {
                Description = dbmodel.Description,
                FullName = dbmodel.FullName,
                Name = dbmodel.Name,
                HtmlUrl = dbmodel.HtmlUrl,
                Id = (int)dbmodel.ProductId,
                IsChecked = true,
                Key = dbmodel.ProductKey,
                ReviewUrl = dbmodel.ReviewUrl,
                Images = ImageMapper.ConvertToModel(dbmodel.Image),
                Reviews = ReviewMapper.ConvertToModel(dbmodel.Review),
                Prices = PriceMapper.ConvertToModel(dbmodel.Price)
            };
        }
    }
}
