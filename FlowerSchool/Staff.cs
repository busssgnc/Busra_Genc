using System;
using System.Linq;

public class Staff
{
    
    public string StaffID { get; private set; }
    public string Name { get; private set; }
    public int Wage { get; private set; } 
    private bool IsTeacher { get; set; } 

    private static readonly Random randomGenerator = new Random();

    private static string GenerateID(string prefix)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return prefix + new string(Enumerable.Repeat(chars, 6)
          .Select(s => s[randomGenerator.Next(s.Length)]).ToArray());
    }

    
    public Staff(bool isTeacher)
    {
        this.IsTeacher = isTeacher;
        
        this.Wage = randomGenerator.Next(50000, 70001); 
        
        string prefix = isTeacher ? "T" : "A";
        this.StaffID = GenerateID(prefix);
        this.Name = (isTeacher ? "Teacher_" : "Assistant_") + StaffID;
    }

    
    public bool CheckIfTeacher()
    {
        return this.IsTeacher;
    }

    
    public void IncreaseWage()
    {
        int oldWage = this.Wage;
        int increaseAmount;
        string justification;

        if (this.IsTeacher)
        {
            increaseAmount = 10000;
            justification = "Teacher";
        }
        else
        {
            increaseAmount = 5000;
            justification = "Assistant";
        }

        this.Wage += increaseAmount;

        Console.WriteLine($"  - Staff **{this.Name}** ({justification}) had their wage increased.");
        Console.WriteLine($"    > Old Wage: {oldWage}TL, New Wage: {this.Wage}TL. Justification: Being a {justification}.");
    }

    
    public void GivePoint(Student student)
    {
        if (this.IsTeacher)
        {
            student.IncreaseScoreByOne(); 
        }
    }
}