using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    private const String JS_KEY = "client_js";
    private const String LOCAL_IP = "218.242.250.212";

    protected void Page_Load(object sender, EventArgs e)
    {

        String clientJS = "<script language=\"javascript\"type=\"text/javascript\"> window.location.href=\"http://{0}{1}\"; </script> ";
       
        String clientIP = Request.UserHostAddress;
        String pathAndQuery = Request.Url.PathAndQuery;
        if (pathAndQuery.Equals("/default.aspx",StringComparison.OrdinalIgnoreCase)) pathAndQuery = "/redmine/projects/basicsop/news";

        if (clientIP.Equals(LOCAL_IP))
        {
            clientJS = String.Format(clientJS, "10.0.10.239", pathAndQuery);
        }
        else
        {
            clientJS = String.Format(clientJS, "218.242.250.212:2223", pathAndQuery);
        }

        WriteClientJS(clientJS);
    }


    private void WriteClientJS(String js)
    {
        if (!ClientScript.IsStartupScriptRegistered(JS_KEY))
        {
            ClientScript.RegisterStartupScript(this.GetType(),JS_KEY, js);
        }
    }
}