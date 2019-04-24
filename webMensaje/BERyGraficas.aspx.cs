using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webMensaje
{
    public partial class BERyGraficas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    long mensajeID = Convert.ToInt64(Request.QueryString["IDmensaje"].ToString());
                    hiddenIDmensaje.Value = mensajeID.ToString();
                    CalculaBER(mensajeID);
                }
            }
            catch (Exception ex)
            { }
        }

        protected void CalculaBER(long mensajeID)
        {
            ControlMensaje xmensaje = new ControlMensaje();
            try
            {
            
                using (var db = new ProyectoCom2Entities1())
                {
                    var consulta = (from x in db.ControlMensajes
                                    where x.IDmensaje == mensajeID
                                    select x).FirstOrDefault();

                    if (consulta != null)
                    {
                        xmensaje = consulta;
                    }


                    //Variables datos recibidos
                    //Se lee el archivo con los datos recibidos desencriptados
                // StreamReader archDatosDecript = new StreamReader("c:\\jp/1s2019/Comunicaciones2/Lab/Receptor/DatosRecibidosDecript.txt");
                string archivoDesencriptado = xmensaje.mensajeRecibido; //archDatosDecript.ReadToEnd();
                char[] arregloCaracteresASCII = archivoDesencriptado.ToCharArray();
                string valorBinario = "";
                int posicionCaracter = 0;
                char[] arregloBinarioRecibido;
                int bitRecibido = 0;

                    //variables datos mensaje original
                //StreamReader archMensajeOriginal = new StreamReader("c:\\jp/1s2019/Comunicaciones2/Lab/Receptor/DatosCorrectos.txt");
                string mensajeOriginal = xmensaje.mensajeOriginal; //archMensajeOriginal.ReadToEnd();
                char[] arregloMensajeOriginal = mensajeOriginal.ToCharArray();
                char caracterOriginal;
                string valorBinarioOriginal;
                char[] arregloBinarioOriginal;
                int comparacionBinarios = 0;
                int bitOriginal = 0;
                int bitsErroneos = 0;
                int bitsTransmitidos = 0;

                //variables BER
                decimal BER = 0;
                    string sBinariosOriginales = "";

                //se recorre el arreglo de caracteres para convertir a binario
                foreach (char caracter in arregloCaracteresASCII)
                {
                    //se convierte el caracter en binario
                    valorBinario = Convert.ToString(caracter, 2).PadLeft(8, '0');
                    //se compara el valor obtenido con el correspondiente del archivo correcto.
                    caracterOriginal = arregloMensajeOriginal[posicionCaracter];
                    valorBinarioOriginal = Convert.ToString(caracterOriginal, 2).PadLeft(8, '0');
                        sBinariosOriginales = sBinariosOriginales + valorBinarioOriginal;
                    //se convierte en arreglo los binarios y se recorre cada uno de los valores en arreglo comparando cuantos bits no coinciden (en cuantos hay error)
                    //dado que se sabe que ambos tendran una longitud de 8 posiciones (Por ASCII) entonces se recorren y comparan al mismo tiempo.
                    arregloBinarioRecibido = valorBinario.ToArray();
                    arregloBinarioOriginal = valorBinarioOriginal.ToArray();

                    foreach (char caracterBinario in arregloBinarioOriginal)
                    {
                        bitRecibido = Convert.ToInt32(arregloBinarioRecibido[comparacionBinarios]);
                        bitOriginal = Convert.ToInt32(caracterBinario);

                        //se comparan ambos valores, si no son iguales entonces se recibio un bit erroneo y se ira acumulando este valor
                        if (bitRecibido != bitOriginal)
                        {
                            bitsErroneos++;
                        }

                        //Se suman los bits transmitidos y la comparacion de binarios (limite de 8)
                        bitsTransmitidos++;
                        comparacionBinarios++;
                    }

                    //Se reinicia la variable para el siguiente caracter de 8 bits
                    comparacionBinarios = 0;
                    //se incrementa la posicion del caracter para la siguiente lectura
                    posicionCaracter++;

                }

                //archDatosDecript.Close();
                //archMensajeOriginal.Close();

                //se calcula el BER (bits erroneos / bits transmitidos)
                BER = decimal.Round((Decimal)bitsErroneos / (Decimal)bitsTransmitidos, 6);
                lblBER.Text = bitsErroneos.ToString() + " " + "de" + " " + bitsTransmitidos.ToString() + " " + "bits fueron erroneos. La tasa de BER es de:" + " " + BER.ToString();
                }

                //mostrar grafico


            }
            catch (Exception ex)
            {
            }
        }

        protected string ObtenerDatosEstadistica()
        {
           


          
                string strDatos;
                strDatos = "[['NumeroBit', 'Bit'],";
                ControlMensaje xmensaje = new ControlMensaje();
            long IDmensaje = 0;
            long.TryParse(hiddenIDmensaje.Value, out IDmensaje);
            using (var db = new ProyectoCom2Entities1())
                {
                    var consulta = (from x in db.ControlMensajes
                                    where x.IDmensaje == IDmensaje
                                    select x).FirstOrDefault();

                    if (consulta != null)
                    {
                        xmensaje = consulta;
                    }

                string valorBinarioOriginal = "";
                string valorBinario = "";
                char caracterOriginal;
                string mensajeOriginal = xmensaje.mensajeOriginal; 
                char[] arregloMensajeOriginal = mensajeOriginal.ToCharArray();
                string sBinariosOriginales = "";
                int posicionCaracter = 0;
                long numeroBit = 0;

                foreach (char caracter in arregloMensajeOriginal)
                {
                    //se convierte el caracter en binario
                    valorBinario = Convert.ToString(caracter, 2).PadLeft(8, '0');
                    //se compara el valor obtenido con el correspondiente del archivo correcto.
                    caracterOriginal = arregloMensajeOriginal[posicionCaracter];
                    valorBinarioOriginal = Convert.ToString(caracterOriginal, 2).PadLeft(8, '0');
                    sBinariosOriginales = sBinariosOriginales + valorBinarioOriginal;
                    posicionCaracter++;
                
                }

                char[] arregloGraficaBinariosOriginales = sBinariosOriginales.ToArray();
               
                foreach (char bit in arregloGraficaBinariosOriginales)
                        {
                            numeroBit++;
                            strDatos = strDatos + "[";
                            strDatos = strDatos + "'" + numeroBit + "'" + "," + bit.ToString();
                            strDatos = strDatos + "],";

                        }
                    strDatos = strDatos.TrimEnd(',') + "]";

                }
            
                return strDatos;

              
        }

        protected string ObtenerDatosEstadisticaRecibidos()
        {




            string strDatos;
            strDatos = "[['NumeroBit', 'Bit'],";
            ControlMensaje xmensaje = new ControlMensaje();
            long IDmensaje = 0;
            long.TryParse(hiddenIDmensaje.Value, out IDmensaje);

            using (var db = new ProyectoCom2Entities1())
            {
                var consulta = (from x in db.ControlMensajes
                                where x.IDmensaje == IDmensaje
                                select x).FirstOrDefault();

                if (consulta != null)
                {
                    xmensaje = consulta;
                }

                string valorBinarioOriginal = "";
                string valorBinario = "";
                char caracterOriginal;
                string mensajeOriginal = xmensaje.mensajeRecibido;
                char[] arregloMensajeOriginal = mensajeOriginal.ToCharArray();
                string sBinariosOriginales = "";
                int posicionCaracter = 0;
                long numeroBit = 0;

                foreach (char caracter in arregloMensajeOriginal)
                {
                    //se convierte el caracter en binario
                    valorBinario = Convert.ToString(caracter, 2).PadLeft(8, '0');
                    //se compara el valor obtenido con el correspondiente del archivo correcto.
                    caracterOriginal = arregloMensajeOriginal[posicionCaracter];
                    valorBinarioOriginal = Convert.ToString(caracterOriginal, 2).PadLeft(8, '0');
                    sBinariosOriginales = sBinariosOriginales + valorBinarioOriginal;
                    posicionCaracter++;

                }

                char[] arregloGraficaBinariosOriginales = sBinariosOriginales.ToArray();

                foreach (char bit in arregloGraficaBinariosOriginales)
                {
                    numeroBit++;
                    strDatos = strDatos + "[";
                    strDatos = strDatos + "'" + numeroBit + "'" + "," + bit.ToString();
                    strDatos = strDatos + "],";

                }
                strDatos = strDatos.TrimEnd(',') + "]";

            }

            return strDatos;


        }
    }
}