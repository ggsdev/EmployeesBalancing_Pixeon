using Pixeon.Classes;

namespace Pixeon.Tests
{
    [TestFixture]
    public class EmployeeTests
    {
        [Test]
        public void CreateEmployee_ShouldInstantiateCorrectly()
        {
            string name = "John Doe";
            byte pLevel = 3;
            int birthYear = 1985;
            int admissionYear = 2010;
            int lastProgressionYear = 2021;

            Employee employee = new(name, pLevel, birthYear, admissionYear, lastProgressionYear);

            Assert.Multiple(() =>
            {
                Assert.That(employee.Name, Is.EqualTo(name));
                Assert.That(employee.PLevel, Is.EqualTo(pLevel));
                Assert.That(employee.BirthYear, Is.EqualTo(birthYear));
                Assert.That(employee.AdmissionYear, Is.EqualTo(admissionYear));
                Assert.That(employee.LastProgressionYear, Is.EqualTo(lastProgressionYear));
            });
        }

        [Test]
        public void GetTotalPromotionScore_ShouldCalculateCorrectly()
        {
            int currentYear = DateTime.Now.Year;
            int birthYear = 1990;
            int admissionYear = 2015;
            int lastProgressionYear = 2021;
            int timeWithoutProgression = currentYear - lastProgressionYear;
            int companyTime = currentYear - admissionYear;
            int ageEmployee = currentYear - birthYear;

            double totalScore = Employee.GetTotalPromotionScore(admissionYear, lastProgressionYear, birthYear);
            double expectedResult = companyTime * 2 + timeWithoutProgression * 3 + ageEmployee / 5;

            Assert.That(totalScore, Is.EqualTo(expectedResult).Within(0.1));
        }
    }
}
