using DLMS.APP.Container;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ShopBridge.APP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.APP.Controllers
{
    public class ItemDetailsController : Controller
    {
        HttpClientHandler _clientHandler = new HttpClientHandler();
        private IConfiguration _config;

        public ItemDetailsController(IConfiguration config)
        {
            _config = config;
        }
       

        [HttpGet]
        public IActionResult ItemMasterDetails(string id)
        {
            if (id != null)
            {
                ViewBag.RelId = id;
            }
            else
            {
                ViewBag.RelId = "0";
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddItemDetails(ItemDetails objItemDetails)
        {
            var result = ""; //calling the web api service
            try
            {
                using (var httpClient = new HttpClient(_clientHandler))
                {
                    StringContent _content = new StringContent(JsonConvert.SerializeObject(objItemDetails), Encoding.UTF8, "application/json"); //converting to JSON

                    Uri _uRl = null;
                    HttpRequestMessage request = null;

                    if (objItemDetails.Itemid == 0)
                    {
                        _uRl = new Uri(_config["BaseURL:_apiRootURL"] + "ItemDetail");
                        request = new HttpRequestMessage
                        {

                            Method = HttpMethod.Post,
                            RequestUri = _uRl,
                            Content = _content
                        };
                    }
                    else
                    {
                        _uRl = new Uri(_config["BaseURL:_apiRootURL"] + "ItemDetail/" + objItemDetails.Itemid);
                        request = new HttpRequestMessage
                        {
                            Method = HttpMethod.Put,
                            RequestUri = _uRl,
                            Content = _content
                        };
                    }

                    var response = await httpClient.SendAsync(request);
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ItemDetails _itemlist = JsonConvert.DeserializeObject<ItemDetails>(apiResponse);

                    if (_itemlist.Itemid > 0)
                    {
                        string _message = "";
                        if (objItemDetails.Itemid == 0)
                        {
                            _message = "Record Inserted Successfully.";
                        }
                        else
                        {
                            _message = "Record Updated Successfully.";
                        }
                        return Content(new AjaxResult { state = ResultType.success.ToString(), message = _message }.ToJson());
                    }
                    else
                    {
                        return Content(new AjaxResult { state = ResultType.error.ToString(), message = "Record does not insert." }.ToJson());
                    }
                }
            }
            catch (Exception ex)
            {
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = ex.Message }.ToJson());
            }
        }

        public IActionResult ViewItemDetails()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetItemList(string id)
        {
            var result = ""; //calling the web api service
            try
            {
                using (var httpClient = new HttpClient(_clientHandler))
                {
                    Uri _uRl = null;
                    if (id == null)
                    {
                        _uRl = new Uri(_config["BaseURL:_apiRootURL"] + "ItemDetail");
                    }
                    else
                    {
                        _uRl = new Uri(_config["BaseURL:_apiRootURL"] + "ItemDetail/" + Convert.ToInt32(id));
                    }

                    HttpRequestMessage request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = _uRl
                        //Content = _content
                    };

                    var response = await httpClient.SendAsync(request);
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode == true)
                    {
                        return Content(new AjaxResult { state = ResultType.success.ToString(), data = apiResponse }.ToJson());
                    }
                    else
                    {
                        return Content(new AjaxResult { state = ResultType.error.ToString(), message = "Record does not exist." }.ToJson());
                    }
                }
            }
            catch (Exception ex)
            {
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = ex.Message }.ToJson());
            }
        }


        [HttpPost]
        public async Task<IActionResult> DeleteItemDetails(string redId)
        {
            var result = ""; //calling the web api service
            try
            {
                using (var httpClient = new HttpClient(_clientHandler))
                {
                    //StringContent _content = new StringContent(JsonConvert.SerializeObject(objItemDetails), Encoding.UTF8, "application/json"); //converting to JSON

                    Uri _uRl = new Uri(_config["BaseURL:_apiRootURL"] + "ItemDetail/" + Convert.ToInt32(redId));
                    HttpRequestMessage request = new HttpRequestMessage
                    {

                        Method = HttpMethod.Delete,
                        RequestUri = _uRl
                        //Content = _content
                    };

                    var response = await httpClient.SendAsync(request);
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    return Content(new AjaxResult { state = ResultType.success.ToString(), message = apiResponse }.ToJson());
                    
                }
            }
            catch (Exception ex)
            {
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = ex.Message }.ToJson());
            }
        }

    }
}
