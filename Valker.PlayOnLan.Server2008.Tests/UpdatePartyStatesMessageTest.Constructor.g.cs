// <copyright file="UpdatePartyStatesMessageTest.Constructor.g.cs" company="Kelman Ltd">Copyright � Kelman Ltd 2011</copyright>
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
using Valker.PlayOnLan.Api.Game;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Pex.Framework.Generated;

namespace Valker.PlayOnLan.Server.Messages.Client
{
    public partial class UpdatePartyStatesMessageTest
    {
[TestMethod]
[PexGeneratedBy(typeof(UpdatePartyStatesMessageTest))]
public void Constructor515()
{
    List<PartyState> list;
    UpdatePartyStatesMessage updatePartyStatesMessage;
    PartyState[] partyStates = new PartyState[0];
    list = new List<PartyState>((IEnumerable<PartyState>)partyStates);
    updatePartyStatesMessage = this.Constructor(list);
    Assert.IsNotNull((object)updatePartyStatesMessage);
    Assert.IsNotNull(updatePartyStatesMessage.Info);
    Assert.AreEqual<int>(0, updatePartyStatesMessage.Info.Length);
}
[TestMethod]
[PexGeneratedBy(typeof(UpdatePartyStatesMessageTest))]
[ExpectedException(typeof(ArgumentNullException))]
public void ConstructorThrowsArgumentNullException427()
{
    UpdatePartyStatesMessage updatePartyStatesMessage;
    updatePartyStatesMessage = this.Constructor((List<PartyState>)null);
}
    }
}
