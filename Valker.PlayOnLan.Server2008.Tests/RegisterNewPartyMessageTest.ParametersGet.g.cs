// <copyright file="RegisterNewPartyMessageTest.ParametersGet.g.cs" company="Kelman Ltd">Copyright � Kelman Ltd 2011</copyright>
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

namespace Valker.PlayOnLan.Server.Messages.Server
{
    public partial class RegisterNewPartyMessageTest
    {
[TestMethod]
[PexGeneratedBy(typeof(RegisterNewPartyMessageTest))]
public void ParametersGet664()
{
    RegisterNewPartyMessage registerNewPartyMessage;
    string s;
    registerNewPartyMessage =
      new RegisterNewPartyMessage((string)null, (string)null);
    s = this.ParametersGet(registerNewPartyMessage);
    Assert.AreEqual<string>((string)null, s);
    Assert.IsNotNull((object)registerNewPartyMessage);
    Assert.AreEqual<string>((string)null, registerNewPartyMessage.GameId);
    Assert.AreEqual<string>((string)null, registerNewPartyMessage.Parameters);
}
    }
}