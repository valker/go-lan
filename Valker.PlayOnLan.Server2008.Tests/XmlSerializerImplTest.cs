// <copyright file="XmlSerializerImplTest.cs" company="Kelman Ltd">Copyright © Kelman Ltd 2011</copyright>

using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Valker.PlayOnLan.Server.Messages;

namespace Valker.PlayOnLan.Server.Messages
{
    [TestClass]
    [PexClass(typeof(XmlSerializerImpl))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class XmlSerializerImplTest
    {
        [PexMethod]
        public string Perform(
            Type baseClass,
            Type thisType,
            object objectToSerialize
        )
        {
            string result = XmlSerializerImpl.Perform(baseClass, thisType, objectToSerialize);
            return result;
            // TODO: add assertions to method XmlSerializerImplTest.Perform(Type, Type, Object)
        }
    }
}
