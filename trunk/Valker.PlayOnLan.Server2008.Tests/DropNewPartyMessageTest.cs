// <copyright file="DropNewPartyMessageTest.cs" company="Kelman Ltd">Copyright © Kelman Ltd 2011</copyright>

using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Valker.PlayOnLan.Server.Messages.Server;
using Valker.PlayOnLan.Api.Communication;

namespace Valker.PlayOnLan.Server.Messages.Server
{
    [TestClass]
    [PexClass(typeof(DropNewPartyMessage))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class DropNewPartyMessageTest
    {
        [PexMethod]
        public void Execute(
            [PexAssumeUnderTest]DropNewPartyMessage target,
            IServerMessageExecuter server,
            IClientInfo sender
        )
        {
            target.Execute(server, sender);
            // TODO: add assertions to method DropNewPartyMessageTest.Execute(DropNewPartyMessage, IServerMessageExecuter, IClientInfo)
        }
    }
}
