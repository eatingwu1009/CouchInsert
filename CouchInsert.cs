using System.Windows;
using System.Reflection;
using System.Runtime.CompilerServices;
using VMS.TPS.Common.Model.API;
using CouchInsert;

// TODO: Replace the following version attributes by creating AssemblyInfo.cs. You can do this in the properties of the Visual Studio project.
[assembly: AssemblyVersion("1.0.0.12")]
[assembly: AssemblyFileVersion("1.0.0.12")]
[assembly: AssemblyInformationalVersion("1.0")]

// TODO: Uncomment the following line if the script requires write access.
[assembly: ESAPIScript(IsWriteable = true)]
namespace VMS.TPS
{
    public class Script
    {
        public Script()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void Execute(ScriptContext scriptContext, Window window, ScriptEnvironment environment)
        {
            // TODO : Add here the code that is called when the script is launched from Eclipse.
            MainView mainView = new MainView();
            MainViewModel mainViewModel = new MainViewModel(scriptContext);
            mainView.DataContext = mainViewModel;

            window.Content = mainView;
            window.Title = "CouchInsert&BODY_EatingWu🌼";
            window.SizeToContent = SizeToContent.Height;
            window.Width = 580;
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
    }
}
