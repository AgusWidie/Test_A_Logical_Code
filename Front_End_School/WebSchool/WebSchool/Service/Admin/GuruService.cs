﻿using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using WebSchool.Configuration;
using WebSchool.Models;
using WebSchool.Service.Interface;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace WebSchool.Service
{
    public class GuruService : IGuruService
    {
        private readonly ApiConfiguration _apiConfig;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        public GuruService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _apiConfig = configuration.GetSection(nameof(ApiConfiguration)).Get<ApiConfiguration>();
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<GlobalObjectListResponseJson<Guru>> CheckAddGuru(CheckGuruModel model, CancellationToken cancellationToken)
        {
            GlobalResponseJson res = new GlobalResponseJson();

            string Token = "";
            var httpWebRequestCheckToken = (HttpWebRequest)WebRequest.Create(_apiConfig.BaseUrl + _apiConfig.UriCheckToken);
            httpWebRequestCheckToken.ContentType = "application/json";
            httpWebRequestCheckToken.Method = "POST";

            CheckTokenModel modelApiKey = new CheckTokenModel();
            modelApiKey.ApiKey = _apiConfig.ApiKey;
            //modelApiKey.NamaPengguna = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            modelApiKey.NamaPengguna = _session.GetString("UserName");
            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var jsonApiKey = JsonSerializer.Serialize(modelApiKey, options);
            using (var streamWriter = new StreamWriter(httpWebRequestCheckToken.GetRequestStream()))
            {
                streamWriter.Write(jsonApiKey);
                streamWriter.Close();
                streamWriter.Dispose();
            }

            var responseCheckToken = httpWebRequestCheckToken.GetResponse();
            var streamCheckToken = responseCheckToken.GetResponseStream();
            var srCheckToken = new StreamReader(streamCheckToken);
            var contentCheckToken = srCheckToken.ReadToEnd();

            GlobalObjectListResponseJson<TokenResponse> resDataToken = JsonSerializer.Deserialize<GlobalObjectListResponseJson<TokenResponse>>(contentCheckToken, options);
            if (resDataToken.Data.Any())
            {
                foreach (var data in resDataToken.Data)
                {
                    Token = data.Token;
                }
            }
            else
            {
                string jsonString =
                                    @"{
                                      ""code"": 401,
                                      ""error"": true,
                                      ""message"": ""Unauthorization"",
                                      ""data"": null
                                    }";
                GlobalObjectListResponseJson<Guru> resUnauthorization = JsonSerializer.Deserialize<GlobalObjectListResponseJson<Guru>>(jsonString, options);
                return resUnauthorization;
            }

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(_apiConfig.BaseUrl + _apiConfig.UriCheckAddGuru);
            httpWebRequest.Headers.Add("Authorization", "Bearer " + Token + "");
            httpWebRequest.Headers.Add("Strict-Transport-Security", "max-age=31536000");
            httpWebRequest.Headers.Add("X-Content-Type-Options", "nosniff");
            httpWebRequest.Headers.Add("X-Xss-Protection", "1; mode=block");
            httpWebRequest.Headers.Add("X-Frame-Options", "SAMEORIGIN");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            var json = JsonSerializer.Serialize(model, options);

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {

                streamWriter.Write(json);
                streamWriter.Close();
                streamWriter.Dispose();
            }

            var response = httpWebRequest.GetResponse();
            var stream = response.GetResponseStream();
            var sr = new StreamReader(stream);
            var content = sr.ReadToEnd();

            srCheckToken.Close();
            srCheckToken.Dispose();
            sr.Close();
            sr.Dispose();

            GlobalObjectListResponseJson<Guru> resData = JsonSerializer.Deserialize<GlobalObjectListResponseJson<Guru>>(content, options);
            return resData;
        }

        public async Task<GlobalResponseJson> AddGuru(GuruInputModel model, CancellationToken cancellationToken)
        {

            string Token = "";
            var httpWebRequestCheckToken = (HttpWebRequest)WebRequest.Create(_apiConfig.BaseUrl + _apiConfig.UriCheckToken);
            httpWebRequestCheckToken.ContentType = "application/json";
            httpWebRequestCheckToken.Method = "POST";

            CheckTokenModel modelApiKey = new CheckTokenModel();
            modelApiKey.ApiKey = _apiConfig.ApiKey;
            //modelApiKey.NamaPengguna = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            modelApiKey.NamaPengguna = _session.GetString("UserName");
            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var jsonApiKey = JsonSerializer.Serialize(modelApiKey, options);
            using (var streamWriter = new StreamWriter(httpWebRequestCheckToken.GetRequestStream()))
            {
                streamWriter.Write(jsonApiKey);
                streamWriter.Close();
                streamWriter.Dispose();
            }

            var responseCheckToken = httpWebRequestCheckToken.GetResponse();
            var streamCheckToken = responseCheckToken.GetResponseStream();
            var srCheckToken = new StreamReader(streamCheckToken);
            var contentCheckToken = srCheckToken.ReadToEnd();

            GlobalObjectListResponseJson<TokenResponse> resDataToken = JsonSerializer.Deserialize<GlobalObjectListResponseJson<TokenResponse>>(contentCheckToken, options);
            if (resDataToken.Data.Any())
            {
                foreach (var data in resDataToken.Data)
                {
                    Token = data.Token;
                }
            }
            else
            {
                string jsonString =
                                    @"{
                                      ""code"": 401,
                                      ""error"": true,
                                      ""message"": ""Unauthorization""
                                    }";
                GlobalResponseJson resUnauthorization = JsonSerializer.Deserialize<GlobalResponseJson>(jsonString, options);
                return resUnauthorization;
            }

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(_apiConfig.BaseUrl + _apiConfig.UriAddGuru);
            httpWebRequest.Headers.Add("Authorization", "Bearer " + Token + "");
            httpWebRequest.Headers.Add("Strict-Transport-Security", "max-age=31536000");
            httpWebRequest.Headers.Add("X-Content-Type-Options", "nosniff");
            httpWebRequest.Headers.Add("X-Xss-Protection", "1; mode=block");
            httpWebRequest.Headers.Add("X-Frame-Options", "SAMEORIGIN");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            var json = JsonSerializer.Serialize(model, options);

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Close();
                streamWriter.Dispose();
            }

            var response = httpWebRequest.GetResponse();
            var stream = response.GetResponseStream();
            var sr = new StreamReader(stream);
            var content = sr.ReadToEnd();

            srCheckToken.Close();
            srCheckToken.Dispose();
            sr.Close();
            sr.Dispose();

            GlobalResponseJson res = JsonSerializer.Deserialize<GlobalResponseJson>(content, options);
            return res;
        }

        public async Task<GlobalResponseJson> UpdateGuru(GuruUpdateModel model, CancellationToken cancellationToken)
        {

            string Token = "";
            var httpWebRequestCheckToken = (HttpWebRequest)WebRequest.Create(_apiConfig.BaseUrl + _apiConfig.UriCheckToken);
            httpWebRequestCheckToken.ContentType = "application/json";
            httpWebRequestCheckToken.Method = "POST";

            CheckTokenModel modelApiKey = new CheckTokenModel();
            modelApiKey.ApiKey = _apiConfig.ApiKey;
            //modelApiKey.NamaPengguna = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            modelApiKey.NamaPengguna = _session.GetString("UserName");
            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var jsonApiKey = JsonSerializer.Serialize(modelApiKey, options);
            using (var streamWriter = new StreamWriter(httpWebRequestCheckToken.GetRequestStream()))
            {
                streamWriter.Write(jsonApiKey);
                streamWriter.Close();
                streamWriter.Dispose();
            }

            var responseCheckToken = httpWebRequestCheckToken.GetResponse();
            var streamCheckToken = responseCheckToken.GetResponseStream();
            var srCheckToken = new StreamReader(streamCheckToken);
            var contentCheckToken = srCheckToken.ReadToEnd();

            GlobalObjectListResponseJson<TokenResponse> resDataToken = JsonSerializer.Deserialize<GlobalObjectListResponseJson<TokenResponse>>(contentCheckToken, options);
            if (resDataToken.Data.Any())
            {
                foreach (var data in resDataToken.Data)
                {
                    Token = data.Token;
                }
            }
            else
            {
                string jsonString =
                                    @"{
                                      ""code"": 401,
                                      ""error"": true,
                                      ""message"": ""Unauthorization""
                                    }";
                GlobalResponseJson resUnauthorization = JsonSerializer.Deserialize<GlobalResponseJson>(jsonString, options);
                return resUnauthorization;
            }

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(_apiConfig.BaseUrl + _apiConfig.UriUpdateGuru);
            httpWebRequest.Headers.Add("Authorization", "Bearer " + Token + "");
            httpWebRequest.Headers.Add("Strict-Transport-Security", "max-age=31536000");
            httpWebRequest.Headers.Add("X-Content-Type-Options", "nosniff");
            httpWebRequest.Headers.Add("X-Xss-Protection", "1; mode=block");
            httpWebRequest.Headers.Add("X-Frame-Options", "SAMEORIGIN");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            var json = JsonSerializer.Serialize(model, options);

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Close();
                streamWriter.Dispose();
            }

            var response = httpWebRequest.GetResponse();
            var stream = response.GetResponseStream();
            var sr = new StreamReader(stream);
            var content = sr.ReadToEnd();

            srCheckToken.Close();
            srCheckToken.Dispose();
            sr.Close();
            sr.Dispose();

            GlobalResponseJson res = JsonSerializer.Deserialize<GlobalResponseJson>(content, options);
            return res;
        }

        public async Task<GlobalObjectListResponseJson<Guru>> ListGuru(long id, CancellationToken cancellationToken)
        {

            string Token = "";
            var httpWebRequestCheckToken = (HttpWebRequest)WebRequest.Create(_apiConfig.BaseUrl + _apiConfig.UriCheckToken);
            httpWebRequestCheckToken.ContentType = "application/json";
            httpWebRequestCheckToken.Method = "POST";

            CheckTokenModel modelApiKey = new CheckTokenModel();
            modelApiKey.ApiKey = _apiConfig.ApiKey;
            //modelApiKey.NamaPengguna = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            modelApiKey.NamaPengguna = _session.GetString("UserName");
            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var jsonApiKey = JsonSerializer.Serialize(modelApiKey, options);
            using (var streamWriter = new StreamWriter(httpWebRequestCheckToken.GetRequestStream()))
            {
                streamWriter.Write(jsonApiKey);
                streamWriter.Close();
                streamWriter.Dispose();
            }

            var responseCheckToken = httpWebRequestCheckToken.GetResponse();
            var streamCheckToken = responseCheckToken.GetResponseStream();
            var srCheckToken = new StreamReader(streamCheckToken);
            var contentCheckToken = srCheckToken.ReadToEnd();

            GlobalObjectListResponseJson<TokenResponse> resDataToken = JsonSerializer.Deserialize<GlobalObjectListResponseJson<TokenResponse>>(contentCheckToken, options);
            if (resDataToken.Data.Any())
            {
                foreach (var data in resDataToken.Data)
                {
                    Token = data.Token;
                }
            }
            else
            {
                string jsonString =
                                    @"{
                                      ""code"": 401,
                                      ""error"": true,
                                      ""message"": ""Unauthorization"",
                                      ""data"": null
                                    }";
                GlobalObjectListResponseJson<Guru> resUnauthorization = JsonSerializer.Deserialize<GlobalObjectListResponseJson<Guru>>(jsonString, options);
                return resUnauthorization;
            }

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(_apiConfig.BaseUrl + _apiConfig.UriGetGuru + "/?id=" + id.ToString());
            httpWebRequest.Headers.Add("Authorization", "Bearer " + Token + "");
            httpWebRequest.Headers.Add("Strict-Transport-Security", "max-age=31536000");
            httpWebRequest.Headers.Add("X-Content-Type-Options", "nosniff");
            httpWebRequest.Headers.Add("X-Xss-Protection", "1; mode=block");
            httpWebRequest.Headers.Add("X-Frame-Options", "SAMEORIGIN");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "GET";
            var response = httpWebRequest.GetResponse();
            var stream = response.GetResponseStream();
            var sr = new StreamReader(stream);
            var content = sr.ReadToEnd();

            srCheckToken.Close();
            srCheckToken.Dispose();
            sr.Close();
            sr.Dispose();

            GlobalObjectListResponseJson<Guru> resData = JsonSerializer.Deserialize<GlobalObjectListResponseJson<Guru>>(content, options);
            return resData;
        }
    }
}
