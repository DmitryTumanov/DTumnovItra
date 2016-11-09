using OnlinerTask.Data.Resources;

namespace OnlinerTask.BLL.Services.ConfigChange.Implementations
{
    public class ConfigSwapper : IConfigChanger
    {
        public void TechnologySwap(string newValue)
        {
            Configurations.NotifyTechnology = newValue;
        }
    }
}
