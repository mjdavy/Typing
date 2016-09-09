using System;

namespace Typing.DataAccess
{
    public interface ITextSource
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
        }

        string Text
        {
            get;
        }
    }
}
