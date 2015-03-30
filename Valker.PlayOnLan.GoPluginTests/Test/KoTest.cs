using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Valker.PlayOnLan.Api;
using Valker.PlayOnLan.Utilities;

namespace Valker.PlayOnLan.GoPlugin.Test
{
    [TestFixture]
    public class KoTest
    {
        [Test]
        [ExpectedException(typeof(GoException))]
        public void SimpleKoDetected()
        {
            var position = Position.CreateInitial(9);
            position = position.Move(new Point(2, 1), Stone.Black).Item1;
            position = position.Move(new Point(2, 3), Stone.Black).Item1;
            position = position.Move(new Point(1, 2), Stone.Black).Item1;
            position = position.Move(new Point(3, 2), Stone.Black).Item1;
            position = position.Move(new Point(3, 1), Stone.White).Item1;
            position = position.Move(new Point(4, 2), Stone.White).Item1;
            position = position.Move(new Point(3, 3), Stone.White).Item1;
            var rules = new Rules {Ko = KoRule.Simple, Score = ScoreRule.EmptyTerritory};
            var storage = new PositionStorage(position, rules);
            position = storage.Move(position, new Point(2, 2), Stone.White).Item1;
            position = storage.Move(position, new Point(3, 2), Stone.Black).Item1;
        }

        [Test]
        public void SimpleKoNotDetected()
        {
            // сделать ситуацию, воспроизводящую позиционное супер КО и удостовериться, что ход не проходит
            PositionedKo(KoRule.Simple);
        }

        private static void PositionedKo(KoRule koRule)
        {
            var position = Position.CreateInitial(9);

            position = position.Move(new Point(1, 0), Stone.Black).Item1;
            position = position.Move(new Point(0, 1), Stone.Black).Item1;
            position = position.Move(new Point(3, 0), Stone.White).Item1;
            position = position.Move(new Point(2, 1), Stone.White).Item1;
            position = position.Move(new Point(1, 1), Stone.White).Item1;
            var rules = new Rules { Ko = koRule, Score = ScoreRule.EmptyTerritory };
            var storage = new PositionStorage(position, rules);
            var result = storage.Move(position, new Point(2, 0), Stone.Black);
            result = storage.Move(result.Item1, new Point(0, 0), Stone.White);
            result = storage.Move(result.Item1, new Point(1, 0), Stone.Black);
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