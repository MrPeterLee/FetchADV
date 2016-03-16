using System;
using System.IO;
using System.Net;

namespace ConsoleApplication1
{
    class Program
    {


        public static string lookUpAnswersMCQ(string lookUpText, string valueContent)
        {
            string selectedChoiceRange = "";
            int selectedChoice = -1;
            int first = valueContent.IndexOf(lookUpText, StringComparison.CurrentCultureIgnoreCase);

            if (first>0) {
                selectedChoiceRange = valueContent.Substring(first, 702);
            }
            
            string ChoiceText1= "Radiobuttonselected";
            string ChoiceText2 = "RadioSelected";

            if (selectedChoiceRange!="") {
                selectedChoice = selectedChoiceRange.IndexOf(ChoiceText1, StringComparison.CurrentCultureIgnoreCase);

                if (selectedChoice<0)
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
            if (selectedChoice >= 17 & selectedChoice <= 107 ) answer = "1";
            if (selectedChoice >= 114 & selectedChoice <= 207) answer = "2";
            if (selectedChoice >= 211 & selectedChoice <= 304) answer = "3";
            if (selectedChoice >= 308 & selectedChoice <= 401) answer = "4";
            if (selectedChoice >= 405 & selectedChoice <= 498) answer = "5";
            if (selectedChoice >= 502 & selectedChoice <= 595) answer = "6";
            if (selectedChoice >= 599 & selectedChoice <= 702) answer = "7";

            System.Console.WriteLine(lookUpText + " selected choice is: '{0}'", answer);

            return answer;
        }

        public static string lookUpAnswersTF(string lookUpText, string valueContent)
        {
            int last = valueContent.IndexOf(lookUpText, StringComparison.CurrentCultureIgnoreCase);
            System.Console.WriteLine("last-108-lookUpText.Length is : '{0}'", last - 108 - lookUpText.Length);

            string selectedChoiceRange = valueContent.Substring(last - 108 - lookUpText.Length, 108 + lookUpText.Length);

            string ChoiceText = "Checkboxchecked";

            int selectedChoice = selectedChoiceRange.IndexOf(ChoiceText, StringComparison.CurrentCultureIgnoreCase);

            System.Console.WriteLine("The Index is : '{0}'", selectedChoice);

            string answer = "0";

            if (selectedChoice >= 0) answer = "1";

            System.Console.WriteLine(lookUpText + " selected choice is: '{0}'", answer);

            return answer;
        }

        static void Main(string[] args)
        {

            string url = "http://www.adviserinfo.sec.gov/iapd/content/viewform/adv112010/Sections/iapd_AdvAdvisoryBusinessSection.aspx?ORG_PK=107966&RGLTR_PK=&STATE_CD=&FLNG_PK=04D66D1C000801550331DF00031388D1056C8CC0";
            string lookUp, lookUpEnd, lookUp1, lookUp2, lookUp3, lookUp4, lookUp5, lookUp6, lookUp7;
            int first, last;
            string toBeReplaced;
            string ans;
            string folder = "D:\\Workspace\\FullData\\";
            string trimFolder = "D:\\Workspace\\TrimData\\";
            string companyName = "ABC Company";

            Console.WriteLine("*** Log Append Tool ***");

            // Extract different URLs
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

                toBeReplaced = url.Substring(first, last - first);

                urls[i] = url.Replace(toBeReplaced, urls[i]);

                // System.Console.WriteLine("URL for '{1}'th element is : '{0}'", urls[i], i.ToString());
            }


            // Start to download URLs
            try
            {
                System.Console.WriteLine("Now starting to download URLs...");
                //Download url
                for (int i = 0; i < urls.Length; i++)
                {
                    using (WebClient client = new WebClient())
                    {
                        System.Console.WriteLine("Now downloading '{0}'", urls[i]); 

                        string value = client.DownloadString(urls[i]);

                        File.WriteAllText(folder + "Part" + i + ".txt", string.Format("--- Data Download Time: {0} ---\r\n--- ADV Auto Fetch Robot Version 1.0 by Peter ---\r\n\r\n", DateTime.Now) + value);

                        // Extract information from the Item 5 Sheet
                        if (i==6)
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

                            File.WriteAllText(folder + "Part" + i + "NoBlank.txt", string.Format("--- Data Download Time: {0} ---\r\n--- ADV Auto Fetch Robot Version 1.0 by Peter ---\r\n\r\n", DateTime.Now) + value);

                            
                            // Find the answer to "Part D High Net Worth Individuals"
                                lookUp = "highnetworthindividuals</i>)</td>";
                                ans = lookUpAnswersMCQ(lookUp, value);
                                File.WriteAllText(trimFolder + companyName + ".csv", string.Format("highnetworthindividuals, {0}\r\n", ans));


                            // Find the answer to "hedge fund clients"
                                lookUp1 = "Pooledinvestmentvehicles(otherthaninvestmentcompanies)</td>";
                                lookUp2 = "Otherpooledinvestmentvehicles(e.g.,hedgefunds)</td>";
                                lookUp2 = "Otherpooledinvestmentvehicles(e.g.,hedgefunds)</td>";

                                ans = lookUpAnswersMCQ(lookUp1, value);
                                                            
                                if (ans==".")
                                {
                                    ans = lookUpAnswersMCQ(lookUp2, value);
                                }

                                File.AppendAllText(trimFolder + companyName + ".csv", string.Format("hedgefundclients, {0}\r\n", ans));

                            // Find the answer to "performance based fees"
                                lookUp1 = "Performance-basedfees";

                                ans = lookUpAnswersTF(lookUp1, value);

                                File.AppendAllText(trimFolder + companyName + ".csv", string.Format("performancefees, {0}\r\n", ans));

                        }


                        
                    }
                }

                
            }
            finally
            {
                Console.WriteLine("[Done]");
                // Keep the console window open in debug mode
                System.Console.WriteLine("Press any key to exit.");
                System.Console.ReadKey();
            }

            
            



        }
    }
}
