#pragma checksum "E:\Front_End_School\WebSchool\WebSchool\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f945ca554e9476ff5bdcbc02329f5f5e70d87e4e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "E:\Front_End_School\WebSchool\WebSchool\Views\_ViewImports.cshtml"
using WebSchool;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "E:\Front_End_School\WebSchool\WebSchool\Views\_ViewImports.cshtml"
using WebSchool.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "E:\Front_End_School\WebSchool\WebSchool\Views\Home\Index.cshtml"
using WebSchool.Service.Interface;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "E:\Front_End_School\WebSchool\WebSchool\Views\Home\Index.cshtml"
using WebSchool.Service;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "E:\Front_End_School\WebSchool\WebSchool\Views\Home\Index.cshtml"
using System.Threading;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "E:\Front_End_School\WebSchool\WebSchool\Views\Home\Index.cshtml"
using System.Threading.Tasks;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f945ca554e9476ff5bdcbc02329f5f5e70d87e4e", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b3121ae47d1aec29aa743219964438ed5429cfc0", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
<section class=""section"">
    <div class=""row"">
        <div class=""col-lg-12"">
            <div class=""card"">
                <div class=""card-body"">
                    <h5 class=""card-title"">Profil</h5>
                    <br />
                    <br />
                    <table class=""table table-striped table-bordered dt-responsive nowrap"" id=""tabelProfil"">
                         <tr>
                            <td style=""width:120px;"">Nama Pengguna </td>
                            <td style=""width:250px;""></td>
                         </tr>
                         <tr>
                            <td style=""width:120px;"">Nama </td>
                            <td style=""width:250px;""></td>
                         </tr>
                         <tr>
                            <td style=""width:120px;"">Status Pengguna </td>
                            <td style=""width:250px;""></td>
                         </tr>
                         <tr>
                            <");
            WriteLiteral(@"td style=""width:120px;"">Email </td>
                            <td style=""width:250px;""></td>
                         </tr>
                         <tr>
                            <td style=""width:120px;"">Kadaluarsa Sandi </td>
                            <td style=""width:250px;""></td>
                         </tr>
                         <tr>
                            <td style=""width:120px;"">Aktif </td>
                            <td style=""width:250px;""></td>
                         </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
</section>");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
