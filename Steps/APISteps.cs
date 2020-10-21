using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace PracticaAPI.Steps
{
    [Binding]
    public class APISteps
    {
        RestClient client;
        Dictionary<string, string> userData;
        IRestResponse response;
        string userName;
        string email;
        string pass = "Qwe1234";
        Dictionary<string, object> companyData;
        Dictionary<string, object> dataCreatUser;
        Dictionary<string, string> taskData;
        Dictionary<string, string> searchCompany;

        [Given(@"creat new rest client")]
        public void GivenCreatNewRestClient()
        {
            client = new RestClient("http://users.bugred.ru");
        }

        [Given(@"data for registration is ready")]
        public void GivenDataForRegistrationIsReady()
        {
            Random random = new Random();
            string time = DateTime.Now.ToString();
            string temp;



            temp = time.Replace(":", ".").Replace(" ", "").Replace("/", "");
            userName = temp;
            email = "user" + temp + "@gmail.com";


            userData = new Dictionary<string, string>
            {
                {"name", userName },
                {"email", email },
                {"password", pass }
            };
        }

        [When(@"I send post registration request")]
        public void WhenISendPostRegistrationRequest()
        {
            RestRequest request = new RestRequest("tasks/rest/doregister", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(userData);
            response = client.Execute(request);
        }

        [Then(@"status code request ok")]
        public void ThenStatusCodeRequestOk()
        {
            Assert.AreEqual("OK", response.StatusCode.ToString());
        }

        [Then(@"name from responce equal name from request")]
        public void ThenNameFromResponceEqualNameFromRequest()
        {
            var temp = response.Content;
            JObject json = JObject.Parse(temp);
            Assert.AreEqual(userName, json["name"]?.ToString());
        }

        [Then(@"email from responce equal email from request")]
        public void ThenEmailFromResponceEqualEmailFromRequest()
        {
            var temp = response.Content;
            JObject json = JObject.Parse(temp);
            Assert.AreEqual(email, json["email"]?.ToString());
        }

        //////////////////////////////////////////////////////////////////

        [Given(@"data for registration with invalid email is ready")]
        public void GivenDataForRegistrationWithInvalidEmailIsReady()
        {
            userName = "Santiago";
            email = "zx@z.x";
            userData = new Dictionary<string, string>
            {
                {"name",userName},
                {"email", email},
                {"password", pass}
            };
        }

        [Then(@"the request response contains an error message")]
        public void ThenTheRequestResponseContainsAnErrorMessage()
        {
            var temp = response.Content;
            JObject json = JObject.Parse(temp);
            Assert.AreEqual(" email zx@z.x уже есть в базе", json["message"]?.ToString());
        }

        ////////////////////////////////////////////////////////////////////////////////////

        [Given(@"data about the new company is ready")]
        public void GivenDataAboutTheNewCompanyIsReady()
        {
            companyData = new Dictionary<string, object>
            {
                {"company_name", "Focal"},
                {"company_type", "ООО" },
                {"company_users", new List<string>{ "blacksTest@i.ua" } },
                {"email_owner", "r1@mail.ru" }
            };

        }
        [When(@"I send post company registration request")]
        public void WhenISendPostCompanyRegistrationRequest()
        {
            RestRequest request = new RestRequest("tasks/rest/createcompany", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(companyData);
            response = client.Execute(request);
        }


        [Then(@"the request response contains an type -success")]
        public void ThenTheRequestResponseContainsAnType_Success()
        {
            var temp = response.Content;
            JObject json = JObject.Parse(temp);
            Assert.AreEqual("success", json["type"]?.ToString());
        }

        /////////////////////////////////////////////////////////////////////////


        [Given(@"data about new user is ready")]
        public void GivenDataAboutNewUserIsReady()
        {
            string time = DateTime.Now.ToString();
            string temp;



            temp = time.Replace(":", ".").Replace(" ", "").Replace("/", "").ToLower();
            userName = temp;
            email = "user" + temp + "@gmail.com";


            dataCreatUser = new Dictionary<string, object>
            {
                {"email", email },
                {"name", userName },
                {"tasks",  new List<string>{"12"} } ,
                {"companies",  new List<string>{"1148"} }
            };
        }

        [When(@"I send new user post request")]
        public void WhenISendNewUserPostRequest()
        {
            RestRequest request = new RestRequest("tasks/rest/createuser", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(dataCreatUser);
            response = client.Execute(request);
        }
        /////////////////////////////////////////////////////////////////////////////////////

        [Given(@"data about new user with task is ready")]
        public void GivenDataAboutNewUserWithTaskIsReady()
        {
            string time = DateTime.Now.ToString();
            string temp;



            temp = time.Replace(":", ".").Replace(" ", "").Replace("/", "").ToLower();
            userName = temp;
            email = "user" + temp + "@gmail.com";


            dataCreatUser = new Dictionary<string, object>
            {
                {"email", email },
                {"name", userName },
                {"tasks",  new List<string>{"title","Первая задача","description","Первая задача11" }

                } };
        }

        [When(@"I send post new user with task is ready")]
        public void WhenISendPostNewUserWithTaskIsReady()
        {
            RestRequest request = new RestRequest("tasks/rest/createuserwithtasks", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(userData);
            response = client.Execute(request);
        }

        //////////////////////////////////////////////////////////////////////////////////////

        [Given(@"data about new task is ready")]
        public void GivenDataAboutNewTaskIsReady()
        {

            string time = DateTime.Now.ToString();
            string temp;



            temp = time.Replace(":", ".").Replace(" ", "").Replace("/", "");
            userName = temp;
            email = "user" + temp + "@gmail.com";


            taskData = new Dictionary<string, string>
            {
                {"task_title", userName },
                {"task_description", userName },
                {"email_owner", "blackstest765@i.ua" },
                {"email_assign", "dmytro4556@i.ua" }
            };
        }


        [When(@"I send new post new task request")]
        public void WhenISendNewPostNewTaskRequest()
        {
            RestRequest request = new RestRequest("tasks/rest/createtask", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(taskData);
            response = client.Execute(request);
        }

        [Then(@"the request response contains an type -success and id")]
        public void ThenTheRequestResponseContainsAnType_SuccessAndId()
        {
            var temp = response.Content;
            JObject json = JObject.Parse(temp);
            Assert.AreEqual("success", json["type"].ToString());
            Assert.IsNotNull( json["id_task"].ToString());
        }
        ///////////////////////////////////////////////////////////
        [Given(@"data for company search is prepared")]
        public void GivenDataForCompanySearchIsPrepared()
        {
            searchCompany = new Dictionary<string, string>
            {
                {"query","ABC"},
                {"type","company"}
            };
        }

        [When(@"I am sending a request to search a company")]
        public void WhenIAmSendingARequestToSearchACompany()
        {
            RestRequest request = new RestRequest("tasks/rest/magicsearch", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(searchCompany);
            response = client.Execute(request);
        }

        [Then(@"the response to the request contains the type -success and has the required id")]
        public void ThenTheResponseToTheRequestContainsTheType_SuccessAndHasTheRequiredId()
        {
            var temp = response.Content;
            JObject json = JObject.Parse(temp);
            Assert.AreEqual("success", json["type"].ToString());
            Assert.AreEqual("1093",json["id_company"].ToString());
        }

    }
}
