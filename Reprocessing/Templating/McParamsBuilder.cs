using System;
using Newtonsoft.Json.Linq;
using WMTool.Reprocessing.Models;

namespace WMTool.Reprocessing.Templating
{
    public class McParamsBuilder
    {
        public JObject Build(MasterParameterSet parameters)
        {
            string idInvoice = Get(parameters, "varIDInvoice");
            string serie = Get(parameters, "varcSerie");
            string branchInvoice = Get(parameters, "varcIDBranchInvoice");
            string company = Get(parameters, "cIDCompany");
            string customer = Get(parameters, "varcIDCustomer");
            string order = Get(parameters, "varcIDOrder");
            string trip = Get(parameters, "varcIDTrip");
            string form = Get(parameters, "varcForm");
            string le = Get(parameters, "varcIDLE");
            string li = Get(parameters, "varcIDLI");
            string ld = Get(parameters, "varcIDLD");

            return new JObject
            {
                ["varVersionDevice"] = DateTime.Now.ToString("yyyyMMddHHmmss"),
                ["varcForm"] = form,
                ["varcIDBranchInvoice"] = branchInvoice,
                ["CIDCustomer"] = customer,
                ["varIDInvoice"] = idInvoice,
                ["varcIDLD"] = ld,
                ["CSerie"] = serie,
                ["CIDBranchInvoice"] = branchInvoice,
                ["varcIDLI"] = li,
                ["varcIDCustomer"] = customer,
                ["CIDCompany"] = company,
                ["varcIDTrip"] = trip,
                ["CIDInvoice"] = idInvoice,
                ["varcIDLE"] = le,
                ["varcSerie"] = serie,
                ["NSeq"] = 0,
                ["CForm"] = form,
                ["cancelReason"] = string.Empty,
                ["varcIDOrder"] = order,
                ["CIDOrder"] = order
            };
        }

        private static string Get(MasterParameterSet parameters, string name)
        {
            return parameters.TryGet(name, out string value) ? value ?? string.Empty : string.Empty;
        }
    }
}
