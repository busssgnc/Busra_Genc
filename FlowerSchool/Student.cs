using System;
using System.Linq; 

public enum Grade
{
    First,
    Second,
    Third
}

public class Student
{
    
    public string StudentID { get; private set; }
    public string Name { get; private set; }
    public int Score { get; private set; } 
    public Grade Grade { get; private set; }

    private static readonly Random randomGenerator = new Random();

    private static string GenerateID(string prefix)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return prefix + new string(Enumerable.Repeat(chars, 6)
          .Select(s => s[randomGenerator.Next(s.Length)]).ToArray());
    }

    
    public Student()
    {
        int maxGradeIndex = Enum.GetNames(typeof(Grade)).Length;
        int randomIndex = randomGenerator.Next(0, maxGradeIndex);
        
        this.Grade = (Grade)randomIndex;

        this.StudentID = GenerateID("S");
        this.Name = "Student_" + StudentID;
        this.Score = 0; 
    }


    public void TakeTest()
    {
        int currentTestScore = randomGenerator.Next(0, 21); 
        if (currentTestScore > this.Score)
        {
            this.Score = currentTestScore; 
        }
    }
    
    
    public void IncreaseScoreByOne()
    {
        if (this.Score < 20)
        {
            this.Score++;
        }
    }

    
    public bool CanGraduate()
    {
        return this.Score >= 10;
    }
    
    
    public bool AdvanceGrade()
    {
        if (this.CanGraduate())
        {
            if (this.Grade == Grade.Third)
            {
                return true; 
            }
            else
            {
                this.Grade = (Grade)((int)this.Grade + 1); 
                return false; 
            }
        }
        return false;
    }

    public override string ToString()
    {
        return $"ID: {StudentID}, Name: {Name}, Grade: {Grade}, Score: {Score}";
    }
}