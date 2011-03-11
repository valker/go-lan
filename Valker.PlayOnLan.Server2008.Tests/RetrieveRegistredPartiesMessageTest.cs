// <copyright file="RetrieveRegistredPartiesMessageTest.cs" company="Kelman Ltd">Copyright © Kelman Ltd 2011</copyright>

using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Valker.PlayOnLan.Server.Messages.Server;
using Valker.PlayOnLan.Api.Communication;

namespace Valker.PlayOnLan.Server.Messages.Server
{
    [TestClass]
    [PexClass(typeof(RetrieveRegistredPartiesMessage))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class RetrieveRegistredPartiesMessageTest
    {
        [PexMethod]
        public void Execute(
            [PexAssumeUnderTest]RetrieveRegistredPartiesMessage target,
            IServerMessageExecuter server,
            IClientInfo sender
        )
        {
            target.Execute(server, sender);
            // TODO: add assertions to method RetrieveRegistredPartiesMessageTest.Execute(RetrieveRegistredPartiesMessage, IServerMessageExecuter, IClientInfo)
        }
    }
}
