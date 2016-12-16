using OnlinerTask.Data.DataBaseModels;
using OnlinerTask.Data.SearchModels;

namespace OnlinerTask.Data.EntityMappers.Implementations
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
