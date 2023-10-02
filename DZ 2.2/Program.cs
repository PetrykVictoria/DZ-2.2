using System;
using System.Collections.Generic;

abstract class Worker
{
    public string Name { get; set; }
    public string Position { get; set; }
    public int WorkDay { get; set; }

    public Worker(string name, string position, int workDay)
    {
        Name = name;
        Position = position;
        WorkDay = workDay;
    }

    public virtual void FillWorkDay()
    {
        // Implementation remains empty because this is the base class.
    }
}

class Developer : Worker
{
    public Developer(string name, int workDay) : base(name, "Developer", workDay)
    {
    }

    public override void FillWorkDay()
    {
        WriteCode();
        Call();
        Relax();
    }

    private void WriteCode()
    {
        Console.WriteLine("Writing code.");
    }

    private void Call()
    {
        Console.WriteLine("Making client calls.");
    }

    private void Relax()
    {
        Console.WriteLine("Relaxing.");
    }
}

class Manager : Worker
{
    public Manager(string name, int workDay) : base(name, "Manager", workDay) { }

    private Random rand = new Random();

    public override void FillWorkDay()
    {
        int callCount1 = rand.Next(1, 11);
        for (int i = 0; i < callCount1; i++)
        {
            Call();
        }
        Relax();

        int callCount2 = rand.Next(1, 6);
        for (int i = 0; i < callCount2; i++)
        {
            Call();
        }
    }

    private void Call()
    {
        Console.WriteLine("Making client calls.");
    }

    private void Relax()
    {
        Console.WriteLine("Relaxing.");
    }
}

class Team
{
    private List<Worker> workers = new List<Worker>();
    public string TeamName { get; set; }

    public Team(string teamName)
    {
        TeamName = teamName;
    }

    public void AddWorker(Worker worker)
    {
        workers.Add(worker);
    }

    public void ShowTeamInfo()
    {
        Console.WriteLine($"Team: {TeamName}");
        foreach (var worker in workers)
        {
            Console.WriteLine($"{worker.Name}");
        }
    }

    public void ShowDetailedTeamInfo()
    {
        Console.WriteLine($"Team: {TeamName}");
        foreach (var worker in workers)
        {
            Console.WriteLine($"{worker.Name} - {worker.Position} - {worker.WorkDay}");
        }
    }
}

internal class Program
{
    static void Main(string[] args)
    {
        
        Console.Write("Enter team name: ");
        string teamName = Console.ReadLine();

        Team team = new Team(teamName);

        bool addMoreWorkers = true;

        while (addMoreWorkers)
        {
            Console.WriteLine("\nChoose the type of employee to add:");
            Console.WriteLine("1. Developer");
            Console.WriteLine("2. Manager");
            Console.WriteLine("3. Finish adding employees");

            Console.Write("Select an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter developer's name: ");
                    string developerName = Console.ReadLine();
                    int developerWorkHours;
                    while (true)
                    {
                        Console.Write("Enter work hours per day for the developer: ");
                        if (int.TryParse(Console.ReadLine(), out developerWorkHours) && developerWorkHours >= 0)
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter a non-negative integer.");
                        }
                    }
                    Developer developer = new Developer(developerName, developerWorkHours);
                    team.AddWorker(developer);
                    break;

                case "2":
                    Console.Write("Enter manager's name: ");
                    string managerName = Console.ReadLine();
                    int managerWorkHours;
                    while (true)
                    {
                        Console.Write("Enter work hours per day for the manager: ");
                        if (int.TryParse(Console.ReadLine(), out managerWorkHours) && managerWorkHours >= 0)
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter a non-negative integer.");
                        }
                    }
                    Manager manager = new Manager(managerName, managerWorkHours);
                    team.AddWorker(manager);
                    break;

                case "3":
                    addMoreWorkers = false;
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        Console.WriteLine("\nTeam Information:");
        team.ShowTeamInfo();

        Console.WriteLine("\nDetailed Team Information:");
        team.ShowDetailedTeamInfo();

        Console.ReadKey();
    }
}
