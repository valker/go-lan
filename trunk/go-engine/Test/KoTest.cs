using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using go_engine.Data;
using Microsoft.Xna.Framework;
using NUnit.Framework;

namespace go_engine.Test
{
    [TestFixture]
    public class KoTest
    {
        [Test]
        [ExpectedException(typeof(GoException))]
        public void SimpleKoDetected()
        {
            var position = Position.CreateInitial(9);
            position = position.Move(new Point(2, 1), MokuState.Black).First;
            position = position.Move(new Point(2, 3), MokuState.Black).First;
            position = position.Move(new Point(1, 2), MokuState.Black).First;
            position = position.Move(new Point(3, 2), MokuState.Black).First;
            position = position.Move(new Point(3, 1), MokuState.White).First;
            position = position.Move(new Point(4, 2), MokuState.White).First;
            position = position.Move(new Point(3, 3), MokuState.White).First;
            var rules = new Rules {Ko = KoRule.Simle, Points = Points.Empty};
            var storage = new PositionStorage(position, rules);
            var result = storage.Move(position, new Point(2, 2), MokuState.White);
            result = storage.Move(result.First, new Point(3, 2), MokuState.Black);
        }

        [Test]
        public void SimpleKoNotDetected()
        {
            // сделать ситуацию, воспроизводящую позиционное супер КО и удостовериться, что ход не проходит
            PositionedKo(KoRule.Simle);
        }

        private static void PositionedKo(KoRule koRule)
        {
            var position = Position.CreateInitial(9);

            position = position.Move(new Point(1, 0), MokuState.Black).First;
            position = position.Move(new Point(0, 1), MokuState.Black).First;
            position = position.Move(new Point(3, 0), MokuState.White).First;
            position = position.Move(new Point(2, 1), MokuState.White).First;
            position = position.Move(new Point(1, 1), MokuState.White).First;
            var rules = new Rules { Ko = koRule, Points = Points.Empty };
            var storage = new PositionStorage(position, rules);
            var result = storage.Move(position, new Point(2, 0), MokuState.Black);
            result = storage.Move(result.First, new Point(0, 0), MokuState.White);
            result = storage.Move(result.First, new Point(1, 0), MokuState.Black);
        }

        [Test]
        [ExpectedException(typeof(GoException))]
        public void SuperKoPositionedDetected()
        {
            // сделать ситуацию, воспроизводящую позиционное супер КО и удостовериться, что ход не проходит
            PositionedKo(KoRule.SuperPositioned);
        }

        [Test]
        public void SuperKoSituationedDetected()
        {
            // сделать ситуацию, воспроизводящую ситуационное супер КО и удостовериться, что ход не проходит
        }
        [Test]
        public void SuperKoSituationedNotDetected()
        {
            // сделать ситуацию, воспроизводящую позиционное супер КО и удостовериться, что ход не проходит
        }
    }
}
