using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DatosEstudiantes;

namespace WebProyecto2_3
{
    public partial class MostrarEstudiantes : System.Web.UI.Page
    {
        EMostrar EM = new EMostrar();
        EEliminar ED = new EEliminar();
        protected void Page_Load(object sender, EventArgs e)
        {
          
            GridView1.DataSource = EM.Mostrar();
            GridView1.DataBind();
           
        }
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
           TableCell cell = GridView1.Rows[e.RowIndex].Cells[1];
            int cod = int.Parse( cell.Text);
            ED.Eliminar(cod);
            this.Page.Response.Write("<script language='JavaScript'>window.alert(' Estudiante Eliminado');</script>");
            Response.Redirect("~/EstudiantesMostrar.aspx");
        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
        //    string codigo = GridView1.SelectedRow.Cells[1].Text;
        //    string nombre = GridView1.SelectedRow.Cells[2].Text;
        //    string cantidad = GridView1.SelectedRow.Cells[3].Text;
        //    string bodega = GridView1.SelectedRow.Cells[4].Text;
        //    Response.Redirect("~/Modificar.aspx" + "?" + "codigo=" + codigo + "&" + "nombre=" + nombre + "&" + "cantidad=" + cantidad + "&" + "bodega=" + bodega);
        }
    }
}