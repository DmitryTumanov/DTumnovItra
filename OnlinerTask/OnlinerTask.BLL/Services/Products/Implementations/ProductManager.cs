using OnlinerTask.BLL.Services.Notification;
using OnlinerTask.BLL.Services.Search;
using OnlinerTask.Data.Repository;

namespace OnlinerTask.BLL.Services.Products.Implementations
{
    public class ProductManager:Manager
    {
        private readonly INotification notification;

        public ProductManager(ISearchService searchService, IRepository repository, INotification notification)
            : base(searchService, repository)
        {
            this.notification = notification;
        }

        public override void AddNotify(string name)
        {
            notification.AddProductFromSearch(name);
        }

        public override void RemoveNotify(string name)
        {
            notification.DeleteProductFromSearch(name);
        }
    }
}
