// Copyright (c) Juan M. Elosegui. All rights reserved.
// Licensed under the GPL v2 license. See LICENSE.txt file in the project root for full license information.

namespace Jmelosegui.Mvc.GoogleMap
{
    using System;
#if NET45
    using System.Web;
#endif
#if NETSTANDARD1_6
    using System.IO;
    using System.Text.Encodings.Web;
    using Microsoft.AspNetCore.Html;
#endif

    public class ScriptRegistrarBuilder
#if NET45
    : IHtmlString
#endif
#if NETSTANDARD1_6
    : IHtmlContent
#endif
    {
        public ScriptRegistrarBuilder(ScriptRegistrar scriptRegistrar)
        {
            if (scriptRegistrar == null)
            {
                throw new ArgumentNullException(nameof(scriptRegistrar));
            }

            this.ScriptRegistrar = scriptRegistrar;
        }

        protected ScriptRegistrarBuilder(ScriptRegistrarBuilder builder)
            : this(PassThroughNonNull(builder).ScriptRegistrar)
        {
        }

        protected ScriptRegistrar ScriptRegistrar { get; }

        public override string ToString()
        {
            return this.ToHtmlString();
        }

        public string ToHtmlString()
        {
            return this.ScriptRegistrar.ToHtmlString();
        }

        public ScriptRegistrarBuilder ScriptsBasePath(string basePath)
        {
            this.ScriptRegistrar.BasePath = basePath;
            return this;
        }

        public ScriptRegistrarBuilder Add(string scriptFileName)
        {
            if (string.IsNullOrWhiteSpace(scriptFileName))
            {
                throw new ArgumentNullException(nameof(scriptFileName));
            }

            this.ScriptRegistrar.FixedScriptCollection.Add(scriptFileName);
            return this;
        }

#if NETSTANDARD1_6
        public void WriteTo(TextWriter writer, HtmlEncoder encoder)
        {
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }

            if (encoder == null)
            {
                throw new ArgumentNullException("encoder");
            }

            writer.Write(this.ToString());
        }
#endif

        private static ScriptRegistrarBuilder PassThroughNonNull(ScriptRegistrarBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder;
        }
    }
}