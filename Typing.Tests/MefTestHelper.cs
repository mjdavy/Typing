using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using Typing.DataAccess;

namespace Typing.Tests
{
    public class MefTestHelper
    {
        private CompositionContainer container;

        [Import]
        public ITextProvider TextProviders
        {
            get;
            set;
        }

        public void Compose()
        {
            // An aggregate catalog that combines multiple catalogs
            var catalog = new AggregateCatalog();

            // Adds all the parts found in the same assembly as the DataProvider class
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(Typing.DataAccess.SimpleTextProvider).Assembly));

            // MJDTODO - figure out where to find extensions
            //catalog.Catalogs.Add(new DirectoryCatalog(@"C:\Work\RND\SimpleCalculator\CS\SimpleCalculator3\Extensions"));


            //Create the CompositionContainer with the parts in the catalog
            this.container = new CompositionContainer(catalog);

            //Fill the imports of this object
            try
            {
                this.container.ComposeParts(this);
            }
            catch (CompositionException compositionException)
            {
                Console.WriteLine(compositionException.ToString());
            }
        }
    }
}
