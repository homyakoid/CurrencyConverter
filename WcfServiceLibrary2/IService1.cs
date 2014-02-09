using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServiceLibrary2
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        double ConvertAmount(string sourceID, string targetID, string date, double sourceAmount);

        [OperationContract]
        ObjectsList GetCurrencyCodes();

        [OperationContract]
        ObjectsList GetDates();

        [OperationContract]
        CurrencyRatesPerDateTypeList GetRates(string startDate, string endDate, string currency);

        [OperationContract]
        string GetStringForCSV(string startDate, string endDate, string currency);

        [OperationContract]
        string GetLastDate();

    }

    [DataContract]
    public class ObjectsList
    {
        List<string> objects = new List<string>();
        [DataMember]
        public List<string> Objects
        {
            get { return objects; }
            set { objects = value; }
        }
    }

    [DataContract]
    public class CurrencyRatesPerDateType
    {
        string date;
        Dictionary<string, string> currencies = new Dictionary<string, string>();
        [DataMember]
        public Dictionary<string, string> Currencies
        {
            get { return currencies; }
            set { currencies = value; }
        }
        [DataMember]
        public string Date
        {
            get { return date; }
            set { date = value; }
        }
    }

    [DataContract]
    public class CurrencyRatesPerDateTypeList
    {
        List<CurrencyRatesPerDateType> ratesObj = new List<CurrencyRatesPerDateType>();
        [DataMember]
        public List<CurrencyRatesPerDateType> RatesObj
        {
            get { return ratesObj; }
            set { ratesObj = value; }
        }
    }
}
