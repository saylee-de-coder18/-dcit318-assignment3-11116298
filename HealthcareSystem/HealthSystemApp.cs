using System;
using System.Collections.Generic;
using System.Linq;

public class HealthSystemApp
{
    private Repository<Patient> _patientRepo = new Repository<Patient>();
    private Repository<Prescription> _prescriptionRepo = new Repository<Prescription>();
    private Dictionary<int, List<Prescription>> _prescriptionMap = new Dictionary<int, List<Prescription>>();

    public void SeedData()
    {
        _patientRepo.Add(new Patient(1, "Alice Johnson", 30, "Female"));
        _patientRepo.Add(new Patient(2, "Bob Smith", 45, "Male"));
        _patientRepo.Add(new Patient(3, "Cynthia Grey", 27, "Female"));

        _prescriptionRepo.Add(new Prescription(1, 1, "Paracetamol", DateTime.Now.AddDays(-3)));
        _prescriptionRepo.Add(new Prescription(2, 2, "Amoxicillin", DateTime.Now.AddDays(-5)));
        _prescriptionRepo.Add(new Prescription(3, 1, "Ibuprofen", DateTime.Now.AddDays(-2)));
        _prescriptionRepo.Add(new Prescription(4, 3, "Vitamin C", DateTime.Now));
    }

    public void BuildPrescriptionMap()
    {
        _prescriptionMap = _prescriptionRepo.GetAll()
            .GroupBy(p => p.PatientId)
            .ToDictionary(g => g.Key, g => g.ToList());
    }

    public void PrintAllPatients()
    {
        foreach (var patient in _patientRepo.GetAll())
        {
            Console.WriteLine($"ID: {patient.Id}, Name: {patient.Name}, Age: {patient.Age}, Gender: {patient.Gender}");
        }
    }

    public void PrintPrescriptionsForPatient(int patientId)
    {
        if (_prescriptionMap.ContainsKey(patientId))
        {
            foreach (var pres in _prescriptionMap[patientId])
            {
                Console.WriteLine($"Prescription ID: {pres.Id}, Medication: {pres.MedicationName}, Date: {pres.DateIssued.ToShortDateString()}");
            }
        }
        else
        {
            Console.WriteLine("No prescriptions found for this patient.");
        }
    }

    public static void Main()
    {
        var app = new HealthSystemApp();
        app.SeedData();
        app.BuildPrescriptionMap();
        app.PrintAllPatients();

        Console.WriteLine("\nEnter Patient ID to view prescriptions: ");
        if (int.TryParse(Console.ReadLine(), out int pid))
        {
            app.PrintPrescriptionsForPatient(pid);
        }
    }
}
