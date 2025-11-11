namespace Lab2CL.Conracts;

public interface IStock
{
    void AddObserver(IProvider provider);
    void RemoveObserver(int index);
    void NotifyObservers(float kgDiff, float mDiff);
    void Provide(int material, float amount);
    List<IProvider> GetProviders();
}