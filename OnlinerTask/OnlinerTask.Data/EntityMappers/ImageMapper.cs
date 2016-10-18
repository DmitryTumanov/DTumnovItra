using OnlinerTask.Data.SearchModels;
using OnlinerTask.Data.DataBaseModels;
using OnlinerTask.Data.EntityMappers.Interfaces;

namespace OnlinerTask.Data.EntityMappers
{
    public class ImageMapper: IDependentMapper<Image, ImageModel>
    {
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
