using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml.Linq;

namespace WcfServiceLibrary2
{
    public class Service1 : IService1
    {
        XDocument doc = XDocument.Load(@"http://www.ecb.europa.eu/stats/eurofxref/eurofxref-hist-90d.xml");

        public double ConvertAmount(string sourceID, string targetID, string date, double sourceAmount)
        {
            string sourceRate = "", targetRate = "";
            bool sourceRateFound = false, targetRateFound = false, dateFound = false;
            foreach (XElement el in doc.Root.Elements())
            {
                if (el.Name.LocalName == "Cube")
                {
                    foreach (XElement elementTime in el.Elements())
                    {
                        if (elementTime.Attribute("time").Value == date)
                        {
                            dateFound = true;
                            foreach (XElement elementRate in elementTime.Elements())
                            {
                                if (elementRate.Attribute("currency").Value == sourceID)
                                {
                                    sourceRate = elementRate.Attribute("rate").Value;
                                    sourceRateFound = true;
                                }
                                if (elementRate.Attribute("currency").Value == targetID)
                                {
                                    targetRate = elementRate.Attribute("rate").Value;
                                    targetRateFound = true;
                                }
                                if (dateFound && sourceRateFound && targetRateFound)
                                {
                                    sourceRate = sourceRate.Replace('.', ',');
                                    targetRate = targetRate.Replace('.', ',');
                                    return (sourceAmount / Convert.ToDouble(sourceRate)) * Convert.ToDouble(targetRate);
                                }
                            }
                        }
                    }
                }
            }
            return -1;
        }

        public ObjectsList GetCurrencyCodes()
        {
            XElement element;
            ObjectsList currencyIDs = new ObjectsList();

            foreach (XElement el in doc.Root.Elements())
            {
                if (el.Name.LocalName == "Cube")
                {
                    element = (XElement)el.FirstNode;
                    foreach (XElement ID in element.Elements())
                    {
                        currencyIDs.Objects.Add(ID.Attribute("currency").Value);
                    }
                }
            }
            return currencyIDs;
        }

        public ObjectsList GetDates()
        {
            ObjectsList dates = new ObjectsList();
            foreach (XElement el in doc.Root.Elements())
            {
                if (el.Name.LocalName == "Cube")
                {
                    foreach (XElement elementTime in el.Elements())
                    {
                        dates.Objects.Add(elementTime.Attribute("time").Value);
                    }
                }
            }
            return dates;
        }

        public CurrencyRatesPerDateTypeList GetRates(string startDate, string endDate, string currency)
        {
            bool periodStarted = false;
            CurrencyRatesPerDateTypeList objs = new CurrencyRatesPerDateTypeList();
            foreach (XElement el in doc.Root.Elements())
            {
                if (el.Name.LocalName == "Cube")
                {
                    foreach (XElement elementTime in el.Elements())
                    {
                        if (elementTime.Attribute("time").Value == endDate)
                        {
                            periodStarted = true;
                        }
                        if (periodStarted)
                        {
                            CurrencyRatesPerDateType temp = new CurrencyRatesPerDateType();
                            temp.Date = elementTime.Attribute("time").Value;
                            foreach (XElement elementRate in elementTime.Elements())
                            {
                                if (currency != null)
                                {
                                    if (elementRate.Attribute("currency").Value == currency)
                                    {
                                        temp.Currencies.Add(elementRate.Attribute("currency").Value,
                                            elementRate.Attribute("rate").Value);
                                        break;
                                    }
                                }
                                else
                                    temp.Currencies.Add(elementRate.Attribute("currency").Value,
                                        elementRate.Attribute("rate").Value);
                            }
                            objs.RatesObj.Add(temp);
                        }
                        if (elementTime.Attribute("time").Value == startDate)
                        {
                            periodStarted = false;
                            objs.RatesObj.Reverse();
                            return objs;
                        }
                    }
                }
            }
            return null;
        }

        public string GetStringForCSV(string startDate, string endDate, string currency)
        {
            CurrencyRatesPerDateTypeList objs;
            objs = GetRates(startDate, endDate, currency);
            bool firstRow = false;
            string tempStr;
            StringBuilder str = new StringBuilder();
            foreach (var obj in objs.RatesObj)
            {
                if (firstRow)
                    str.Append('\n');
                str.Append(obj.Date);
                firstRow = true;
                foreach (var curr in obj.Currencies.Keys)
                {
                    str.Append('\n');
                    str.Append(curr);
                    str.Append(';');
                    tempStr = obj.Currencies[curr].Replace('.', ',');
                    str.Append(tempStr);
                }
            }
            return str.ToString();
        }

        public string GetLastDate()
        {
            XElement element = null;
            foreach (XElement el in doc.Root.Elements())
            {
                if (el.Name.LocalName == "Cube")
                {
                    element = (XElement)el.FirstNode;
                    break;
                }
            }
            return element.Attribute("time").Value;
        }
    }
}
