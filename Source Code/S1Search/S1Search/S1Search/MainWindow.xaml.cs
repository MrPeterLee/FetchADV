using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Layout.Core;
using DevExpress.Xpf.Docking;
using WatiN.Core;
using HtmlAgilityPack;
using System.Data.OleDb;
using System.IO;
using System.Data;
using GemBox.Spreadsheet;
using System.Net;
using System.Threading;
using System.ComponentModel;

namespace S1Search
{
    public partial class MainWindow : Window
    {
        string sourceFilename, outputFileName1, outputFileName2;
        string mgrno, mgrname, rdate;
        string[] xlsFiles;
        int totalRows=1;
        string sourcePath;
        private FireFox browser = null;

        private BackgroundWorker bw = new BackgroundWorker();

        public MainWindow()
        {
            InitializeComponent();

            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);

            browser = new FireFox();
        }




 





        public void Log(string sLogLine)
        {
            /*
            TxtOutput.Items.Insert(0, DateTime.Now.ToShortDateString() +
                        " " + DateTime.Now.ToShortTimeString() + " Log " + " : " +
                        sLogLine);

            if (TxtOutput.Items.Count > 1000)
            {
                TxtOutput.Items.Clear();
            }
             * */

        }

        private void btnOpenSourcePath_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension
            dlg.DefaultExt = ".xls";
            dlg.Filter = "Excel 2003 Files (*.xls)|*.xls"; 

