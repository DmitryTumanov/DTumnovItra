using OnlinerTask.Data.SearchModels;
using OnlinerTask.Data.DataBaseModels;
using OnlinerTask.Data.EntityMappers.Interfaces;

namespace OnlinerTask.Data.EntityMappers
{
    public class ProductMapper : IProductMapper<Product, ProductModel>
    {
        private readonly IDependentMapper<Image, ImageModel> imageMapper;
        private readonly IDependentMapper<Review, ReviewModel> reviewMapper;
        private readonly IPriceMapper<Price, PriceModel> priceMapper;

        public ProductMapper(IDependentMapper<Image, ImageModel> imageMapper,
                                IDependentMapper<Review, ReviewModel> reviewMapper,
                                IPriceMapper<Price, PriceModel> priceMapper)
        {
            this.imageMapper = imageMapper;
            this.priceMapper = priceMapper;
            this.reviewMapper = reviewMapper;
        }

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
                Images = imageMapper.ConvertToModel(dbmodel.Image),
                Reviews = reviewMapper.ConvertToModel(dbmodel.Review),
                Prices = priceMapper.ConvertToModel(dbmodel.Price)
            };
        }

        public Product ConvertToModel(ProductModel model, string useremail, int pricemaxid, int priceminid)
        {
            var product = new Product()
            {
                UserEmail = useremail,
                Description = model.Description,
                FullName = model.FullName,
                Name = model.Name,
                HtmlUrl = model.HtmlUrl,
                ProductId = model.Id,
                ProductKey = model.Key,
                ReviewUrl = model.ReviewUrl,
                Image = imageMapper.ConvertToModel(model.Images),
                Review = reviewMapper.ConvertToModel(model.Reviews)
            };
            if (pricemaxid != 0 && priceminid != 0)
            {
                product.Price = priceMapper.ConvertToModel(model.Prices, pricemaxid, priceminid);
            }
            return product;
        }
    }
}
