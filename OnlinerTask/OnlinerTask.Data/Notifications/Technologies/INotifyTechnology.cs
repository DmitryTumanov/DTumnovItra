namespace OnlinerTask.Data.Notifications.Technologies
{
    public interface INotifyTechnology
    {
        void AddProduct(dynamic name, dynamic path = null);

        void RemoveProduct(dynamic name, dynamic path = null);

        void ChangeInfo(dynamic time);
    }
}
