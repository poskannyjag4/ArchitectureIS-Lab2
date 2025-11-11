using Lab2CL.Aggregates;
using Lab2CL.Conracts;
using Lab2CL.Entities;

void WriteStudents(IStudentList students)
{
    IStudentIterator iterator = students.CreateIterator();

    for (int i = 0; i < students.Length; i++)
    {
        Console.WriteLine(iterator.GetNext().ToString());
        Console.WriteLine();
    }
}

Console.WriteLine("Демонстрация работы шаблона Итератор");
Console.WriteLine();

Console.WriteLine("Список студентов для премии");
IStudentList rewards = new StudentListForReward(new List<Student>()
{
    new Student(1, "Иванов Иван Иванович", Student.SexEnum.Man, DateOnly.FromDateTime(DateTime.Now),5),
    new Student(2, "Мариева Мария Мариеновна", Student.SexEnum.Woman, DateOnly.FromDateTime(new DateTime(2004,3,5)),4.9f),
    new Student(3, "Никитов Никита Никитович", Student.SexEnum.Man, DateOnly.FromDateTime(new DateTime(2005,7,12)),4.8f),
});
Console.WriteLine("Студенты в списке:");
WriteStudents(rewards);

var rewardAsReward = rewards as StudentListForReward;
Console.WriteLine(rewardAsReward?.GiveReward(1, 12345));

Console.WriteLine("Список студентов в порядке очереди");
IStudentList queue = new StudentSelectionQueue(new List<Student>()
{
    new Student(1, "Иванов Иван Иванович", Student.SexEnum.Man, DateOnly.FromDateTime(DateTime.Now),5),
    new Student(2, "Мариева Мария Мариеновна", Student.SexEnum.Woman, DateOnly.FromDateTime(new DateTime(2004,3,5)),4.9f),
    new Student(3, "Никитов Никита Никитович", Student.SexEnum.Man, DateOnly.FromDateTime(new DateTime(2005,7,12)),4.8f),
});
Console.WriteLine("Студенты в списке:");
WriteStudents(queue);

var queueAsQueue = queue as StudentSelectionQueue;
Console.WriteLine("Максимальная длина очереди");
Console.WriteLine(queueAsQueue.MaxQueueSize);
var std = new Student(4, "Юлиева Юлия Юлиевна", Student.SexEnum.Woman, DateOnly.FromDateTime(new DateTime(2005,7,12)),4.7f);
queueAsQueue.Append(std);
Console.WriteLine("Максимальная длина очереди");
Console.WriteLine(queueAsQueue.MaxQueueSize);
queueAsQueue.Remove(std.Id);
Console.WriteLine("Максимальная длина очереди");
Console.WriteLine(queueAsQueue.MaxQueueSize);
Console.WriteLine("Список студентов");
queueAsQueue.Students.ForEach(Console.WriteLine);

Console.WriteLine("Список студентов в порядке очереди");
IStudentList avgScore = new StudentSelectionByAvgScore(new List<Student>()
{
    new Student(1, "Иванов Иван Иванович", Student.SexEnum.Man, DateOnly.FromDateTime(DateTime.Now),5),
    new Student(2, "Мариева Мария Мариеновна", Student.SexEnum.Woman, DateOnly.FromDateTime(new DateTime(2004,3,5)),4.9f),
    new Student(3, "Никитов Никита Никитович", Student.SexEnum.Man, DateOnly.FromDateTime(new DateTime(2005,7,12)),4.8f),
});
Console.WriteLine("Студенты в списке:");
WriteStudents(avgScore);

var avgAsAvg = avgScore as StudentSelectionByAvgScore;
Console.WriteLine("Минимальный средний балл");
Console.WriteLine(avgAsAvg.MinAvgScore);
var std1 = new Student(4, "Юлиева Юлия Юлиевна", Student.SexEnum.Woman, DateOnly.FromDateTime(new DateTime(2005,7,12)),4.7f);
avgAsAvg.Append(std1);
Console.WriteLine("Минимальный средний балл");
Console.WriteLine(avgAsAvg.MinAvgScore);
avgAsAvg.Remove(std1.Id);
Console.WriteLine("Минимальный средний балл");
Console.WriteLine(avgAsAvg.MinAvgScore);

Console.WriteLine("Список студентов");
avgAsAvg.Students.ForEach(Console.WriteLine);