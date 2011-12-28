using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OpenGridWebDemo
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

    


      

        protected void ObjectDataSourceDetalle_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters[0] = OpenGridViewFacturas.DataKeys[OpenGridViewFacturas.DataKeys.Count - 1].Value.ToString();
        
        }

       

    

     
    }
}
