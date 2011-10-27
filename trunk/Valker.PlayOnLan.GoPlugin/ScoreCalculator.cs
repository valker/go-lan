using System;
using System.Collections.Generic;
using NUnit.Framework;
using Valker.PlayOnLan.Api;
using Valker.PlayOnLan.Utilities;

namespace Valker.PlayOnLan.GoPlugin
{    public class ScoreCalculator
    {
        public Pair<Stone, int>[] Calculate(IPosition position)
        {
            var result = new Pair<Stone, int>[2] {new Pair<Stone, int>(Stone.Black, 0),new Pair<Stone, int>(Stone.White, 0) };

            var empties = GetEmpties(position);

            var emptyClusters = GetEmptyClusters(empties);

            var ownedClusters = GetOwnedClusters(position, emptyClusters);

            foreach (var pair in ownedClusters)
            {
                int i = ((int) pair.Second) - 1;
                result[i] = new Pair<Stone, int>(result[i].First, result[i].Second + pair.First.Count);
            }

            return result;
        }

        private static List<Pair<List<Point>, Stone>> GetOwnedClusters(IPosition position, IEnumerable<List<Point>> emptyClasters)
        {
            var ownedClasters = new List<Pair<List<Point>, Stone>>();
            foreach (var claster in emptyClasters)
            {
                Stone type = Stone.None;
                foreach (var point in claster)
                {
                    foreach (var neighbour in point.Neighbours(position.Size))
                    {
                        var owner = position.GetStoneAt(neighbour);
                        if (owner != Stone.None)
                        {
                            if (type == Stone.None)
                            {
                                type = owner;
                            } else if (owner != type)
                            {
                                type = Stone.Both;
                                break;
                            }
                        }
                    }
                    if (type == Stone.Both)
                    {
                        break;
                    }

                }

                if (type != Stone.Both  && type != Stone.None)
                {
                    ownedClasters.Add(new Pair<List<Point>, Stone>(claster, type));
                }
            }
            return ownedClasters;
        }

        private static List<List<Point>> GetEmptyClusters(IEnumerable<Point> empties)
        {
            var emptyClusters = new List<List<Point>>();

            foreach (var point in empties)
            {
                // 1. find existing claster
                var acceptableClusters = new List<List<Point>>();
                foreach (var claster in emptyClusters)
                {
                    foreach (var pnt in claster)
                    {
                        if (Math.Abs(point.X - pnt.X) + Math.Abs(point.Y - pnt.Y) == 1)
                        {
                            acceptableClusters.Add(claster);
                            break;
                        }
                    }
                }

                switch(acceptableClusters.Count)
                {
                    case 0:
                        // create new claster
                        {
                            var claster = new List<Point> {point};
                            emptyClusters.Add(claster);
                        }
                        break;
                    case 1:
                        // append to existing claster
                        acceptableClusters[0].Add(point);
                        break;
                    default:
                        {
                            // sort acceptable clusters by descending of its size
                            acceptableClusters.Sort((points,list) => -points.Count.CompareTo(list.Count));
                            var cluster = acceptableClusters[0];
                            for (int i = 1; i < acceptableClusters.Count; i++)
                            {
                                cluster.AddRange(acceptableClusters[i]);
                                emptyClusters.Remove(acceptableClusters[i]);
                            }
                            cluster.Add(point);
                        }

                        break;
                }
            }
            return emptyClusters;
        }

        private static List<Point> GetEmpties(IPosition position)
        {
            var empties = new List<Point>();
            int size = position.Size;
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    var point = new Point(x, y);
                    if (position.GetStoneAt(point) == Stone.None)
                    {
                        empties.Add(point);
                    }
                }
            }

            return empties;
        }
    }

    [TestFixture]
    public class TestCalc
    {
        [Test]
        public void TestEmptyField()
        {
            var p = Position.CreateInitial(3);
            var c = new ScoreCalculator();
            var r = c.Calculate(p);
            Assert.AreEqual(0, r[0].Second);
            Assert.AreEqual(0, r[1].Second);
        }

        [Test]
        public void TestOneStoneField()
        {
            var p = Position.CreateInitial(3);
            p = p.Move(new Point(0, 0), Stone.Black).First;
            var c = new ScoreCalculator();
            var r = c.Calculate(p);
            Assert.AreEqual(8, r[0].Second);
            Assert.AreEqual(0, r[1].Second);
        }

        [Test]
        public void TestTwoStonesField()
        {
            var p = Position.CreateInitial(3);
            p = p.Move(new Point(0, 0), Stone.Black).First;
            p = p.Move(new Point(2, 2), Stone.White).First;
            var c = new ScoreCalculator();
            var r = c.Calculate(p);
            Assert.AreEqual(0, r[0].Second);
            Assert.AreEqual(0, r[1].Second);
        }

        [Test]
        public void TestClosedTerritory()
        {
            var p = Position.CreateInitial(3);
            p = p.Move(new Point(1, 0), Stone.Black).First;
            p = p.Move(new Point(1, 1), Stone.Black).First;
            p = p.Move(new Point(0, 1), Stone.Black).First;
            p = p.Move(new Point(2, 2), Stone.White).First;
            var c = new ScoreCalculator();
            var r = c.Calculate(p);
            Assert.AreEqual(1, r[0].Second);
            Assert.AreEqual(0, r[1].Second);
        }

    }
}
