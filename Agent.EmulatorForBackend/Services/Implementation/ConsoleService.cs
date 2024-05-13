using System.Runtime.InteropServices;

namespace Agent.EmulatorForBackend.Services.Implementation;
public class ConsoleService : IConsoleService
{
    [System.Security.SuppressUnmanagedCodeSecurity]
    [DllImport("kernel32", CharSet = CharSet.Auto)]
    internal static extern bool AllocConsole();

    [System.Security.SuppressUnmanagedCodeSecurity]
    [DllImport("kernel32", CharSet = CharSet.Auto)]
    internal static extern bool FreeConsole();
    public void OpenConsole()
    {
        try
        {
            var consoleTitle = "App Runtime Log";
            AllocConsole();
            Console.BackgroundColor = ConsoleColor.Black;

            Console.CursorVisible = false;
            Console.Title = consoleTitle;
        }
        catch (Exception)
        {
            throw;
        }
    }


    /// <summary>
    ///  This method is only called when exiting the program
    /// </summary>
    public void CloseConsole()
    {
        FreeConsole();
    }
}
