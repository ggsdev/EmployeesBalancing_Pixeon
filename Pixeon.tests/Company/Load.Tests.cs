using Pixeon.Classes;

namespace Pixeon.Tests
{
    [TestFixture]
    public class LoadTests
    {
        private List<Team> teamsData = new();
        private List<Employee> employeesData = new();

        [SetUp]
        public void LoadTestData()
        {
            (teamsData, employeesData) = Company.Load();
        }

        [Test]
        public void Load_ShouldReturnTeamsData()
        {
            Assert.That(teamsData, Is.Not.Null);
        }

        [Test]
        public void Load_ShouldReturnEmployeesData()
        {
            Assert.That(employeesData, Is.Not.Null);
        }

        [Test]
        public void Load_ShouldMatchNumberOfLinesInTeamsDataFile()
        {
            int teamLines = File.ReadAllLines("Data/teams.csv").Length;

            Assert.That(teamsData, Has.Count.EqualTo(teamLines));
        }

        [Test]
        public void Load_ShouldMatchNumberOfLinesInEmployeesDataFile()
        {
            int employeeLines = File.ReadAllLines("Data/employees.csv").Length;

            Assert.That(employeesData, Has.Count.EqualTo(employeeLines));
        }

        [Test]
        public void Load_ShouldLoadFirstTeamCorrectly()
        {
            Team firstTeam = teamsData[0];

            Assert.Multiple(() =>
            {
                Assert.That(firstTeam.Name, Is.EqualTo("Client 1"));
                Assert.That(firstTeam.MinMaturity, Is.EqualTo(3));
            });
        }

        [Test]
        public void Load_ShouldLoadFirstEmployeeCorrectly()
        {
            Employee firstEmployee = employeesData[0];

            Assert.Multiple(() =>
            {
                Assert.That(firstEmployee.Name, Is.EqualTo("Diego"));
                Assert.That(firstEmployee.PLevel, Is.EqualTo(3));
                Assert.That(firstEmployee.BirthYear, Is.EqualTo(1991));
                Assert.That(firstEmployee.AdmissionYear, Is.EqualTo(2011));
                Assert.That(firstEmployee.LastProgressionYear, Is.EqualTo(2015));
            });
        }

        [Test]
        public void Load_ShouldHandleDataFileWithMissingFields()
        {
            string pathToEmployeesData = "Data/missing_fields_employees.csv";
            string data = "John,2,1999,2009"; // missing LastProgressionYear
            File.WriteAllText(pathToEmployeesData, data);

            Assert.DoesNotThrow(() => Company.Load());

            File.Delete(pathToEmployeesData);
        }

        [Test]
        public void Load_ShouldHandleDataFileWithExtraFields()
        {
            // Create a data file with extra fields
            string pathToTeamsData = "Data/extra_fields_teams.csv";
            string data = "Team A,3,extra"; // extra field
            File.WriteAllText(pathToTeamsData, data);

            Assert.DoesNotThrow(() => Company.Load());

            File.Delete(pathToTeamsData);
        }

        [Test]
        public void Load_ShouldHandleDataFileWithWhitespaceCharacters()
        {
            string pathToEmployeesData = "Data/whitespace_employees.csv";
            string data = "John,2,1999, 2009"; // whitespace before LastProgressionYear
            File.WriteAllText(pathToEmployeesData, data);

            Assert.DoesNotThrow(() => Company.Load());

            // Delete the data file
            File.Delete(pathToEmployeesData);
        }

        [Test]
        public void Load_ShouldHandleDataFileWithNonNumericValues()
        {
            string pathToTeamsData = "Data/non_numeric_teams.csv";
            string data = "Team A,invalid"; // non-numeric value for MinMaturity
            File.WriteAllText(pathToTeamsData, data);

            Assert.DoesNotThrow(() => Company.Load());

            File.Delete(pathToTeamsData);
        }


    }
}
