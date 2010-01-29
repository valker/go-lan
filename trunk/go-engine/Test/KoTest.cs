using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace go_engine.Test
{
    [TestFixture]
    public class KoTest
    {
        [Test]
        public void SimpleKoDetected()
        {
            // сделать ситуацию, воспроизводящую простое КО и удостовериться, что ход не проходит
        }
        [Test]
        public void SimpleKoNotDetected()
        {
            // сделать ситуацию, воспроизводящую позиционное супер КО и удостовериться, что ход не проходит
        }

        [Test]
        public void NoKo()
        {
            // сделать ситуацию, воспроизводящую простое КО и удостовериться, что ход проходит нормально    
        }

        [Test]
        public void SuperKoPositionedDetected()
        {
            // сделать ситуацию, воспроизводящую позиционное супер КО и удостовериться, что ход не проходит
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
