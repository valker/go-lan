// <copyright file="AcknowledgeRegistrationMessageTest.Constructor.g.cs" company="Kelman Ltd">Copyright � Kelman Ltd 2011</copyright>
// <auto-generated>
// This file contains automatically generated unit tests.
// Do NOT modify this file manually.
// 
// When Pex is invoked again,
// it might remove or update any previously generated unit tests.
// 
// If the contents of this file becomes outdated, e.g. if it does not
// compile anymore, you may delete this file and invoke Pex again.
// </auto-generated>
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Pex.Framework.Generated;

namespace Valker.PlayOnLan.Server.Messages.Client
{
    public partial class AcknowledgeRegistrationMessageTest
    {
[TestMethod]
[PexGeneratedBy(typeof(AcknowledgeRegistrationMessageTest))]
public void Constructor504()
{
    AcknowledgeRegistrationMessage acknowledgeRegistrationMessage;
    acknowledgeRegistrationMessage = this.Constructor(false);
    Assert.IsNotNull((object)acknowledgeRegistrationMessage);
    Assert.AreEqual<bool>(false, acknowledgeRegistrationMessage.Status);
}
    }
}
