using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OpenGridWebDemo
{
    public partial class TiposDeArticulo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void OpenGridViewTiposDeArticulo_FilterCommand(object sender, OpenControls.OpenGridView.FilterCommandEventArgs e)
        {
            ObjectDataSourceTiposDeArticulo.FilterExpression = e.FilterExpression;
        }
    }
}
