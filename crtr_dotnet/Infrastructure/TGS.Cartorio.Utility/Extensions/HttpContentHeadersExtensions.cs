using System;
using System.Net.Http.Headers;

namespace TGS.Cartorio.Infrastructure.Utility.Extensions
{
    public static class HttpContentHeadersExtensions
    {
        public static bool IsTextHtmlMediaType(this HttpContentHeaders httpContentHeaders)
        {
            try
            {
                return IsMediaType(httpContentHeaders, "text/html");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static bool IsApplicationProblemJsonMediaType(this HttpContentHeaders httpContentHeaders)
        {
            try
            {
                return IsMediaType(httpContentHeaders, "application/problem+json");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static bool IsMediaType(HttpContentHeaders httpContentHeaders, string type)
        {
            try
            {
                return httpContentHeaders != null
                    && httpContentHeaders.ContentType != null
                    && httpContentHeaders.ContentType.MediaType != null
                    && httpContentHeaders.ContentType.MediaType == type;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
