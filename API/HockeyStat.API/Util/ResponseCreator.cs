using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HockeyStat.API.Util
{
    public static class ResponseCreator
    {
        public static HttpResponseMessage CreateNoCacheResponse(HttpRequestMessage request, object responseData)
        {
            HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, responseData);
            response.Headers.CacheControl = new System.Net.Http.Headers.CacheControlHeaderValue() 
            { 
                NoCache = true, 
                NoStore = true, 
                MaxAge = new TimeSpan(0), 
                MustRevalidate = true 
            };
            response.Content.Headers.Expires = DateTime.Now;
            return response;
        }
    }
}
