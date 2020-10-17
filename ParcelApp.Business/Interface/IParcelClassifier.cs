using ParcelApp.Common;
using ParcelApp.Common.Interface;

namespace ParcelApp.Business.Interface
{
    public interface IParcelClassifier
    {
        IParcel ClassifyParcelBySize(double size);
    }
}