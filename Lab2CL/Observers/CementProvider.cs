using Lab2CL.Conracts;
using Lab2CL.Entities;

namespace Lab2CL.Observers;

public class CementProvider: IProvider
{
    private List<DeliveryRecord> _records;

    private readonly float _minAmount;
    private readonly float _trashhold;
    private readonly Random _random;

    public CementProvider(float trashhold, float minAmount)
    {
        _minAmount = minAmount;
        _trashhold = trashhold;
        _records = new List<DeliveryRecord>();
        _random = new Random();
    }
    public void Update(float amountKg, float kgDiff, float amountM , float mDiff)
    {
        if(mDiff != 0)
            Ship(mDiff);
        if(amountM < _trashhold)
            Ship(_minAmount);
    }

    public void Ship(float diff)
    {
        _records.Add(new DeliveryRecord()
        {
            Amount = diff,
            DeliveryTime = _random.Next(1000, 10000),
            Status = diff > 0 ? DeliveryRecord.StatusEnum.Отгрузка : DeliveryRecord.StatusEnum.Доставка
        });
    }

    public List<string> GetData()
    {
        var result = new List<string>();
        foreach (var record in _records)
        {
            result.Add($"{record.Status.ToString()} цемента в количестве {record.Amount}кг, время доставки - {TimeSpan.FromSeconds(record.DeliveryTime).ToString()}\n");
        }
        return result;
    }
}