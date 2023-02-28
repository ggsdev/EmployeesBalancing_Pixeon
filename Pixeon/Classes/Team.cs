using Pixeon.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Pixeon.Classes
{
    public class Team : ITeam
    {
        public string Name { get; private set; }
        public int MinMaturity { get; set; }
        public int CurrentMaturity { get; set; }
        public List<Employee> TeamEmployees { get; set; } = new();

        public int GetExtraMaturityPoints()
        { return CurrentMaturity - MinMaturity; }
        public Team(string name, int minMaturity)
        {
            Name = name;
            MinMaturity = minMaturity;
        }
    } 
}
