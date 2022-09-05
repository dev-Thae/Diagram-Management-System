using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Diyagram_Yönetim_Sistemi
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            highlightCurrentPage(sender, e);
        }

        private void highlightCurrentPage(object sender, EventArgs e){
            switch (this.Page.Title)
            {
                case "Ana Sayfa":
                    home.Attributes.Add("class", home.Attributes["class"] + " active");
                    break;
                case "Uygulama":
                    app.Attributes.Add("class", app.Attributes["class"] + " active");
                    break;
                case "UygulamaV2":
                    app2.Attributes.Add("class", app.Attributes["class"] + " active");
                    break;
                case "Hakkında":
                    about.Attributes.Add("class", about.Attributes["class"] + " active");
                    break;
                case "İletişim":
                    contact.Attributes.Add("class", contact.Attributes["class"] + " active");
                    break;
                default:
                    break;
            }
        }
    }
}