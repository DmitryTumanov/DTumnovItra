using OnlinerTask.Data.SearchModels;
using OnlinerTask.Data.DataBaseModels;
using System.ComponentModel.DataAnnotations.Schema;
using OnlinerTask.Data.EntityMappers.Interfaces;

namespace OnlinerTask.Data.EntityMappers
{
    public class ReviewMapper: IDependentMapper<Review, ReviewModel>
    {
        public ReviewMapper() { }

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
                Count = (int)dbmodel.Count,
                HtmlUrl = dbmodel.HtmlUrl,
                Rating = (int)dbmodel.Rating
            };
        }
    }
}
