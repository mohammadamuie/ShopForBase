#pragma checksum "D:\git\manoshop\Project.Web.RazorShop\Areas\Customer\Views\Shared\_DatepickerJs.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "cfb332b56f350930cf0125937c4ce93ed36131ff7b9987f9e4f5938326d53e34"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCoreGeneratedDocument.Areas_Customer_Views_Shared__DatepickerJs), @"mvc.1.0.view", @"/Areas/Customer/Views/Shared/_DatepickerJs.cshtml")]
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
    #line default
    #line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"cfb332b56f350930cf0125937c4ce93ed36131ff7b9987f9e4f5938326d53e34", @"/Areas/Customer/Views/Shared/_DatepickerJs.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"bedd53a138b069785550fa10c8b08d3652affa513a594c43af8203f4666e40ff", @"/Areas/Customer/Views/_ViewImports.cshtml")]
    #nullable restore
    internal sealed class Areas_Customer_Views_Shared__DatepickerJs : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<!-- Datepicker -->
<script src=""/admin/vendors/datepicker/daterangepicker.js""></script>
<!-- Clockpicker -->
<script src=""/admin/vendors/clockpicker/bootstrap-clockpicker.min.js""></script>

<script src=""/admin/assets/js/examples/clockpicker.js""></script>
<!-- // -->
<script src=""/admin/assets/Datepiker/persian-date.js""></script>
<script src=""/admin/assets/Datepiker/persian-datepicker.js""></script>
<script type=""text/javascript"">
    $(function () {
        $(""#input1, #span1"", ""#input2"", ""#span2"").persianDatepicker();
    });
    $(""#input1,#input2"").persianDatepicker({
        months: [""فروردین"", ""اردیبهشت"", ""خرداد"", ""تیر"", ""مرداد"", ""شهریور"", ""مهر"", ""آبان"", ""آذر"", ""دی"", ""بهمن"", ""اسفند""],
        dowTitle: [""شنبه"", ""یکشنبه"", ""دوشنبه"", ""سه شنبه"", ""چهارشنبه"", ""پنج شنبه"", ""جمعه""],
        shortDowTitle: [""ش"", ""ی"", ""د"", ""س"", ""چ"", ""پ"", ""ج""],
        showGregorianDate: !1,
        persianNumbers: !0,
        formatDate: ""YYYY/MM/DD"",
        selectedBefore: !1,
        selectedDate: null,
   ");
            WriteLiteral(@"     startDate: null,
        endDate: null,
        prevArrow: '\u25c4',
        nextArrow: '\u25ba',
        theme: 'default',
        alwaysShow: !1,
        selectableYears: null,
        selectableMonths: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12],
        cellWidth: 25, // by px
        cellHeight: 20, // by px
        fontSize: 13, // by px
        isRTL: !1,
        format: 'YYYY/MM/DD',


        calendarPosition: {
            x: 0,
            y: 0,
        },
        onShow: function () { },
        onHide: function () { },
        onSelect: function () { },
        onRender: function () { }
    });





</script>
");
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
