// <copyright file="GameInfoTest.cs" company="Kelman Ltd">Copyright © Kelman Ltd 2011</copyright>

using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Valker.PlayOnLan.Server;
using Valker.PlayOnLan.Api.Communication;

namespace Valker.PlayOnLan.Server
{
    [TestClass]
    [PexClass(typeof(GameInfo))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class GameInfoTest
    {
        [PexMethod]
        public GameInfo Constructor(string gameName, IMessageConnector connector)
        {
            GameInfo target = new GameInfo(gameName, connector);
            return target;
            // TODO: add assertions to method GameInfoTest.Constructor(String, IMessageConnector)
        }
    }
}
