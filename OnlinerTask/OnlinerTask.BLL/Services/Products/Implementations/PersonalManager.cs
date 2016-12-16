using System.Threading.Tasks;
using OnlinerTask.BLL.Services.Notification;
using OnlinerTask.BLL.Services.Search;
using OnlinerTask.Data.Repository;
using OnlinerTask.Data.Responses;

namespace OnlinerTask.BLL.Services.Products.Implementations
{
    public class PersonalManager : Manager
    {
        private readonly INotification notification;
        private readonly IPersonalRepository personalRepository;

        public PersonalManager(ISearchService searchService, IPersonalRepository personalRepository, IRepository repository, INotification notification)
            : base(searchService, repository)
        {
            this.personalRepository = personalRepository;
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

        public override async Task<PersonalPageResponse> GetProducts(string userName)
        {
            return await personalRepository.PersonalProductsResponse(userName);
        }
    }
}
