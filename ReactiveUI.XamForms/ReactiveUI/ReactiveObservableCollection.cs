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
    public class ReactiveObservableCollection<T> : ReactiveList<T>
    {
        public ReactiveObservableCollection() : this(new List<T>())
       {
          
       }

        public ReactiveObservableCollection(IEnumerable<T> collection) : base(collection)
        {
            this.ItemSource = new ObservableRangeCollection<T>(collection);
        }

        public ObservableCollection<T> ItemSource { get; }
        
        public override void Clear()
        {
            base.Clear();
            this.ItemSource.Clear();
        }

        public override void Add(T item)
        {
            base.Add(item);
            this.ItemSource.Add(item);
        }

        public override void AddRange(IEnumerable<T> collection)
        {
            base.AddRange(collection);

            var r = this.ItemSource as ObservableRangeCollection<T>;

            if (r != null) {
                r.AddRange(collection);
            }
            else {
                collection.ForEach(m => this.ItemSource.Add(m));
            }
        }

        public override void Insert(int index, T item)
        {
            base.Insert(index, item);
            this.ItemSource.Insert(index, item);
        }

        public override void InsertRange(int index, IEnumerable<T> collection)
        {
            base.InsertRange(index, collection);
            collection.ForEach(m => this.ItemSource.Insert(index, m));
           
        }

        public override void Move(int oldIndex, int newIndex)
        {
            base.Move(oldIndex, newIndex);
            this.ItemSource.Move(oldIndex,newIndex);
        }

        public override bool Remove(T item)
        {
            this.ItemSource.Remove(item);
            return base.Remove(item);
        }

        public override void RemoveAll(IEnumerable<T> items)
        {
            base.RemoveAll(items);
            var r = this.ItemSource as ObservableRangeCollection<T>;

            if (r != null)
            {
                r.RemoveRange(items);
            }
            else
            {
                items.ForEach(m => this.ItemSource.Add(m));
            }
        }

        public override void RemoveAt(int index)
        {
            base.RemoveAt(index);
            this.ItemSource.RemoveAt(index);
        }

        public override void RemoveRange(int index, int count)
        {
            base.RemoveRange(index, count);
            for (var i = index; i < count; i++) {
                this.ItemSource.RemoveAt(i);
            }
        }

        public override void Reset()
        {
            base.Reset();
            this.ItemSource.Clear();
        }

      
        /// <summary> 
        /// Clears the current collection and replaces it with the specified item. 
        /// </summary> 
        public void Replace(T item)
        {
            ReplaceRange(new T[] { item });
        }

        /// <summary> 
        /// Clears the current collection and replaces it with the specified collection. 
        /// </summary> 
        public void ReplaceRange(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");

            this.Clear();
            AddRange(collection);
        }
    }
}
