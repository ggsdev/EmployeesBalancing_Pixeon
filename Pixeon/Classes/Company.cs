using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pixeon.Utils;
using NUnit.Framework.Interfaces;
using System.Runtime.InteropServices;
using System.Diagnostics.Metrics;

namespace Pixeon.Classes
{
    public static class Company
    {
        private static (List<Team> teamsData, List<Employee> employeesData) loadedData;
        public static readonly (List<Team> teamsData, List<Employee> employeesData) data = Load();

        public static (List<Team>, List<Employee>) Load()
        {
            if (loadedData.teamsData != null && loadedData.employeesData != null) return loadedData;

            string pathToTeamsData = "Data/teams.csv";
            string pathToEmployeesData = "Data/employees.csv";

            List<Team> teams = new();
            using (StreamReader reader = new(pathToTeamsData))
            {
                string? line ;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] info = line.Split(',');
                    Team team = new(info[0], int.Parse(info[1]));
                    teams.Add(team);
                }
            }

            List<Employee> employees = new();
            using (StreamReader reader = new(pathToEmployeesData))
            {
                string? line;
                while ((line = reader.ReadLine())!= null)
                {
                    string[] info = line.Split(',');
                    Employee employee = new(info[0], byte.Parse(info[1]), int.Parse(info[2]), int.Parse(info[3]), int.Parse(info[4]));
                    employees.Add(employee);
                }
            }
            loadedData = (teams, employees);

            Print.ShowInConsole("load");
            return (teams, employees);
        }
        public static List<Team> Allocate()
        {
            int indexEmployee = 0;
            int employeesToAllocate = data.employeesData.Count;

            List<Team> teams = data.teamsData;
            List<Employee> employees = data.employeesData;

            List<Team> outputData = new();

            foreach (Team team in teams)
            {
                while (team.MinMaturity > team.CurrentMaturity)
                {
                    if (employeesToAllocate == 0) break;

                    Employee employee = employees[indexEmployee];

                    team.TeamEmployees.Add(employee);
                    team.CurrentMaturity += employee.PLevel;

                    indexEmployee++;
                    employeesToAllocate--;
                }

                outputData.Add(team);
            }
 
            while (employeesToAllocate > 0)
            {
                foreach (Team team in teams)
                {
                    if (employeesToAllocate == 0) break;

                    Employee employee = employees[indexEmployee];

                    team.TeamEmployees.Add(employee);
                    team.CurrentMaturity += employee.PLevel;

                    indexEmployee++;
                    employeesToAllocate--;
                }
            }

            Print.ShowInConsole("allocate", teams: outputData);
            return outputData;
        }

        public static List<Employee> Promote(int countOfEmployeesToBePromoted, int currentYear)
        {
            List<Employee> employees = data.employeesData;
            List<Team> teams = data.teamsData;
            List<Employee> toBePromotedList = new();

            List<Employee> employeesOrderedByScore = employees.OrderByDescending(employee => employee.TotalScoreForPromotion).ToList();
            
                foreach(Employee employee in employeesOrderedByScore)
                {
                    int timeWithoutProgression = currentYear - employee.LastProgressionYear;
                    int companyTime = currentYear - employee.AdmissionYear;

                    if (countOfEmployeesToBePromoted == 0) break;

                    if (companyTime >= 1 && employee.PLevel < 4 || employee.PLevel == 4 && timeWithoutProgression >= 2)
                    {
                        employee.PLevel += 1;
                        employee.LastProgressionYear = currentYear;
                        toBePromotedList.Add(employee);
    
                        countOfEmployeesToBePromoted--;     

                        Print.ShowInConsole("promote", employee);
                    }
                }

                foreach (Team team in teams)
                    team.CurrentMaturity = team.TeamEmployees.Sum(emp => emp.PLevel);

            return toBePromotedList;
        }

        public static void Balance()
        {
            Console.WriteLine("\"BALANCE\" STILL IN DEVELOPMENT.\n");

            List<Employee> employees = data.employeesData;
            List<Team> teams = data.teamsData;

            int highestExtraMaturity = teams.Max(team => team.TeamEmployees.Sum(emp => emp.PLevel) - team.MinMaturity);
            int lowestExtraMaturity = teams.Min(team => team.TeamEmployees.Sum(emp => emp.PLevel) - team.MinMaturity);


            List<Team> sortedTeams = teams.OrderByDescending((team) => team.MinMaturity - team.CurrentMaturity).ToList();

            int difference = highestExtraMaturity - lowestExtraMaturity;

            //fila de prioridades TeamEmployees, sortear funcionarios antes com base no PLevel, algoritimo: Knapsack (algoritimo do guloso), problema da mochila**
            for (int i = 0; i < teams.Count; i++)
            {

                List<Employee> sortedEmployees = teams[i].TeamEmployees.OrderByDescending(emp => emp.PLevel).ToList();

                highestExtraMaturity = teams.Max(team => team.TeamEmployees.Sum(emp => emp.PLevel) - team.MinMaturity);
                lowestExtraMaturity = teams.Min(team => team.TeamEmployees.Sum(emp => emp.PLevel) - team.MinMaturity);

                int differenceAfter = highestExtraMaturity - lowestExtraMaturity;



                Employee transferedEmployee = teams[i].TeamEmployees[0];

                teams[i + 1].TeamEmployees.Add(transferedEmployee);
                teams[i].TeamEmployees.RemoveAt(0); //removing transfered employee   

                teams[i].CurrentMaturity = teams[i].TeamEmployees.Sum(emp => emp.PLevel);

                if (i + 1 >= teams.Count - 1) break;
                teams[i + 1].CurrentMaturity = teams[i + 1].TeamEmployees.Sum(emp => emp.PLevel);
            }

            Print.ShowInConsole("balance", teams: teams);
        }
    }
}
