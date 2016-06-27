using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;

namespace ReactiveUI.XamForms
{
    public class ReactiveObservableCollection<T> : ReactiveList<T>, IDisposable
    {
        private readonly CompositeDisposable disposables = new CompositeDisposable();
        public ReactiveObservableCollection()
        {
            this.ItemSource = new ObservableCollection<T>();
            ItemsAdded.Subscribe(ItemSource.Add).DisposeWith(disposables);
            ItemsRemoved.Subscribe((x) => ItemSource.Remove(x)).DisposeWith(disposables);
            ItemsMoved.Subscribe((x) => ItemSource.Move(x.From, x.To)).DisposeWith(disposables);
            ItemChanged.Subscribe(x => {
                var index = ItemSource.IndexOf(x.Sender);
                ItemSource[index] = x.Sender;
            }).DisposeWith(disposables);
        }

        public ObservableCollection<T> ItemSource { get; }
        public void Dispose()
        {
            if (disposables.IsDisposed) return;
            disposables.Dispose();
            disposables.Clear();
        }
    }
}
