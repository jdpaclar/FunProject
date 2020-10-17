using ParcelApp.Common;

namespace ParcelApp.Business.Interface
{
    public interface IParcelClassifier
    {
        IParcel ClassifyParcelBySize(double size);
    }
}