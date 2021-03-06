#pragma checksum "C:\Users\Tobias\OneDrive\Dokumenter\Programming\Automated_Environment_Controller\DMoA\Pages\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c3b80c702433d0897f03b6e3097b18932805e301"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(Data_Monitoring_Application.Pages.Pages_Index), @"mvc.1.0.razor-page", @"/Pages/Index.cshtml")]
namespace Data_Monitoring_Application.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\Tobias\OneDrive\Dokumenter\Programming\Automated_Environment_Controller\DMoA\Pages\_ViewImports.cshtml"
using Data_Monitoring_Application;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c3b80c702433d0897f03b6e3097b18932805e301", @"/Pages/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c005fd390a1142b0848b60bebd41a196a5dde74a", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Index : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "C:\Users\Tobias\OneDrive\Dokumenter\Programming\Automated_Environment_Controller\DMoA\Pages\Index.cshtml"
  
    ViewData["Title"] = "Current Conditions";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>Overview over the current environment conditions.</h1>\r\n<br />\r\n<div id=\"overviews-container\"></div>\r\n<br />\r\n<br />\r\n\r\n<script>\r\n    let measuringData = ");
#nullable restore
#line 14 "C:\Users\Tobias\OneDrive\Dokumenter\Programming\Automated_Environment_Controller\DMoA\Pages\Index.cshtml"
                   Write(Json.Serialize(Model.CurrentDataAndThreshold));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"

    if (measuringData.length == 0) {
        document.getElementById(""overviews-container"").innerHTML = ""There hasn't been gathered any data yet!""
    } else {
        createOverviewDivsContent(measuringData, [[""Temperature"", ""??C""], [""Light"", ""lux""]]);
    }
</script>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Data_Monitoring_Application.Pages.IndexModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<Data_Monitoring_Application.Pages.IndexModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<Data_Monitoring_Application.Pages.IndexModel>)PageContext?.ViewData;
        public Data_Monitoring_Application.Pages.IndexModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591
