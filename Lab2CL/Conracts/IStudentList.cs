using Lab2CL.Entities;

namespace Lab2CL.Conracts;

public interface IStudentList
{
    int Length { get; }

    IStudentIterator CreateIterator();

    Student this[int index] { get; }
}