            // Display openFileDialog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected filename and display in a textbox
            if (result == true)
            {
                sourceFilename = dlg.FileName;
                textboxSourcePath.Text = dlg.FileName;
            }
            
        }

        private void btnOpenOutputPath_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension
            dlg.DefaultExt = ".xls";
            dlg.Filter = "Excel XLS Files (*.xls)|*.xls"; 

            // Display openFileDialog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected filename and display in a textbox
            if (result == true)
            {
                textboxOutputPath.Text = dlg.FileName;
            }
        }

        private void btnOpenOutputPath2_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension
            dlg.DefaultExt = ".xls";
            dlg.Filter = "Excel XLS Files (*.xls)|*.xls"; 

            // Display openFileDialog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected filename and display in a textbox
            if (result == true)
            {
                textboxOutputPath2.Text = dlg.FileName;
            }
        }


        private void fetchCompany(string mgrName, string mgrNo, string rDate)
        {
            try
            {
                Log("Beginning the fetching process...");
                
                browser.GoTo("http://www.adviserinfo.sec.gov/IAPD/Content/Search/iapd_Search.aspx");

                System.Threading.Thread.Sleep(1000);

                // click the firm button
                browser.RadioButton(Find.ById("ctl00_cphMainContent_ucUnifiedSearch_rdoOrg")).Click();

                System.Threading.Thread.Sleep(500);

                // find and fill the search text box
                browser.TextField(Find.ById("ctl00_cphMainContent_ucUnifiedSearch_txtFirm")).TypeText(mgrName);

                // click the search button
                browser.Button(Find.ById("ctl00_cphMainContent_ucUnifiedSearch_btnFreeFormSearch")).Click();
                System.Threading.Thread.Sleep(1000);

                // Find the type of firm (Investment Adviser Firm or Brokerage Firm or else)
                var searchItems = browser.Div(Find.ById("ctl00_cphMainContent_pnlOrgResults")).ListItems;

                string[] typeOfFirm = new string[100];
                
                Log("search count is :" + searchItems.Count);

                if (searchItems.Count > 0)
                {
                    for (int k = 0; k <= searchItems.Count - 1; k++)
                    {
                        Log("Firm # " + k.ToString() + " has a type of: " + searchItems[k].Text);
                        typeOfFirm[k] = searchItems[k].Text;
                        typeOfFirm[k] = typeOfFirm[k].Replace("\r\n", "").Replace("\n", "").Replace("\r", "");

                    }

                    // Find the url of firm (Investment Adviser Firm or Brokerage Firm or else)
                    searchItems = browser.Table(Find.ById("ctl00_cphMainContent_pnlOrgResults")).ListItems;

                    string[] urlOfFirm = new string[100];

                    if (searchItems.Count > 0)
                    {
                        for (int k = 0; k <= searchItems.Count - 1; k++)
                        {
                            Log("Firm # " + k.ToString() + " has a type of: " + searchItems[k].Text);
                            typeOfFirm[k] = searchItems[k].Text;
                            typeOfFirm[k] = typeOfFirm[k].Replace("\r\n", "").Replace("\n", "").Replace("\r", "");

                        }
                    }

                    // Begin parsing the document
                    int[] idx = new int[100];
                    for (int i = 0; i < 100; i++)
                    {
                        // to hold the begining position of the string
                        idx[i] = 1;

                    }

                    string[] idxText = new string[100];
                    string[] firmNames = new string[100];
                    string[] firmCRDs = new string[100];
                    for (int i = 0; i < 100; i++)
                    {
                        // to take down the content after the beginning
                        idxText[i] = ".";
                        firmNames[i] = ".";
                        firmCRDs[i] = ".";
                    }

                    Log("Check point 1");

                    string value = browser.Html.ToString();
                    // replace all html breaks for line separators
                    string replaceWith = "";
                    value = value.Replace("\r\n", replaceWith).Replace("\n", replaceWith).Replace("\r", replaceWith);
                    value = value.Replace("\"", "'");
                    value = value.Replace("\t", "");

                    Log("Check point 2");
                    // Locate the firm names
                    string lookup1 = "<td align='left' valign='top'><b>";   // beginning of firm name
                    string lookup2 = "</b>";        // ending of firm name
                    string lookup3 = "(";           // beginning of CRD number
                    string lookup4 = ")";           // ending of CRD number

                    for (int i = 0; i < 100; i++)
                    {
                        idx[i] = value.IndexOf(lookup1);
                        if (idx[i] > 0)
                        {
                            idxText[i] = value.Substring(idx[i] + lookup1.Length, 150);
                            value = value.Substring(idx[i] + lookup1.Length + idxText[i].Length, value.Length - (idx[i] + lookup1.Length + idxText[i].Length));

                            firmNames[i] = idxText[i].Substring(0, idxText[i].IndexOf(lookup2));
                            firmCRDs[i] = idxText[i].Substring(idxText[i].IndexOf(lookup3) + 1, idxText[i].IndexOf(lookup4) - idxText[i].IndexOf(lookup3) - 1);

                            Log("idx is :" + idx[i] + ", firmName is: " + firmNames[i] + ", CRD # is: " + firmCRDs[i]);

                            File.AppendAllText(outputFileName1, mgrNo + "\t" + rDate + "\t" + mgrName + "\t" + firmNames[i] + "\t" + firmCRDs[i] + "\t" + typeOfFirm[i]);
                            File.AppendAllText(outputFileName1, "\r\n");
                        }
                    }
                }
                else
                {
                    Log("This firm returns no results");
                    File.AppendAllText(outputFileName2, mgrNo + "\t" + rDate + "\t" + mgrName);
                    File.AppendAllText(outputFileName2, "\r\n");
                }           



                
            }
            catch (Exception ex)
            {
                Log("Some error has happened!");
            }
            finally 
            {
                Log("Done!");
            }
            
        }

        private void buttonStart_Click(object sender, RoutedEventArgs e)
        {
            sourcePath = textboxSourcePath.Text;
            outputFileName1 = textboxOutputPath.Text;
            outputFileName2 = textboxOutputPath2.Text;


            if (bw.IsBusy != true)
            {
                bw.RunWorkerAsync();
            }
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            if (bw.WorkerSupportsCancellation == true)
            {
                bw.CancelAsync();
            }
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");

            // Get file names within the source data folder
            sourcePath = sourcePath.Replace(@"\", @"\\");

            xlsFiles = Directory.GetFiles(sourcePath, "*.xls");

            // Start to deal with each individual file
            foreach (string file in xlsFiles)
            {
                totalRows = 1;

                while (totalRows >= 1)
                {

                    if ((worker.CancellationPending == true))
                    {
                        e.Cancel = true;
                        break;
                    }
                    else
                    {
                        // Load excel file
                        var workbook = ExcelFile.Load(file);

                        // Select active worksheet from the file
                        var worksheet = workbook.Worksheets.ActiveWorksheet;

                        // Find total number of columns in the sheet
                        Log("# of Remaining Companies: " + worksheet.Rows.Count.ToString());

                        totalRows = worksheet.Rows.Count;

                        // Select the first row from the worksheet.
                        var row = worksheet.Rows[0];

                        // Write every cell of the first row to Output.
                        int j = 0;
                        foreach (var cell in row.AllocatedCells)
                        {
                            if (j == 0) mgrno = cell.GetFormattedValue();
                            if (j == 1) mgrname = cell.GetFormattedValue();
                            if (j == 2) rdate = cell.GetFormattedValue();
                            j++;
                        }


                        // check if the current row is the Variable Name
                        if (mgrno == "mgrno" | mgrno == "")
                        {
                            // Delete this row from excel
                            worksheet.Rows[0].Delete();
                        }
                        else
                        {
                            Log("Begin to Fetch Information on Company: " + mgrname + ", MGRNO: " + mgrno);

                            // Begin to fetch this company
                            fetchCompany(mgrname, mgrno, rdate);

                            // Delete this row from excel
                            worksheet.Rows[0].Delete();
                        }

                        if (totalRows == 1)
                        {
                            File.Delete(file);
                        }
                        else
                        {
                            // Save the file in XLS format.
                            workbook.Save(file);
                        }

                        totalRows--;




                        // Perform a time consuming operation and report progress.

                        System.Threading.Thread.Sleep(10);
                        worker.ReportProgress((totalRows));
                    }
                }
            }

        }


        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ((e.Cancelled == true))
            {
                this.tbProgress.Text = "Canceled!";
            }

            else if (!(e.Error == null))
            {
                this.tbProgress.Text = ("Error: " + e.Error.Message);
            }

            else
            {
                this.tbProgress.Text = "Done!";
            }
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.tbProgress.Text = ((100 - e.ProgressPercentage).ToString() + "% of current xls");
        }

    }


}
