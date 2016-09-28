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

        public ReactiveObservableCollection(ObservableRangeCollection<T> collection) : base(collection)
        {
            this.ItemSource = collection;
        }

        public ReactiveObservableCollection(IEnumerable<T> collection) : base(collection)
        {
            var r = collection as ObservableRangeCollection<T>;
            this.ItemSource = r ?? new ObservableRangeCollection<T>(collection);
        }


        private ObservableRangeCollection<T> Items => this.ItemSource as ObservableRangeCollection<T>;

        public ObservableCollection<T> ItemSource { get; }
        
        public override void Clear()
        {
            base.Clear();
            Items.Clear();
        }

        public override void Add(T item)
        {
            base.Add(item);
            Items.Add(item);
        }

        public override void AddRange(IEnumerable<T> collection)
        {
            base.AddRange(collection);

            Items.AddRange(collection);
        }

        public override void Insert(int index, T item)
        {
            base.Insert(index, item);
            Items.Insert(index, item);
        }

        public override void InsertRange(int index, IEnumerable<T> collection)
        {
            base.InsertRange(index, collection);
            Items.InsertRange(index, collection);

        }

        public override void Move(int oldIndex, int newIndex)
        {
            base.Move(oldIndex, newIndex);
            Items.Move(oldIndex,newIndex);
        }

        public override bool Remove(T item)
        {
            Items.Remove(item);
            return base.Remove(item);
        }

        public override void RemoveAll(IEnumerable<T> items)
        {
            base.RemoveAll(items);
            Items.RemoveRange(items);
        }

        public override void RemoveAt(int index)
        {
            base.RemoveAt(index);
            Items.RemoveAt(index);
        }

        public override void RemoveRange(int index, int count)
        {
            base.RemoveRange(index, count);
            var items = new List<T>();
            for (var i = index; i < count; i++)
            {
                items.Add(this[i]);
            }

            Items.RemoveRange(items);


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

            base.Clear();
            base.AddRange(collection);
            Items.ReplaceRange(collection);

        }
    }
}
