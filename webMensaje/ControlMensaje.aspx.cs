using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webMensaje
{
    public partial class ControlMensaje : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObtenerMensajes();
            }
           
        }

        public ControlMensaje[] CatalogoMensajes()
        {
            ControlMensaje[] resultado = null;
            try
            {
                

                using (var db = new ProyectoCom2Entities1())
                {
                    var consulta = from x in db.ControlMensajes
                                   select x;

                    if (consulta != null && consulta.Count() > 0)
                    {
                        resultado = consulta.ToArray();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return resultado;
        }

        public void ObtenerMensajes()
        {
            try
            {
                ControlMensaje[] data = CatalogoMensajes();
                if (data != null)
                {


                    gvCatalogo.DataSource = data;
                    gvCatalogo.DataBind();
                    gvCatalogo.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                else
                {
                    gvCatalogo.DataSource = string.Empty;
                    gvCatalogo.DataBind();
                    gvCatalogo.HeaderRow.TableSection = TableRowSection.TableHeader;
                }


            }
            catch (Exception ex)
            {
            }
        }

        protected void Paginacion(object sender, EventArgs e)
        {
            LinkButton linkButton = (LinkButton)sender;
            switch (linkButton.CommandName)
            {
                case "anterior":
                    if (gvCatalogo.PageIndex > 0)
                    {
                        gvCatalogo.PageIndex--;
                        CatalogoMensajes();
                    }
                    break;
                case "siguiente":
                    if (gvCatalogo.PageIndex < gvCatalogo.PageCount)
                    {
                        gvCatalogo.PageIndex++;
                        CatalogoMensajes();
                    }
                    break;
            }
        }

        protected void btnResultadosTransmision_Click(object sender, EventArgs e)
        {
            int rowIndex = ((sender as Button).NamingContainer as GridViewRow).RowIndex;
            long IDmensaje = Convert.ToInt32(gvCatalogo.DataKeys[rowIndex].Values[0]);

            Response.Redirect("BERyGraficas.aspx?IDmensaje=" + IDmensaje.ToString(), false);
        }

        //protected string ObtenerDatosEstadistica()
        //{
        //    long idencabezado = 0;
        //    long.TryParse(hiddenIdEncabezado.Value, out idencabezado);


        //    using (var db = new IncubadoraEntities())
        //    {
        //        var queryValores = (from x in db.DetalleMuestra
        //                            where x.IDencabezado == idencabezado
        //                            select new { x.NumeroMuestra, x.presion }).ToList();

        //        string strDatos;
        //        strDatos = "[['NumeroMuestra', 'Presion'],";

        //        foreach (var fila in queryValores)
        //        {
        //            strDatos = strDatos + "[";
        //            strDatos = strDatos + "'" + fila.NumeroMuestra + "'" + "," + fila.presion;
        //            strDatos = strDatos + "],";

        //        }
        //        strDatos = strDatos.TrimEnd(',') + "]";

        //        // string strDatosprueba = "[" + "[" + "'Element'" + "," + "'Density'" + "]" + "," + "[" + "'Copper'" + "," + "15.94" + "]," + "[" + "'Copper'" + "," + "15.94" + "]" + "]";

        //        return strDatos;

        //        // gvDetalleProduccion.Columns[0].HeaderText = "TextoAMostrarEnLaCabecera";
        //    }
        //}


    }
}