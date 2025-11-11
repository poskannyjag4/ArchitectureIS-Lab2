using Lab2CL.Conracts;
using Lab2CL.Entities;
using Lab2CL.Iterators;

namespace Lab2CL.Aggregates;

public class StudentListForReward : IStudentList
{
    private readonly List<Student> _students;

    public StudentListForReward(List<Student> students)
    {
        _students = students;
    }

    public int Length => _students.Count;

    public IStudentIterator CreateIterator()
    {
        return new StudentIterator(this);
    }

    public Student this[int index] => _students[index];

    public void Append(Student student)
    {
        _students.Add(student);
    }

    public string GiveReward(int id, double amount)
    {
        var student = _students.FirstOrDefault(s => s.Id == id);
        string result = student == null
            ? "Такого студента не существует"
            : $"Студенту {student.Fio} выдана премия в размере {amount}";
        return result;
    }
}