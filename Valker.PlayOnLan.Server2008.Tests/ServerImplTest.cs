// <copyright file="ServerImplTest.cs" company="Kelman Ltd">Copyright © Kelman Ltd 2011</copyright>

using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Valker.PlayOnLan.Server;
using Valker.PlayOnLan.Api.Communication;
using System.Collections.Generic;

namespace Valker.PlayOnLan.Server
{
    [TestClass]
    [PexClass(typeof(ServerImpl))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class ServerImplTest
    {
        [PexMethod]
        public void UpdatePartyStates([PexAssumeUnderTest]ServerImpl target, IClientInfo clientInfo)
        {
            target.UpdatePartyStates(clientInfo);
            // TODO: add assertions to method ServerImplTest.UpdatePartyStates(ServerImpl, IClientInfo)
        }
        [PexMethod]
        public void Send(
            [PexAssumeUnderTest]ServerImpl target,
            IClientInfo recepient,
            string message
        )
        {
            target.Send(recepient, message);
            // TODO: add assertions to method ServerImplTest.Send(ServerImpl, IClientInfo, String)
        }
        [PexMethod]
        public string[] RetrieveSupportedGames([PexAssumeUnderTest]ServerImpl target)
        {
            string[] result = target.RetrieveSupportedGames();
            return result;
            // TODO: add assertions to method ServerImplTest.RetrieveSupportedGames(ServerImpl)
        }
        [PexMethod]
        public void RegisterNewPlayer(
            [PexAssumeUnderTest]ServerImpl target,
            IClientInfo client,
            string Name
        )
        {
            target.RegisterNewPlayer(client, Name);
            // TODO: add assertions to method ServerImplTest.RegisterNewPlayer(ServerImpl, IClientInfo, String)
        }
        [PexMethod]
        public PartyStatus RegisterNewParty(
            [PexAssumeUnderTest]ServerImpl target,
            IClientInfo client,
            string gameId,
            string parameters
        )
        {
            PartyStatus result = target.RegisterNewParty(client, gameId, parameters);
            return result;
            // TODO: add assertions to method ServerImplTest.RegisterNewParty(ServerImpl, IClientInfo, String, String)
        }
        [PexMethod]
        public void Dispose([PexAssumeUnderTest]ServerImpl target)
        {
            target.Dispose();
            // TODO: add assertions to method ServerImplTest.Dispose(ServerImpl)
        }
        [PexMethod]
        public ServerImpl Constructor(IEnumerable<IMessageConnector> connectors)
        {
            ServerImpl target = new ServerImpl(connectors);
            return target;
            // TODO: add assertions to method ServerImplTest.Constructor(IEnumerable`1<IMessageConnector>)
        }
        [PexMethod]
        public void AddConnector([PexAssumeUnderTest]ServerImpl target, IMessageConnector connector)
        {
            target.AddConnector(connector);
            // TODO: add assertions to method ServerImplTest.AddConnector(ServerImpl, IMessageConnector)
        }
        [PexMethod]
        public void AcceptPartyRequest(
            [PexAssumeUnderTest]ServerImpl target,
            string RequesterName,
            string GameType,
            string AccepterName
        )
        {
            target.AcceptPartyRequest(RequesterName, GameType, AccepterName);
            // TODO: add assertions to method ServerImplTest.AcceptPartyRequest(ServerImpl, String, String, String)
        }
    }
}
