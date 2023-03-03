using Pixeon.Utils;

namespace Pixeon.Classes
{
    public static class Company
    {
        private static (List<Team> teamsData, List<Employee> employeesData) loadedData = new();
        public static (List<Team> teamsData, List<Employee> employeesData) data = Load(); //public because i have to access for testing(probably better way to do it), should be private

        public static (List<Team>, List<Employee>) Load()
        {
            if (loadedData.teamsData != null && loadedData.employeesData != null) return loadedData;

            string pathToTeamsData = "Data/teams.csv";
            string pathToEmployeesData = "Data/employees.csv";

            List<Team> teams = new();
            List<Employee> employees = new();

            try
            {
                using (StreamReader readerEmployees = new(pathToEmployeesData))
                {
                    string? line;
                    while ((line = readerEmployees.ReadLine()) != null)
                    {
                        string[] info = line.Split(',');
                        Employee employee = new(info[0], byte.Parse(info[1]), int.Parse(info[2]), int.Parse(info[3]), int.Parse(info[4]));
                        employees.Add(employee);
                    }
                }

                using StreamReader readerTeams = new(pathToTeamsData);
                {
                    string? line;
                    while ((line = readerTeams.ReadLine()) != null) //maybe implement ReadLineAsync(??) in order to read both files at the same time
                    {
                        string[] info = line.Split(',');
                        Team team = new(info[0], int.Parse(info[1]));
                        teams.Add(team);
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"File(s) weren't found.");
            }

            if (teams.Count > 0 && employees.Count > 0) { 
                loadedData = (teams, employees);
                Print.ShowInConsole("load");
                return (teams, employees);
            }

            throw new Exception("Internal error");
        }
        public static List<Team> Allocate()
        {
            (List<Team> teams, List<Employee> employees) = data;

            int employeesToAllocate = employees.Count;
            int indexEmployee = 0;

            List<Team> outputData = new();

            for (int i = 0; i < teams.Count; i++)
            {
                Team team = teams[i];

                while (team.MinMaturity > team.CurrentMaturity || employeesToAllocate > 0 && i >= teams.Count - 1)
                {
                    Employee employee = employees[indexEmployee];

                    team.TeamEmployees.Add(employee);
                    team.CurrentMaturity += employee.PLevel;

                    indexEmployee++;
                    employeesToAllocate--;
                }

                outputData.Add(team);
            }

            Print.ShowInConsole("allocate", teams: outputData);
            return outputData;
        }

        public static List<Employee> Promote(int countOfEmployeesToBePromoted, int currentYear)
        {
            (List<Team> teams, List<Employee> employees) = data;

            List<Employee> employeesOrderedByScore = employees.OrderByDescending(employee => employee.TotalScoreForPromotion).ToList();

            List<Employee> outputDataWithPromoted = new();

            foreach (Employee employee in employeesOrderedByScore)
            {
                if (countOfEmployeesToBePromoted == 0) break;

                int timeWithoutProgression = currentYear - employee.LastProgressionYear;
                int companyTime = currentYear - employee.AdmissionYear;

                if (companyTime >= 1 && (employee.PLevel < 4 || (employee.PLevel == 4 && timeWithoutProgression > 2)))
                {
                    employee.PLevel++;
                    employee.LastProgressionYear = currentYear;
                    outputDataWithPromoted.Add(employee);

                    int j = employees.FindIndex(emp => emp == employee);
                    employees[j].PLevel = employee.PLevel;
                    employees[j].LastProgressionYear = employee.LastProgressionYear;

                    countOfEmployeesToBePromoted--;

                    Print.ShowInConsole("promote", employee);
                }
            }

            data.employeesData = employees; //updates the data with the PLevel after promotion (probably better way to do it)
            return outputDataWithPromoted;
        }

        public static void Balance()
        {
            (List<Team> teams, List<Employee> employees) = data;

            List<Team> sortedTeams = teams.OrderBy(team => team.ExtraMaturity).ToList();

            int totalOfEmployeesAllocated = teams.Sum(team => team.TeamEmployees.Count);
            int timesBalancing = totalOfEmployeesAllocated * teams.Count; // trying to reach an optimal amount of loops;

            while (timesBalancing > 0) // not optimal way of doing it
            {
                int highestExtraMaturity = sortedTeams.Last().ExtraMaturity;
                int lowestExtraMaturity = sortedTeams.First().ExtraMaturity;

                int differenceOfMaturity = highestExtraMaturity - lowestExtraMaturity;

                if (differenceOfMaturity <= 0) break;

                Team fromTeam = sortedTeams.Last();
                Team toTeam = sortedTeams.First();
                Employee transferedEmployee = fromTeam.TeamEmployees.OrderBy(employee => employee.PLevel).First();

                fromTeam.TeamEmployees.Remove(transferedEmployee);
                toTeam.TeamEmployees.Add(transferedEmployee);

                fromTeam.ExtraMaturity = fromTeam.TeamEmployees.Sum(employee => employee.PLevel) - fromTeam.MinMaturity;
                toTeam.ExtraMaturity = toTeam.TeamEmployees.Sum(employee => employee.PLevel) - toTeam.MinMaturity;

                sortedTeams = teams.OrderBy(team => team.ExtraMaturity).ToList();

                timesBalancing--;
            }

            sortedTeams.Sort((x, y) => x.Name.CompareTo(y.Name)); //resorting ordering by Client name

            Print.ShowInConsole("balance", teams: sortedTeams);
        }

        public static void BalanceGreedy()
        {
            //greedy algoritmn approach
            (List < Team > teams, List < Employee > employees) = data;

            List<Employee> availableEmployees = employees.ToList();

            foreach (Team team in teams)
            {
                while (team.ExtraMaturity > 0)
                {
                    Employee bestEmployee = null;
                    int bestMaturityDifference = int.MaxValue;

                    foreach (Employee employee in availableEmployees)
                    {
                        int maturityDifference = Math.Abs(team.ExtraMaturity + employee.PLevel - team.MinMaturity);

                        if (maturityDifference < bestMaturityDifference)
                        {
                            bestEmployee = employee;
                            bestMaturityDifference = maturityDifference;
                        }
                    }

                    if (bestEmployee == null) break;

                    team.TeamEmployees.Add(bestEmployee);
                    team.ExtraMaturity += bestEmployee.PLevel;
                    availableEmployees.Remove(bestEmployee);
                }
            }

            List<Team> sortedTeams = teams.OrderBy(team => team.Name).ToList();

            Print.ShowInConsole("balance", teams: sortedTeams);
        }
    }
}
