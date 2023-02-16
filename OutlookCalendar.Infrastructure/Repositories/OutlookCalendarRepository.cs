using Nancy.Json;
using OutlookCalendar.Domain.Core.Models;
using OutlookCalendar.Domain.Core.Repositories;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;

namespace OutlookCalendar.Infaestructure.Repositories
{
    public class OutlookCalendarRepository : IOutlookCalendarRequestRepository
    {
        private readonly IOutlookCalendarConfiguration _config;

        private const string UrlBase = "https://graph.microsoft.com/v1.0/";
        public OutlookCalendarRepository(IOutlookCalendarConfiguration config) {
            _config = config;
        }

        private async Task<T> GetResponse<T>(string uri, string method)
        {
            return await GetResponse<T>(uri, method, new Chilkat.JsonObject());
        }

        private Chilkat.JsonObject EventAsSendData(OutlookEvent @event)
        {

            // Build the JSON body of the POST.
            Chilkat.JsonObject json = new Chilkat.JsonObject();
            json.UpdateString("subject", @event.Description ?? String.Empty);
            json.UpdateString("body.contentType", "HTML");
            json.UpdateString("body.content", @event.Description ?? String.Empty);
            json.UpdateString("start.dateTime", @event.Start_time.ToUniversalTime().ToString("s") + "Z");
            json.UpdateString("start.timeZone", "Pacific Standard Time");
            json.UpdateString("end.dateTime", @event.End_time.ToUniversalTime().ToString("s") + "Z");
            json.UpdateString("end.timeZone", "Pacific Standard Time");
            json.UpdateString("location.displayName", @event.Name);
            json.UpdateString("attendees[0].emailAddress.address", "samanthab@contoso.onmicrosoft.com");
            json.UpdateString("attendees[0].emailAddress.name", @event.Name);
            json.UpdateString("attendees[0].type", "required");
            json.UpdateBool("allowNewTimeProposals", true);

            // Generate a UUID.
            Chilkat.Crypt2 crypt = new Chilkat.Crypt2();
            json.UpdateString("transactionId", crypt.GenerateUuid());

            return json;
        }

        private async Task<T> GetResponse<T>(string uri, string method, Chilkat.JsonObject data)
        {
            //Obtiene el tokean
            Chilkat.JsonObject jsonToken =  getToken();

            string responseText = string.Empty;

            Chilkat.Http http = new Chilkat.Http();

            http.AuthToken = jsonToken.StringOf("access_token");

            Chilkat.HttpResponse resp;

            if (data != null)
            {
                // Add the "Prefer" request header.
                http.SetRequestHeader("Prefer", "outlook.timezone=\"Pacific Standard Time\"");
                resp = http.PostJson2(uri, "application/json", data.Emit());
            }
            else
            {
                resp = http.QuickRequest(method, uri);
            }

            if (http.LastMethodSuccess != true)
            {
                Debug.WriteLine(http.LastErrorText);
                return new JavaScriptSerializer().Deserialize<T>(responseText);
            }

            Debug.WriteLine("Response status code = " + Convert.ToString(resp.StatusCode));

            // The HTTP request succeeded if the response status code = 200.
            if (resp.StatusCode != 200)
            {
                Debug.WriteLine(resp.BodyStr);
                Debug.WriteLine("Failed");

                return new JavaScriptSerializer().Deserialize<T>(responseText);
            }

            Chilkat.JsonObject json = new Chilkat.JsonObject();
            //json.Load(resp.BodyStr);
            //json.EmitCompact = false;
            //Debug.WriteLine(json.Emit());

            using (var reader = new StreamReader(resp.BodyStr))
            {
                responseText = reader.ReadToEnd();
            }
            return new JavaScriptSerializer().Deserialize<T>(responseText);
        }

        public async Task<string> CreateEvent(OutlookEvent @event, string calendarId)
        {
            var sendData = EventAsSendData(@event);
            var receivedData = await GetResponse<OutlookEvent>(UrlBase + calendarId + "/me/events", "POST", sendData);
            return receivedData.Id;
        }

        public async Task<string> UpdateEvent(OutlookEvent @event)
        {
            var sendData = EventAsSendData(@event);
            var receivedData = await GetResponse<OutlookEvent>(UrlBase + @event.Id, "PUT", sendData);
            return receivedData.Id.ToString();
        }

        public async Task DeleteEvent(string eventId)
        {
            Chilkat.StringBuilder sbUrl = new Chilkat.StringBuilder();
            sbUrl.Append(UrlBase + "me/events/");
            sbUrl.Append(eventId);

            await GetResponse<object>(sbUrl.GetAsString(), "DELETE");
        }

        public Chilkat.JsonObject getToken()
        {
            Chilkat.OAuth2 oauth2 = new Chilkat.OAuth2();
            oauth2.ListenPort = _config.OAuth2Model.ListenPort;

            oauth2.AuthorizationEndpoint = _config.OAuth2Model.AuthorizationEndpoint;
            oauth2.TokenEndpoint = _config.OAuth2Model.TokenEndpoint;

            // Replace these with actual values.
            oauth2.ClientId = _config.OAuth2Model.ClientId;
            // This is your app password:
            oauth2.ClientSecret = _config.OAuth2Model.ClientSecret;

            oauth2.CodeChallenge = false;

            oauth2.Scope = _config.OAuth2Model.Scope;

            // Begin the OAuth2 three-legged flow.  This returns a URL that should be loaded in a browser.
            string url = oauth2.StartAuth();
            if (oauth2.LastMethodSuccess != true)
            {
                Debug.WriteLine(oauth2.LastErrorText);
                return null;
            }
            int numMsWaited = 0;
            while ((numMsWaited < 30000) && (oauth2.AuthFlowState < 3))
            {
                oauth2.SleepMs(100);
                numMsWaited = numMsWaited + 100;
            }

            if (oauth2.AuthFlowState < 3)
            {
                oauth2.Cancel();
                Debug.WriteLine("No response from the browser!");
                return null;
            }

            if (oauth2.AuthFlowState == 5)
            {
                Debug.WriteLine("OAuth2 failed to complete.");
                Debug.WriteLine(oauth2.FailureInfo);
                return null;
            }

            if (oauth2.AuthFlowState == 4)
            {
                Debug.WriteLine("OAuth2 authorization was denied.");
                Debug.WriteLine(oauth2.AccessTokenResponse);
                return null;
            }

            if (oauth2.AuthFlowState != 3)
            {
                Debug.WriteLine("Unexpected AuthFlowState:" + Convert.ToString(oauth2.AuthFlowState));
                return null;
            }

            Debug.WriteLine("OAuth2 authorization granted!");
            Debug.WriteLine("Access Token = " + oauth2.AccessToken);

            // Get the full JSON response:
            Chilkat.JsonObject json = new Chilkat.JsonObject();
            json.Load(oauth2.AccessTokenResponse);
            json.EmitCompact = false;

            return json;
        }

        public async Task<ReceivedCalendarsData> GetOutlookCalendars()
        {
            return await GetResponse<ReceivedCalendarsData>(UrlBase + "me/calendars", "GET");
        }
    }
}
