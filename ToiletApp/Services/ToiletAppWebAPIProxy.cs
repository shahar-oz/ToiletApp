using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ToiletApp.Models;

namespace ToiletApp.Services
{
    public class ToiletAppWebAPIProxy
    {
        #region without tunnel
        /*
        //Define the serevr IP address! (should be realIP address if you are using a device that is not running on the same machine as the server)
        private static string serverIP = "localhost";
        private HttpClient client;
        private string baseUrl;
        public static string BaseAddress = (DeviceInfo.Platform == DevicePlatform.Android &&
            DeviceInfo.DeviceType == DeviceType.Virtual) ? "http://10.0.2.2:5110/api/" : $"http://{serverIP}:5110/api/";
        private static string ImageBaseAddress = (DeviceInfo.Platform == DevicePlatform.Android &&
            DeviceInfo.DeviceType == DeviceType.Virtual) ? "http://10.0.2.2:5110" : $"http://{serverIP}:5110";
        */
        #endregion

        #region with tunnel
        //Define the serevr IP address! (should be realIP address if you are using a device that is not running on the same machine as the server)
        private static string serverIP = "nr3zcjt7-5159.euw.devtunnels.ms";
        private HttpClient client;
        private string baseUrl;
        public static string BaseAddress = "https://nr3zcjt7-5159.euw.devtunnels.ms/api/";
        public static string ImageBaseAddress = "https://nr3zcjt7-5159.euw.devtunnels.ms/";
        #endregion

        public ToiletAppWebAPIProxy()
        {
            //Set client handler to support cookies!!
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = new System.Net.CookieContainer();

            this.client = new HttpClient(handler);
            this.baseUrl = BaseAddress;
        }

        public string GetImagesBaseAddress()
        {
            return ToiletAppWebAPIProxy.ImageBaseAddress;
        }

        public string GetDefaultProfilePhotoUrl()
        {
            return $"{ToiletAppWebAPIProxy.ImageBaseAddress}/profileImages/default.png";
        }



        public async Task<UserInfo?> LoginAsync(LoginInfo userInfo)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}login";
            try
            {
                //Call the server API
                string json = JsonSerializer.Serialize(userInfo);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    UserInfo? result = JsonSerializer.Deserialize<UserInfo>(resContent, options);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<UserInfo?> Register(UserInfo user)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}signup";
            try
            {
                //Call the server API
                string json = JsonSerializer.Serialize(user);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    UserInfo? result = JsonSerializer.Deserialize<UserInfo>(resContent, options);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<CurrentToiletInfo?> AddToilet(CurrentToiletInfo toilet)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}addToilet";
            try
            {
                //Call the server API
                string json = JsonSerializer.Serialize(toilet);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    CurrentToiletInfo? result = JsonSerializer.Deserialize<CurrentToiletInfo>(resContent, options);
                    return result;
                }
                else
                    return null;
            }

            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<CurrentToiletInfo?> UploadToiletImage(string imagePath, int toiletId)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}UploadToiletImage?toiletId={toiletId}";
            try
            {
                //Create the form data
                MultipartFormDataContent form = new MultipartFormDataContent();
                var fileContent = new ByteArrayContent(File.ReadAllBytes(imagePath));
                form.Add(fileContent, "file", imagePath);
                //Call the server API
                HttpResponseMessage response = await client.PostAsync(url, form);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    CurrentToiletInfo? result = JsonSerializer.Deserialize<CurrentToiletInfo>(resContent, options);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<List<CurrentToiletInfo>> GetAllApprovedToilets()
        {
            string url = $"{this.baseUrl}GetAllApprovedToilets";
            try
            {
                // Call the server API
                HttpResponseMessage response = await client.GetAsync(url);

                // Check status
                if (response.IsSuccessStatusCode)
                {
                    // Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    // Deserialize result to List<GaragePartsDTO>
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    List<CurrentToiletInfo> result = JsonSerializer.Deserialize<List<CurrentToiletInfo>>(resContent, options);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<CurrentToiletInfo>> GetAllPendingToilets()
        {
            string url = $"{this.baseUrl}GetAllPendingToilets";
            try
            {
                // Call the server API
                HttpResponseMessage response = await client.GetAsync(url);

                // Check status
                if (response.IsSuccessStatusCode)
                {
                    // Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    // Deserialize result to List<GaragePartsDTO>
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    List<CurrentToiletInfo> result = JsonSerializer.Deserialize<List<CurrentToiletInfo>>(resContent, options);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<CurrentToiletInfo>> GetAllDeclinedToilets()
        {
            string url = $"{this.baseUrl}GetAllDeclinedToilets";
            try
            {
                // Call the server API
                HttpResponseMessage response = await client.GetAsync(url);

                // Check status
                if (response.IsSuccessStatusCode)
                {
                    // Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    // Deserialize result to List<GaragePartsDTO>
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    List<CurrentToiletInfo> result = JsonSerializer.Deserialize<List<CurrentToiletInfo>>(resContent, options);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #region Change Status
        //will return true if it worked and false if it didnt
        public async Task<bool> ChangeStatusToApprove(CurrentToiletInfo toiletInfo)
        {
            string url = $"{this.baseUrl}ChangeStatusToApprove";
            try
            {
                //Call the server API
                string json = JsonSerializer.Serialize(toiletInfo);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    bool result = JsonSerializer.Deserialize<bool>(resContent, options);
                    return result;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> ChangStatusToDecline(CurrentToiletInfo toiletInfo)
        {
            string url = $"{this.baseUrl}ChangStatusToDecline";
            try
            {
                //Call the server API
                string json = JsonSerializer.Serialize(toiletInfo);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    bool result = JsonSerializer.Deserialize<bool>(resContent, options);
                    return result;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion


    }
}

