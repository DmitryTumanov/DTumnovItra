using OnlinerTask.Data.SearchModels;
using OnlinerTask.Data.DataBaseModels;
using OnlinerTask.Data.EntityMappers.Interfaces;

namespace OnlinerTask.Data.EntityMappers
{
    public class ReviewMapper: IDependentMapper<Review, ReviewModel>
    {
        public ReviewModel ConvertToModel(Review dbmodel)
        {
            return new ReviewModel()
            {
                Count = (int)dbmodel.Count,
                HtmlUrl = dbmodel.HtmlUrl,
                Rating = (int)dbmodel.Rating
            };
        }

        public Review ConvertToModel(ReviewModel dbmodel)
        {
            return new Review()
            {
                Count = dbmodel.Count,
                HtmlUrl = dbmodel.HtmlUrl,
                Rating = dbmodel.Rating
            };
        }
    }
}
