using OnlinerTask.DAL.SearchModels;
using OnlinerTask.Data.DBModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlinerTask.Data.EntityMappers
{
    public class ImageMapper
    {
        public ImageMapper() { }

        public ImageModel ConvertToModel(Image dbmodel)
        {
            return new ImageModel()
            {
                Header = dbmodel.Header
            };
        }

        public Image ConvertToModel(ImageModel model)
        {
            return new Image()
            {
                Header = model.Header
            };
        }
    }
}
