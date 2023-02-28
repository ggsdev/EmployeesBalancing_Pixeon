using Pixeon.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixeon.Interfaces
{
    public interface ITeam
    {
        string Name { get; }
        int MinMaturity { get; }
        int CurrentMaturity { get; }
        List<Employee> TeamEmployees { get; }
    }
}
