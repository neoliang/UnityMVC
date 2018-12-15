
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

public class FList<T> : IEnumerable, ICollection, IList, ICollection<T>, IEnumerable<T>, IList<T>
{
    private List<T> _innerList;
    public FList()
    {
        _innerList = new List<T>();
        Count = new Observable<int>();
    }
    public FList(IEnumerable<T> collection)
    {
        _innerList = new List<T>(collection);
        Count = new Observable<int>();
        UpdateCount();
    }
    public FList(int capacity)
    {
        _innerList = new List<T>(capacity);
    }
    public static implicit operator FList<T>(List<T> list)
    {
        return new FList<T>(list);
    }
    public T this[int index]
    {
        get { return _innerList[index]; }
        set { _innerList[index] = value; }
    }

    public IObservable<int> Count
    {
        get;
        private set;
    }
    void UpdateCount()
    {
        (Count as Observable<int>).Value = _innerList.Count;
    }

    public int Capacity
    {
        get { return _innerList.Capacity; }
        set { _innerList.Capacity = value; }
    }

    int ICollection.Count
    {
        get { return ((ICollection)_innerList).Count; }
    }

    public bool IsSynchronized
    {
        get { return ((ICollection)_innerList).IsSynchronized; }
    }

    object ICollection.SyncRoot { get { return ((ICollection)_innerList).SyncRoot; } }

    bool IList.IsFixedSize { get { return ((IList)_innerList).IsFixedSize; } }

    bool IList.IsReadOnly { get { return ((IList)_innerList).IsReadOnly; } }

    int ICollection<T>.Count { get { return ((ICollection<T>)_innerList).Count; } }

    bool ICollection<T>.IsReadOnly
    {
        get
        {
            return ((ICollection<T>)_innerList).IsReadOnly;
        }
    }

    object IList.this[int index] { get { return ((IList)_innerList)[index]; } set { ((IList)_innerList)[index] = value; } }

    public void Add(T item)
    {
        _innerList.Add(item);
        UpdateCount();
    }
    public void AddRange(IEnumerable<T> collection)
    {
        _innerList.AddRange(collection);
        UpdateCount();
    }

