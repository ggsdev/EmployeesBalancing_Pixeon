using Pixeon.Classes;

namespace Pixeon.Tests
{
    [TestFixture]
    public class AllocateTests
    {
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

