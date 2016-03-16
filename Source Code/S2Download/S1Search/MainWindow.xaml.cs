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
        string mgrno, mgrname, crd;
        string advVersion;
        string temp;
        string folder;

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


        private void fetchCompany(string mgrName, string mgrNo, string crd)
        {
            try
            {
                Log("Beginning the fetching process...");

                browser.GoTo("http://www.adviserinfo.sec.gov/IAPD/crd_iapd_AdvVersionSelector.aspx?ORG_PK=" + crd);
                System.Threading.Thread.Sleep(500);


                /*
                browser.GoTo("http://www.adviserinfo.sec.gov/IAPD/Content/Search/iapd_Search.aspx");

                System.Threading.Thread.Sleep(1000);

                // click the firm button
                browser.RadioButton(Find.ById("ctl00_cphMainContent_ucUnifiedSearch_rdoOrg")).Click();

                System.Threading.Thread.Sleep(500);

                // find and fill the search text box
                browser.TextField(Find.ById("ctl00_cphMainContent_ucUnifiedSearch_txtFirm")).TypeText(crd);

                // click the search button
                browser.Button(Find.ById("ctl00_cphMainContent_ucUnifiedSearch_btnFreeFormSearch")).Click();
                System.Threading.Thread.Sleep(1000);

                // Find the type of firm (Investment Adviser Firm or Brokerage Firm or else)
                browser.Link(Find.ByTitle("Link to Form ADV")).Click();
                System.Threading.Thread.Sleep(1000);

                browser.Link(Find.ById("ctl00_cphMainContent_trSECHyperlink")).Click();
                System.Threading.Thread.Sleep(1000);
                */

                string url = browser.Url;
                string replaceUrl = "/Sections/iapd_AdvAdvisoryBusinessSection.aspx";

                // Extract part of the content to find out the rest of ADV forms to download
                string lookUp = "/Sections";
                string lookUpEnd = ".aspx";
                string toBeReplaced;

                int first = url.IndexOf(lookUp, StringComparison.CurrentCultureIgnoreCase);
                int last = url.IndexOf(lookUpEnd, StringComparison.CurrentCultureIgnoreCase) + lookUpEnd.Length;

                if (first > 0 & last >= 0 & last >= first)
                {
                    toBeReplaced = url.Substring(first, last - first);
                    url = url.Replace(toBeReplaced, replaceUrl);
                }
                else
                {
                    url = "www.google.com";
                }

                browser.GoTo(url);

                File.WriteAllText(folder + mgrNo + ".csv", string.Format("{0}\t{1}\t{2}\r\n", mgrName, mgrNo, crd));

                Parsing(url, mgrName, mgrNo, crd);

                //System.Threading.Thread.Sleep(1000);

       

                

                
            }
            catch (Exception e)
            {
                Log("Some error has happened!" + e);
            }
            finally 
            {
                Log("Done!");
            }
            
        }


        private void Parsing(string website, string mgrName, string mgrNo, string crd)
        {
            try
            {

                WebClient client = new WebClient();

                client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

                // Download the URL
                Stream data = client.OpenRead(website);
                StreamReader reader = new StreamReader(data);

                string response = reader.ReadToEnd();

                data.Close();
                reader.Close();

                string source = WebUtility.HtmlDecode(response);

                HtmlDocument resultAt = new HtmlDocument();
                resultAt.LoadHtml(source);

                //Log(this, "Let's get the download data and see what they look like");
                //Log(this, response);

                // Write down the URL

                File.AppendAllText(folder + mgrNo + ".csv", string.Format("{0}\t{1}\t{2}\tURL\t{3}\r\n", mgrName, mgrNo, crd, website));

                // To Parse ADV Version Number (Version 2005, 2012)              
                if (resultAt.DocumentNode.Descendants().Where
                    (x => (x.Name == "td" &&
                        x.InnerText.Contains("Rev.")
                        )).Count() > 0)
                {
                    var Result_1 = resultAt.DocumentNode.Descendants().Where
                    (x => (x.Name == "td" &&
                        x.InnerText.Contains("Rev.")
                        )).Last();

                    File.AppendAllText(folder + mgrNo + ".csv", string.Format("{0}\t{1}\t{2}\tAdvVersion\t{3}\r\n", mgrName, mgrNo, crd, Result_1.InnerText.ToString().Replace("\r", "")
                        .Replace("\n", "")
                        .Replace("\t", "")
                        .Replace("  ", "")
                        .Replace("  ", "")
                        .Replace("  ", "")
                        ));
                }                

                // To Parse Names
                if (resultAt.DocumentNode.Descendants().Where
                    (x => (x.Id == "ctl00_ctl00_cphMainContent_ucADVHeader_lblPrimaryBusinessName"
                        && x.Attributes["class"] != null
                        )).Count() > 0)
                {
                    List<HtmlNode> tNames = resultAt.DocumentNode.Descendants().Where
                                        (x => (x.Id == "ctl00_ctl00_cphMainContent_ucADVHeader_lblPrimaryBusinessName"
                                            && x.Attributes["class"] != null
                                            )).ToList();

                    foreach (var item in tNames)
                    {
                        Log("Company name: " + item.InnerText.ToString());
                        File.AppendAllText(folder + mgrNo + ".csv", string.Format("{0}\t{1}\t{2}\tAdvName\t{3}\r\n", mgrName, mgrNo, crd, item.InnerText.ToString().Replace("\t", "")));
                    }
                }
                

                // Parse Cell (Question E)
                if (resultAt.DocumentNode.Descendants().Where
                    (x => (x.Name == "tr" &&
                        x.InnerText.Contains("Performance-based fees")
                        )).Count() > 0)
                {
                    var Result = resultAt.DocumentNode.Descendants().Where
                                        (x => (x.Name == "tr" &&
                                            x.InnerText.Contains("Performance-based fees")
                                            )).Last();

                    Log("Parsed data, Question E");

                    // Log(this, item.InnerText.ToString());
                    Log(replaceTxt(Result.InnerHtml.ToString()));

                    File.AppendAllText(folder + mgrNo + ".csv", string.Format("{0}\t{1}\t{2}\tQuesE\t{3}\r\n", mgrName, mgrNo, crd,
                        replaceTxt(Result.InnerHtml.ToString())
                     ));
                }
                


                // Parse Table (Question D)
                if (resultAt.DocumentNode.Descendants().Where
                    (x => (x.Name == "tr" && x.Attributes["class"] != null &&
                        x.Attributes["class"].Value.Contains("tableBGAlt")
                        )).Count() > 0)
                {
                    List<HtmlNode> tOfTitle = resultAt.DocumentNode.Descendants().Where
                                        (x => (x.Name == "tr" && x.Attributes["class"] != null &&
                                            x.Attributes["class"].Value.Contains("tableBGAlt")
                                            )).ToList();

                    foreach (var item in tOfTitle)
                    {
                        Log("Parsed data, Question D");

                        // Log(this, item.InnerText.ToString());
                        Log(replaceTxt(item.InnerHtml.ToString()));

                        File.AppendAllText(folder + mgrNo + ".csv", string.Format("{0}\t{1}\t{2}\tQuesD\t{3}\r\n", mgrName, mgrNo, crd,
                            replaceTxt(item.InnerHtml.ToString())
                         ));

                    }
                }
                


                // Parse Infomation (AUM) - Version 2012
                if (resultAt.DocumentNode.Descendants().Where
                    (x => (x.Name == "td" &&
                        x.InnerText.Contains("what is the amount of your regulatory assets under management and total")
                        )).Count() > 0)
                {
                    var Result5 = resultAt.DocumentNode.Descendants().Where
                        (x => (x.Name == "td" &&
                            x.InnerText.Contains("what is the amount of your regulatory assets under management and total")
                            )).Last();

                    HtmlDocument parse3 = new HtmlDocument();
                    parse3.LoadHtml(Result5.InnerHtml);

                    // Parse inner table
                    List<HtmlNode> parse3Items = parse3.DocumentNode.Descendants().Where
                        (x => (x.Name == "td" &&
                            x.InnerText.Contains("$")
                            )).ToList();

                    if (parse3Items.Count <= 0)
                    {
                        //Log(this, "Search returns with no results!");
                    }
                    else
                    {
                        int i = 1;
                        foreach (var item in parse3Items)
                        {

                            File.AppendAllText(folder + mgrNo + ".csv", string.Format("{0}\t{1}\t{2}\tAUM" + i + "\t{3}\r\n", mgrName, mgrNo, crd,
                                 item.InnerText.ToString().Replace("\r", "")
                                .Replace("\n", "")
                                .Replace("\t", "")
                                .Replace("  ", "")
                                .Replace("  ", "")
                                .Replace("  ", "")
                                .Replace(",", "")
                             ));
                            i++;
                        }
                    }
                }


                // Parse Question D for older firms (Versino 2005)
                if (resultAt.DocumentNode.Descendants().Where
                    (x => (x.Name == "tr" &&
                        x.InnerText.Contains("High net worth individuals")
                        )).Count() > 0)
                {
                    var Result2 = resultAt.DocumentNode.Descendants().Where
                        (x => (x.Name == "tr" &&
                            x.InnerText.Contains("High net worth individuals")
                            )).Last();

                    File.AppendAllText(folder + mgrNo + ".csv", string.Format("{0}\t{1}\t{2}\tQuesD1\t{3}\r\n", mgrName, mgrNo, crd, 
                        replaceTxt(Result2.InnerHtml.ToString())
                     ));
                }

                if (resultAt.DocumentNode.Descendants().Where
                           (x => (x.Name == "tr" &&
                               x.InnerText.Contains("Other pooled investment vehicles (e.g., hedge funds)")
                               )).Count() > 0)
                {
                    var Result3 = resultAt.DocumentNode.Descendants().Where
                               (x => (x.Name == "tr" &&
                                   x.InnerText.Contains("Other pooled investment vehicles (e.g., hedge funds)")
                                   )).Last();

                    File.AppendAllText(folder + mgrNo + ".csv", string.Format("{0}\t{1}\t{2}\tQuesD2\t{3}\r\n", mgrName, mgrNo, crd,
                        replaceTxt(Result3.InnerHtml.ToString())
                     ));
                }


                // Parse Infomation (AUM) - Older version 2005
                if (resultAt.DocumentNode.Descendants().Where
                    (x => (x.Name == "tr" &&
                        x.InnerText.Contains("If yes, what is the amount of your assets under management and total number of accounts?")
                        )).Count() > 0)
                {
                    var Result4 = resultAt.DocumentNode.Descendants().Where
                                        (x => (x.Name == "tr" &&
                                            x.InnerText.Contains("If yes, what is the amount of your assets under management and total number of accounts?")
                                            )).Last();

                    //Log(this, "Parsed data, AUM");

                    HtmlDocument parse2 = new HtmlDocument();
                    parse2.LoadHtml(Result4.InnerHtml);

                    // Parse inner table
                    List<HtmlNode> parse2Items = parse2.DocumentNode.Descendants().Where
                        (x => (x.Name == "td" &&
                            x.InnerText.Contains("$")
                            )).ToList();

                    if (parse2Items.Count <= 0)
                    {
                        //Log(this, "Search returns with no results!");
                    }
                    else
                    {
                        int i = 1;
                        foreach (var item in parse2Items)
                        {

                            File.AppendAllText(folder + mgrNo + ".csv", string.Format("{0}\t{1}\t{2}\tAUM" + i + "\t{3}\r\n", mgrName, mgrNo, crd,
                                 item.InnerText.ToString().Replace("\r", "")
                                .Replace("\n", "")
                                .Replace("\t", "")
                                .Replace("  ", "")
                                .Replace("  ", "")
                                .Replace("  ", "")
                                .Replace(",", "")
                             ));
                            i++;
                        }
                    }
                }
                




            }
            catch (Exception e)
            {
                // Network problem
                Log("Error - " + e);
            }
        }

        string replaceTxt(string parseTxt)
        {
            string replaced = parseTxt.Replace("<td style=\"text-align:left\">", "")
                    .Replace("<td class=\"WhiteCenterTD\">", "")
                    .Replace("<td width=\"100%\">", "")
                    .Replace("<td width=\"0\" class=\"WhiteCenterTD\">", "")
                    .Replace("</td>", "")
                    .Replace("<td>", "")
                    .Replace("<i>", "")
                    .Replace("</i>", "")
                    .Replace("\r", "")
                    .Replace("\n", "")
                    .Replace("\t", "")
                    .Replace("  ", "")
                    .Replace("  ", "")
                    .Replace("  ", "")

                    .Replace("<img alt=\"Checkbox checked, changed\" src=\"/Images/crd_pgm_RedlineCheckSelected.gif\"></td>", "YES\t")
                    .Replace("<img src=\"/Iapd/Images/RedlineCheckSelected.gif\" alt=\"\">", "YES\t")
                    .Replace("<img alt=\"Checkbox not checked\" src=\"/Images/crd_pgm_whtchk.gif\">", "NO\t")
                    .Replace("<img src=\"/Iapd/Images/CheckDeselected.gif\" alt=\"\">", "No\t")

                    .Replace("<img alt=\" Radio button not selected\" src=\"/Images/crd_pgm_whtradio.gif\">", "\tNO")
                    .Replace("<img src=\"/Iapd/Images/RadioDeselected.gif\" alt=\"\">", "\tNO")
                    .Replace("<img alt=\" Radio button selected, changed\" src=\"/Images/crd_pgm_RedlineRadioSelected.gif\">", "\tYES")
                    .Replace("<img src=\"/Iapd/Images/RedlineRadioSelected.gif\" alt=\"\">", "\tYES")
                    ;
            return replaced;
        }

        private void buttonStart_Click(object sender, RoutedEventArgs e)
        {
            sourcePath = textboxSourcePath.Text;
            outputFileName1 = textboxOutputPath.Text;
            outputFileName2 = textboxOutputPath2.Text;
            folder = textboxOutputPath2.Text;

         

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
                            if (j == 2) crd = cell.GetFormattedValue();
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
                            fetchCompany(mgrname, mgrno, crd);

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
