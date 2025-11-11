using Lab2CL.Conracts;
using Lab2CL.Entities;

namespace Lab2CL.Iterators;

public class StudentIterator : IStudentIterator
{
    private readonly IStudentList _studentList;
    private int _curIndex;

    public StudentIterator(IStudentList studentList)
    {
        _studentList = studentList;
        _curIndex = 0;
    }

    public Student GetNext()
    {
        _curIndex++;
        if (_curIndex >= _studentList.Length)
            _curIndex = 0;
        return _studentList[_curIndex];
    }

    public Student GetFirst()
    {
        _curIndex = 0;
        return _studentList[_curIndex];
    }
}