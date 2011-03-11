// <copyright file="UpdatePartyStatesMessageTest.cs" company="Kelman Ltd">Copyright © Kelman Ltd 2011</copyright>

using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Valker.PlayOnLan.Server.Messages.Client;
using Valker.PlayOnLan.Api.Communication;
using Valker.PlayOnLan.Api.Game;
using System.Collections.Generic;

namespace Valker.PlayOnLan.Server.Messages.Client
{
    [TestClass]
    [PexClass(typeof(UpdatePartyStatesMessage))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class UpdatePartyStatesMessageTest
    {
        [PexMethod]
        public void Execute(
            [PexAssumeUnderTest]UpdatePartyStatesMessage target,
            IClientMessageExecuter client,
            object sender
        )
        {
            target.Execute(client, sender);
            // TODO: add assertions to method UpdatePartyStatesMessageTest.Execute(UpdatePartyStatesMessage, IClientMessageExecuter, Object)
        }
        [PexMethod]
        public UpdatePartyStatesMessage Constructor(List<PartyState> requests)
        {
            UpdatePartyStatesMessage target = new UpdatePartyStatesMessage(requests);
            return target;
            // TODO: add assertions to method UpdatePartyStatesMessageTest.Constructor(List`1<PartyState>)
        }
    }
}
