#if NETSTANDARD1_6

namespace Jmelosegui.Mvc.GoogleMap
{
    using System;
    using Microsoft.AspNetCore.Http;

    public static class AjaxRequestExtensions
    {
        public static bool IsAjaxRequest(this HttpRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            return (request.Headers != null) && (request.Headers["X-Requested-With"] == "XMLHttpRequest");
        }
    }
}

#endif