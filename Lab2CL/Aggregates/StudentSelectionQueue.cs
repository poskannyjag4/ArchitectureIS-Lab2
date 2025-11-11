using Lab2CL.Conracts;
using Lab2CL.Entities;
using Lab2CL.Iterators;

namespace Lab2CL.Aggregates;

public class StudentSelectionQueue : IStudentList
{
    private readonly List<Student> _students;
    private int _maxQueueSize;

    public StudentSelectionQueue(List<Student> students)
    {
        _students = students;
        _maxQueueSize = students.Count;
    }

    public int Length => _students.Count;
    public int MaxQueueSize => _maxQueueSize;
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
    }

    public void Append(Student student)
    {
        _students.Add(student);
        _maxQueueSize = _maxQueueSize < _students.Count ? _students.Count : _maxQueueSize;
    }

    public Student this[int index] => _students[index];


}