// <copyright file="ServerMessageTest.cs" company="Kelman Ltd">Copyright © Kelman Ltd 2011</copyright>

using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Valker.PlayOnLan.Server.Messages.Server;
using Valker.PlayOnLan.Api.Communication;

namespace Valker.PlayOnLan.Server.Messages.Server
{
    [TestClass]
    [PexClass(typeof(ServerMessage))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class ServerMessageTest
    {
        [PexMethod]
        public void Execute(
            [PexAssumeNotNull]ServerMessage target,
            IServerMessageExecuter server,
            IClientInfo sender
        )
        {
            target.Execute(server, sender);
            // TODO: add assertions to method ServerMessageTest.Execute(ServerMessage, IServerMessageExecuter, IClientInfo)
        }
    }
}
