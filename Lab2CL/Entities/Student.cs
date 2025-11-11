using System.Text;

namespace Lab2CL.Entities;

public class Student
{
    public enum SexEnum
    {
        Man,
        Woman
    }

    public int Id { get; set; }
    public string Fio { get; set; }
    public SexEnum Sex { get; set; }
    public DateOnly BirthDate { get; set; }
    public float AvgScore { get; set; }

    public Student(int id, string fio, SexEnum sex, DateOnly birthData, float avgScore)
    {
        Id = id;
        Fio = fio;
        Sex = sex;
        BirthDate = birthData;
        AvgScore = avgScore;
    }

    public override string ToString()
    {
        return $"Номер: \t {Id}\n" +
               $"ФИО: \t {Fio}\n" +
               $"Пол: \t {Sex.ToString()}\n" +
               $"Дата рождения: \t {BirthDate}\n" +
               $"Средний балл: \t {AvgScore}";
    }
}