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
    public class AllocateTests
    {
        [Test]
        public void Allocate_Should_Increase_CurrentMaturity_And_AddEmployees()
        {
            List<Employee> employeesData = Company.data.employeesData;

            List<Team> allocatedTeams = Company.Allocate();

            Assert.Multiple(() =>
            {
                // Assert that one team has expected maturity and team length
                Assert.That(allocatedTeams[0].TeamEmployees, Has.Count.EqualTo(2));
                Assert.That(allocatedTeams[0].TeamEmployees[0].Name, Is.EqualTo(employeesData[0].Name));
                Assert.That(allocatedTeams[0].CurrentMaturity, Is.EqualTo(6));

            });

            Assert.Multiple(() =>
            {
                // Assert that second team has expected maturity and team length
                Assert.That(allocatedTeams[1].TeamEmployees, Has.Count.EqualTo(2));
                Assert.That(allocatedTeams[1].TeamEmployees[0].Name, Is.EqualTo(employeesData[1].Name));
                Assert.That(allocatedTeams[1].TeamEmployees[1].Name, Is.EqualTo(employeesData[2].Name));
                Assert.That(allocatedTeams[1].CurrentMaturity, Is.EqualTo(employeesData[1].PLevel + employeesData[2].PLevel));
            });


            // Assert that all employees have been allocated.
            int totalEmployeesAllocated = allocatedTeams[0].TeamEmployees.Count + allocatedTeams[1].TeamEmployees.Count + allocatedTeams[2].TeamEmployees.Count;
            int totalEmployeesInCompany = Company.data.employeesData.Count;

            Assert.That(totalEmployeesAllocated, Is.EqualTo(totalEmployeesInCompany));
        }
    }
}
