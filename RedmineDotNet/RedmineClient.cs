using Newtonsoft.Json;
using RedmineDotNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RedmineDotNet
{
    public class RedmineClient
    {
        public HttpClient Client { get; set; }
        public string BaseUrl { get; set; }
        public JsonSerializer Serializer { get; set; }

        public RedmineClient(string baseUrl, string token)
        {
            Client = new HttpClient();
            Client.DefaultRequestHeaders.Add("Accept", "application/json");
            Client.DefaultRequestHeaders.Add("X-Redmine-API-Key", token);
            BaseUrl = baseUrl;
        }

        #region Projects

        public async Task<Projects> ListProjects()
        {
            HttpResponseMessage response = await Client.GetAsync(BaseUrl + "/projects.json");
            response.EnsureSuccessStatusCode();

            var response_body = await response.Content.ReadAsStringAsync();
            Projects projects = JsonConvert.DeserializeObject<Projects>(response_body);
            return projects;
        }

        #endregion

        #region Issues

        public async Task<Issues> ListIssues(string projectId = null, int? offset = null, int? limit = null, string statusId = null, string createdOn = null, string updatedOn = null, string trackerId = null, bool returnAllIssues = false)
        {
            var parameters = new Dictionary<string, string>
            {
                {"project_id", projectId},
                {"offset", offset.ToString()},
                {"limit", limit.ToString()},
                {"status_id", statusId},
                {"created_on", createdOn},
                {"updated_on", updatedOn},
                {"tracker_id", trackerId}
            };

            var usedParameters = parameters
                .Where(f => !String.IsNullOrEmpty(f.Value))
                .ToDictionary(x => x.Key, x => x.Value);

            var queryContent = new FormUrlEncodedContent(usedParameters);
            var queryString = await queryContent.ReadAsStringAsync();
            var requestUrl = String.IsNullOrEmpty(queryString) ? BaseUrl + "/issues.json" : BaseUrl + "/issues.json?" + queryString;

            HttpResponseMessage response = await Client.GetAsync(requestUrl);
            response.EnsureSuccessStatusCode();
            var response_body = await response.Content.ReadAsStringAsync();
            Issues issues = JsonConvert.DeserializeObject<Issues>(response_body);

            return issues;
        }

        public async Task<Issues> ListAllIssues(string projectId = null, int? offset = null, int? limit = null, string statusId = null, string createdOn = null, string updatedOn = null, string trackerId = null)
        {
            var increment = 100;
            offset = 0;
            limit = increment;

            var summaryResult = await ListIssues(projectId: projectId, offset: offset, limit: limit, statusId: statusId, createdOn: createdOn, updatedOn: updatedOn, trackerId: trackerId);

            while (summaryResult.TotalCount > offset + limit)
            {
                offset += increment;
                var result = await ListIssues(projectId: projectId, offset: offset, limit: limit, statusId: statusId, createdOn: createdOn, updatedOn: updatedOn, trackerId: trackerId);
                summaryResult.AllIssues = summaryResult.AllIssues.Concat(result.AllIssues).ToList();
            }
            
            return summaryResult;
        }

        public async Task<Issue> GetIssue(string issueId)
        {
            HttpResponseMessage response = await Client.GetAsync(BaseUrl + "/issues/" + issueId + ".json");
            response.EnsureSuccessStatusCode();
            var response_body = await response.Content.ReadAsStringAsync();
            Issue issue = JsonConvert.DeserializeObject<Issue>(response_body);
            return issue;
        }

        #endregion

        #region trackers

        public async Task<List<Tracker>> ListTrackers()
        {
            HttpResponseMessage response = await Client.GetAsync(BaseUrl + "/trackers.json");
            response.EnsureSuccessStatusCode();

            var response_body = await response.Content.ReadAsStringAsync();
            var trackers = JsonConvert.DeserializeObject<TrackerResponse>(response_body);
            return trackers.Trackers;
        }

        #endregion

        #region issue statuses

        public async Task<List<IssueStatus>> ListIssueStatuses()
        {
            HttpResponseMessage response = await Client.GetAsync(BaseUrl + "/issue_statuses.json");
            response.EnsureSuccessStatusCode();

            var response_body = await response.Content.ReadAsStringAsync();
            var issueStatuses = JsonConvert.DeserializeObject<IssueStatuses>(response_body);
            return issueStatuses.Statuses;
        }
        #endregion
    }
}
