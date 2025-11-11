using Lab2CL.Conracts;
using Lab2CL.Entities;
using Lab2CL.Iterators;

namespace Lab2CL.Aggregates;

public class StudentSelectionByAvgScore: IStudentList
{
    private readonly List<Student> _students;
    private  float _minAvgScore;

    public StudentSelectionByAvgScore(List<Student> students)
    {
        _students = students;
        _minAvgScore = students.Min(s=>s.AvgScore);
    }

    public int Length => _students.Count;
    public float MinAvgScore => _minAvgScore;
    public List<string> Students => _students.Select(s => s.Fio).ToList();

    public IStudentIterator CreateIterator()
    {
        return new StudentIterator(this);
    }

    public void Remove(int id)
    {
        var student = _students.FirstOrDefault(s => s.Id == id);
        if (student == null)
            throw new Exception("Такого студента не существует!");
        _students.Remove(student);
        _minAvgScore = _students.Min(s => s.AvgScore);
    }

    public void Append(Student student)
    {
        _students.Add(student);
        _minAvgScore = _students.Min(s => s.AvgScore);
    }

    public Student this[int index] => _students[index];

}