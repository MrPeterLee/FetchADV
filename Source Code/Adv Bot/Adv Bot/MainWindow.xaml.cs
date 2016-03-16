using System;
using System.IO;
using System.Net;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Xpf.Core;

namespace Adv_Bot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : DXWindow
    {
        string url;
        string lookUp, lookUpEnd, lookUp1, lookUp2, lookUp3, lookUp4, lookUp5, lookUp6, lookUp7;
        int first, last;
        string toBeReplaced;
        string ans;
        string folder;
        string trimFolder;
        string companyName;
        string active;
        string q41Ans, q42Ans, q51Ans;
        string mgrno;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void FetchClick(object sender, RoutedEventArgs e)
        {
            
            // To clear output listbox
            TxtOutput.Items.Clear();

            // To declare variables
            url = urlText.Text;
            folder = fullDataText.Text;
            trimFolder = trimFolderText.Text;
            mgrno = mgrnoNum.Text.Replace(" ","");

            active = activeTrackBar.Value.ToString();

            Log("Working folder is: " + folder);

            // set url array
            string[] urls = new string[15];
            // Part 1A
            urls[0] = "/Sections/iapd_AdvDisciplinary1ASection.aspx";
            // DRPs
            urls[1] = "/Sections/iapd_AdvDisciplinaryDrpSection.aspx";
            // Item 1 Identifying Information
            urls[2] = "/Sections/iapd_AdvIdentifyingInfoSection.aspx";
            // Item 2 SEC Registration
            urls[3] = "/Sections/iapd_AdvSecRegistrationSection.aspx";
            // Item 3 Form Of Organization
            urls[4] = "/Sections/iapd_AdvFormOfOrgSection.aspx";
            // Item 4 Successions
            urls[5] = "/Sections/iapd_AdvSuccessionsSection.aspx";
            // Item 5 information about your advisory
            urls[6] = "/Sections/iapd_AdvAdvisoryBusinessSection.aspx";
            // Item 6 Other Business Activities
            urls[7] = "/Sections/iapd_AdvOtherBusinessSection.aspx";
            // Item 7 Financial Industry Affiliations and Private Fund Reporting
            urls[8] = "/Sections/iapd_AdvFinancialAffiliationsSection.aspx";
            // Item 7.B Private Fund Reporting
            urls[9] = "/Sections/iapd_AdvPrivateFundReportingSection.aspx";
            // Item 8 Participation or Interest in Client Transactions
            urls[10] = "/Sections/iapd_AdvClientTransSection.aspx";
            // Item 9 Custody
            urls[11] = "/Sections/iapd_AdvCustodySection.aspx";
            // Item 10 Control Persons
            urls[12] = "/Sections/iapd_AdvControlPersonsSection.aspx";
            // Item 11 Disclosure Information
            urls[13] = "/Sections/iapd_AdvDisciplinarySection.aspx";
            // Item 12 Small Businesses
            urls[14] = "/Sections/iapd_AdvSmallBusinessSection.aspx";

            for (int i = 0; i < urls.Length; i++)
            {
                // Extract part of the content to find out the rest of ADV forms to download
                lookUp = "/Sections";
                lookUpEnd = ".aspx";

                first = url.IndexOf(lookUp, StringComparison.CurrentCultureIgnoreCase);
                last = url.IndexOf(lookUpEnd, StringComparison.CurrentCultureIgnoreCase) + lookUpEnd.Length;

                if (first>0 & last>=0 & last>=first)
                {
                    toBeReplaced = url.Substring(first, last - first);
                    urls[i] = url.Replace(toBeReplaced, urls[i]);
                }
                else
                {
                    urls[i] = "www.google.com";
                }

                
            }



            // Start to download URLs
            try
            {
                Log("Now starting to download ADV URLs for company " + mgrno + "...");
                
                //Download url
                for (int i = 0; i < urls.Length; i++)
                {
                    using (WebClient client = new WebClient())
                    {
                        Log("Now downloading from url " + urls[i]);

                        string value = client.DownloadString(urls[i]);

                        File.WriteAllText(folder + mgrno + "Part" + i + ".txt", string.Format("--- Data Download Time: {0} ---\r\n--- ADV Auto Fetch Robot Version 1.0 by Peter ---\r\n\r\n", DateTime.Now) + value);

                        // Extract information from the Item 1 Sheet
                        if (i == 2)
                        {
                            // replace all html breaks for line separators
                            string replaceWith = "";
                            value = value.Replace("\r\n", replaceWith).Replace("\n", replaceWith).Replace("\r", replaceWith);

                            // replace all tabs
                            value = value.Replace("	", "");

                            // replace all double quotes
                            value = value.Replace("\"", "`");

                            FullDataOutput.Text = "Information related to Part 1 of ADV is provided below: " + "\r\n" + value;

                            File.WriteAllText(folder + mgrno + "Part" + i + "NoBlank.txt", string.Format("--- Data Download Time: {0} ---\r\n--- ADV Auto Fetch Robot Version 1.0 by Peter ---\r\n\r\n", DateTime.Now) + value);
                            
                            // Find the name of the company
                            lookUp1 = "<span id=`ctl00_ctl00_cphMainContent_ucADVHeader_lblPrimaryBusinessName` class=`formTextBold`>";
                            lookUp2 = "Primary Business Name";

                            companyName = ".";

                            string selectedChoiceRange = "";
                            int selectedChoice = -1;
                            int first;

                            first = value.IndexOf(lookUp1, StringComparison.CurrentCultureIgnoreCase) + lookUp1.Length;

                            if (first<0)
                            {
                                first = value.IndexOf(lookUp2, StringComparison.CurrentCultureIgnoreCase) + lookUp2.Length;
                            }

                            if (first > 0)
                            {
                                selectedChoiceRange = value.Substring(first, 200);
                            }

                            string ChoiceText1 = "</span>";
                            string ChoiceText2 = "</font>";

                            if (selectedChoiceRange != "")
                            {
                                selectedChoice = selectedChoiceRange.IndexOf(ChoiceText1, StringComparison.CurrentCultureIgnoreCase);

                                if (selectedChoice < 0)
                                {
                                    selectedChoice = selectedChoiceRange.IndexOf(ChoiceText2, StringComparison.CurrentCultureIgnoreCase);
                                }
                            }

                            if (selectedChoice > 0) 
                            {
                                companyName = selectedChoiceRange.Substring(0, selectedChoice);
                            }

                            Log("The company name is: " + companyName);


                        }
                        // Extract information from the Item 5 Sheet
                        if (i == 6)
                        {
                            // replace all html breaks for line separators
                            string replaceWith = "";
                            value = value.Replace("\r\n", replaceWith).Replace("\n", replaceWith).Replace("\r", replaceWith);

                            // replace all spaces
                            value = value.Replace(" ", "");

                            // replace all tabs
                            value = value.Replace("	", "");

                            // replace all double quotes
                            value = value.Replace("\"", "`");

                            FullDataOutput.Text = "Information related to Part 5 of ADV is provided below: " + "\r\n" + value;

                            File.WriteAllText(folder + mgrno + "Part" + i + "NoBlank.txt", string.Format("--- Data Download Time: {0} ---\r\n--- ADV Auto Fetch Robot Version 1.0 by Peter ---\r\n\r\n", DateTime.Now) + value);

                            // Write down the name of the firm
                            File.WriteAllText(trimFolder + mgrno + ".csv", string.Format("AdvName, {0}\r\n", companyName));

                            // Find the answer to "Part D High Net Worth Individuals"
                            lookUp1 = "><i>Highnetworthindividuals</i></td>";
                            lookUp2 = "Highnetworthindividuals</i></td>";
                            ans = ".";

                            ans = lookUpAnswersMCQ(lookUp1, value);

                            if (ans == ".")
                            {
                                ans = lookUpAnswersMCQ(lookUp2, value);
                            }                             
                            
                            q41Ans = ans;

                            File.AppendAllText(trimFolder + mgrno + ".csv", string.Format("highnetworthindividuals, {0}\r\n", ans));

                            // Write down whether the firm is active
                            File.AppendAllText(trimFolder + mgrno + ".csv", string.Format("active, {0}\r\n", active));

                            // Find the answer to "hedge fund clients"
                            lookUp1 = "Pooledinvestmentvehicles(otherthaninvestmentcompanies)</td>";
                            lookUp2 = "Otherpooledinvestmentvehicles(e.g.,hedgefunds)</td>";

                            ans = lookUpAnswersMCQ(lookUp1, value);

                            if (ans == ".")
                            {
                                ans = lookUpAnswersMCQ(lookUp2, value);
                            }

                            File.AppendAllText(trimFolder + mgrno + ".csv", string.Format("hedgefundclients, {0}\r\n", ans));
                            q42Ans = ans;

                            // Find the answer to "performance based fees"
                            lookUp1 = "Performance-basedfees";

                            ans = lookUpAnswersTF(lookUp1, value);
                            
                            if (ans=="1")
                            {
                                q51Ans = "Yes";
                            }
                            else {
                                q51Ans = "No";
                            }

                            File.AppendAllText(trimFolder + mgrno + ".csv", string.Format("performancefees, {0}\r\n", ans));

                        }



                    }
                }


            }
            finally
            {
                Log("Awesome! All parts of the ADV form for " + mgrno + " have been successfully fetched!");
                Log("[Done]");
                
                string activeIndicator;

                if (active == "1")
                {
                    activeIndicator = "Yes";
                }
                else
                {
                    activeIndicator = "No";
                }

                TrimDataOutput.Text = "The Trim Dataset comprises of the following information: " + "\r\n" + "\r\n" +
                                      "  ADV Firm Name: " + companyName  + "\r\n" +
                                      "  Active Indicator: " + activeIndicator + "\r\n" + "\r\n" +
                                      "Questionare responses: " + "\r\n" + 
                                      "  D) 'High Net Worth Individuals' : Choice " + q41Ans + "\r\n" + 
                                      "  D) 'Hedge Fund Clients' : Choice " + q42Ans + "\r\n" + 
                                      "  E) 'Performanced based fees' :" + q51Ans + "\r\n" ;
            }


        }

        public void Log(string sLogLine)
        {
            TxtOutput.Items.Insert(0, DateTime.Now.ToShortDateString() +
                        " " + DateTime.Now.ToShortTimeString() + " Log " + " : " +
                        sLogLine);

            if (TxtOutput.Items.Count > 1000)
            {
                TxtOutput.Items.Clear();
            }

        }

        public string lookUpAnswersMCQ(string lookUpText, string valueContent)
        {

            string selectedChoiceRange = "";
            int selectedChoice = -1;
            int first = valueContent.IndexOf(lookUpText, StringComparison.CurrentCultureIgnoreCase);

            if (first > 0)
            {
                selectedChoiceRange = valueContent.Substring(first, 702);
            }

            string ChoiceText1 = "Radiobuttonselected";
            string ChoiceText2 = "RadioSelected";

            if (selectedChoiceRange != "")
            {
                selectedChoice = selectedChoiceRange.IndexOf(ChoiceText1, StringComparison.CurrentCultureIgnoreCase);

                if (selectedChoice < 0)
                {
                    selectedChoice = selectedChoiceRange.IndexOf(ChoiceText2, StringComparison.CurrentCultureIgnoreCase);
                }
            }
            
            System.Console.WriteLine("The Index is : '{0}'", selectedChoice);

            string answer = ".";

            if (selectedChoice == 67) answer = "1";
            if (selectedChoice == 164) answer = "2";
            if (selectedChoice == 261) answer = "3";
            if (selectedChoice == 358) answer = "4";
            if (selectedChoice == 455) answer = "5";
            if (selectedChoice == 552) answer = "6";
            if (selectedChoice == 649) answer = "7";
            if (selectedChoice >= 17 & selectedChoice <= 107) answer = "1";
            if (selectedChoice >= 114 & selectedChoice <= 207) answer = "2";
            if (selectedChoice >= 211 & selectedChoice <= 304) answer = "3";
            if (selectedChoice >= 308 & selectedChoice <= 401) answer = "4";
            if (selectedChoice >= 405 & selectedChoice <= 498) answer = "5";
            if (selectedChoice >= 502 & selectedChoice <= 595) answer = "6";
            if (selectedChoice >= 599 & selectedChoice <= 702) answer = "7";
            
            Log(lookUpText + " selected choice is: " + answer);

            return answer;
        }

        public string lookUpAnswersTF(string lookUpText, string valueContent)
        {
            string selectedChoiceRange = "";
            int selectedChoice = -1;
            int last = valueContent.IndexOf(lookUpText, StringComparison.CurrentCultureIgnoreCase);

            if (last > 0)
            {
                selectedChoiceRange = valueContent.Substring(last - 108 - lookUpText.Length, 108 + lookUpText.Length);
            }
            
            string ChoiceText1 = "Checkboxchecked";
            string ChoiceText2 = "CheckSelected";

            if (selectedChoiceRange != "")
            {
                selectedChoice = selectedChoiceRange.IndexOf(ChoiceText1, StringComparison.CurrentCultureIgnoreCase);

                if (selectedChoice < 0)
                {
                    selectedChoice = selectedChoiceRange.IndexOf(ChoiceText2, StringComparison.CurrentCultureIgnoreCase);
                }
            }
            
            string answer = "0";

            if (selectedChoice >= 0) answer = "1";

            Log(lookUpText + " selected choice is: " + answer);

            return answer;
        }



    }
}
