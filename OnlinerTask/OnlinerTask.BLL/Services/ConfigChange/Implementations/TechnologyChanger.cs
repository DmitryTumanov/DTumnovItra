using OnlinerTask.Data.Resources;

namespace OnlinerTask.BLL.Services.ConfigChange.Implementations
{
    public class TechnologyChanger : ITechnologyChanger
    {
        public void ChangeTechnology(string technologyName)
        {
            Configurations.NotifyTechnology = technologyName;
        }
    }
}
