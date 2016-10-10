using OnlinerTask.DAL.SearchModels;
using OnlinerTask.Data.DBModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlinerTask.Data.EntityMappers
{
    public class ReviewMapper
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
