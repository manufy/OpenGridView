using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OpenGridWebDemo
{
    public partial class MasterDetailGrid : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void OpenGridViewFacturas_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Session["id"] = OpenGridViewFacturas.DataKeys[e.Row.DataItemIndex].Value.ToString();
            }
        }
    }
}
