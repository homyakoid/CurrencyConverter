﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WindowsFormsApplication1.ServiceReference1 {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ObjectsList", Namespace="http://schemas.datacontract.org/2004/07/WcfServiceLibrary2")]
    [System.SerializableAttribute()]
    public partial class ObjectsList : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string[] ObjectsField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string[] Objects {
            get {
                return this.ObjectsField;
            }
            set {
                if ((object.ReferenceEquals(this.ObjectsField, value) != true)) {
                    this.ObjectsField = value;
                    this.RaisePropertyChanged("Objects");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CurrencyRatesPerDateTypeList", Namespace="http://schemas.datacontract.org/2004/07/WcfServiceLibrary2")]
    [System.SerializableAttribute()]
    public partial class CurrencyRatesPerDateTypeList : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private WindowsFormsApplication1.ServiceReference1.CurrencyRatesPerDateType[] RatesObjField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public WindowsFormsApplication1.ServiceReference1.CurrencyRatesPerDateType[] RatesObj {
            get {
                return this.RatesObjField;
            }
            set {
                if ((object.ReferenceEquals(this.RatesObjField, value) != true)) {
                    this.RatesObjField = value;
                    this.RaisePropertyChanged("RatesObj");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CurrencyRatesPerDateType", Namespace="http://schemas.datacontract.org/2004/07/WcfServiceLibrary2")]
    [System.SerializableAttribute()]
    public partial class CurrencyRatesPerDateType : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.Dictionary<string, string> CurrenciesField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DateField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.Dictionary<string, string> Currencies {
            get {
                return this.CurrenciesField;
            }
            set {
                if ((object.ReferenceEquals(this.CurrenciesField, value) != true)) {
                    this.CurrenciesField = value;
                    this.RaisePropertyChanged("Currencies");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Date {
            get {
                return this.DateField;
            }
            set {
                if ((object.ReferenceEquals(this.DateField, value) != true)) {
                    this.DateField = value;
                    this.RaisePropertyChanged("Date");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IService1")]
    public interface IService1 {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/ConvertAmount", ReplyAction="http://tempuri.org/IService1/ConvertAmountResponse")]
        double ConvertAmount(string sourceID, string targetID, string date, double sourceAmount);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/ConvertAmount", ReplyAction="http://tempuri.org/IService1/ConvertAmountResponse")]
        System.Threading.Tasks.Task<double> ConvertAmountAsync(string sourceID, string targetID, string date, double sourceAmount);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetCurrencyCodes", ReplyAction="http://tempuri.org/IService1/GetCurrencyCodesResponse")]
        WindowsFormsApplication1.ServiceReference1.ObjectsList GetCurrencyCodes();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetCurrencyCodes", ReplyAction="http://tempuri.org/IService1/GetCurrencyCodesResponse")]
        System.Threading.Tasks.Task<WindowsFormsApplication1.ServiceReference1.ObjectsList> GetCurrencyCodesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetDates", ReplyAction="http://tempuri.org/IService1/GetDatesResponse")]
        WindowsFormsApplication1.ServiceReference1.ObjectsList GetDates();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetDates", ReplyAction="http://tempuri.org/IService1/GetDatesResponse")]
        System.Threading.Tasks.Task<WindowsFormsApplication1.ServiceReference1.ObjectsList> GetDatesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetRates", ReplyAction="http://tempuri.org/IService1/GetRatesResponse")]
        WindowsFormsApplication1.ServiceReference1.CurrencyRatesPerDateTypeList GetRates(string startDate, string endDate, string currency);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetRates", ReplyAction="http://tempuri.org/IService1/GetRatesResponse")]
        System.Threading.Tasks.Task<WindowsFormsApplication1.ServiceReference1.CurrencyRatesPerDateTypeList> GetRatesAsync(string startDate, string endDate, string currency);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetStringForCSV", ReplyAction="http://tempuri.org/IService1/GetStringForCSVResponse")]
        string GetStringForCSV(string startDate, string endDate, string currency);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetStringForCSV", ReplyAction="http://tempuri.org/IService1/GetStringForCSVResponse")]
        System.Threading.Tasks.Task<string> GetStringForCSVAsync(string startDate, string endDate, string currency);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetLastDate", ReplyAction="http://tempuri.org/IService1/GetLastDateResponse")]
        string GetLastDate();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetLastDate", ReplyAction="http://tempuri.org/IService1/GetLastDateResponse")]
        System.Threading.Tasks.Task<string> GetLastDateAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IService1Channel : WindowsFormsApplication1.ServiceReference1.IService1, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Service1Client : System.ServiceModel.ClientBase<WindowsFormsApplication1.ServiceReference1.IService1>, WindowsFormsApplication1.ServiceReference1.IService1 {
        
        public Service1Client() {
        }
        
        public Service1Client(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public Service1Client(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public double ConvertAmount(string sourceID, string targetID, string date, double sourceAmount) {
            return base.Channel.ConvertAmount(sourceID, targetID, date, sourceAmount);
        }
        
        public System.Threading.Tasks.Task<double> ConvertAmountAsync(string sourceID, string targetID, string date, double sourceAmount) {
            return base.Channel.ConvertAmountAsync(sourceID, targetID, date, sourceAmount);
        }
        
        public WindowsFormsApplication1.ServiceReference1.ObjectsList GetCurrencyCodes() {
            return base.Channel.GetCurrencyCodes();
        }
        
        public System.Threading.Tasks.Task<WindowsFormsApplication1.ServiceReference1.ObjectsList> GetCurrencyCodesAsync() {
            return base.Channel.GetCurrencyCodesAsync();
        }
        
        public WindowsFormsApplication1.ServiceReference1.ObjectsList GetDates() {
            return base.Channel.GetDates();
        }
        
        public System.Threading.Tasks.Task<WindowsFormsApplication1.ServiceReference1.ObjectsList> GetDatesAsync() {
            return base.Channel.GetDatesAsync();
        }
        
        public WindowsFormsApplication1.ServiceReference1.CurrencyRatesPerDateTypeList GetRates(string startDate, string endDate, string currency) {
            return base.Channel.GetRates(startDate, endDate, currency);
        }
        
        public System.Threading.Tasks.Task<WindowsFormsApplication1.ServiceReference1.CurrencyRatesPerDateTypeList> GetRatesAsync(string startDate, string endDate, string currency) {
            return base.Channel.GetRatesAsync(startDate, endDate, currency);
        }
        
        public string GetStringForCSV(string startDate, string endDate, string currency) {
            return base.Channel.GetStringForCSV(startDate, endDate, currency);
        }
        
        public System.Threading.Tasks.Task<string> GetStringForCSVAsync(string startDate, string endDate, string currency) {
            return base.Channel.GetStringForCSVAsync(startDate, endDate, currency);
        }
        
        public string GetLastDate() {
            return base.Channel.GetLastDate();
        }
        
        public System.Threading.Tasks.Task<string> GetLastDateAsync() {
            return base.Channel.GetLastDateAsync();
        }
    }
}