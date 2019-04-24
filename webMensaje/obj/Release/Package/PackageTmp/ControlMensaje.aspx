<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="ControlMensaje.aspx.cs" Inherits="webMensaje.ControlMensaje" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


    <%-- <script type="text/javascript">
       google.charts.load("current", {packages:['corechart']});
    google.charts.setOnLoadCallback(drawChart);
    function drawChart() {
      //var data = google.visualization.arrayToDataTable([
      //  ["Element", "Density" ],
      //  ["Copper", 8.94],
      //  ["Silver", 10.49],
      // ["Gold", 19.30],
      //  ["Platinum", 21.45]
      //  ]);

        var data = google.visualization.arrayToDataTable(<%=ObtenerMensajes()%>);

      var view = new google.visualization.DataView(data);
      view.setColumns([0, 1]);
  


      var options = {
        title: "Grafica Temperatura por Muestra",
        titleTextStyle: {color: 'white'},
        width: 600,
        height: 400,
        backgroundColor: '#000000',
        bar: {groupWidth: "95%"},
          legend: { position: "none" },
          
       vAxis: {
   title: 'Temperatura', titleTextStyle: {color: '#FFFFFF'}
          }
        
      };
      var chart = new google.visualization.LineChart(document.getElementById("columnchart_values"));
      chart.draw(view, options);
  }
  </script>--%>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

      <div class="grid-x grid-padding-x ">
        <div class="large-12 cell box--large tituloPantalla">
            <h1><strong>CONTROL DE MENSAJES</strong></h1>
        </div>
    </div>
    <div class="row expanded">
        <div class="small-12 medium-10 large-10 columns medium-centered large-centered">
            <form method="post" action="ControlMensaje.aspx" runat="server" name="frmControlMensaje">
                <div id="dm" style="position: absolute; width: 100%; top: 0; left: 0;"></div>
            
                <div class="small-12 medium-12 large-12 columns">
                    

                    <div class="table-scroll">
                        <asp:GridView 
                            ID="gvCatalogo" 
                            runat="server" 
                            CssClass="responsive-card-table unstriped"
                            AutoGenerateColumns="false" 
                            DataKeyNames="IDmensaje" 
                         
                            ShowHeaderWhenEmpty="true" 
                            AllowPaging="true" 
                            PageSize="5">
                            <Columns>
                              
                                <asp:TemplateField HeaderText="ID">
                                    <ItemTemplate>
                                        <div class="show-for-small-only encabezado-card">
                                            <label>ID</label>
                                        </div>
                                        <div class="contenido-card">
                                            <asp:Label runat="server" Text='<%#Eval("IDmensaje") %>' ID="etqIdmensaje" />
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Fecha">
                                    <ItemTemplate>
                                        <div class="show-for-small-only encabezado-card">
                                            <label>Fecha</label>
                                        </div>
                                        <div class="contenido-card">
                                            <%# Eval("fecha") %>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mensaje Original">
                                    <ItemTemplate>
                                        <div class="show-for-small-only encabezado-card">
                                            <label>Mensaje Original</label>
                                        </div>
                                        <div class="contenido-card">
                                            <%# Eval("mensajeOriginal") %>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mensaje Encriptado">
                                    <ItemTemplate>
                                        <div class="show-for-small-only encabezado-card">
                                            <label>Mensaje Encriptado</label>
                                        </div>
                                        <div class="contenido-card">
                                            <%# Eval("mensajeEncriptado") %>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mensaje Recibido">
                                    <ItemTemplate>
                                        <div class="show-for-small-only encabezado-card">
                                            <label>Mensaje Recibido</label>
                                        </div>
                                        <div class="contenido-card">
                                            <%# Eval("mensajeRecibido") %>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mensaje Corregido">
                                    <ItemTemplate>
                                        <div class="show-for-small-only encabezado-card">
                                            <label>Mensaje Corregido</label>
                                        </div>
                                        <div class="contenido-card">
                                            <%# Eval("mensajeCorregido") %>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Resultados">
                                    <ItemTemplate>
                                        <div class="show-for-small-only encabezado-card">
                                            <label>Resultados</label>
                                        </div>
                                        <div class=" contenido-card">
                                             <asp:Button ID="btnResultadosTransmision" runat="server" CssClass="button btnPersonalizadoBase button-card" Text="VER RESULTADOS" OnClick="btnResultadosTransmision_Click" />

                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               
                              
                            </Columns>
                            <PagerTemplate>
                                <div class="paginacion">
                                    <ul class="pagination">
                                        <li class="pagination-previous">
                                            <asp:LinkButton ID="lbAnterior" runat="server" Text="Anterior" OnClick="Paginacion" 
                                                CommandName="anterior" />
                                        </li>
                                        <li class="current">
                                            <%=gvCatalogo.PageIndex + 1 %>
                                        </li>
                                        <li class="pagination-next">
                                            <asp:LinkButton ID="lbSiguente" runat="server" Text="Siguente" OnClick="Paginacion" 
                                                CommandName="siguiente" />
                                        </li>
                                    </ul>
                                </div>
                            </PagerTemplate>
                            <PagerStyle CssClass = "cPager" />
                        </asp:GridView>
                    </div>

                    

                </div>

                <%--INICIA AREA DE MODALES--%>

                <div id="mymodal" class="reveal modalBase" tabindex="-1" role="dialog" aria-hidden="true" data-reveal>
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Comunicaciones</h5>
                        </div>
                        <div class="modal-body">
                            <div id="columnchart_values" style="width:inherit; height: 300px;" ></div>
                        </div>
                        <asp:Button ID="btnCerrarModal" runat="server" CssClass="btnCerrarModal" Text="x" aria-label="Close modal" />
                    </div>
                </div>

                <%--FINALIZA AREA DE MODALES--%>

                <script src="js/vendor/jquery.js"></script>
                <script src="js/vendor/what-input.js"></script>
                <script src="js/vendor/foundation.js"></script>
                <script src="js/app.js"></script>
                <script src="js/Chart.js"></script>

            </form>
        </div>
    </div>


</asp:Content>
