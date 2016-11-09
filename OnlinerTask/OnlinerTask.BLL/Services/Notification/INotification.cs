namespace OnlinerTask.BLL.Services.Notification
{
    public interface INotification
    {
        void ChangeSettings(dynamic time);

        void DeleteProduct(dynamic name);

        void AddProduct(dynamic name);

        void AddProductFromSearch(dynamic name);

        void DeleteProductFromSearch(dynamic name);
    }
}
