using OnlinerTask.Data.SearchModels;
using OnlinerTask.Data.DataBaseModels;
using OnlinerTask.Data.EntityMappers.Interfaces;

namespace OnlinerTask.Data.EntityMappers
{
    public class ProductMapper : IProductMapper<Product, ProductModel>
    {
        private readonly IDependentMapper<Image, ImageModel> _imageMapper;
        private readonly IDependentMapper<Review, ReviewModel> _reviewMapper;
        private readonly IPriceMapper<Price, PriceModel> _priceMapper;

        public ProductMapper(IDependentMapper<Image, ImageModel> imageMapper,
                                IDependentMapper<Review, ReviewModel> reviewMapper,
                                IPriceMapper<Price, PriceModel> priceMapper)
        {
            _imageMapper = imageMapper;
            _priceMapper = priceMapper;
            _reviewMapper = reviewMapper;
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
                Images = _imageMapper.ConvertToModel(dbmodel.Image),
                Reviews = _reviewMapper.ConvertToModel(dbmodel.Review),
                Prices = _priceMapper.ConvertToModel(dbmodel.Price)
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
                ProductId = model.Id,
                ProductKey = model.Key,
                ReviewUrl = model.ReviewUrl,
                Image = _imageMapper.ConvertToModel(model.Images),
                Review = _reviewMapper.ConvertToModel(model.Reviews),
                Price = _priceMapper.ConvertToModel(model.Prices, pricemaxid, priceminid)
            };
        }
    }
}
