﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using WebSchool.Configuration;
using WebSchool.Models;
using WebSchool.Service.Interface;

namespace WebSchool.Service
{
    public class SPPService : ISPPService
    {
        private readonly ApiConfiguration _apiConfig;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        public SPPService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _apiConfig = configuration.GetSection(nameof(ApiConfiguration)).Get<ApiConfiguration>();
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<GlobalObjectListResponseJson<SPP>> CheckAddSPP(CheckSPPModel model, CancellationToken cancellationToken)
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
                GlobalObjectListResponseJson<SPP> resUnauthorization = JsonSerializer.Deserialize<GlobalObjectListResponseJson<SPP>>(jsonString, options);
                return resUnauthorization;
            }

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(_apiConfig.BaseUrl + _apiConfig.UriCheckAddSPP);
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

            GlobalObjectListResponseJson<SPP> resData = JsonSerializer.Deserialize<GlobalObjectListResponseJson<SPP>>(content);
            return resData;
        }

        public async Task<GlobalResponseJson> AddSPP(SPPInputModel model, CancellationToken cancellationToken)
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

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(_apiConfig.BaseUrl + _apiConfig.UriAddSPP);
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

            GlobalResponseJson res = JsonSerializer.Deserialize<GlobalResponseJson>(content);
            return res;
        }

        public async Task<GlobalResponseJson> UpdateSPP(SPPUpdateModel model, CancellationToken cancellationToken)
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

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(_apiConfig.BaseUrl + _apiConfig.UriUpdateSPP);
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

            GlobalResponseJson res = JsonSerializer.Deserialize<GlobalResponseJson>(content);
            return res;
        }

        public async Task<GlobalResponseJson> DeleteSPP(IdSPP model, CancellationToken cancellationToken)
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

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(_apiConfig.BaseUrl + _apiConfig.UriDeleteSPP);
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

            GlobalResponseJson res = JsonSerializer.Deserialize<GlobalResponseJson>(content);
            return res;
        }

        public async Task<GlobalObjectListResponseJson<SPP>> SPPSearch(SearchSPP model, CancellationToken cancellationToken)
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
                GlobalObjectListResponseJson<SPP> resUnauthorization = JsonSerializer.Deserialize<GlobalObjectListResponseJson<SPP>>(jsonString, options);
                return resUnauthorization;
            }

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(_apiConfig.BaseUrl + _apiConfig.UriGetSPP);
            httpWebRequest.Headers.Add("Authorization", "Bearer " + Token + "");
            httpWebRequest.Headers.Add("Strict-Transport-Security", "max-age=31536000");
            httpWebRequest.Headers.Add("X-Content-Type-Options", "nosniff");
            httpWebRequest.Headers.Add("X-Xss-Protection", "1; mode=block");
            httpWebRequest.Headers.Add("X-Frame-Options", "SAMEORIGIN");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            var jsonApi = JsonSerializer.Serialize(model, options);
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(jsonApi);
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

            GlobalObjectListResponseJson<SPP> resData = JsonSerializer.Deserialize<GlobalObjectListResponseJson<SPP>>(content);
            return resData;
        }
    }
}
