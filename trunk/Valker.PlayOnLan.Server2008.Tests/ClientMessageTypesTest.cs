// <copyright file="ClientMessageTypesTest.cs" company="Kelman Ltd">Copyright © Kelman Ltd 2011</copyright>

using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Valker.PlayOnLan.Server.Messages.Client;

namespace Valker.PlayOnLan.Server.Messages.Client
{
    [TestClass]
    [PexClass(typeof(ClientMessageTypes))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class ClientMessageTypesTest
    {
        [PexMethod]
        public Type[] TypesGet()
        {
            Type[] result = ClientMessageTypes.Types;
            return result;
            // TODO: add assertions to method ClientMessageTypesTest.TypesGet()
        }
    }
}
