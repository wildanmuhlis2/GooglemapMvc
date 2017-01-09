// Copyright (c) Juan M. Elosegui. All rights reserved.
// Licensed under the GPL v2 license. See LICENSE.txt file in the project root for full license information.

namespace Jmelosegui.Mvc.GoogleMap
{
    using System;
#if NET45
    using System.Web;
#endif
#if NETSTANDARD1_6
    using Microsoft.AspNetCore.Http;
#endif

    internal static class StringExtensions
    {
        public static bool HasValue(this string source)
        {
            return !string.IsNullOrWhiteSpace(source);
        }

        public static string ToAbsoluteUrl(this string url)
        {
            if (url == null)
            {
                throw new ArgumentNullException(nameof(url));
            }

            if (url.IndexOf("://", StringComparison.Ordinal) > -1)
            {
                return url;
            }

#if NET45
            string pathWithoutHostName = VirtualPathUtility.ToAbsolute(url);
            Uri originalUri = HttpContext.Current.Request.Url;
            var result = $"{originalUri.Scheme}://{originalUri.Authority}{pathWithoutHostName}";            
#endif
#if NETSTANDARD1_6
            var contextAccessor = new HttpContextAccessor();
            var request = contextAccessor.HttpContext.Request;

            var result = string.Concat(
                        request.Scheme,
                        "://",
                        request.Host.ToUriComponent(),
                        request.PathBase.ToUriComponent(),
                        request.Path.ToUriComponent(),
                        request.QueryString.ToUriComponent());
#endif
            return result;

        }
    }
}