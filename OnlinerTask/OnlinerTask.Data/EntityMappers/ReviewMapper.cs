using OnlinerTask.DAL.SearchModels;
using OnlinerTask.Data.DBModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlinerTask.Data.EntityMappers
{
    [NotMapped]
    public class ReviewMapper: Review
    {
        public ReviewMapper() : base() { }
        public ReviewMapper(ReviewModel model) : base()
        {
            this.Rating = model.Rating;
            this.HtmlUrl = model.HtmlUrl;
            this.Count = model.Count;
        }

        public ReviewModel ConvertToModel(Review dbmodel)
        {
            return new ReviewModel()
            {
                Count = (int)dbmodel.Count,
                HtmlUrl = dbmodel.HtmlUrl,
                Rating = (int)dbmodel.Rating
            };
        }
    }
}
