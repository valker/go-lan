using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Valker.PlayOnLan.Api.Game;
using Valker.PlayOnLan.GoPlugin.Abstract;

namespace Valker.PlayOnLan.GoPlugin
{
    public interface IGroup : ICollection<ICoordinates>, IComparable<Group>
    {
        IPlayer Player { get; }
    }

    public class Group : IGroup
    {
        private List<ICoordinates> _points = new List<ICoordinates>();
        public IPlayer Player { get; private set; }

        public Group(ICoordinates point, IPlayer player)
        {
            Player = player;
            _points.Add(point);
        }

        public bool Equals(Group other)
        {
            return (Equals(Player, other.Player)) && (!_points.Except(other._points).Take(1).Any()) &&
                   (!other._points.Except(_points).Take(1).Any());
        }

        public int CompareTo(Group other)
        {
            var result = _points.Count.CompareTo(other.Count);
            if (result == 0)
            {
                for (int i = 0; i < _points.Count; ++i)
                {
                    result = _points[i].CompareTo(other._points[i]);
                    if (result != 0) break;
                }
            }

            return result;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != typeof(Group))
            {
                return false;
            }
            return Equals((Group)obj);
        }

        public override int GetHashCode()
        {
            return 0;
        }

        private Group(IGroup first, IGroup second)
        {
            if (!Equals(first.Player, second.Player))
            {
                throw new ArgumentException("Different players of parent group");
            }
            Player = first.Player;
            _points.AddRange(first);
            _points.AddRange(second);
        }

        private Group()
        {
        }

        public bool Remove(ICoordinates item)
        {
            throw new InvalidOperationException();
        }
        public Group RemovePoint(ICoordinates item)
        {
            Group grp = new Group();
            grp._points = new List<ICoordinates>(_points);
            if (!grp._points.Remove(item))
            {
                throw new ArgumentException("This point is not in this group.");
            }
            grp.Player = Player;
            return grp;
        }
        public int Count => _points.Count;

        public bool IsReadOnly => true;

        public void Add(ICoordinates point)
        {
            throw new InvalidOperationException();
        }

        public Group AddPoint(ICoordinates point)
        {
            if (_points.Contains(point))
            {
                throw new ArgumentException("This point is in this group.");
            }
            Group grp = new Group(point, Player);
            grp = new Group(this, grp);
            return grp;
        }

        public void Clear()
        {
            throw new InvalidOperationException();
        }

        public bool Contains(ICoordinates item)
        {
            return _points.Contains(item);
        }

        public void CopyTo(ICoordinates[] array, int arrayIndex)
        {
            _points.CopyTo(array, arrayIndex);
        }

        #region Implementation of IEnumerable

        public IEnumerator<ICoordinates> GetEnumerator()
        {
            return _points.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}