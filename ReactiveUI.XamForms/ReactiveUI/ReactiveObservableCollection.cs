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
    public class ReactiveObservableCollection<T> : ReactiveList<T>, IDisposable
    {
        private readonly CompositeDisposable disposables = new CompositeDisposable();
       public ReactiveObservableCollection() : this(new ObservableCollection<T>())
       {
          
       }

        public ReactiveObservableCollection(ObservableCollection<T> collection)
        {
            this.ItemSource = collection;
            ItemsAdded
                 .Subscribe(x => {

                     if (!this.Any()) return;

                     var lastItem = this.LastOrDefault();

                     if (x.Equals(lastItem))
                     {
                         ItemSource.Add(x);
                     }
                     else
                     {
                         var indexOf = this.IndexOf(x);
                         this.ItemSource.Insert(indexOf, x);
                     }

                 }).DisposeWith(disposables);

            ItemsRemoved.Subscribe((x) => { ItemSource.Remove(x); }).DisposeWith(disposables);
            ItemsMoved.Subscribe((x) => ItemSource.Move(x.From, x.To)).DisposeWith(disposables);
            
        }

        public ObservableCollection<T> ItemSource { get; }
        public void Dispose()
        {
            if (disposables.IsDisposed) return;
            disposables.Dispose();
            disposables.Clear();
        }



        
        public override void Clear()
        {
            base.Clear();
            this.ItemSource.Clear();
        }
    }
}
