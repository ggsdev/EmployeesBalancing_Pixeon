using Pixeon.Interfaces;

namespace Pixeon.Classes
{
    public class Team : ITeam
    {
        public string Name { get; private set; }
        public int MinMaturity { get; private set; }
        public int CurrentMaturity { get; set; }
        public List<Employee> TeamEmployees { get; set; } = new();
        public int ExtraMaturity { get; set; }   

        public Team(string name, int minMaturity)
        {
            Name = name;
            MinMaturity = minMaturity;
            ExtraMaturity = GetExtraMaturityPoints(CurrentMaturity, MinMaturity);
        }

        public static int GetExtraMaturityPoints(int currentMaturity, int minMaturity)
        { 
            return currentMaturity - minMaturity; 
        }
    } 
}
