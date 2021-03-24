using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebProyecto2_3
{
    public partial class Estudiantes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BTNVer_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/EstudiantesMostrar.aspx");
        }
    }
}