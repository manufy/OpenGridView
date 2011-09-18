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

        protected void OpenGridViewFacturas_OnInsertion(object sender, EventArgs e)
        {
            // TODO: Insertar material de composición. Establecer parámetros por defecto en el ObjectDataSource.

            // Page.IsValid se efectua si las propiedades del OpenGridView al respecto está establecidas correctamente
               ObjectDataSourceFacturas.Insert();
        }

        protected void ObjectDataSourceFacturas_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            // Hacer el "binding" a mano con los parámetros de inserción

            var fecha =
                (TextBox)OpenGridViewFacturas.FooterRow.FindControl("TextBoxInsertFecha");
             e.InputParameters["fecha"] = fecha.Text;
           
        }

      
    }
}
