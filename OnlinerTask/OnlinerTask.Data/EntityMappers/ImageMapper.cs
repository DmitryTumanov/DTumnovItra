using OnlinerTask.DAL.SearchModels;
using OnlinerTask.Data.DBModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlinerTask.Data.EntityMappers
{
    [NotMapped]
    public class ImageMapper : Image
    {
        public ImageMapper() : base() { }
        public ImageMapper(ImageModel model):base()
        {
            Header = model.Header;
        }

        public static ImageModel ConvertToModel(Image dbmodel)
        {
            return new ImageModel()
            {
                Header = dbmodel.Header
            };
        }
    }
}
