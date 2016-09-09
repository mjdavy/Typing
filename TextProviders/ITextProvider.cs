using System;
using System.Collections.Generic;

namespace Typing.DataAccess
{
    public interface ITextProvider
    {
        event EventHandler DataChanged;

        void Refresh();

        string Title
        {
            get;
        }

        string Description
        {
            get;
            set;
        }

        IEnumerable<ITextSource> Sources
        {
            get;
        }
    }
}
