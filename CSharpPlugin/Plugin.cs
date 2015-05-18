using SSP.Core;

namespace CSharpPlugin
{
    public class Plugin : IPlugin
    {
        int IPlugin.Run()
        {
            return 1;
        }
    }
}
