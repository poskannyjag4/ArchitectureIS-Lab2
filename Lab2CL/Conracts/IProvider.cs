namespace Lab2CL.Conracts;

public interface IProvider
{
    void Update(float amountKg, float kgDiff, float amountM , float mDiff);
    void Ship(float diff);

    List<string> GetData();
}