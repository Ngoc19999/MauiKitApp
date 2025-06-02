using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace MauiKit.Services
{
    public class ApiServiceBase
    {
        public async Task<ResponseBase<T>> Getasync<T>(string url, params KeyValuePair<string, object>[] args)
        {
            try
            {
                var returnResponse = new ResponseBase<T>();
                string param = "";
                if (args.Length > 0)
                {
                    param = "?";

                    for (int i = 0; i < args.Count(); i++)
                    {
                        if (i == 0)
                        {
                            param += $"{args[i].Key}={args[i].Value}";
                        }
                        else
                        {
                            param += $"&{args[i].Key}={args[i].Value}";
                        }

                    }
                }
                HttpClient client = new HttpClient();

                client.BaseAddress = new Uri($"https://apiapp.realtech.com.vn/{url}{param}");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = TimeSpan.FromMinutes(1);
                HttpResponseMessage response = await client.GetAsync("");

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var str = await response.Content.ReadAsStringAsync();

                    returnResponse = JsonConvert.DeserializeObject<ResponseBase<T>>(str)!;
                    return returnResponse;
                }
                else
                {
                    return new ResponseBase<T>
                    {
                        Code = (int)response.StatusCode,
                        Message = response.StatusCode.GetEnumDescription()
                    };
                }

            }
            catch (Exception ex)
            {
                return new ResponseBase<T>
                {
                    Code = 99,
                    Message = ex.Message.ToString()
                };
            }
        }

        public ResponseBase<T> Get<T>(string url, params KeyValuePair<string, object>[] args)
        {
            try
            {
                string param = "";
                if (args.Length > 0)
                {
                    param = "?";

                    for (int i = 0; i < args.Count(); i++)
                    {
                        if (i == 0)
                        {
                            param += $"{args[i].Key}={args[i].Value}";
                        }
                        else
                        {
                            param += $"&{args[i].Key}={args[i].Value}";
                        }

                    }
                }
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(@$"https://apiapp.realtech.com.vn/{url}{param}");
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = client.GetAsync("").Result;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var str = response.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<ResponseBase<T>>(str)!;
                    if (result != null)
                    {
                        return result;
                    }
                }
                else
                {
                    return new ResponseBase<T>
                    {
                        Code = (int)response.StatusCode,
                        Message = response.StatusCode.GetEnumDescription()
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<T>
                {
                    Code = 99,
                    Message = ex.Message.ToString()
                };
                // Log error
            }

            return default;
        }

        public ResponseBase<T> Delete<T>(string url, params KeyValuePair<string, object>[] args)
        {
            try
            {
                string param = "";
                if (args.Length > 0)
                {
                    param = "?";

                    for (int i = 0; i < args.Count(); i++)
                    {
                        if (i == 0)
                        {
                            param += $"{args[i].Key}={args[i].Value}";
                        }
                        else
                        {
                            param += $"&{args[i].Key}={args[i].Value}";
                        }

                    }
                }
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(@$"https://apiapp.realtech.com.vn/{url}{param}");
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = client.DeleteAsync("").Result;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var str = response.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<ResponseBase<T>>(str)!;
                    if (result != null)
                    {
                        return result;
                    }
                }
                else
                {
                    return new ResponseBase<T>
                    {
                        Code = (int)response.StatusCode,
                        Message = response.StatusCode.GetEnumDescription()
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<T>
                {
                    Code = 99,
                    Message = $"Service base exception :{ex.Message}"
                };
                // Log error
            }

            return default;
        }


        public async Task<ResponseBase<Tout>> Post<Tin, Tout>(string url, Tin body, params string[] args)
        {
            try
            {
                var returnResponse = new ResponseBase<Tout>();
                using (var client = new HttpClient())
                {
                    string Serialized = JsonConvert.SerializeObject(body);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromMinutes(1);
                    HttpContent content = new StringContent(Serialized, Encoding.Unicode, "application/json");

                    var response = await client.PostAsync(@$"https://apiapp.realtech.com.vn/{url}", content);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var str = response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ResponseBase<Tout>>(str.Result);

                        if (result.Data != null)
                        {
                            returnResponse = result;
                        }
                        else
                        {
                            returnResponse.Message = result.Message;
                        }
                        return returnResponse;
                    }
                    else
                    {
                        return new ResponseBase<Tout>
                        {
                            Code = (int)response.StatusCode,
                            Message = response.StatusCode.GetEnumDescription()
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<Tout>
                {
                    Code = 99,
                    Message = $"Service base exception :{ex.Message}"
                };
            }

        }

        public async Task<ResponseBase<Tout>> PostNews<Tin, Tout>(string url, Tin body, params string[] args)
        {
            try
            {
                var returnResponse = new ResponseBase<Tout>();
                using (var client = new HttpClient())
                {
                    string Serialized = JsonConvert.SerializeObject(body);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromMinutes(1);
                    HttpContent content = new StringContent(Serialized, Encoding.Unicode, "application/json");

                    var response = await client.PostAsync(@$"https://api.realtech.com.vn/{url}", content);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var str = response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ResponseBase<Tout>>(str.Result);

                        if (result.Data != null)
                        {
                            returnResponse = result;
                        }
                        return returnResponse;
                    }
                    else
                    {
                        return new ResponseBase<Tout>
                        {
                            Code = (int)response.StatusCode,
                            Message = response.StatusCode.GetEnumDescription()
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<Tout>
                {
                    Code = 99,
                    Message = $"Service base exception :{ex.Message}"
                };
            }

        }

        public async Task<ResponseBase<T>> GetNews<T>(string url, params KeyValuePair<string, object>[] args)
        {
            try
            {
                var returnResponse = new ResponseBase<T>();
                string param = "";
                if (args.Length > 0)
                {
                    param = "?";

                    for (int i = 0; i < args.Count(); i++)
                    {
                        if (i == 0)
                        {
                            param += $"{args[i].Key}={args[i].Value}";
                        }
                        else
                        {
                            param += $"&{args[i].Key}={args[i].Value}";
                        }

                    }
                }
                HttpClient client = new HttpClient();

                client.BaseAddress = new Uri($"https://api.realtech.com.vn/{url}{param}");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = TimeSpan.FromMinutes(1);
                HttpResponseMessage response = await client.GetAsync("");

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var str = await response.Content.ReadAsStringAsync();
                    returnResponse = JsonConvert.DeserializeObject<ResponseBase<T>>(str)!;
                    return returnResponse;
                }
                else
                {
                    return new ResponseBase<T>
                    {
                        Code = (int)response.StatusCode,
                        Message = response.StatusCode.GetEnumDescription()
                    };
                }

            }
            catch (Exception ex)
            {
                return new ResponseBase<T>
                {
                    Code = 99,
                    Message = "Có vẻ kết nối của bạn có vấn đề, hãy kiểm tra lại kết nối internet"
                };
            }
        }

        public async Task<ResponseBase<Tout>> Put<Tin, Tout>(string url, Tin body, params string[] args)
        {
            var returnResponse = new ResponseBase<Tout>();
            using (var client = new HttpClient())
            {
                string Serialized = JsonConvert.SerializeObject(body);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpContent content = new StringContent(Serialized, Encoding.Unicode, "application/json");

                string _url = @$"https://apiapp.realtech.com.vn/{url}";


                var response = await client.PutAsync(_url, content);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var str = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ResponseBase<Tout>>(str);

                    if (result.Data != null)
                    {
                        returnResponse = result;
                    }
                }

            }
            return returnResponse;

        }

        // API Signin 
        public async Task<ResponseBase<TResponse>> PostAsync<TRequest, TResponse>(string url, TRequest data)
        {
            try
            {
                var returnResponse = new ResponseBase<TResponse>();

                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://apiapp.realtech.com.vn/");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromMinutes(1);

                    HttpResponseMessage response = await client.PostAsJsonAsync(url, data);
                    var str = await response.Content.ReadAsStringAsync();

                    Debug.WriteLine($"API Response: {str}"); //  Ghi log phản hồi API để kiểm tra lỗi thực tế

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        returnResponse = JsonConvert.DeserializeObject<ResponseBase<TResponse>>(str)!;
                    }
                    else
                    {
                        try
                        {
                            var jsonObj = JObject.Parse(str);
                            returnResponse.Code = (int)response.StatusCode;

                            //  Kiểm tra nếu JSON chứa "errors" và nó là một danh sách (`JObject["errors"]` là `JObject`)
                            if (jsonObj["errors"] != null && jsonObj["errors"].Type == JTokenType.Object)
                            {
                                var errorMessages = new List<string>();

                                foreach (var property in jsonObj["errors"].Children<JProperty>())
                                {
                                    foreach (var errorMessage in property.Value)
                                    {
                                        errorMessages.Add(errorMessage.ToString()); // Thêm tất cả lỗi vào danh sách
                                    }
                                }

                                returnResponse.Message = string.Join("\n", errorMessages); // Gộp tất cả lỗi thành một chuỗi
                            }
                            else
                            {
                                returnResponse.Message = jsonObj["message"]?.ToString() ?? "Lỗi không xác định";
                            }
                        }
                        catch (JsonReaderException)
                        {
                            returnResponse.Code = (int)response.StatusCode;
                            returnResponse.Message = $"Lỗi API ({response.StatusCode}): {str}";
                        }
                    }


                    return returnResponse;
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<TResponse>
                {
                    Code = 99,
                    Message = $"Lỗi hệ thống: {ex.Message}"
                };
            }
        }

        public async Task<ResponseBase<MauiKitUser>> LoginUserAsync(LoginDto loginDto)
        {
            // Gọi POST đến endpoint "ThueLai/login" với dữ liệu loginDto
            return await PostAsync<LoginDto, MauiKitUser>("ThueLai/login", loginDto);
        }

        // API Register
        public async Task<ResponseBase<int>> RegisterUserAsync(RegisterUserDto data)
        {
            try
            {
                var returnResponse = new ResponseBase<int>();

                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://apiapp.realtech.com.vn/");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromMinutes(1);

                    HttpResponseMessage response = await client.PostAsJsonAsync("ThueLai/insert", data);
                    string responseContent = await response.Content.ReadAsStringAsync();

                    Debug.WriteLine($"API Response: {responseContent}");

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        returnResponse = JsonConvert.DeserializeObject<ResponseBase<int>>(responseContent)!;
                    }
                    else
                    {
                        try
                        {
                            var jsonObj = JObject.Parse(responseContent);
                            returnResponse.Code = (int)response.StatusCode;

                            // Nếu JSON chứa "errors" dưới dạng object
                            if (jsonObj["errors"] != null && jsonObj["errors"].Type == JTokenType.Object)
                            {
                                var errorMessages = new List<string>();

                                foreach (var property in jsonObj["errors"].Children<JProperty>())
                                {
                                    foreach (var error in property.Value)
                                    {
                                        errorMessages.Add(error.ToString());
                                    }
                                }

                                returnResponse.Message = string.Join("\n", errorMessages);
                            }
                            else
                            {
                                returnResponse.Message = jsonObj["message"]?.ToString() ?? "Lỗi không xác định";
                            }
                        }
                        catch (JsonReaderException)
                        {
                            returnResponse.Code = (int)response.StatusCode;
                            returnResponse.Message = $"Lỗi API ({response.StatusCode}): {responseContent}";
                        }
                    }

                    return returnResponse;
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<int>
                {
                    Code = 99,
                    Message = $"Lỗi hệ thống: {ex.Message}"
                };
            }
        }
    }
}

