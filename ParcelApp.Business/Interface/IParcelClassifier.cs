using ParcelApp.Common;
using ParcelApp.Common.Interface;

namespace ParcelApp.Business.Interface
{
    public interface IParcelClassifier
    {
        ISizeParcel ClassifyParcelBySize(double size);
        IWeightParcel ClassifyHeavyParcelByWeight(double weight);
    }
}