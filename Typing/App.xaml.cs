using System.Windows;


namespace Typing
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>

    public partial class App : System.Windows.Application
    {
        protected override void OnStartup(StartupEventArgs e) 
        { 
          base.OnStartup(e); 

            /*
          AggregateCatalog catalog = new AggregateCatalog(); 
          catalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly())); 

            Assembly.

          string path = System.IO.Path.Combine(NativeMethods.StartupPath(), "Plugins"); 

          catalog.Catalogs.Add(new DirectoryCatalog(path)); 


          _container = new CompositionContainer(catalog); 
          CompositionBatch batch = new CompositionBatch(); 
          // We've put an Import on the MainWindow so that we can add this to the 
        composition batch. 
          batch.AddPart(this); 
          _container.Compose(batch); 
          MainWindow.Show(); 
             */

       }
    }
}
