using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using ReactiveUI.XamForms;

namespace ReactiveUI.XamForms
{
    public class ObservableCollectionExtended<T> : ObservableCollection<T>
    {
        public ObservableCollectionExtended() : base() { }

        public ObservableCollectionExtended(IEnumerable<T> collection) : base(collection) { }

        public ObservableCollectionExtended(List<T> list) : base(list) { }

        public void AddRange(IEnumerable<T> range)
        {
            foreach (var item in range)
            {
                Items.Add(item);
            }

            this.OnPropertyChanged(new PropertyChangedEventArgs("Count"));
            this.OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));

        }

        public void Reset(IEnumerable<T> range)
        {
            this.Items.Clear();

            AddRange(range);
        }
    }

    public class ReactiveObservableCollection<T> : ReactiveList<T>, IDisposable
    {
        private readonly CompositeDisposable disposables = new CompositeDisposable();
        private bool isAddedByRange;
        private IDisposable itemAddedDisposable;
        public ReactiveObservableCollection() : this(new ObservableCollectionExtended<T>())
        {
           
        }

        public ReactiveObservableCollection(ObservableCollection<T> collection)
        {
            this.ItemSource = collection;
            itemAddedDisposable = ItemsAdded.Subscribe(x => {
                ItemSource.Add(x);

            }).DisposeWith(disposables);

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

    
        public void AddRange(IEnumerable<T> collection, bool bulkUpdate)
        {
            var isExtendedList = (this.ItemSource is ObservableCollectionExtended<T>);

            if (isExtendedList && bulkUpdate)
            {
                using (itemAddedDisposable)
                {
                    (this.ItemSource as ObservableCollectionExtended<T>).AddRange(collection);
                }
            }

            base.AddRange(collection);

            if (isExtendedList && bulkUpdate)
            {
                itemAddedDisposable = ItemsAdded.Subscribe(x => {
                    ItemSource.Add(x);
                });
            }
        }
    }
}
