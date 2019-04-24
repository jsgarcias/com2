<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="BERyGraficas.aspx.cs" Inherits="webMensaje.BERyGraficas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="js/Chart.js"></script>
     <script type="text/javascript">
       google.charts.load("current", {packages:['corechart']});
    google.charts.setOnLoadCallback(drawChart);
    function drawChart() {
      

        var data = google.visualization.arrayToDataTable(<%=ObtenerDatosEstadistica()%>);

      var view = new google.visualization.DataView(data);
      view.setColumns([0, 1]);
  


      var options = {
        title: "Grafica Bits Originales",
        titleTextStyle: {color: 'white'},
        width: 600,
        height: 400,
        backgroundColor: '#000000',
        bar: {groupWidth: "95%"},
          legend: { position: "none" },
          
       vAxis: {
   title: 'Bit', titleTextStyle: {color: '#FFFFFF'}
          }
        
      };
      var chart = new google.visualization.LineChart(document.getElementById("columnchart_values"));
      chart.draw(view, options);
  }
  </script>

      <script type="text/javascript">
       google.charts.load("current", {packages:['corechart']});
    google.charts.setOnLoadCallback(drawChart);
    function drawChart() {
      

        var data = google.visualization.arrayToDataTable(<%=ObtenerDatosEstadisticaRecibidos()%>);

      var view = new google.visualization.DataView(data);
      view.setColumns([0, 1]);
  


      var options = {
        title: "Grafica Bits Recibidos",
        titleTextStyle: {color: 'white'},
        width: 600,
        height: 400,
        backgroundColor: '#000000',
        bar: {groupWidth: "95%"},
          legend: { position: "none" },
          
       vAxis: {
   title: 'Bit', titleTextStyle: {color: '#FFFFFF'}
          }
        
      };
      var chart = new google.visualization.LineChart(document.getElementById("columnchart_values1"));
      chart.draw(view, options);
  }
  </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div class="grid-x grid-padding-x ">
        <div class="large-12 cell box--large tituloPantalla">
            <h1><strong>DETALLE DE LA TRANSMISION</strong></h1>
        </div>
    </div>
    <div class="row expanded">
        <div class="small-12 medium-10 large-10 columns medium-centered large-centered">
            <form method="post" action="BERyGraficas.aspx" runat="server" name="frmBERyGraficas">
                <div id="dm" style="position: absolute; width: 100%; top: 0; left: 0;"></div>
            
                <div class="small-12 medium-12 large-12 columns">
                    
                    <div class="row box--small centrarDiv">
                    <div class="small-12 medium-12 large-12 columns">
                        <asp:Label ID="lblBER" runat="server" Text="" CssClass="label"></asp:Label>
                    </div>
                        </div>
                    <div class="row">
                     <div class="small-12 medium-12 large-12 columns">
                                  <div class="table-scroll">
                                        <div class="row" style="margin-bottom:20px;">
                                            <div class="small-12 medium-6 large-6 columns small-centered medium-centered large-centered">
                                                <div id="columnchart_values" ></div>
                                            </div>
                                               <div class="small-12 medium-6 large-6 columns small-centered medium-centered large-centered">
                                                <div id="columnchart_values1" ></div>
                                            </div>
                                          
                                        </div>
                                
                                </div>
                                    </div>
                        </div>

                </div>

                <%--INICIA AREA DE MODALES--%>

                <div id="mymodal" class="reveal modalBase" tabindex="-1" role="dialog" aria-hidden="true" data-reveal>
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Comunicaciones</h5>
                        </div>
                        <div class="modal-body">
                           
                        </div>
                        <asp:Button ID="btnCerrarModal" runat="server" CssClass="btnCerrarModal" Text="x" aria-label="Close modal" />
                    </div>
                </div>

                <%--FINALIZA AREA DE MODALES--%>

                
                <div>
                    <asp:HiddenField ID="hiddenIDmensaje" Value="0" Visible="false" runat="server" />
                </div>

                <script src="js/vendor/jquery.js"></script>
                <script src="js/vendor/what-input.js"></script>
                <script src="js/vendor/foundation.js"></script>
                <script src="js/app.js"></script>
                

            </form>
        </div>
    </div>



</asp:Content>
