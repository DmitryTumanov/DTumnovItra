using System.Threading.Tasks;
using OnlinerTask.BLL.Services.Job;
using OnlinerTask.BLL.Services.Search;
using OnlinerTask.Data.Repository;
using OnlinerTask.Data.Responses;

namespace OnlinerTask.BLL.Services.Products.Implementations
{
    public class PersonalManager : Manager
    {
        private readonly INotification notification;
        private readonly IPersonalRepository repository;

        public PersonalManager(ISearchService searchService, IPersonalRepository repository, INotification notification) : base(searchService, repository)
        {
            this.repository = repository;
            this.notification = notification;
        }

        public override void AddNotify(string name)
        {
            notification.AddProduct(name);
        }

        public override void RemoveNotify(string name)
        {
            notification.DeleteProduct(name);
        }

        public override async Task<PersonalPageResponse> GetAllProducts(string userName)
        {
            return await repository.PersonalProductsResponse(userName);
        }
    }
}
