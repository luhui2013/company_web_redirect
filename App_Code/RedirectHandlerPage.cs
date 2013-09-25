using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// RedirectHandlerPage 的摘要说明
/// </summary>
public class RedirectHandlerPage : IHttpHandler
{
    private  const String JS_KEY = "client_js";
    private  const String LOCAL_IP = "218.242.250.212";

    public static string defaultHome
    {
        get{
            return System.Configuration.ConfigurationManager.AppSettings["default_home"].ToString();
        }
    }

    public void ProcessRequest(HttpContext context)
    {
        String clientJS = "<script language=\"javascript\"type=\"text/javascript\"> window.location.href=\"http://{0}{1}\"; </script>";

        HttpRequest Request = context.Request;

        String clientIP = Request.UserHostAddress;
        String pathAndQuery = Request.Url.PathAndQuery;
        pathAndQuery=pathAndQuery.Trim().ToLower();

        if (pathAndQuery.Length == 0 || pathAndQuery.Equals("/") || pathAndQuery.Equals("/default.aspx"))
        {
            pathAndQuery = RedirectHandlerPage.defaultHome;
        }
        if (clientIP.Equals(LOCAL_IP))
        {
            clientJS = String.Format(clientJS, "10.0.10.239", pathAndQuery);
        }
        else
        {
            clientJS = String.Format(clientJS, "218.242.250.212:2223", pathAndQuery);
        }

        context.Response.ContentType = "text/html";
        context.Response.Write(clientJS);
        context.Response.Flush();
        context.Response.Close();

    }

    public bool IsReusable
    {
        get
        {
            return true;
        }
    }
}