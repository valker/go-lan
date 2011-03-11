// <copyright file="AcceptNewPartyMessageTest.cs" company="Kelman Ltd">Copyright © Kelman Ltd 2011</copyright>

using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Valker.PlayOnLan.Server.Messages.Server;
using Valker.PlayOnLan.Api.Communication;

namespace Valker.PlayOnLan.Server.Messages.Server
{
    [TestClass]
    [PexClass(typeof(AcceptNewPartyMessage))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class AcceptNewPartyMessageTest
    {
        [PexMethod]
        public void RequesterNameSet([PexAssumeUnderTest]AcceptNewPartyMessage target, string value)
        {
            target.RequesterName = value;
            // TODO: add assertions to method AcceptNewPartyMessageTest.RequesterNameSet(AcceptNewPartyMessage, String)
        }
        [PexMethod]
        public void GameTypeSet([PexAssumeUnderTest]AcceptNewPartyMessage target, string value)
        {
            target.GameType = value;
            // TODO: add assertions to method AcceptNewPartyMessageTest.GameTypeSet(AcceptNewPartyMessage, String)
        }
        [PexMethod]
        public void AccepterNameSet([PexAssumeUnderTest]AcceptNewPartyMessage target, string value)
        {
            target.AccepterName = value;
            // TODO: add assertions to method AcceptNewPartyMessageTest.AccepterNameSet(AcceptNewPartyMessage, String)
        }
        [PexMethod]
        public string RequesterNameGet([PexAssumeUnderTest]AcceptNewPartyMessage target)
        {
            string result = target.RequesterName;
            return result;
            // TODO: add assertions to method AcceptNewPartyMessageTest.RequesterNameGet(AcceptNewPartyMessage)
        }
        [PexMethod]
        public string GameTypeGet([PexAssumeUnderTest]AcceptNewPartyMessage target)
        {
            string result = target.GameType;
            return result;
            // TODO: add assertions to method AcceptNewPartyMessageTest.GameTypeGet(AcceptNewPartyMessage)
        }
        [PexMethod]
        public string AccepterNameGet([PexAssumeUnderTest]AcceptNewPartyMessage target)
        {
            string result = target.AccepterName;
            return result;
            // TODO: add assertions to method AcceptNewPartyMessageTest.AccepterNameGet(AcceptNewPartyMessage)
        }
        [PexMethod]
        public void Execute(
            [PexAssumeUnderTest]AcceptNewPartyMessage target,
            IServerMessageExecuter server,
            IClientInfo sender
        )
        {
            target.Execute(server, sender);
            // TODO: add assertions to method AcceptNewPartyMessageTest.Execute(AcceptNewPartyMessage, IServerMessageExecuter, IClientInfo)
        }
        [PexMethod]
        public AcceptNewPartyMessage Constructor(
            string requesterName,
            string gameType,
            string accepterName
        )
        {
            AcceptNewPartyMessage target = new AcceptNewPartyMessage(requesterName, gameType, accepterName);
            return target;
            // TODO: add assertions to method AcceptNewPartyMessageTest.Constructor(String, String, String)
        }
    }
}
