using System;
using System.Collections;
using System.Collections.Generic;

namespace go_engine.Data
{
    internal class GroupCollection : ICollection<Group>
    {
        public IEnumerator<Group> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(Group item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(Group item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Group[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(Group item)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }
    }
}