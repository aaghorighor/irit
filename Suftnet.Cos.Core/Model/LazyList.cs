namespace Suftnet.Cos.Core
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Recupération d'une liste d'item de manière asynchrone
    /// </summary>
    public delegate List<T> GetLazyItemList<T>();

    /// <summary>
    /// Liste asynchrone, le remplissage est déclenché au premier appel
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class LazyList<T> : IList<T>, IEnumerable<T>
    {
        private GetLazyItemList<T> m_Query;
        private List<T> m_InnerList;

        public LazyList()
        {
            m_InnerList = new List<T>();
        }

        public LazyList(GetLazyItemList<T> query)
        {
            this.m_Query = query;
        }

        public List<T> Inner
        {
            get
            {
                if (m_InnerList == null)
                {
                    m_InnerList = m_Query.Invoke();
                }
                return m_InnerList;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public int IndexOf(T item)
        {
            return Inner.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            Inner.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            Inner.RemoveAt(index);
        }

        public T this[int index]
        {
            get { return Inner[index]; }
            set { Inner[index] = value; }
        }

        public void Add(T item)
        {
            m_InnerList = m_InnerList ?? new List<T>();
            Inner.Add(item);
        }

        public void Add(object ob)
        {
            throw new NotImplementedException("This is for serialization");
        }

        public void Clear()
        {
            if (m_InnerList != null)
            {
                Inner.Clear();
            }
        }

        public bool Contains(T item)
        {
            return Inner.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            Inner.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            return Inner.Remove(item);
        }

        public int Count
        {
            get { return Inner.Count; }
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return Inner.GetEnumerator();
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)Inner).GetEnumerator();
        }


    }
}
