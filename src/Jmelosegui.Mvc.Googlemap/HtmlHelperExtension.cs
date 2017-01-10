// Copyright (c) Juan M. Elosegui. All rights reserved.
// Licensed under the GPL v2 license. See LICENSE.txt file in the project root for full license information.

namespace Jmelosegui.Mvc.GoogleMap
{
    using System;
#if NET45
    using System.Web.Mvc;
#endif
#if NETSTANDARD1_6
    using Microsoft.AspNetCore.Mvc.Rendering;
#endif

    public static class HtmlHelperExtension
    {
        public static MapBuilder GoogleMap(
#if NET45
            this HtmlHelper helper)
#endif
#if NETSTANDARD1_6
            this IHtmlHelper helper)
#endif
        {
            if (helper == null)
            {
                throw new ArgumentNullException(nameof(helper));
            }

            return new MapBuilder(helper.ViewContext);
        }
    }
}