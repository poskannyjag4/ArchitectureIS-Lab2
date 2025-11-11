using Lab2CL.Conracts;

namespace Lab2CL.Subjects;

public class MainStock: IStock
{
    private List<IProvider> _providers;
    public float AmountKg { get; set; }
    public float AmountM { get; set; }
    
    public void AddObserver(IProvider provider)
    {
        _providers.Add(provider);
    }

    public void RemoveObserver(int index)
    {
        _providers.RemoveAt(index);
    }

    public void NotifyObservers(float kgDiff, float mDiff)
    {
        foreach (var provider in _providers)    
        {
            provider.Update(AmountKg, kgDiff,  AmountM, mDiff);
        }
    }

    public void Provide(int material, float amount)
    {
        switch (material)
        {
            case 0:
                if(amount > AmountKg || amount < 0)
                    throw new Exception("Неверное количество!");
                AmountKg -= amount;
                NotifyObservers(amount, 0);
                break;
            case 1:
                if(amount > AmountM || amount < 0)
                    throw new Exception("Неверное количество!");
                AmountM -= amount;
                NotifyObservers(0, amount);
                break;
        }
    }

    public List<IProvider> GetProviders()
    {
        return _providers;
    }
}