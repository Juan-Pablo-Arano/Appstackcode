using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace WebAppStack
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            // Boton Consulta Registro
            if (TextBox1.Text == String.Empty)
            {
                Salida.Text = "Debe ingresar un número de Telefono";
            }
            else
            {
                String Conecta = System.Configuration.ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;
                SqlConnection Conexion = new SqlConnection(Conecta);
                Conexion.Open();
                SqlCommand BuscarNum = new SqlCommand("Select * From Agenda"
                           + " where phone ='" + Convert.ToDecimal(TextBox1.Text) + "'", Conexion);

                SqlDataReader Registros = BuscarNum.ExecuteReader();
                if (Registros.Read())
                {
                    TextBox2.Text = Registros[1].ToString();
                    TextBox3.Text = Registros[2].ToString();
                    TextBox4.Text = Registros[3].ToString();
                    Salida.Text = "Registro Consultado";                   
                }
                else
                {
                    Salida.Text = "Registro no encontrado";
                }

            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            // Boton Crear Registro
            if ((TextBox1.Text == String.Empty) || (TextBox2.Text == String.Empty) || (TextBox3.Text == String.Empty))
            {
                Salida.Text = "Debe ingresar todos los datos al formulario";
            }
            else
            {
                Decimal telefono = Convert.ToDecimal(TextBox1.Text);
                DateTime fechaCrea = DateTime.Now;
                string NombreC = TextBox2.Text;
                string EmailC = TextBox3.Text;
                try
                {
                    String Conecta = System.Configuration.ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;
                    SqlConnection Conexion = new SqlConnection(Conecta);
                    Conexion.Open();

                    SqlCommand Subirr = new SqlCommand("insert into Agenda (Phone,Nombre,Email,CreateDate) values ('" + telefono + "','" + NombreC + "','" + EmailC + "','" + fechaCrea + "') ", Conexion);

                    Subirr.ExecuteNonQuery();
                    Salida.Text = "Registro creado con exito...";
                    Conexion.Close();
                    TextBox1.Text = "";
                    TextBox2.Text = "";
                    TextBox3.Text = "";
                    TextBox4.Text = "";
                }
                catch (Exception)
                {
                    Salida.Text = "Error: Este número ya fue registrado con anterioridad";
                }                
                Response.Redirect("WebForm1.aspx");
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            // Boton Actualiza registro 
            String Valor = TextBox1.Text;
            if (TextBox1.Text == String.Empty)
            {
                Salida.Text = "Debe ingresar un número de Telefono";
            }
            else
            {
                String Conecta = System.Configuration.ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;
                SqlConnection Conexion = new SqlConnection(Conecta);
                Conexion.Open();
                SqlCommand BuscarNum = new SqlCommand("update Agenda set" + " Nombre='" + TextBox2.Text +
                                                                           "',Email='" + TextBox3.Text +
                                                    "' where Phone='" + Convert.ToDecimal(TextBox1.Text) + "'", Conexion);

                int borrado = BuscarNum.ExecuteNonQuery();
                if (borrado == 1)
                {
                    Salida.Text = "Registro Actualizado";
                }
                else
                {
                    Salida.Text = "Registro No Actualizado";
                }
                Response.Redirect("WebForm1.aspx");
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            // Boton Eliminar Registro
            if (TextBox1.Text == String.Empty)
            {
                Salida.Text = "Debe ingresar un número de Telefono";
            }
            else
            {
                String Conecta = System.Configuration.ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;
                SqlConnection Conexion = new SqlConnection(Conecta);
                Conexion.Open();

                SqlCommand BorrarNum = new SqlCommand("delete from Agenda"
                           + " where phone ='" + Convert.ToDecimal(TextBox1.Text) + "'", Conexion);

                TextBox1.Text = "";
                TextBox2.Text = "";
                TextBox3.Text = "";
                TextBox4.Text = "";

                int borrado = BorrarNum.ExecuteNonQuery();
                if (borrado == 1)
                {
                    Salida.Text = "Registro Eliminado";
                }
                else
                {
                    Salida.Text = "Registro no encontrado";
                }
                Response.Redirect("WebForm1.aspx");
            }
        }
    }
}