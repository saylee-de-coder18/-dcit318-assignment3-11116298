using System;
using System.IO;

public class StudentgradeApp
{
    public static void Main()
    {
        string inputPath = "students.txt";
        string outputPath = "report.txt";

        var processor = new StudentResultProcessor();

        try
        {
            var students = processor.ReadStudentsFromFile(inputPath);
            processor.WriteReportToFile(students, outputPath);
            Console.WriteLine("Report written successfully to report.txt");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Error: The input file was not found.");
        }
        catch (InvalidScoreFormatException ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        catch (MissingFieldException ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Unexpected error: " + ex.Message);
        }
    }
}
