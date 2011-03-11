// <copyright file="ServerImplTest.Constructor.g.cs" company="Kelman Ltd">Copyright � Kelman Ltd 2011</copyright>
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
using Microsoft.Pex.Framework.Generated;
using Valker.PlayOnLan.Api.Communication;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Valker.PlayOnLan.Server
{
    public partial class ServerImplTest
    {
[TestMethod]
[PexGeneratedBy(typeof(ServerImplTest))]
public void Constructor814()
{
    using (PexDisposableContext disposables = PexDisposableContext.Create())
    {
      ServerImpl serverImpl;
      IMessageConnector[] iMessageConnectors = new IMessageConnector[2];
      serverImpl =
        this.Constructor((IEnumerable<IMessageConnector>)iMessageConnectors);
      disposables.Add((IDisposable)serverImpl);
      disposables.Dispose();
      Assert.IsNotNull((object)serverImpl);
    }
}
[TestMethod]
[PexGeneratedBy(typeof(ServerImplTest))]
public void Constructor16()
{
    using (PexDisposableContext disposables = PexDisposableContext.Create())
    {
      ServerImpl serverImpl;
      IMessageConnector[] iMessageConnectors = new IMessageConnector[1];
      serverImpl =
        this.Constructor((IEnumerable<IMessageConnector>)iMessageConnectors);
      disposables.Add((IDisposable)serverImpl);
      disposables.Dispose();
      Assert.IsNotNull((object)serverImpl);
    }
}
[TestMethod]
[PexGeneratedBy(typeof(ServerImplTest))]
public void Constructor65()
{
    using (PexDisposableContext disposables = PexDisposableContext.Create())
    {
      ServerImpl serverImpl;
      IMessageConnector[] iMessageConnectors = new IMessageConnector[0];
      serverImpl =
        this.Constructor((IEnumerable<IMessageConnector>)iMessageConnectors);
      disposables.Add((IDisposable)serverImpl);
      disposables.Dispose();
      Assert.IsNotNull((object)serverImpl);
    }
}
[TestMethod]
[PexGeneratedBy(typeof(ServerImplTest))]
[ExpectedException(typeof(ArgumentNullException))]
public void ConstructorThrowsArgumentNullException344()
{
    using (PexDisposableContext disposables = PexDisposableContext.Create())
    {
      ServerImpl serverImpl;
      serverImpl = this.Constructor((IEnumerable<IMessageConnector>)null);
      disposables.Add((IDisposable)serverImpl);
      disposables.Dispose();
    }
}
    }
}
