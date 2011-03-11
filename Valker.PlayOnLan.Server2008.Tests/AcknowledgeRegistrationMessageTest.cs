// <copyright file="AcknowledgeRegistrationMessageTest.cs" company="Kelman Ltd">Copyright © Kelman Ltd 2011</copyright>

using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Valker.PlayOnLan.Server.Messages.Client;
using Valker.PlayOnLan.Api.Communication;

namespace Valker.PlayOnLan.Server.Messages.Client
{
    [TestClass]
    [PexClass(typeof(AcknowledgeRegistrationMessage))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class AcknowledgeRegistrationMessageTest
    {
        [PexMethod]
        public void Execute(
            [PexAssumeUnderTest]AcknowledgeRegistrationMessage target,
            IClientMessageExecuter client,
            object sender
        )
        {
            target.Execute(client, sender);
            // TODO: add assertions to method AcknowledgeRegistrationMessageTest.Execute(AcknowledgeRegistrationMessage, IClientMessageExecuter, Object)
        }
        [PexMethod]
        public AcknowledgeRegistrationMessage Constructor(bool status)
        {
            AcknowledgeRegistrationMessage target = new AcknowledgeRegistrationMessage(status);
            return target;
            // TODO: add assertions to method AcknowledgeRegistrationMessageTest.Constructor(Boolean)
        }
    }
}
