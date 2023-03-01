using Pixeon.Classes;


namespace Pixeon.Interfaces
{
    public interface ITeam
    {
        string Name { get; }
        static int MinMaturity { get; }
        static int CurrentMaturity { get; }
        List<Employee> TeamEmployees { get; }
    }
}
