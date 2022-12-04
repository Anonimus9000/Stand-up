namespace Script.Observer.Characteristics
{
public class HumorNotifyData : ICharacteristicsChangedNotifyData
{
    public int HumorCharacteristicDifference { get; }
    public HumorNotifyData(int humorCharacteristicDifference)
    {
        HumorCharacteristicDifference = humorCharacteristicDifference;
    }
}
}