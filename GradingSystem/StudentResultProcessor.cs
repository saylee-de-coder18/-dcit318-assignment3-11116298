using System;
using System.Collections.Generic;
using System.IO;

public class StudentResultProcessor
{
    public List<Student> ReadStudentsFromFile(string inputFilePath)
    {
        var students = new List<Student>();

        using (var reader = new StreamReader(inputFilePath))
        {
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                var parts = line.Split(',');

                if (parts.Length != 3)
                    throw new MissingFieldException($"Missing fields in line: {line}");

                try
                {
                    int id = int.Parse(parts[0].Trim());
                    string name = parts[1].Trim();
                    int score = int.Parse(parts[2].Trim());

                    students.Add(new Student(id, name, score));
                }
                catch (FormatException)
                {
                    throw new InvalidScoreFormatException($"Invalid score format in line: {line}");
                }
            }
        }

        return students;
    }

    public void WriteReportToFile(List<Student> students, string outputFilePath)
    {
        using (var writer = new StreamWriter(outputFilePath))
        {
            foreach (var student in students)
            {
                writer.WriteLine($"{student.FullName} (ID: {student.Id}): Score = {student.Score}, Grade = {student.GetGrade()}");
            }
        }
    }
}
