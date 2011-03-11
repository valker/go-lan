// <copyright file="ServerMessageTypesTest.cs" company="Kelman Ltd">Copyright © Kelman Ltd 2011</copyright>

using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Valker.PlayOnLan.Server.Messages.Server;

namespace Valker.PlayOnLan.Server.Messages.Server
{
    [TestClass]
    [PexClass(typeof(ServerMessageTypes))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class ServerMessageTypesTest
    {
        [PexMethod]
        public Type[] TypesGet()
        {
            Type[] result = ServerMessageTypes.Types;
            return result;
            // TODO: add assertions to method ServerMessageTypesTest.TypesGet()
        }
    }
}
