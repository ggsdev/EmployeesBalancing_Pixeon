using System.Text;
using Pixeon.Classes;


bool isInsideProgram = true;
bool hasExitedProgram = false;
bool hasLoadedData = false;
int currentYear = DateTime.Now.Year;

while (isInsideProgram)
{
    Console.WriteLine("\n< PRESS [ 1 ]LOAD || [ 2 ]ALLOCATE || [ Q ]EXIT >\n");
    string? input = Console.ReadKey(intercept: true).KeyChar.ToString();

    if (input == "q".ToLower())
    {
        hasExitedProgram = true;
        break;
    };

    switch (input)
    {
        case "1":
            if (hasLoadedData)
            {
                Console.WriteLine("You already loaded data, PRESS \"2\" to allocate employees.");
                break;
            }
            Company.Load();
            hasLoadedData = true;
            break;
        case "2":
            if (hasLoadedData)
            {
                Company.Allocate();
                isInsideProgram = false;
                break;
            }

            Console.WriteLine("You need to load data before allocating employees, PRESS \"1\".");
            break;

        default:
            Console.WriteLine("Invalid input. Please try again.");
            break;
    }
}

isInsideProgram = true;
while (isInsideProgram && !hasExitedProgram)
{
    Console.WriteLine("\n< PRESS [ 3 ]PROMOTE || [ 4 ]BALANCE || [ Q ]EXIT >\n");

    string? input = Console.ReadKey(intercept: true).KeyChar.ToString();
    if (input?.ToLower() == "q") break;

    switch (input)
    {
        case "1":
            Console.WriteLine("You already loaded data.");
            break;
        case "2":
            Console.WriteLine("You already allocated employees.");
            break;
        case "3":
            Console.WriteLine("Number of employees to be promoted: ");

            string? inputEmployeesToBePromoted = Console.ReadLine();
            int countOfEmployeesToBePromoted;

            if (int.TryParse(inputEmployeesToBePromoted, out countOfEmployeesToBePromoted))
            {
                List<Employee> promotedEmployees = Company.Promote(countOfEmployeesToBePromoted, currentYear);
                currentYear += 1;
                if (countOfEmployeesToBePromoted > promotedEmployees.Count) Console.WriteLine($"\nYou tryied to promote {countOfEmployeesToBePromoted} but only {promotedEmployees.Count} could be promoted.");
                break;
            }

            Console.WriteLine("Invalid input. Please try again.");
            break;
        case "4":
            Company.Balance();
            break;
        default:
            Console.WriteLine("Invalid input. Please try again.");
            break;
    }
}

