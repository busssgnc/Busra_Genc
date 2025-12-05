using System;
using System.Collections.Generic;
using System.Linq; 


public class School
{
    
    private List<Student> Students { get; set; } = new List<Student>();
    private List<Staff> StaffMembers { get; set; } = new List<Staff>();

    public int NumberOfStudents { get; private set; }
    public int NumberOfTeachers { get; private set; }
    public int NumberOfAssistants { get; private set; }

    private static readonly Random random = new Random();

    private int GetPositiveIntegerInput(string prompt)
    {
        int value;
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out value) && value > 0)
            {
                return value;
            }
            Console.WriteLine("Invalid input. Please enter a non-zero positive number.");
        }
    }

    public static void Main(string[] args)
    {
        Console.WriteLine(" **Flower School Management Program** \n");
        School flowerSchool = new School();
        flowerSchool.RunProgram();
    }
    
    public void RunProgram()
    {
        
        NumberOfStudents = GetPositiveIntegerInput("Enter the number of students: ");
        NumberOfTeachers = GetPositiveIntegerInput("Enter the number of teachers: ");
        NumberOfAssistants = GetPositiveIntegerInput("Enter the number of assistants: ");

        Console.WriteLine("\n--- School Setup ---\n");
        
        
        for (int i = 0; i < NumberOfStudents; i++)
        {
            Students.Add(new Student()); 
        }
        Console.WriteLine($" Student list populated with **{Students.Count}** students.");

        
        for (int i = 0; i < NumberOfTeachers; i++)
        {
            StaffMembers.Add(new Staff(true)); 
        }
        Console.WriteLine($" Staff list populated with **{StaffMembers.Where(s => s.CheckIfTeacher()).Count()}** teachers.");

    
        for (int i = 0; i < NumberOfAssistants; i++)
        {
            StaffMembers.Add(new Staff(false)); 
        }
        Console.WriteLine($" Staff list populated with **{StaffMembers.Where(s => !s.CheckIfTeacher()).Count()}** assistants.");
        Console.WriteLine($"  > Total staff members: **{StaffMembers.Count}**.");

        Console.WriteLine("\n--- School Year Simulation ---\n");

        
        PreGraduationPointIncreaseEvent();

        GraduationProcess(); 

       
        WageIncreaseEvent(); 
    }

    
    private void PreGraduationPointIncreaseEvent()
    {
        Console.WriteLine(" **Pre-Graduation Event: Staff attempts to give points to students.**");
        
        if (StaffMembers.Count < 5 || Students.Count == 0)
        {
            Console.WriteLine("  > Event skipped: Not enough staff or students.");
            return;
        }

        
        List<Staff> randomStaff = StaffMembers.OrderBy(x => random.Next()).Take(5).ToList();
        
        foreach (var staff in randomStaff)
        {
            
            List<Student> studentsToAssist = Students.OrderBy(x => random.Next()).Take(5).ToList();
            
            if (staff.CheckIfTeacher()) 
            {
                int pointsGiven = 0;
                foreach (var student in studentsToAssist)
                {
                    staff.GivePoint(student); 
                    pointsGiven++;
                }
                Console.WriteLine($"  - **Teacher {staff.Name}** increased the score of {pointsGiven} students.");
            }
            else
            {
                Console.WriteLine($"  - **Assistant {staff.Name}** cannot increase score as they are not a Teacher.");
            }
        }
        Console.WriteLine("---");
    }


    private void GraduationProcess()
    {
        Console.WriteLine(" **Graduation Ceremony is starting...**");

        
        foreach (var student in Students)
        {
            student.TakeTest();
            student.TakeTest();
            student.TakeTest();
        }
        
        int totalStudents = Students.Count;
        int studentsToNextGrade = 0;
        int studentsGraduatedAndRemoved = 0;

        List<Student> removedStudents = new List<Student>();
        
        
        foreach (var student in Students)
        {
            if (student.CanGraduate())
            {
                if (student.AdvanceGrade()) 
                {
                    removedStudents.Add(student);
                    studentsGraduatedAndRemoved++;
                }
                else
                {
                    studentsToNextGrade++;
                }
            }
        }

    
        foreach (var student in removedStudents)
        {
            Students.Remove(student);
        }

        
        Console.WriteLine($"  - **Total Students at Start:** {totalStudents}");
        Console.WriteLine($"  - **Students Advancing to Next Grade:** **{studentsToNextGrade}**");
        Console.WriteLine($"  - **Students Graduated and Removed:** **{studentsGraduatedAndRemoved}**");
        Console.WriteLine($"  - **Total Students Remaining:** {Students.Count}");
        Console.WriteLine("---");
    }

    
    private void WageIncreaseEvent()
    {
        Console.WriteLine(" **Year-End Wage Increase (Random 5 Staff Members)**");

        if (StaffMembers.Count == 0)
        {
            Console.WriteLine("  > Event skipped: No staff members.");
            return;
        }

        int staffToSelect = Math.Min(5, StaffMembers.Count);
        
        
        List<Staff> staffForRaise = StaffMembers.OrderBy(x => random.Next()).Take(staffToSelect).ToList();

        
        for (int i = 0; i < staffForRaise.Count; i++)
        {
            Staff staff = staffForRaise[i];
            staff.IncreaseWage();
        }
        Console.WriteLine("---");
    }
}