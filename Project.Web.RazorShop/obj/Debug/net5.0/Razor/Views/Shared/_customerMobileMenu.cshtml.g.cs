#pragma checksum "D:\git\manoshop\Project.Web.RazorShop\Views\Shared\_customerMobileMenu.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "34c8cd5d539a5360173f900133a87b5b81f07af40b71cb4e1446bbbcb483df99"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCoreGeneratedDocument.Views_Shared__customerMobileMenu), @"mvc.1.0.view", @"/Views/Shared/_customerMobileMenu.cshtml")]
namespace AspNetCoreGeneratedDocument
{
    #line default
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Threading.Tasks;
    using global::Microsoft.AspNetCore.Mvc;
    using global::Microsoft.AspNetCore.Mvc.Rendering;
    using global::Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\git\manoshop\Project.Web.RazorShop\Views\_ViewImports.cshtml"
using Project

#line default
#line hidden
#nullable disable
    ;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"34c8cd5d539a5360173f900133a87b5b81f07af40b71cb4e1446bbbcb483df99", @"/Views/Shared/_customerMobileMenu.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"2885f54451faa4d6474c9817bbb3841c5707726850dae79cba1f50c434f03a6c", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    internal sealed class Views_Shared__customerMobileMenu : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-area", "customer", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "home", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "profile", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("nav-link"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<div class=""custom-filter d-lg-none d-block"">
    <button class=""btn btn-filter-float border-0 main-color-two-bg shadow-box px-4 rounded-3 position-fixed"" style=""z-index: 999;bottom:80px;"" type=""button"" data-bs-toggle=""offcanvas"" data-bs-target=""#offcanvasRight"" aria-controls=""offcanvasRight"">
        <i class=""bi bi-list font-20 fw-bold text-white""></i>
        <span class=""d-block font-14 text-white"">منو</span>
    </button>

    <div class=""offcanvas offcanvas-start"" tabindex=""-1"" id=""offcanvasRight"" aria-labelledby=""offcanvasRightLabel"">
        <div class=""offcanvas-header"">
            <h5 class=""offcanvas-title"">منو</h5>
            <button type=""button"" class=""btn-close"" data-bs-dismiss=""offcanvas"" aria-label=""Close""></button>
        </div>
        <div class=""offcanvas-body"">
            <div class=""panel-nav-logo"">
                <a");
            BeginWriteAttribute("href", " href=\"", 869, "\"", 876, 0);
            EndWriteAttribute();
            WriteLiteral(" class=\"text-center d-block mb-3\">\r\n                    <img src=\"assets/img/logo.png\"");
            BeginWriteAttribute("alt", " alt=\"", 963, "\"", 969, 0);
            EndWriteAttribute();
            WriteLiteral(@" class=""img-fluid"" width=""200"">
                </a>
            </div>
            <div class=""penel-nav"">
                <div class=""panel-nav-nav"">
                    <nav class=""navbar profile-box-nav"">
                        <ul class=""navbar-nav flex-column"">
                            <li class=""nav-item "">
                                <a href=""/customer"" class=""nav-link"">
                                    <i class=""bi bi-house-door""></i>حساب کاربری
                                </a>
                            </li>
                            <li class=""nav-item"">
                                <a");
            BeginWriteAttribute("href", " href=\"", 1608, "\"", 1615, 0);
            EndWriteAttribute();
            WriteLiteral(@" class=""nav-link"">
                                    <i class=""bi bi-cart-check""></i>سفارش های من <span class=""badge rounded-pill badge-spn"">5</span>
                                </a>
                            </li>
                            <li class=""nav-item"">
                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "34c8cd5d539a5360173f900133a87b5b81f07af40b71cb4e1446bbbcb483df996796", async() => {
                WriteLiteral("\r\n                                    <i class=\"bi bi-pin-map\"></i>پروفایل\r\n                                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Area = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                            </li>\r\n                            <li class=\"nav-item\">\r\n                                <a");
            BeginWriteAttribute("href", " href=\"", 2243, "\"", 2250, 0);
            EndWriteAttribute();
            WriteLiteral(@" class=""nav-link"">
                                    <i class=""bi bi-gift""></i>کد های تخفیف من
                                </a>
                            </li>
                            <li class=""nav-item"">
                                <a href=""/Account/Logout"" class=""nav-link"">
                                    <i class=""bi bi-arrow-right-square""></i>خروج از حساب کاربری
                                </a>
                            </li>
                        </ul>
                    </nav>
                </div>
            </div>
        </div>
    </div>
</div>");
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
