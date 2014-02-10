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
        // method converts amount of one currency into another currency for chosen date
        [OperationContract]
        double ConvertAmount(string sourceID, string targetID, string date, double sourceAmount);

        // method returns an object that holds list of existing currency codes
        [OperationContract]
        ObjectsList GetCurrencyCodes();

        // method returns an object that holds list of existing dates
        [OperationContract]
        ObjectsList GetDates();

        // method returns a list of objects that hold date and currency rates for this date
        [OperationContract]
        CurrencyRatesPerDateTypeList GetRates(string startDate, string endDate, string currency);

        // method returns a string to be saved into .CSV file
        [OperationContract]
        string GetStringForCSV(string startDate, string endDate, string currency);

        // method returns the last listed date
        [OperationContract]
        string GetLastDate();

    }

    // class helper holds a list of string objects
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

    // class helper holds a date and a list of currency rates for this date
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


    // class helper holds a list of CurrencyRatesPerDateType objects
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
