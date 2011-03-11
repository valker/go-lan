// <copyright file="ClientMessageTest.cs" company="Kelman Ltd">Copyright © Kelman Ltd 2011</copyright>

using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Valker.PlayOnLan.Server.Messages.Client;
using Valker.PlayOnLan.Api.Communication;

namespace Valker.PlayOnLan.Server.Messages.Client
{
    [TestClass]
    [PexClass(typeof(ClientMessage))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class ClientMessageTest
    {
        [PexMethod]
        public void Execute(
            [PexAssumeNotNull]ClientMessage target,
            IClientMessageExecuter client,
            object sender
        )
        {
            target.Execute(client, sender);
            // TODO: add assertions to method ClientMessageTest.Execute(ClientMessage, IClientMessageExecuter, Object)
        }
    }
}
