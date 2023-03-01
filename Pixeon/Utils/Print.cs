using Pixeon.Classes;

namespace Pixeon.Utils
{
    public static class Print
    {
        public static void ShowInConsole(string operation, Employee? promotedEmployee = null, List<Team>? teams = null)
        {
            switch (operation)
            {
                case "load":
                    Console.WriteLine("Loaded!");
                    break;

                case "allocate": case "balance":
                    if (teams == null) break;

                    foreach(Team team in teams)
                    {
                        Console.WriteLine($"{team.Name} - Min. Maturity {team.MinMaturity} - Current Maturity {team.TeamEmployees.Sum((emp) => emp.PLevel)}");
                        foreach (Employee employee in team.TeamEmployees)
                        {
                            Console.WriteLine($"{employee.Name} - {employee.PLevel}");
                        }
                    }
                    break;

                case "promote":
                    if (promotedEmployee == null) break;
                    Console.WriteLine($"{promotedEmployee.Name} - From: {promotedEmployee.PLevel - 1} - To: {promotedEmployee.PLevel}");               
                    break;

                default:
                    Console.WriteLine("Invalid input for ShowInConsole, try: load, allocate, promote or balance");
                    break;
            }
        }
    }
}
