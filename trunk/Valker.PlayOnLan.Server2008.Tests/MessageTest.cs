// <copyright file="MessageTest.cs" company="Kelman Ltd">Copyright © Kelman Ltd 2011</copyright>

using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Valker.PlayOnLan.Server.Messages;

namespace Valker.PlayOnLan.Server.Messages
{
    [TestClass]
    [PexClass(typeof(Message))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class MessageTest
    {
        [PexMethod]
        public string ToString01([PexAssumeNotNull]Message target)
        {
            string result = target.ToString();
            return result;
            // TODO: add assertions to method MessageTest.ToString01(Message)
        }
    }
}
