using Lab2CL.Entities;

namespace Lab2CL.Conracts;

public interface IStudentIterator
{
    Student GetNext();
    Student GetFirst();
}