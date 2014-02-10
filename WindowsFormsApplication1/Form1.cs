using System;
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
        bool formIsLoaded = false; // is used not to initiate some control events before the form is completely loaded
        public Form1()
        {
            InitializeComponent();
        }

        // method that is called when buttonConvert event Click is initiated
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

        // method that is called when the form load event is initiated
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                ServiceReference1.ObjectsList ObjectsList = new ServiceReference1.ObjectsList();
                // get all currencies IDs
                ObjectsList = client.GetCurrencyCodes();
                // get the last listed date
                string lastDateStr = client.GetLastDate();
                DateTime lastDate = DateTime.Parse(lastDateStr);
                // set the last listed date for both start and end date
                dateTimePicker2.Value = lastDate;
                dateTimePicker3.Value = lastDate;
                // fill the combobox with all currencies IDs + "All" for all currencies IDs to be involved 
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
                // fill dates list with all dates for chosen period
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

        // method that is called when the data in comboBoxSource is changed
        private void comboBoxSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ServiceReference1.ObjectsList ObjectsList = new ServiceReference1.ObjectsList();
                // get all currencies IDs
                ObjectsList = client.GetCurrencyCodes();
                comboBoxTarget.Items.Clear();
                // fill in comboBoxTarget with currencies IDs, except ID chosen in comboBoxSource to avoid the possibility
                // to choose the same currency ID so the conversion will have no sense
                foreach (string id in ObjectsList.Objects)
                {
                    if (id != comboBoxSource.SelectedItem.ToString())
                        comboBoxTarget.Items.Add(id);
                }
                comboBoxTarget.Text = comboBoxTarget.Items[0].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // method checks the dates chosen in dateTimePicker2 and dateTimePicker3, 
        // to avoid incorrect behaviour of the program and returns the corretly set dates
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

        // method checks the ComboBoxCurrencies to return the correctly set currency ID
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

        // method gets currency rated for chosen period
        private ServiceReference1.CurrencyRatesPerDateTypeList GetRates()
        {
            string startDateStr, endDateStr;
            string currency;
            CheckDates(out startDateStr, out endDateStr);
            CheckComboBoxCurrencies(out currency);
            return client.GetRates(startDateStr, endDateStr, currency);
        }

        // method checks if specified date is listed
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

        // method fills datesList with all dates for chosen period
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

        // method is called when there is another data chosen in datesList and
        // fills the ratesList according to the chosen date
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

        // method is called when buttonCSVExport Click event is initiated, 
        //gets the string to be saved as .CSV file, 
        //opens the dialog for the file to be saved on the local computer
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

        // method fills datesList everytime the dateTimePicker2 value is changed
        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (formIsLoaded)
                ShowRates();
        }

        // method fills datesList everytime the dateTimePicker3 value is changed
        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            if (formIsLoaded)
                ShowRates();
        }

        // method fills datesList everytime the comboBoxCurrencies value is changed
        private void comboBoxCurrencies_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (formIsLoaded)
                ShowRates();
        }
    }
}
