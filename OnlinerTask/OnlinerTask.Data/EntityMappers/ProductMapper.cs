using OnlinerTask.DAL.SearchModels;
using OnlinerTask.Data.DBModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlinerTask.Data.EntityMappers
{
    public class ProductMapper
    {
        public ProductMapper() { }

        public ProductModel ConvertToModel(Product dbmodel)
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
                Images = new ImageMapper().ConvertToModel(dbmodel.Image),
                Reviews = new ReviewMapper().ConvertToModel(dbmodel.Review),
                Prices = new PriceMapper().ConvertToModel(dbmodel.Price)
            };
        }

        public Product ConvertToModel(ProductModel model, string useremail, int pricemaxid, int priceminid)
        {
            return new Product()
            {
                UserEmail = useremail,
                Description = model.Description,
                FullName = model.FullName,
                Name = model.Name,
                HtmlUrl = model.HtmlUrl,
                ProductId = (int)model.Id,
                ProductKey = model.Key,
                ReviewUrl = model.ReviewUrl,
                Image = new ImageMapper().ConvertToModel(model.Images),
                Review = new ReviewMapper().ConvertToModel(model.Reviews),
                Price = new PriceMapper().ConvertToModel(model.Prices, pricemaxid, priceminid)
            };
        }
    }
}
