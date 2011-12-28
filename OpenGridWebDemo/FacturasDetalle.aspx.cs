using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OpenGridWebDemo
{
    public partial class FacturasDetalle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void OpenGridViewFacturasDetalle_InsertCommand(object sender, EventArgs e)
        {
            OpenControls.OpenGridView opengridview = (OpenControls.OpenGridView) sender;
            opengridview.ShowFooter = !opengridview.ShowFooter;
            //ObjectDataSourceFacturasDetalle.Insert();
        }
    }
}
