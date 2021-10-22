using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Database")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Julian Ohrt")]
[assembly: AssemblyProduct("Database")]
[assembly: AssemblyCopyright("Copyright © 2021")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

[assembly: CLSCompliant(false)]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision and Build Numbers
// by using the '*' as shown below:
[assembly: AssemblyVersion(WebDatabase.Version.VersionString + ".0")]
[assembly: AssemblyFileVersion(WebDatabase.Version.VersionString + ".0")]

[assembly: InternalsVisibleTo("WebTest")]

// GlobalSuppressions
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1601:Partial elements should be documented", Justification = "auto-generated code", Scope = "namespaceanddescendants", Target = "Database.Migrations")]
[assembly: SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1505:Opening braces should not be followed by blank line", Justification = "auto-generated code", Scope = "namespaceanddescendants", Target = "Database.Migrations")]
[assembly: SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1508:Closing braces should not be preceded by blank line", Justification = "auto-generated code", Scope = "namespaceanddescendants", Target = "Database.Migrations")]
[assembly: SuppressMessage("Design", "CA1062:Argumente von öffentlichen Methoden validieren", Justification = "auto-generated code", Scope = "namespaceanddescendants", Target = "Database.Migrations")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1413:Use trailing comma in multi-line initializers", Justification = "auto-generated code", Scope = "namespaceanddescendants", Target = "Database.Migrations")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1122:Use string.Empty for empty strings", Justification = "auto-generated code", Scope = "namespaceanddescendants", Target = "Database.Migrations")]
