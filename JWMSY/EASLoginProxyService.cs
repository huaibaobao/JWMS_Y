﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:2.0.50727.5485
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

// 
// 此源代码由 wsdl 自动生成, Version=2.0.50727.3038。
// 


/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Web.Services.WebServiceBindingAttribute(Name="EASLoginSoapBinding", Namespace="http://172.16.18.212:7888/ormrpc/services/EASLogin")]
public partial class EASLoginProxyService : System.Web.Services.Protocols.SoapHttpClientProtocol {
    
    /// <remarks/>
    public EASLoginProxyService() {
        this.Url = "http://172.16.18.212:7888/ormrpc/services/EASLogin";
    }
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace="http://login.webservice.bos.kingdee.com", ResponseNamespace="http://172.16.18.212:7888/ormrpc/services/EASLogin")]
    [return: System.Xml.Serialization.SoapElementAttribute("loginReturn")]
    public WSContext login(string userName, string password, string slnName, string dcName, string language, int dbType) {
        object[] results = this.Invoke("login", new object[] {
                    userName,
                    password,
                    slnName,
                    dcName,
                    language,
                    dbType});
        return ((WSContext)(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult Beginlogin(string userName, string password, string slnName, string dcName, string language, int dbType, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("login", new object[] {
                    userName,
                    password,
                    slnName,
                    dcName,
                    language,
                    dbType}, callback, asyncState);
    }
    
    /// <remarks/>
    public WSContext Endlogin(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((WSContext)(results[0]));
    }
    
    /// <remarks/>
    [System.Web.Services.WebMethodAttribute(MessageName="login1")]
    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace="http://login.webservice.bos.kingdee.com", ResponseNamespace="http://172.16.18.212:7888/ormrpc/services/EASLogin")]
    [return: System.Xml.Serialization.SoapElementAttribute("loginReturn")]
    public WSContext login(string userName, string password, string slnName, string dcName, string language, int dbType, string authPattern) {
        object[] results = this.Invoke("login1", new object[] {
                    userName,
                    password,
                    slnName,
                    dcName,
                    language,
                    dbType,
                    authPattern});
        return ((WSContext)(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult Beginlogin1(string userName, string password, string slnName, string dcName, string language, int dbType, string authPattern, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("login1", new object[] {
                    userName,
                    password,
                    slnName,
                    dcName,
                    language,
                    dbType,
                    authPattern}, callback, asyncState);
    }
    
    /// <remarks/>
    public WSContext Endlogin1(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((WSContext)(results[0]));
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.SoapTypeAttribute(Namespace="urn:client")]
public partial class WSContext {
    
    private string slnNameField;
    
    private string passwordField;
    
    private int dbTypeField;
    
    private string userNameField;
    
    private string sessionIdField;
    
    private string dcNameField;
    
    /// <remarks/>
    [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
    public string slnName {
        get {
            return this.slnNameField;
        }
        set {
            this.slnNameField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
    public string password {
        get {
            return this.passwordField;
        }
        set {
            this.passwordField = value;
        }
    }
    
    /// <remarks/>
    public int dbType {
        get {
            return this.dbTypeField;
        }
        set {
            this.dbTypeField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
    public string userName {
        get {
            return this.userNameField;
        }
        set {
            this.userNameField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
    public string sessionId {
        get {
            return this.sessionIdField;
        }
        set {
            this.sessionIdField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
    public string dcName {
        get {
            return this.dcNameField;
        }
        set {
            this.dcNameField = value;
        }
    }
}