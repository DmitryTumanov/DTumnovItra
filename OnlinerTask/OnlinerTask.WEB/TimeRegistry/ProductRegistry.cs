using FluentScheduler;

namespace OnlinerTask.WEB.TimeRegistry
{
    public class ProductRegistry : Registry
    {
        public ProductRegistry()
        {
            Schedule<ProductRefreshJob>().ToRunNow().AndEvery(1).Hours();
            Schedule<SendEmailJob>().ToRunNow().AndEvery(15).Minutes();
        }
    }
}
