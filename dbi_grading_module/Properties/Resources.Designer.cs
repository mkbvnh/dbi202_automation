﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace dbi_grading_module.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private  global::System.Resources.ResourceManager resourceMan;
        
        private  global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal  global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("dbi_grading_module.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal  global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to PROC sp_CompareDb
        ///(
        ///	@SourceDB SYSNAME,
        ///	@TargetDb SYSNAME
        ///)
        ///AS
        ///BEGIN
        ////*
        ///	DECLARE @SourceDB SYSNAME=&apos;DB1&apos;,@TargetDb SYSNAME=&apos;DB2&apos;
        ///*/
        ///	SET NOCOUNT ON;
        ///	SET ANSI_WARNINGS ON;
        ///	SET ANSI_NULLS ON;   
        ///
        ///	DECLARE @sqlStr VARCHAR(8000)
        ///	SET @SourceDB = RTRIM(LTRIM(@SourceDB))
        ///	IF DB_ID(@SourceDB) IS NULL 
        ///	BEGIN
        ///		PRINT &apos;Error: Unable to find the database &apos;+ @SourceDB +&apos;!!!&apos;
        ///		RETURN
        ///	END
        ///
        ///	SET @TargetDb = RTRIM(LTRIM(@TargetDb))
        ///	IF DB_ID(@SourceDB) IS NULL 
        ///	BEGIN
        ///		PRINT &apos;Error: Unable t [rest of string was truncated]&quot;;.
        /// </summary>
        internal  string ProcCompareDb {
            get {
                return ResourceManager.GetString("ProcCompareDb", resourceCulture);
            }
        }
    }
}
