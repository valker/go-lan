// <copyright file="RetrieveSupportedGamesResponceMessageTest.Execute.g.cs" company="Kelman Ltd">Copyright � Kelman Ltd 2011</copyright>
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
using Valker.PlayOnLan.Api.Communication;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Pex.Framework.Generated;

namespace Valker.PlayOnLan.Server.Messages.Client
{
    public partial class RetrieveSupportedGamesResponceMessageTest
    {
[TestMethod]
[PexGeneratedBy(typeof(RetrieveSupportedGamesResponceMessageTest))]
[ExpectedException(typeof(ArgumentNullException))]
public void ExecuteThrowsArgumentNullException644()
{
    RetrieveSupportedGamesResponceMessage s0
       = new RetrieveSupportedGamesResponceMessage();
    s0.Responce = (string[])null;
    this.Execute(s0, (IClientMessageExecuter)null, (object)null);
}
    }
}