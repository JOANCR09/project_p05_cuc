<%@ Page Title="" Language="C#" MasterPageFile="~/PMPrincipal.Master" AutoEventWireup="true" CodeBehind="Estudiantes.aspx.cs" Inherits="WebProyecto2_3.Estudiantes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1>Mantenimientos de los 
estudiantes</h1>
    	<div class="fh5co-explore fh5co-explore1">
			<div class="container">
				<div class="row">
					<div class="col-md-8 col-md-push-5 animate-box">
						<img class="img-responsive" src="images/work_1.png" alt="work">
					</div>
					<div class="col-md-4 col-md-pull-8 animate-box">
						<div class="mt">
							<h3>Ver Listado de Estudiantes</h3>
							<p>En este modulo podemos realizar distintans acciones con los estudiantes ya registrados</p>
							<ul class="list-nav">
								<li><i class="icon-check2"></i>Ver los Estudiantes Matriculados</li>
								<li><i class="icon-check2"></i>Modificar el Estudiante seleccionado</li>
								<li><i class="icon-check2"></i>Eliminar Estudiantes</li>
							</ul>
							<%--<p><a  href="EstudiantesMostrar.aspx"><i class="icon-eye"></i> Ver Estudiantes</a></p>--%>
                            <asp:Button ID="BTNVer" runat="server" Text="Ver Estudiantes" CssClass="btn btn-primary" OnClick="BTNVer_Click"/>
						</div>
					</div>
				</div>
			</div>
		</div>

</asp:Content>
