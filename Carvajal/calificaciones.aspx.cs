using Carvajal.configuracion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Carvajal
{
    public partial class calificaciones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            dgvDatos.DataSource = Index();
            dgvDatos.DataBind();
        }

        public static DataTable Index()
        {
            Conexion.Conectar();
            DataTable data = new DataTable();
            string sql = "select * from alumno";
            SqlCommand cmd = new SqlCommand(sql, Conexion.Conectar());
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(data);
            return data;
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Conexion.Conectar();
            string sql_insertar = ("insert into alumno (rut,nombre,nota1,nota2,nota3) values(@rut,@nombre,@nota1,@nota2,@nota3)");
            SqlCommand cmd = new SqlCommand(sql_insertar, Conexion.Conectar());
            decimal nota1 = decimal.Parse(txtNota1.Text.Replace(",", "."),
                System.Globalization.CultureInfo.InvariantCulture);

            decimal nota2 = decimal.Parse(txtNota2.Text.Replace(",", "."),
                System.Globalization.CultureInfo.InvariantCulture);

            decimal nota3 = decimal.Parse(txtNota3.Text.Replace(",", "."),
                System.Globalization.CultureInfo.InvariantCulture);

            cmd.Parameters.AddWithValue("@rut", txtRut.Text);
            cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);
            cmd.Parameters.AddWithValue("@nota1", Convert.ToDecimal(txtNota1.Text));
            cmd.Parameters.AddWithValue("@nota2", Convert.ToDecimal(txtNota2.Text));
            cmd.Parameters.AddWithValue("@nota3", Convert.ToDecimal(txtNota3.Text));
            cmd.ExecuteNonQuery();
            dgvDatos.DataSource = Index();
            dgvDatos.DataBind();
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            Conexion.Conectar();
            string sql_eliminar = ("delete from alumno where rut=@rut");
            SqlCommand cmd = new SqlCommand(sql_eliminar, Conexion.Conectar());
            cmd.Parameters.AddWithValue("@rut", txtRut.Text);
            int i = cmd.ExecuteNonQuery();
            if (i != 0)
            {
                dgvDatos.DataSource = Index();
                dgvDatos.DataBind();

                txtRut.Text = "";
                txtNombre.Text = "";
                txtNota1.Text = "";
                txtNota2.Text = "";
                txtNota3.Text = "";
            }
        }

        protected void btnPromedio_Click(object sender, EventArgs e)
        {
            decimal nota1 = Convert.ToDecimal(txtNota1.Text);
            decimal nota2 = Convert.ToDecimal(txtNota2.Text);
            decimal nota3 = Convert.ToDecimal(txtNota3.Text);

            decimal promedio = (nota1 + nota2 + nota3) / 3;

            lblPromedio.Text = "Promedio: " + promedio.ToString("0.0");
        }

        protected void btnListado_Click(object sender, EventArgs e)
        {
            Conexion.Conectar();
            string sql = "select * from alumno";
            SqlCommand cmd = new SqlCommand(sql, Conexion.Conectar());
            SqlDataReader dr = cmd.ExecuteReader();

            lstAlumnos.Items.Clear();

            while (dr.Read())
            {
                decimal promedio =
                    (Convert.ToDecimal(dr["nota1"]) +
                    Convert.ToDecimal(dr["nota2"]) +
                    Convert.ToDecimal(dr["nota3"])) / 3;

                lstAlumnos.Items.Add(
                    dr["rut"].ToString() + "-" +
                    dr["nombre"].ToString() + " Promedio: " + promedio.ToString("0.0"));
            }
            dr.Close();
        }
    }
}