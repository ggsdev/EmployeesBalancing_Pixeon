using Pixeon.Classes;

namespace Pixeon.Tests
{
    [TestFixture]
    public class TeamTests
    {
        [Test]
        public void CreateTeam_ShouldInstantiateCorrectly()
        {
            string teamName = "Test Team";
            int minMaturity = 3;

            Team team = new(teamName, minMaturity);

            Assert.Multiple(() =>
            {
                Assert.That(team.Name, Is.EqualTo(teamName));
                Assert.That(team.MinMaturity, Is.EqualTo(minMaturity));
            });

            Assert.Multiple(() =>
            {
                Assert.That(team.TeamEmployees, Is.Empty);
            });
        }

        [Test]
        public void GetExtraMaturityPoints_ShouldCalculateCorrectly()
        {
            int currentMaturity = 5;
            int minMaturity = 3;

            int extraMaturityPoints = Team.GetExtraMaturityPoints(currentMaturity, minMaturity);

            Assert.That(extraMaturityPoints, Is.EqualTo(2));
        }
    }
}
