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
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Layout.Core;
using DevExpress.Xpf.Docking;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Printing;
using System.ComponentModel;
using System.Collections.ObjectModel;
using DevExpress.Xpf.NavBar;
using HtmlAgilityPack;
using System.Net;
using System.IO;
using GemBox.Spreadsheet;
using WatiN.Core;

// Notes
// Add htmlAgilityPack from Nuget
// Add httpClient from Nuget following link: http://www.nuget.org/packages/HttpClient


namespace PeterApp
{
    public partial class MainWindow : DXWindow
    {

        string sourceFilename, outputFileName1, outputFileName2;
        string mgrno, mgrname, crd;
        string advVersion;


        string[] xlsFiles;
        int totalRows = 1;
        string sourcePath;
        private FireFox browser = null;

        private BackgroundWorker bw1 = new BackgroundWorker();

        public MainWindow()
        {
          
            InitializeComponent();
            DataContext = new DataSource();

            bw1.WorkerReportsProgress = true;
            bw1.WorkerSupportsCancellation = true;
            bw1.DoWork += new DoWorkEventHandler(bw1_DoWork);
            bw1.ProgressChanged += new ProgressChangedEventHandler(bw1_ProgressChanged);
            bw1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw1_RunWorkerCompleted);
        }

        private void btnStart3_Click(object sender, RoutedEventArgs e)
        {
            sourcePath = textboxSourcePath.Text;
            outputFileName1 = textboxOutputPath.Text;
            outputFileName2 = textboxOutputPath2.Text;


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

                    // Load excel file
                    var workbook = ExcelFile.Load(file);

                    // Select active worksheet from the file
                    var worksheet = workbook.Worksheets.ActiveWorksheet;

                    // Find total number of columns in the sheet
                    Log(this, "# of Remaining Companies: " + worksheet.Rows.Count.ToString());

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
                        Log(this, "Begin to Fetch Information on Company: " + mgrname + ", MGRNO: " + mgrno);

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
                }
            }

        

        }


        private void fetchCompany(string mgrName, string mgrNo, string crd)
        {

            Log(this, "Beginning the fetching process...");

            browser.GoTo("http://www.adviserinfo.sec.gov/IAPD/crd_iapd_AdvVersionSelector.aspx?ORG_PK=" + crd);
            System.Threading.Thread.Sleep(1000);


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








            System.Threading.Thread.Sleep(100000);








            



        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            // Check if bw1 is NOT running
            if (bw1.IsBusy != true)
            {
                btnStart.Content = "Cancel";
                lProgress.Content = "0%";
                bw1.RunWorkerAsync();
            }
            else
            {
                // BW1 is Running
                btnStart.Content = "Canceling";
                btnStart.IsEnabled = false;
                if (bw1.WorkerSupportsCancellation == true)
                {
                    Log(this, "Preparing to cancel the program...");
                    bw1.CancelAsync();
                }
                else
                {
                    Log(this, "Error: Worker Supports Cancellaiton is not activated");
                }
            }
        }

        private void btnStart2_Click(object sender, RoutedEventArgs e)
        {
               btnStart2.IsEnabled = false;
                // Version 2012
               Parsing("http://www.adviserinfo.sec.gov/iapd/content/viewform/adv/Sections/iapd_AdvAdvisoryBusinessSection.aspx?ORG_PK=10722&RGLTR_PK=&STATE_CD=&FLNG_PK=01A278200008017601058920050582D9056C8CC0");
               // Version 2005
                //Parsing("http://www.adviserinfo.sec.gov/iapd/content/viewform/adv022005/Sections/iapd_AdvAdvisoryBusinessSection.aspx?ORG_PK=110245&RGLTR_PK=&STATE_CD=&FLNG_PK=058D3D800008014C0203E65002872F85056C8CC0");
               //Parsing("http://www.adviserinfo.sec.gov/iapd/content/viewform/adv022005/Sections/iapd_AdvAdvisoryBusinessSection.aspx?ORG_PK=4&RGLTR_PK=&STATE_CD=&FLNG_PK=0128393400080143006FD8D001F9D0E5056C8CC0");

        }

        private void bw1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            for (int i = 1; (i <= 1); i++)
            {
                if ((worker.CancellationPending == true))
                {
                    e.Cancel = true;

                    break;
                }
                else
                {
                    // Report Progress Percentage (1-100%): non-zero goes to progress indicator
                    worker.ReportProgress((i * 10));

                    // Log outputs: 0 + string goes to the log
                    worker.ReportProgress(0, "Testing String " + i * 10);
                }
            }
        }

        private void Parsing(string website)
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
                /*
                // To Parse ADV Version Number 2012
                List<HtmlNode> parseItems = resultAt.DocumentNode.Descendants().Where
                    (x => (x.Id == "ctl00_ctl00_cphMainContent_ucADVHeader_lblVersion"
                        )).ToList();

                foreach (var item in parseItems)
                {
                    Log(this, "ADV Version: " + item.InnerText.ToString());
                }
                */
                // To Parse ADV Version Number 2005 & 2012
                if (resultAt.DocumentNode.Descendants().Where
                    (x => (x.Name == "td" &&
                        x.InnerText.Contains("Rev.")
                        )).Count() > 0)
                {
                    var Result_1 = resultAt.DocumentNode.Descendants().Where
                    (x => (x.Name == "td" &&
                        x.InnerText.Contains("Rev.")
                        )).Last();

                    Log(this, "ADV Version: " + Result_1.InnerText.ToString().Replace("\r", "")
                        .Replace("\n", "")
                        .Replace("\t", "")
                        .Replace("  ", "")
                        .Replace("  ", "")
                        .Replace("  ", "")
                        );
                }

                

                // To Parse Names
                List<HtmlNode> tNames = resultAt.DocumentNode.Descendants().Where
                    (x => (x.Id == "ctl00_ctl00_cphMainContent_ucADVHeader_lblPrimaryBusinessName"
                        && x.Attributes["class"] != null 
                        )).ToList();

                foreach (var item in tNames)
                {
                    Log(this, "Company name: " + item.InnerText.ToString());
                }

                // Parse Table (Question D)
                List<HtmlNode> tOfTitle = resultAt.DocumentNode.Descendants().Where
                    (x => (x.Name == "tr" && x.Attributes["class"] != null &&
                        x.Attributes["class"].Value.Contains("tableBGAlt")
                        )).ToList();

                foreach (var item in tOfTitle)
                {
                    Log(this, "Parsed data, Question D");

                    // Log(this, item.InnerText.ToString());
                    Log(this, item.InnerHtml.ToString().Replace("<td style=\"text-align:left\">", "")
                        .Replace("<td class=\"WhiteCenterTD\">", "")
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
                        .Replace("<img alt=\" Radio button not selected\" src=\"/Images/crd_pgm_whtradio.gif\">", "\tNO")
                        .Replace("<img alt=\" Radio button selected, changed\" src=\"/Images/crd_pgm_RedlineRadioSelected.gif\">", "\tYES")
                        );
                }

                // Parse Cell (Question E)
                var Result = resultAt.DocumentNode.Descendants().Where
                    (x => (x.Name == "tr" &&
                        x.InnerText.Contains("Performance-based fees")                      
                        )).Last();
                
                Log(this, "Parsed data, Question E");

                // Log(this, item.InnerText.ToString());
                Log(this, replaceTxt(Result.InnerHtml.ToString())
                    );
                
                // Parse Question D for older firms
                var Result2 = resultAt.DocumentNode.Descendants().Where
                    (x => (x.Name == "tr" &&
                        x.InnerText.Contains("High net worth individuals")
                        )).Last();

                Log(this, "Parsed data, Question D1");

                // Log(this, item.InnerText.ToString());
                Log(this, replaceTxt(Result2.InnerHtml.ToString()
                    ));

                var Result3 = resultAt.DocumentNode.Descendants().Where
                           (x => (x.Name == "tr" &&
                               x.InnerText.Contains("Other pooled investment vehicles (e.g., hedge funds)")
                               )).Last();

                Log(this, "Parsed data, Question D2");

                // Log(this, item.InnerText.ToString());
                Log(this, replaceTxt(Result3.InnerHtml.ToString()
                    ));
                

                // Parse Infomation (AUM) - Older version 2005
                var Result4 = resultAt.DocumentNode.Descendants().Where
                    (x => (x.Name == "tr" &&
                        x.InnerText.Contains("If yes, what is the amount of your assets under management and total number of accounts?")
                        )).Last();

                Log(this, "Parsed data, AUM");

                HtmlDocument parse2 = new HtmlDocument();
                parse2.LoadHtml(Result4.InnerHtml);

                                // Parse inner table
                List<HtmlNode> parse2Items = parse2.DocumentNode.Descendants().Where
                    (x => (x.Name == "td" &&
                        x.InnerText.Contains("$")
                        )).ToList();

                if (parse2Items.Count<=0) {
                    Log(this, "Search returns with no results!");
                }
                else {
                    int i = 1;
                    foreach (var item in parse2Items)
                    {

                        // Log(this, item.InnerText.ToString());
                        Log(this, "AUM " + i + " :" + item.InnerText.ToString().Replace("\r", "")
                        .Replace("\n", "")
                        .Replace("\t", "")
                        .Replace("  ", "")
                        .Replace("  ", "")
                        .Replace("  ", "")
                            );
                        i++;
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

                    Log(this, "Parsed data, AUM, version 2012");

                    HtmlDocument parse3 = new HtmlDocument();
                    parse3.LoadHtml(Result5.InnerHtml);

                    // Parse inner table
                    List<HtmlNode> parse3Items = parse3.DocumentNode.Descendants().Where
                        (x => (x.Name == "td" &&
                            x.InnerText.Contains("$")
                            )).ToList();

                    if (parse3Items.Count <= 0)
                    {
                        Log(this, "Search returns with no results!");
                    }
                    else
                    {
                        int i = 1;
                        foreach (var item in parse3Items)
                        {

                            // Log(this, item.InnerText.ToString());
                            Log(this, "AUM " + i + " :" + item.InnerText.ToString().Replace("\r", "")
                            .Replace("\n", "")
                            .Replace("\t", "")
                            .Replace("  ", "")
                            .Replace("  ", "")
                            .Replace("  ", "")
                            .Replace(",", "")
                                );
                            i++;
                        }
                    }

                }
                //If yes, what is the amount of your regulatory assets under management and total
                //        number of accounts?


            }
            catch (Exception e)
            {
                // Network problem
                Log(this, "Error - " + e);
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
                    .Replace("<img src=\"/Iapd/Images/CheckDeselected.gif\" alt=\"\">", "NO\t")

                    .Replace("<img alt=\" Radio button not selected\" src=\"/Images/crd_pgm_whtradio.gif\">", "\tNO")
                    .Replace("<img src=\"/Iapd/Images/RadioDeselected.gif\" alt=\"\">", "\tNO")
                    .Replace("<img alt=\" Radio button selected, changed\" src=\"/Images/crd_pgm_RedlineRadioSelected.gif\">", "\tYES")
                    .Replace("<img src=\"/Iapd/Images/RedlineRadioSelected.gif\" alt=\"\">", "\tYES")
                    ;
            return replaced;
        }

        private void bw1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ((e.Cancelled == true))
            {
                Log(this, "Canceled!");
                this.btnStart.Content = "Start";
                this.btnStart.IsEnabled = true;
            }

            else if (!(e.Error == null))
            {
                Log(this, ("Error: " + e.Error.Message));
                this.btnStart.IsEnabled = true;
            }

            else
            {
                Log(this, ("Done!"));
                this.btnStart.Content = "Start";
                this.btnStart.IsEnabled = true;
            }
        }

        private void bw1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //this.tbOutput.AppendText(e.ProgressPercentage.ToString() + "%" + "\r\n" );
            
            // Check if this is a log
            if (e.ProgressPercentage!=0)
            {
                this.lProgress.Content = (e.ProgressPercentage.ToString() + "%");
            }
            else
            {
                Log(this, e.UserState.ToString());
            }
            
            
        }

        void Log(object sender, string txt)
        {
            if (tbOutput.LineCount > 20000)
            {
                tbOutput.Clear();
            } 
            this.tbOutput.AppendText(DateTime.Now.ToShortDateString() +
                        " " + DateTime.Now.ToShortTimeString() + " : " + txt + "\r\n");
            this.tbOutput.ScrollToEnd();
            
        }

        private void bNavigation_ItemClick(object sender, ItemClickEventArgs e)
        {
            dockLayoutManager.DockController.Restore(lpNavigation);
            dockLayoutManager.DockController.Activate(lpNavigation);
        }

        private void bTools_ItemClick(object sender, ItemClickEventArgs e)
        {
            dockLayoutManager.DockController.Restore(lpTools);
            dockLayoutManager.DockController.Activate(lpTools);
        }

        private void bPanels_ItemClick(object sender, ItemClickEventArgs e)
        {
            dockLayoutManager.DockController.Restore(dpPanel1);

            dockLayoutManager.DockController.Activate(dpPanel1);
        }

        private void bOutput_ItemClick(object sender, ItemClickEventArgs e)
        {
            dockLayoutManager.DockController.Restore(lpOutput);
            dockLayoutManager.DockController.Activate(lpOutput);
        }

        private void bExit_ItemClick(object sender, ItemClickEventArgs e)
        {
            Application.Current.Shutdown();
        }

    


    }

    public class TestData
    {
        public string Text { get; set; }
        public int Number { get; set; }
    }

    public class TestDataViewModel : INotifyPropertyChanged
    {
        TestData data;
        public TestDataViewModel()
        {
            data = new TestData() { Text = string.Empty, Number = 0 };
        }
        public string Text
        {
            get { return Data.Text; }
            set
            {
                if (Data.Text == value)
                    return;
                Data.Text = value;
                RaisePropertyChanged("Text");
            }
        }
        public int Number
        {
            get { return Data.Number; }
            set
            {
                if (Data.Number == value)
                    return;
                Data.Number = value;
                RaisePropertyChanged("Number");
            }
        }
        protected TestData Data
        {
            get { return data; }
        }
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }
        protected void RaisePropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }

    public class DataSource
    {
        ObservableCollection<TestDataViewModel> source;
        public DataSource()
        {
            source = CreateDataSource();
        }
        protected ObservableCollection<TestDataViewModel> CreateDataSource()
        {
            ObservableCollection<TestDataViewModel> res = new ObservableCollection<TestDataViewModel>();
            res.Add(new TestDataViewModel() { Text = "Row0", Number = 0 });
            res.Add(new TestDataViewModel() { Text = "Row1", Number = 1 });
            res.Add(new TestDataViewModel() { Text = "Row2", Number = 2 });
            res.Add(new TestDataViewModel() { Text = "Row3", Number = 3 });
            res.Add(new TestDataViewModel() { Text = "Row4", Number = 4 });
            res.Add(new TestDataViewModel() { Text = "Row5", Number = 5 });
            res.Add(new TestDataViewModel() { Text = "Row6", Number = 6 });
            res.Add(new TestDataViewModel() { Text = "Row7", Number = 7 });
            res.Add(new TestDataViewModel() { Text = "Row8", Number = 8 });
            res.Add(new TestDataViewModel() { Text = "Row9", Number = 9 });
            return res;
        }
        public ObservableCollection<TestDataViewModel> Data { get { return source; } }
    }
}
