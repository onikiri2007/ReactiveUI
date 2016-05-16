using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactiveUI.XamForms
{
    public class ReactiveObservableCollection<T> : ReactiveList<T>
    {
        public ReactiveObservableCollection()
        {
            this.ItemSource = new ObservableCollection<T>();
            ItemsAdded.Subscribe(ItemSource.Add);
            ItemsRemoved.Subscribe((x) => ItemSource.Remove(x));
            ItemsMoved.Subscribe((x) => ItemSource.Move(x.From, x.To));
            ItemsMoved.Subscribe((x) => ItemSource.Move(x.From, x.To));
        }

        public ObservableCollection<T> ItemSource { get; private set; }
    }
}
