﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Text;

namespace System.Windows.Forms.Generators
{
    internal static class ApplicationConfigurationInitializeBuilder
    {
        public static string GenerateInitialize(string? projectNamespace, ApplicationConfig projectConfig)
        {
            bool topLevelApp = string.IsNullOrWhiteSpace(projectNamespace);
            string? defaultFont = projectConfig.DefaultFont?.ToString();
            string indent = topLevelApp ? string.Empty : "    ";
            return string.Format(topLevelApp ? TopLevelStatements : BoilerPlate,
                                 topLevelApp ? string.Empty : projectNamespace,
                                 GenerateCode(projectConfig, defaultFont, $"{indent}    ///  "),
                                 GenerateCode(projectConfig, defaultFont, $"{indent}        "));

            static string GenerateCode(ApplicationConfig projectConfig, string? defaultFont, string indent)
            {
                StringBuilder code = new();
                if (projectConfig.EnableVisualStyles)
                {
                    code.AppendLine($"{indent}Application.EnableVisualStyles();");
                }

                code.AppendLine($"{indent}Application.SetCompatibleTextRenderingDefault({projectConfig.UseCompatibleTextRendering.ToString().ToLowerInvariant()});");

                if (!string.IsNullOrWhiteSpace(defaultFont))
                {
                    code.AppendLine($"{indent}Application.SetDefaultFont({defaultFont});");
                }

                // Don't append line as we don't need the trailing \r\n!
                code.Append($"{indent}Application.SetHighDpiMode(HighDpiMode.{projectConfig.HighDpiMode});");

                return code.ToString();
            }
        }

        private const string BoilerPlate = @"// <auto-generated />

using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace {0}
{{
    /// <summary>
    ///  Bootstrap the application configuration.
    /// </summary>
    [CompilerGenerated]
    internal static partial class ApplicationConfiguration
    {{
        /// <summary>
        ///  Bootstrap the application as follows:
        ///  <code>
{1}
        /// </code>
        /// </summary>
        public static void Initialize()
        {{
{2}
        }}
    }}
}}
";

        private const string TopLevelStatements = @"// <auto-generated />

using System.Drawing;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
{0}
/// <summary>
///  Bootstrap the application configuration.
/// </summary>
[CompilerGenerated]
internal static partial class ApplicationConfiguration
{{
    /// <summary>
    ///  Bootstrap the application as follows:
    ///  <code>
{1}
    /// </code>
    /// </summary>
    public static void Initialize()
    {{
        // Set STAThread
        Thread.CurrentThread.SetApartmentState(ApartmentState.Unknown);
        Thread.CurrentThread.SetApartmentState(ApartmentState.STA);

{2}
    }}
}}
";
    }
}
