using FluentScheduler;

namespace OnlinerTask.WEB.TimeRegistry
{
    public class ProductRegistry : Registry
    {
        public ProductRegistry()
        {
            Schedule<ProductRefreshJob>().ToRunNow().AndEvery(30).Seconds();
        }
    }
}
