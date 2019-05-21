
namespace KainLabs.CliUtilities
{
    public interface IMenuItem
    {
        string Name { get; }
        int Order { get; }
        int Key { get; set; }
        bool Execute();
    }
}