    public ReadOnlyCollection<T> AsReadOnly()
    {
        return _innerList.AsReadOnly();
    }
    public int BinarySearch(T item)
    {
        return _innerList.BinarySearch(item);
    }
    public int BinarySearch(T item, IComparer<T> comparer)
    {
        return _innerList.BinarySearch(item, comparer);
    }
    public int BinarySearch(int index, int count, T item, IComparer<T> comparer)
    {
        return BinarySearch(index, count, item, comparer);
    }
    public void Clear()
    {
        _innerList.Clear();
        UpdateCount();
    }
    public bool Contains(T item)
    {
        return _innerList.Contains(item);
    }
    public FList<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter)
    {
        return new FList<TOutput>(_innerList.ConvertAll(converter));
    }
    public void CopyTo(int index, T[] array, int arrayIndex, int count)
    {
        _innerList.CopyTo(index, array, arrayIndex, count);
    }
    public void CopyTo(T[] array, int arrayIndex)
    {
        _innerList.CopyTo(array, arrayIndex);
    }
    public void CopyTo(T[] array)
    {
        _innerList.CopyTo(array);
    }

    public bool Exists(Predicate<T> match)
    {
        return _innerList.Exists(match);
    }
    public T Find(Predicate<T> match)
    {
        return _innerList.Find(match);
    }
    public List<T> FindAll(Predicate<T> match)
    {
        return _innerList.FindAll(match);
    }
    public int FindIndex(Predicate<T> match)
    {
        return _innerList.FindIndex(match);
    }
    public int FindIndex(int startIndex, Predicate<T> match)
    {
        return _innerList.FindIndex(startIndex, match);
    }
    public int FindIndex(int startIndex, int count, Predicate<T> match)
    {
        return _innerList.FindIndex(startIndex, count, match);
    }
    public T FindLast(Predicate<T> match)
    {
        return _innerList.FindLast(match);
    }
    public int FindLastIndex(Predicate<T> match)
    {
        return _innerList.FindLastIndex(match);
    }
    public int FindLastIndex(int startIndex, Predicate<T> match)
    {
        return _innerList.FindLastIndex(startIndex, match);
    }
    public int FindLastIndex(int startIndex, int count, Predicate<T> match)
    {
        return _innerList.FindLastIndex(startIndex, count, match);
    }
    public void ForEach(Action<T> action)
    {
        _innerList.ForEach(action);
    }
    public Enumerator GetEnumerator()
    {
        return new Enumerator(_innerList.GetEnumerator());
    }
    public FList<T> GetRange(int index, int count)
    {
        return new FList<T>(_innerList.GetRange(index, count));
    }
    public int IndexOf(T item, int index, int count)
    {
        return _innerList.IndexOf(item, index, count);
    }
    public int IndexOf(T item, int index)
    {
        return _innerList.IndexOf(item, index);
    }
    public int IndexOf(T item)
    {
        return _innerList.IndexOf(item);
    }
    public void Insert(int index, T item)
    {
        _innerList.Insert(index, item);
        UpdateCount();
    }
    public void InsertRange(int index, IEnumerable<T> collection)
    {
        _innerList.InsertRange(index, collection);
        UpdateCount();
    }
    public int LastIndexOf(T item)
    {
        return _innerList.LastIndexOf(item);
    }
    public int LastIndexOf(T item, int index)
    {
        return _innerList.LastIndexOf(item,index);
    }
    public int LastIndexOf(T item, int index, int count)
    {
        return _innerList.LastIndexOf(item,index,count);
    }
    public bool Remove(T item)
    {
        bool removed = _innerList.Remove(item);
        if(removed)
        {
            UpdateCount();
        }
        return removed;
    }
    public int RemoveAll(Predicate<T> match)
    {
        int removedCount = _innerList.RemoveAll(match);
        if(removedCount > 0)
        {
            UpdateCount();
        }
        return removedCount;
    }
    public void RemoveAt(int index)
    {
        _innerList.RemoveAt(index);
        UpdateCount();
    }
    public void RemoveRange(int index, int count)
    {
        _innerList.RemoveRange(index, count);
        UpdateCount();
    }
    public void Reverse(int index, int count)
    {
        _innerList.Reverse(index, count);
    }
    public void Reverse()
    {
        _innerList.Reverse();
    }
    public void Sort(Comparison<T> comparison)
    {
        _innerList.Sort(comparison);
    }
    public void Sort(int index, int count, IComparer<T> comparer)
    {
        _innerList.Sort(index, count, comparer);
    }
    public void Sort()
    {
        _innerList.Sort();
    }
    public void Sort(IComparer<T> comparer)
    {
        _innerList.Sort(comparer);
    }
    public T[] ToArray()
    {
        return _innerList.ToArray();
    }
    public void TrimExcess()
    {
        _innerList.TrimExcess();
    }
    public bool TrueForAll(Predicate<T> match)
    {
        return _innerList.TrueForAll(match);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_innerList).GetEnumerator();
    }

    public void CopyTo(Array array, int index)
    {
        ((ICollection)_innerList).CopyTo(array, index);
    }

    int IList.Add(object value)
    {
        return ((IList)_innerList).Add(value);
    }

    bool IList.Contains(object value)
    {
        return ((IList)_innerList).Contains(value);
    }

    int IList.IndexOf(object value)
    {
        return ((IList)_innerList).IndexOf(value);
    }

    void IList.Insert(int index, object value)
    {
        ((IList)_innerList).Insert(index, value);
    }

    void IList.Remove(object value)
    {
        ((IList)_innerList).Remove(value);
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        return ((ICollection<T>)_innerList).GetEnumerator();
    }

    public struct Enumerator : IEnumerator, IDisposable, IEnumerator<T>
    {
        List<T>.Enumerator _innere;
        public Enumerator(List<T>.Enumerator e)
        {
            _innere = e;
        }
        public T Current
        {
            get { return _innere.Current; }
        }

        object IEnumerator.Current
        {
            get
            {
                return ((IEnumerator)_innere).Current;
            }
        }

        public void Dispose()
        {
            _innere.Dispose();
        }
        public bool MoveNext()
        {
            return _innere.MoveNext();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}

