using ParcelApp.Common;

namespace ParcelApp.Interface
{
    public interface IParcelWorker
    {
        void ExecuteOrder(ParcelOrder order);
    }
}