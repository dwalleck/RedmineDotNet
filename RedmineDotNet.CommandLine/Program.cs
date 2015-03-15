using RazorEngine.Templating;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RedmineDotNet.CommandLine
{
    class Program
    {
        static readonly string templateFolderPath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory + "../..", "EmailTemplates");

        static void Main(string[] args)
        {
            var baseUrl = "";
            var timePeriod = "";
            var redmineApiKey = "";
            var projectId = "";
            var mailgunUrl = "";
            var mailgunApiKey = "";
            var mailgunDomain = "";
            var emailFrom = "";
            var emailTo = "";

            RedmineClient client = new RedmineClient(
                baseUrl, redmineApiKey);

            var trackers = client.ListTrackers().Result;
            var bugTracker = trackers.Where(t => t.Name == "Bug").FirstOrDefault();

            var projects = client.ListProjects().Result;
            

            var openIssues = client.ListAllIssues(projectId: projectId, trackerId: bugTracker.Id, statusId: "open").Result;
            var openIssueSummary = new IssueSummary(openIssues);

            var newIssues = client.ListIssues(projectId: projectId, createdOn: timePeriod, trackerId: bugTracker.Id).Result;
            var newIssueSummary = new IssueSummary(newIssues);

            var updatedIssues = client.ListIssues(projectId: projectId, updatedOn: timePeriod, trackerId: bugTracker.Id).Result;
            var updatedIssueSummary = new IssueSummary(updatedIssues);
            
            // This may not be taking into account refused to fix, etc. Need to check
            var closedIssues = client.ListIssues(projectId: projectId, updatedOn: timePeriod, statusId: "closed", trackerId: bugTracker.Id).Result;
            var closedIssueSummary = new IssueSummary(closedIssues);

            var issueReport = new IssueStatusReport
            {
                OpenIssues = openIssueSummary,
                NewIssues = newIssueSummary,
                UpdatedIssues = updatedIssueSummary,
                ClosedIssues = closedIssueSummary,
            };

            var templateFilePath = Path.Combine(templateFolderPath, "BugSummary.cshtml");
            var templateService = new TemplateService();
            var emailHtmlBody = templateService.Parse(File.ReadAllText(templateFilePath), issueReport, null, null);

            RestClient restClient = new RestClient();
            restClient.BaseUrl = new Uri(mailgunUrl);
            restClient.Authenticator = new HttpBasicAuthenticator("api", mailgunApiKey);

            RestRequest request = new RestRequest();
            request.AddParameter("domain",
                mailgunDomain, ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", emailFrom);
            request.AddParameter("to", emailTo);

            request.AddParameter("subject", "Issue Report for week of " + DateTime.Today.ToShortDateString());
            request.AddParameter("html", emailHtmlBody);
            request.Method = Method.POST;
            restClient.Execute(request);
        }

        
    }
}
