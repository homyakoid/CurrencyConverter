﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
        bool formIsLoaded = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonConvert_Click(object sender, EventArgs e)
        {
            double resultAmount;
            DateTime date = dateTimePicker1.Value;
            string dateStr = date.Date.ToString("yyyy-MM-dd");
            try
            {
                resultAmount = client.ConvertAmount(comboBoxSource.Text, comboBoxTarget.Text, dateStr,
                    Convert.ToDouble(textBoxSource.Text));
                if (resultAmount == -1)
                    throw new KeyNotFoundException("Chosen date is not listed!");
                textBoxTarget.Text = resultAmount.ToString();
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
                textBoxTarget.Text = "";
            }
            catch (FormatException)
            {
                MessageBox.Show("Field 'You have' must be specified in double format!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
                 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                ServiceReference1.ObjectsList ObjectsList = new ServiceReference1.ObjectsList();
                ObjectsList = client.GetCurrencyCodes();
                string lastDateStr = client.GetLastDate();
                DateTime lastDate = DateTime.Parse(lastDateStr);
                dateTimePicker2.Value = lastDate;
                dateTimePicker3.Value = lastDate;
                comboBoxCurrencies.Items.Add("All");
                foreach (string id in ObjectsList.Objects)
                {
                    comboBoxSource.Items.Add(id);
                    if (id != comboBoxSource.Items[0].ToString())
                        comboBoxTarget.Items.Add(id);
                    comboBoxCurrencies.Items.Add(id);
                }
                comboBoxSource.Text = comboBoxSource.Items[0].ToString();
                comboBoxTarget.Text = comboBoxTarget.Items[0].ToString();
                comboBoxCurrencies.Text = "All";
                ShowRates();
                formIsLoaded = true;
            }
            catch (EndpointNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void comboBoxSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ServiceReference1.ObjectsList ObjectsList = new ServiceReference1.ObjectsList();
                ObjectsList = client.GetCurrencyCodes();
                comboBoxTarget.Items.Clear();
                foreach (string id in ObjectsList.Objects)
                {
                    if (id != comboBoxSource.SelectedItem.ToString())
                        comboBoxTarget.Items.Add(id);
                }
                comboBoxTarget.Text = comboBoxTarget.Items[0].ToString();
            }
            catch (EndpointNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CheckDates(out string startDateStr, out string endDateStr)
        {
            DateTime startDate = dateTimePicker2.Value;
            DateTime endDate = dateTimePicker3.Value;
            startDateStr = startDate.Date.ToString("yyyy-MM-dd");
            endDateStr = endDate.Date.ToString("yyyy-MM-dd");
            if (!IfDateListed(startDateStr))
                throw new KeyNotFoundException("Start date is not listed!");
            if (!IfDateListed(endDateStr))
                throw new KeyNotFoundException("End date is not listed!");
            if (startDate > endDate)
                throw new Exception("Start date must be less that end date!");
        }

        private void CheckComboBoxCurrencies(out string currency)
        {
            if (comboBoxCurrencies.Text == "All")
            {
                currency = null;
                return;
            }
            else
            {
                bool exists = false;
                ServiceReference1.ObjectsList currencyIDs = new ServiceReference1.ObjectsList();
                currencyIDs = client.GetCurrencyCodes();
                foreach (var id in currencyIDs.Objects)
                {
                    if (comboBoxCurrencies.Text == id)
                    {
                        exists = true;
                        break;
                    }
                }
                if (exists)
                {
                    currency = comboBoxCurrencies.Text;
                    return;
                }
                else
                    throw new KeyNotFoundException("There is no such currency code listed!");
            }
        }

        private ServiceReference1.CurrencyRatesPerDateTypeList GetRates()
        {
            string startDateStr, endDateStr;
            string currency;
            CheckDates(out startDateStr, out endDateStr);
            CheckComboBoxCurrencies(out currency);
            return client.GetRates(startDateStr, endDateStr, currency);
        }

        private bool IfDateListed(string date)
        {
            ServiceReference1.ObjectsList dates = new ServiceReference1.ObjectsList();
            dates = client.GetDates();
            foreach (var d in dates.Objects)
            {
                if (d == date)
                {
                    return true;
                }
            }
            return false;
        }

        private void ShowRates()
        {
            datesList.Items.Clear();
            ratesList.Items.Clear();
            try
            {
                ServiceReference1.CurrencyRatesPerDateTypeList objs;
                objs = GetRates();
                foreach (var obj in objs.RatesObj)
                {
                    datesList.Items.Add(obj.Date);
                }
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void datesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ratesList.Items.Clear();
            try
            {
                ServiceReference1.CurrencyRatesPerDateTypeList objs;
                objs = GetRates();
                foreach (var obj in objs.RatesObj)
                {
                    if (datesList.SelectedItem.ToString() == obj.Date)
                    {
                        foreach (var rate in obj.Currencies)
                            ratesList.Items.Add(rate);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonCSVExport_Click(object sender, EventArgs e)
        {
            string startDateStr, endDateStr;
            string currency;
            try
            {
                CheckDates(out startDateStr, out endDateStr);
                CheckComboBoxCurrencies(out currency);
                string str = client.GetStringForCSV(startDateStr, endDateStr, currency);
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "CSV file|*.csv";
                saveFileDialog1.Title = "Save rates as CSV File";
                saveFileDialog1.ShowDialog();
                File.WriteAllText(saveFileDialog1.FileName, str.ToString());
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (formIsLoaded)
                ShowRates();
        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            if (formIsLoaded)
                ShowRates();
        }

        private void comboBoxCurrencies_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (formIsLoaded)
                ShowRates();
        }
    }
}
