using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Collections.Generic;
using Pixeon.Classes;

namespace Pixeon.Tests
{
    [TestFixture]
    public class LoadTests
    {
        [Test]
        public void Load_Should_Return_Correct_Data()
        {
            string pathToTeamsData = "Data/teams.csv";
            string pathToEmployeesData = "Data/employees.csv";

            (List<Team> TeamsData, List<Employee> EmployeesData) = Company.Load();

            Assert.Multiple(() =>
            {
                //Assert that data is not null.
                Assert.That(TeamsData, Is.Not.Null);
                Assert.That(EmployeesData, Is.Not.Null);
            });

            Assert.Multiple(() =>
            {
                // Assert that the number of teams and employees matches the number of lines in the data files.
                int teamLines = File.ReadAllLines(pathToTeamsData).Length;
                int employeeLines = File.ReadAllLines(pathToEmployeesData).Length;

                Assert.That(TeamsData, Has.Count.EqualTo(teamLines));
                Assert.That(EmployeesData, Has.Count.EqualTo(employeeLines));
            });

            Assert.Multiple(() =>
            {
                // Assert that the teams and employees were loaded correctly, analyzing the first Client and Employee.
                Team firstTeam = TeamsData[0];
                Employee firstEmployee = EmployeesData[0];

                Assert.That(firstTeam.Name, Is.EqualTo("Client 1"));
                Assert.That(firstTeam.MinMaturity, Is.EqualTo(3));
                Assert.That(firstEmployee.Name, Is.EqualTo("Diego"));
                Assert.That(firstEmployee.PLevel, Is.EqualTo(3));
                Assert.That(firstEmployee.BirthYear, Is.EqualTo(1991));
                Assert.That(firstEmployee.AdmissionYear, Is.EqualTo(2011));
                Assert.That(firstEmployee.LastProgressionYear, Is.EqualTo(2015));
            });
        }
    }
}
