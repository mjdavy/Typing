using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Typing.DataAccess
{
    interface IDataProvider
    {
        event EventHandler DataChanged;
        void Refresh();
        void Initialize();
    }
}
