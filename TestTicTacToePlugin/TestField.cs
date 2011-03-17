using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Valker.PlayOnLan.Api;
using Valker.TicTacToePlugin;

namespace TestTicTacToePlugin
{
    [TestFixture]
    public class TestField
    {
        [Test]
        public void TestEmptyField()
        {
            for (int w = 1; w < 20; w++)
            {
                var f = new Field(w);

                for (int x = 0; x < w; x++)
                {
                    for (int y = 0; y < w; y++)
                    {
                        Assert.IsTrue(f.Get(x,y) == Stone.None);
                    }
                }
            }
        }

        [Test]
        public void TestHorizontalWinSimple()
        {
            var f = new Field(3);
            Assert.IsTrue(f.Win(3) == Stone.None);
            f.Set(0, 0, Stone.Black);
            Assert.IsTrue(f.Win(3) == Stone.None);
            f.Set(1, 0, Stone.Black);
            Assert.IsTrue(f.Win(3) == Stone.None);
            f.Set(2, 0, Stone.Black);
            Assert.IsTrue(f.Win(3) == Stone.Black);
        }

        [Test]
        public void TestHorizontalFromStart()
        {
            var f = new Field(5);
            Assert.IsTrue(f.Win(2) == Stone.None);
            f.Set(0, 0, Stone.Black);
            Assert.IsTrue(f.Win(2) == Stone.None);
            f.Set(1, 0, Stone.Black);
            Assert.IsTrue(f.Win(2) == Stone.Black);
        }
        [Test]
        public void TestHorizontalFromEnd()
        {
            var f = new Field(5);
            Assert.IsTrue(f.Win(2) == Stone.None);
            f.Set(3, 0, Stone.Black);
            Assert.IsTrue(f.Win(2) == Stone.None);
            f.Set(4, 0, Stone.Black);
            Assert.IsTrue(f.Win(2) == Stone.Black);
        }


        [Test]
        public void TestVerticalWin()
        {
            var f = new Field(3);
            Assert.IsTrue(f.Win(3) == Stone.None);
            f.Set(0, 0, Stone.Black);
            Assert.IsTrue(f.Win(3) == Stone.None);
            f.Set(0, 1, Stone.Black);
            Assert.IsTrue(f.Win(3) == Stone.None);
            f.Set(0, 2, Stone.Black);
            Assert.IsTrue(f.Win(3) == Stone.Black);
        }

        [Test]
        public void TestDiagonalWin()
        {
            var f = new Field(3);
            Assert.IsTrue(f.Win(3) == Stone.None);
            f.Set(0, 0, Stone.Black);
            Assert.IsTrue(f.Win(3) == Stone.None);
            f.Set(1, 1, Stone.Black);
            Assert.IsTrue(f.Win(3) == Stone.None);
            f.Set(2, 2, Stone.Black);
            Assert.IsTrue(f.Win(3) == Stone.Black);
        }

        [Test]
        public void TestDiagonal2Win()
        {
            var f = new Field(3);
            Assert.IsTrue(f.Win(3) == Stone.None);
            f.Set(0, 2, Stone.Black);
            Assert.IsTrue(f.Win(3) == Stone.None);
            f.Set(1, 1, Stone.Black);
            Assert.IsTrue(f.Win(3) == Stone.None);
            f.Set(2, 0, Stone.Black);
            Assert.IsTrue(f.Win(3) == Stone.Black);
        }
    
    }
}
