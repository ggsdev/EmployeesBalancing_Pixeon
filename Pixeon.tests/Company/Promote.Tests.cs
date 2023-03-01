using Pixeon.Classes;

namespace Pixeon.Tests
{
    [TestFixture]
    public class PromoteTests
    {
        public int currentYear = DateTime.Now.Year;

        [Test]
        public void Promote_ReturnsCorrectNumberOfEmployees()
        {
            int numberOfPromotedEmployees = 1;

            List<Employee> promotedEmployees = Company.Promote(numberOfPromotedEmployees, currentYear);
                
            //Assert that number of promoted employees given through parameter was respected
            Assert.That(promotedEmployees, Has.Count.EqualTo(numberOfPromotedEmployees));
        }

        [Test]
        public void Promote_ReturnsEmployeesWithIncreasedPLevel()
        {
            List<Employee> employeesData = Company.data.employeesData;

            int numberOfPromotedEmployees = 5;
            
            List<Employee> promotedEmployees = Company.Promote(numberOfPromotedEmployees, currentYear);

            Console.WriteLine(promotedEmployees.Count);
            Employee promotedEmployee = promotedEmployees[0];

            Employee? employeeBeforePromotion = employeesData.Find((employee) => employee.Name == promotedEmployee.Name);

            //Assert that promoted employee got his PLevel raised
            Assert.That(promotedEmployee.PLevel, Is.EqualTo(employeeBeforePromotion?.PLevel));
        }       
    }
}
