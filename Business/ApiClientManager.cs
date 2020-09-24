using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace Business
{
    public static class ApiClientManager
    {

        public static async Task<User> Login(User user)
        {
            UserLoginRequest userLoginRequest = new UserLoginRequest();
            userLoginRequest.Username = user.Username;
            userLoginRequest.Password = user.Password;

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(userLoginRequest), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("http://localhost:59794/api/user/authenticate", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<User>(apiResponse);
                }
            }

            return user;
        }

        public static async Task<List<Student>> GetAllStudents(string token)
        {
            List<Student> students = new List<Student>();

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response = await httpClient.GetAsync("http://localhost:59794/api/StudentDirectory/"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    students = JsonConvert.DeserializeObject<List<Student>>(apiResponse);
                }
            }
            return students;
        }

        public static async Task<Student> GetById(int id, string token)
        {
            Student student = new Student();

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response = await httpClient.GetAsync("http://localhost:59794/api/StudentDirectory/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    student = JsonConvert.DeserializeObject<Student>(apiResponse);
                }
            }
            return student;
        }

        public static async Task<string> Create(Student student, string token)
        {
            Student studentCreated = new Student();
            HttpResponseMessage response = null;
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(student), Encoding.UTF8, "application/json");

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (response = await httpClient.PostAsync("http://localhost:59794/api/StudentDirectory/", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    studentCreated = JsonConvert.DeserializeObject<Student>(apiResponse);
                }
            }
            return response.ReasonPhrase;
        }

        public static async Task<string> Edit(Student student, string token)
        {
            Student studentUpdated = new Student();
            HttpResponseMessage response = null;
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(student), Encoding.UTF8, "application/json");

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (response = await httpClient.PutAsync("http://localhost:59794/api/StudentDirectory/", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    studentUpdated = JsonConvert.DeserializeObject<Student>(apiResponse);
                }
            }
            return response.ReasonPhrase;
        }

        public static async Task<string> Delete(int id, string token)
        {
            bool result = false;
            HttpResponseMessage response = null;

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (response = await httpClient.DeleteAsync("http://localhost:59794/api/StudentDirectory/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    //result = JsonConvert.DeserializeObject<bool>(apiResponse);
                }
            }
            return response.ReasonPhrase;
        }
    }
}
