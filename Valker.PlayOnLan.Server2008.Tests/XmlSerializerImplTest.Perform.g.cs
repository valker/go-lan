// <copyright file="XmlSerializerImplTest.Perform.g.cs" company="Kelman Ltd">Copyright � Kelman Ltd 2011</copyright>
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

namespace Valker.PlayOnLan.Server.Messages
{
    public partial class XmlSerializerImplTest
    {
[TestMethod]
[PexGeneratedBy(typeof(XmlSerializerImplTest))]
[ExpectedException(typeof(ArgumentNullException))]
public void PerformThrowsArgumentNullException323()
{
    string s;
    s = this.Perform((Type)null, (Type)null, (object)null);
}
    }
}
