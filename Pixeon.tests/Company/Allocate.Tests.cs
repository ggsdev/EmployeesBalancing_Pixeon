using Pixeon.Classes;

namespace Pixeon.Tests
{
    [TestFixture]
    public class AllocateTests
    {
        [SetUp] public void SetUp() 
        {
            List<Team> teams = new()
            {
                new Team("Client 1", 4),
                new Team("Client 2", 7),
                new Team("Client 3", 5),
            };

            List<Employee> employees = new()
            {
                new Employee("Diego",3,1990,2012,2014),
                new Employee("Lucio",2,1980,2011,2014),
                new Employee("Lucas",4,1990,2012,2014),
                new Employee("Guilherme",5,1990,2012,2014),
                new Employee("Luciana",1,1990,2012,2014),
                new Employee("Renato",2,1990,2012,2014),
                new Employee("Alex",2,1990,2012,2014)
            };

            Company.data.employeesData = employees;
            Company.data.teamsData = teams;
        }

        [Test] 
        public void Allocate_ShouldReturnCorrectData()
        {
            List<Employee> employees = Company.data.employeesData;
            List<Team> teams = Company.data.teamsData;

            List<Team> allocatedTeams = Company.Allocate();

            Assert.Multiple(() =>
            {
                // Assert that first team has expected maturity and team length
                Assert.That(allocatedTeams[0].TeamEmployees, Has.Count.EqualTo(2));
                Assert.That(allocatedTeams[0].TeamEmployees[0].Name, Is.EqualTo(employees[0].Name));
                Assert.That(allocatedTeams[0].TeamEmployees[1].Name, Is.EqualTo(employees[1].Name));
                Assert.That(allocatedTeams[0].CurrentMaturity, Is.EqualTo(employees[0].PLevel + employees[1].PLevel));
            });

            Assert.Multiple(() =>
            {
                // Assert that second team has expected maturity and team length
                Assert.That(allocatedTeams[1].TeamEmployees, Has.Count.EqualTo(2));
                Assert.That(allocatedTeams[1].TeamEmployees[0].Name, Is.EqualTo(employees[2].Name));
                Assert.That(allocatedTeams[1].TeamEmployees[1].Name, Is.EqualTo(employees[3].Name));
                Assert.That(allocatedTeams[1].CurrentMaturity, Is.EqualTo(employees[2].PLevel + employees[3].PLevel));
            });
        }

        [Test]
        public void Allocate_ReturnsListOfTeams()
        {
            List<Team> teams = Company.Allocate();

            Assert.That(teams, Is.InstanceOf<List<Team>>());
        }

        [Test]
        public void Allocate_AllocatesAllEmployees()
        {
            List<Team> teams = Company.Allocate();
            int totalEmployees = Company.data.employeesData.Count;

            int allocatedEmployees = teams.Sum(t => t.TeamEmployees.Count);

            Assert.That(allocatedEmployees, Is.EqualTo(totalEmployees));
        }

        [Test]
        public void Allocate_AllocatesEmployeesBasedOnPLevel()
        {
            List<Team> teams = Company.Allocate();

            foreach (Team team in teams)
            {
                int pLevelSum = team.TeamEmployees.Sum(e => e.PLevel);

                Assert.That(team.CurrentMaturity, Is.GreaterThanOrEqualTo(team.MinMaturity));
            }
        }
    }
}

