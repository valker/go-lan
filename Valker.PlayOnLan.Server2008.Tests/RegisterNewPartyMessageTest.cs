// <copyright file="RegisterNewPartyMessageTest.cs" company="Kelman Ltd">Copyright © Kelman Ltd 2011</copyright>

using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Valker.PlayOnLan.Server.Messages.Server;
using Valker.PlayOnLan.Api.Communication;

namespace Valker.PlayOnLan.Server.Messages.Server
{
    [TestClass]
    [PexClass(typeof(RegisterNewPartyMessage))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class RegisterNewPartyMessageTest
    {
        [PexMethod]
        public void ParametersSet([PexAssumeUnderTest]RegisterNewPartyMessage target, string value)
        {
            target.Parameters = value;
            // TODO: add assertions to method RegisterNewPartyMessageTest.ParametersSet(RegisterNewPartyMessage, String)
        }
        [PexMethod]
        public void GameIdSet([PexAssumeUnderTest]RegisterNewPartyMessage target, string value)
        {
            target.GameId = value;
            // TODO: add assertions to method RegisterNewPartyMessageTest.GameIdSet(RegisterNewPartyMessage, String)
        }
        [PexMethod]
        public string ParametersGet([PexAssumeUnderTest]RegisterNewPartyMessage target)
        {
            string result = target.Parameters;
            return result;
            // TODO: add assertions to method RegisterNewPartyMessageTest.ParametersGet(RegisterNewPartyMessage)
        }
        [PexMethod]
        public string GameIdGet([PexAssumeUnderTest]RegisterNewPartyMessage target)
        {
            string result = target.GameId;
            return result;
            // TODO: add assertions to method RegisterNewPartyMessageTest.GameIdGet(RegisterNewPartyMessage)
        }
        [PexMethod]
        public void Execute(
            [PexAssumeUnderTest]RegisterNewPartyMessage target,
            IServerMessageExecuter server,
            IClientInfo client
        )
        {
            target.Execute(server, client);
            // TODO: add assertions to method RegisterNewPartyMessageTest.Execute(RegisterNewPartyMessage, IServerMessageExecuter, IClientInfo)
        }
        [PexMethod]
        public RegisterNewPartyMessage Constructor(string gameId, string parameters)
        {
            RegisterNewPartyMessage target = new RegisterNewPartyMessage(gameId, parameters);
            return target;
            // TODO: add assertions to method RegisterNewPartyMessageTest.Constructor(String, String)
        }
    }
}
