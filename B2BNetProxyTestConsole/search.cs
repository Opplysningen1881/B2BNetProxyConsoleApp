using O1881.PartnerApi;
using System;
using System.Collections.Generic;

namespace B2BNetProxyTestConsole
{
    class search
    {
        public string doSearch(string query)
        {
            List<string> result = new List<string>();

            const string ApiUrl = "https://api.1881bedrift.no/search//";

            //GetResult - list
            var qList = new SearchQuery
            {
                Msisdn = "",
                Password = "",
                UserName = "",
                QueryLevel = QueryLevels.Medium,
                Query = query,
                PageSize = "5",
                Page = "1",
                IncludedCatalogues = new List<string>() { "0", "1" }
            };

            using (var target = new SearchProxy())
            {
                var r = new SearchResponse();

                try { 
                   r = target.GetResult(new Uri(ApiUrl), qList);
                }
                catch (SearchProxyException e)
                {
                    return e.Message;
                }
                
                foreach (ResultItem l in r.Results)
                {
                    string thisresult = string.Format("{0}, {1}, {2}, {3}",
                        l.ResultType,
                        this.getContactPoint(l),
                        l.ResultType.ToString() == "Person" ? this.getPersonName(l) : this.getCompanyName(l),
                        this.getAddress(l));

                    result.Add(thisresult);
                }
            }


            return string.Join("\n", result);
        }

        private string getPersonName(ResultItem ri)
        {
            string name = string.Format("{0} {1}", ri.FirstName, ri.LastName);

            return name;
        }

        private string getCompanyName(ResultItem ri)
        {
            string name = ri.CompanyName;

            return name;
        }

        private string getAddress(ResultItem ri)
        {
            string address = "";

            if (ri.Addresses.Count > 0)
                address = ri.Addresses[0].FormattedAddress;

            return address;
        }

        private string getContactPoint(ResultItem ri)
        {
            string phone = "";

            if (ri.ContactPoints.Count > 0)
                phone = ri.ContactPoints[0].Address;

            return phone;
        }
    }
}
