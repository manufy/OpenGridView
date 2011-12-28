using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

using OpenGridView;

namespace OpenControls
{
     public  class ResourceFinder : OpenGridView
    {

         public string GetEditImageUrl2()
         {
             var html = Page.ClientScript.GetWebResourceUrl(typeof(OpenGridView), "OpenGridView.Images.lapizadd.png");
             return html;
         }


        private void cds()
        {
            

        }

        public string GetEditImageUrl()
        {
            var html = Page.ClientScript.GetWebResourceUrl(typeof(OpenGridView), "OpenGridView.Images.lapizadd.png");
            return html;
        }

        static public string Get(string image)
        {
            
            //string gg = this.GetEditImageUrl();
            //    return this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "OpenGridView.Images.lapizadd.png");
            return "";
        }


          public string GetCommandButtonUrl(string command)
        {
            string result = "";
            switch (command)
            {
                case "Edit":
                    result = GetEditImageUrl();
                    break;
            }
             return result;
        }
    }
}
