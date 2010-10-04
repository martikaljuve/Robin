using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Windows;
using System.Collections.Generic;

namespace Robin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void Compose()
        {
            var catalog = new DirectoryCatalog("Components");
            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }

		[ImportMany]
		public IList<ISensor> Sensors { get; set; }
    }
}
