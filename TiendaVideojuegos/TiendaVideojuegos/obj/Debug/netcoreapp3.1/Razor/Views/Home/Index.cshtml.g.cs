#pragma checksum "C:\Users\antcu\Desktop\DDSI\DDSI\TiendaVideojuegos\TiendaVideojuegos\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9a67927ea2c65632b2c7e477bf3056d786eb1580"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
namespace AspNetCore
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
#line 1 "C:\Users\antcu\Desktop\DDSI\DDSI\TiendaVideojuegos\TiendaVideojuegos\Views\_ViewImports.cshtml"
using TiendaVideojuegos;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\antcu\Desktop\DDSI\DDSI\TiendaVideojuegos\TiendaVideojuegos\Views\_ViewImports.cshtml"
using TiendaVideojuegos.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9a67927ea2c65632b2c7e477bf3056d786eb1580", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7f782265c2d2c857b9115349562005959e5d4fbb", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<TiendaVideojuegos.ViewModels.HomeViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n\r\n<div class=\"text-center\">\r\n    <h1 class=\"display-4\">Home Page</h1>\r\n\r\n");
#nullable restore
#line 7 "C:\Users\antcu\Desktop\DDSI\DDSI\TiendaVideojuegos\TiendaVideojuegos\Views\Home\Index.cshtml"
     if (Model != null && Model.abonado.Logueado == true)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <p>Bienvenido ");
#nullable restore
#line 9 "C:\Users\antcu\Desktop\DDSI\DDSI\TiendaVideojuegos\TiendaVideojuegos\Views\Home\Index.cshtml"
                 Write(Model.abonado.Nombre);

#line default
#line hidden
#nullable disable
            WriteLiteral(" !!</p>\r\n");
#nullable restore
#line 10 "C:\Users\antcu\Desktop\DDSI\DDSI\TiendaVideojuegos\TiendaVideojuegos\Views\Home\Index.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<TiendaVideojuegos.ViewModels.HomeViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
