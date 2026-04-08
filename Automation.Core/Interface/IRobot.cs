

namespace Automation.Core
{
    public interface IRobot
    {
        string Name { get; }

        IEnumerable<string> RequiredCredentials { get; }

        Task ExecuteAsync(RobotContext context, IBrowserPage page);
    }
}
