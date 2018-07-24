using System;
using FWS.Generic.Framework.Helpers.String.Html;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FWS.Generic.Framework.Testing.Helpers.String.Html
{
    [TestClass]
    public class HtmlStringHelperTests
    {
        [TestMethod]
        public void ExtractHeadFromHtmlStringShallReturnOnlyATheHeadTagFromAHtmlBlob()
        {
            //Small file test

            //Setup
            const string headTagHtml1 = "<head><script something blah blah /></head>";
            const string htmlString1 = "<html>" + headTagHtml1 + "</html>";

            //Exercise
            var result1 = HtmlStringHelper.ExtractHeadFromHtmlString(htmlString1);

            //Verify
            Assert.AreEqual(headTagHtml1, result1);

            //Large file test

            //Setup
            string htmlString2 = GetLiveTestHtml();

            //Exercise
            var result2 = HtmlStringHelper.ExtractHeadFromHtmlString(htmlString2);

            //Verify
            Assert.IsTrue(result2.Contains("text-decoration: none; color: purple}"));
        }

        private static string GetLiveTestHtml()
        {
            return $@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">
<html>
<head>
<title>pg00001</title>
<meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8""/>
<style>
<!-- 
body {{ background:#ffffff;}}
a:link {{text-decoration: none; color: blue}}
a:visited {{text-decoration: none; color: purple}}
a:active {{text-decoration: none; color: red}}
a:hover {{text-decoration: none; color:red}}
-->
</style>
<script type=""text/javascript""> 
<!-- hide 
function deserrs()
{{ 
return true; 
}} 
window.onerror = deserrs; 
// --> 
</script>
</head>
<body style = ""background-image:url(bgimg/backImg.jpg);"">
<script type=""text/javascript"">
var currentpos,timer; 
function initialize() 
{{ 
timer=setInterval(""scrollwindow()"",10);
}} 
function sc(){{
clearInterval(timer); 
}}
function scrollwindow() 
{{ 
currentpos=document.body.scrollTop; 
window.scroll(0,++currentpos); 
if (currentpos != document.body.scrollTop) 
sc();
}} 
document.onmousedown = sc;
document.ondblclick = initialize;
var tmp = ""<div style=\""position:absolute; top:"" + parent.offsetY + ""; left:"" + parent.offsetX + "";height:1032px; width:730px\"">"";
document.writeln(tmp);
</script>
<table border=""0"" height=""1032"" width=""730"">
<tr><td>
<div style=""position:absolute; top:0; left:0;""><img height=""1032"" width=""730""src=""bgimg/bg00001.jpg""/></div>
<div style=""position:absolute;top:336.361;left:46.662;""><nobr>
<span style=""font-size:18.863;color: #231f20;"">Activities</span>
<span style=""font-size:18.863;color: #231f20;"">to</span>
<span style=""font-size:18.863;color: #231f20;"">help</span>
<span style=""font-size:18.863;color: #231f20;"">students</span>
<span style=""font-size:18.863;color: #231f20;"">apply</span>
<span style=""font-size:18.863;color: #231f20;"">mathematical</span>
<span style=""font-size:18.863;color: #231f20;"">knowledge</span>
<span style=""font-size:18.863;color: #231f20;"">and</span>
<span style=""font-size:18.863;color: #231f20;"">strategies</span>
</nobr></div>
<div style=""position:absolute;top:897.056;left:449.558;""><nobr>
<span style=""font-size:28.601;color: #231f20;"">Suzi</span>
<span style=""font-size:28.601;color: #231f20;"">de</span>
<span style=""font-size:28.601;color: #231f20;"">Gouveia,</span>
</nobr></div>
<div style=""position:absolute;top:925.514;left:454.620;""><nobr>
<span style=""font-size:28.601;color: #231f20;"">Jackie</span>
<span style=""font-size:28.601;color: #231f20;"">Andrews</span>
</nobr></div>
<div style=""position:absolute;top:953.972;left:430.166;""><nobr>
<span style=""font-size:28.601;color: #ffffff;"">and</span>
<span style=""font-size:28.601;color: #231f20;"">Jude</span>
<span style=""font-size:28.601;color: #231f20;"">Callaghan</span>
</nobr></div>
</td></tr>
</table>
</div>
<script type=""text/javascript"">
var currentZoom = parent.toolbarWin.currentZoom;
if(currentZoom != undefined)
document.body.style.zoom=currentZoom/100;
</script>
</body>
</html>";
        }
    }
}
