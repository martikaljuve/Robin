using System.ComponentModel.Composition.Hosting;
using System.Windows;
using System.ComponentModel.Composition;

namespace Robin
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
    	public App()
    	{
    		var catalog = new DirectoryCatalog(@".\");
    		var container = new CompositionContainer(catalog);
			container.ComposeParts(this);
    	}
    }
}
