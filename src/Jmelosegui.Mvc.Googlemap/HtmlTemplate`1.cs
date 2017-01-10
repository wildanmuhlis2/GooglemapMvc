// Copyright (c) Juan M. Elosegui. All rights reserved.
// Licensed under the GPL v2 license. See LICENSE.txt file in the project root for full license information.

namespace Jmelosegui.Mvc.GoogleMap
{
    using System;
    using Newtonsoft.Json;
#if NET45
    using System.Web.WebPages;
#endif
#if NETSTANDARD1_6
    using System.Text.Encodings.Web;
    using Microsoft.AspNetCore.Mvc.Razor;
#endif

    public class HtmlTemplate<T>
        where T : class
    {
        private string html;
        private Action<T> codeBlockTemplate;
        private Func<T, object> inlineTemplate;
        private Action<T, IHtmlNode> binder;

        public string Html
        {
            get
            {
                return this.html;
            }

            set
            {
                this.html = value;

                this.binder = (dataItem, node) => node.Html(this.html);

                this.codeBlockTemplate = null;
                this.inlineTemplate = null;
            }
        }

        [JsonIgnore]
        public Action<T> CodeBlockTemplate
        {
            get
            {
                return this.codeBlockTemplate;
            }

            set
            {
                this.codeBlockTemplate = value;

                this.binder = (dataItem, node) => node.Template((writer) => this.CodeBlockTemplate(dataItem));

                this.html = null;
                this.inlineTemplate = null;
            }
        }

        [JsonIgnore]
        public Func<T, object> InlineTemplate
        {
            get
            {
                return this.inlineTemplate;
            }

            set
            {
                this.inlineTemplate = value;

                this.binder = (dataItem, node) => node.Template((writer) =>
                {
                    var result = this.InlineTemplate(dataItem);

                    var helperResult = result as HelperResult;

                    if (helperResult != null)
                    {
#if NET45
                        helperResult.WriteTo(writer);
#endif
#if NETSTANDARD1_6
                        helperResult.WriteTo(writer, HtmlEncoder.Default);
#endif
                        return;
                    }

                    if (result != null)
                    {
                        writer.Write(result.ToString());
                    }
                });

                this.codeBlockTemplate = null;
                this.html = null;
            }
        }

        public void Apply(T dataItem, IHtmlNode node)
        {
            if (this.HasValue())
            {
                this.binder(dataItem, node);
            }
        }

        public bool HasValue()
        {
            return this.Html.HasValue() || this.InlineTemplate != null || this.CodeBlockTemplate != null;
        }
    }
}
