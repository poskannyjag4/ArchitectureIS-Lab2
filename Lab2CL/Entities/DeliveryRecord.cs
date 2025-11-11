namespace Lab2CL.Entities;

public class DeliveryRecord
{
    public enum StatusEnum
    {
        Доставка,
        Отгрузка
    }
    public float DeliveryTime { get; set; }
    public StatusEnum Status { get; set; }
    
    public float Amount { get; set; }
    
}