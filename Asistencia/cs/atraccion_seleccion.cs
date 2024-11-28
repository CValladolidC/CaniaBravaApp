using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;
using CaniaBrava.Interface;

namespace CaniaBrava.cs
{
    class atraccion_seleccion
    {

        SqlCommand cmd;
        SqlDataReader dr;
        

        public void niveleducativo(ComboBox cb)
        {

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string squerye = "SELECT DISTINCT(idmaesgen) id, desmaesgen FROM Asistencia.dbo.maesgen WHERE idmaesgen = '006' AND statemaesgen = 'V' ";

            try
            {
                cmd = new SqlCommand(squerye, conexion);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cb.Items.Add(dr["desmaesgen"].ToString());
                }
                cb.SelectedIndex = 0;
                dr.Close();

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }
        }

        public void tipodocumento(ComboBox cb)
        {

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string squerye = "SELECT DISTINCT(idmaesgen) id, desmaesgen  FROM Asistencia.dbo.maesgen (nolock) WHERE idmaesgen = '002' AND statemaesgen = 'V' ";

            try
            {
                cmd = new SqlCommand(squerye, conexion);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cb.Items.Add(dr["desmaesgen"].ToString());
                }
                cb.SelectedIndex = 0;
                dr.Close();

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }
        }

        public void sexo(ComboBox cb)
        {

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string squerye = "SELECT DISTINCT(idmaesgen) id, desmaesgen  FROM Asistencia.dbo.maesgen (nolock) WHERE idmaesgen = '019' AND statemaesgen = 'V' ";

            try
            {
                cmd = new SqlCommand(squerye, conexion);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cb.Items.Add(dr["desmaesgen"].ToString());
                }
                cb.SelectedIndex = 0;
                dr.Close();

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }
        }

        public void estadocivil(ComboBox cb)
        {

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string squerye = "SELECT DISTINCT(idmaesgen) id, desmaesgen  FROM Asistencia.dbo.maesgen (nolock) WHERE idmaesgen = '001' AND statemaesgen = 'V' ";

            try
            {
                cmd = new SqlCommand(squerye, conexion);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cb.Items.Add(dr["desmaesgen"].ToString());
                }
                cb.SelectedIndex = 0;
                dr.Close();

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }
        }

        public void licenciaconducir(ComboBox cb)
        {

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string squerye = "SELECT DISTINCT(idmaesgen) id, desmaesgen FROM Asistencia.dbo.maesgen (nolock) WHERE idmaesgen = '004' AND statemaesgen = 'V' ";

            try
            {
                cmd = new SqlCommand(squerye, conexion);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cb.Items.Add(dr["desmaesgen"].ToString());
                }
                cb.SelectedIndex = 0;
                dr.Close();

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }
        }

        public void gerencia(ComboBox cb)
        {

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string squerye = "SELECT DISTINCT(idmaesgen) id, desmaesgen FROM Asistencia.dbo.maesgen WHERE idmaesgen = '040' AND statemaesgen = 'V' ";

            try
            {
                cmd = new SqlCommand(squerye, conexion);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cb.Items.Add(dr["desmaesgen"].ToString());
                }
                cb.SelectedIndex = 0;
                dr.Close();

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }
        }

        public void unidadorganizativa(ComboBox cb)
        {

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string squerye = "SELECT DISTINCT(idmaesgen) id, desmaesgen FROM Asistencia.dbo.maesgen WHERE idmaesgen = '008' AND statemaesgen = 'V' ";

            try
            {
                cmd = new SqlCommand(squerye, conexion);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cb.Items.Add(dr["desmaesgen"].ToString());
                }
                cb.SelectedIndex = 0;
                dr.Close();

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }
        }

        public void posicion(ComboBox cb)
        {

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string squerye = "SELECT DISTINCT(idmaesgen) id, desmaesgen  FROM Asistencia.dbo.maesgen (nolock) WHERE idmaesgen = '050' AND statemaesgen = 'V' ";

            try
            {
                cmd = new SqlCommand(squerye, conexion);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cb.Items.Add(dr["desmaesgen"].ToString());
                }
                cb.SelectedIndex = 0;
                dr.Close();

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }
        }

        public void sociedad(ComboBox cb)
        {

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string squerye = "SELECT DISTINCT(codaux) id_soc, descia FROM Asistencia.dbo.ciafile ";

            try
            {
                cmd = new SqlCommand(squerye, conexion);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    cb.Items.Add(dr["id_soc"].ToString());
                }

                cb.SelectedIndex = 0;
                dr.Close();

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }
        }

        public void eliminar_user_atraccion_seleccion(string idusr)
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "DELETE FROM Asistencia.dbo.gestiontalento WHERE idregistro ='" + @idusr + "';";
           
            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
        }


        public bool CargarData(DataTable tbData)
        {
            bool resultado = true;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            {
                conexion.Open();
                using (SqlBulkCopy s = new SqlBulkCopy(conexion))
                {

                    //ingresamos COLUMNAS ORIGEN | COLUMNAS DESTINOS

                    s.ColumnMappings.Add("Cod. empleado", "idempleado");
                    s.ColumnMappings.Add("Nombre de ingreso", "nombres");
                    s.ColumnMappings.Add("Nivel Educativo", "niveleducativo");
                    s.ColumnMappings.Add("Tipo Documento", "tipodocumento");
                    s.ColumnMappings.Add("DNI", "dni");
                    s.ColumnMappings.Add("Celular", "celular");
                    s.ColumnMappings.Add("Telefono", "telefono");
                    s.ColumnMappings.Add("Nacionalidad", "nacionalidad");
                    s.ColumnMappings.Add("Distrito", "distrito");
                    s.ColumnMappings.Add("Provincia", "provincia");
                    s.ColumnMappings.Add("Departamento", "departamento");
                    s.ColumnMappings.Add("Referencia", "referencia");
                    s.ColumnMappings.Add("Fecha Nacimiento", "fechanacimieto");
                    s.ColumnMappings.Add("Sexo", "sexo");
                    s.ColumnMappings.Add("Estado Civil", "estadocivil");
                    s.ColumnMappings.Add("Cat. Licencia", "categoria");
                    s.ColumnMappings.Add("Num Licencia", "licencia");
                    s.ColumnMappings.Add("Gerencia", "gerencia");
                    s.ColumnMappings.Add("Unidad Organizativa", "unidorganizativa");
                    s.ColumnMappings.Add("Nombre de la posición", "posicion");
                    s.ColumnMappings.Add("Sociedad", "sociedad");
                    s.ColumnMappings.Add("Nombre de Sociedad", "nomsociedad");
                    s.ColumnMappings.Add("email", "email");
                    s.ColumnMappings.Add("Jefatura C.I", "jefatura");
                    s.ColumnMappings.Add("Responsable RRHH", "responsablerrhh");
                    s.ColumnMappings.Add("Nivel Organizacional", "nivelorganizacional");
                    s.ColumnMappings.Add("Sede", "sede");
                    s.ColumnMappings.Add("Origen", "origen");
                    s.ColumnMappings.Add("Reemplazo de", "reemplazode");
                    s.ColumnMappings.Add("Tipo de proceso", "tipoproceso");
                    s.ColumnMappings.Add("Medio de Atención", "medioatencion");
                    s.ColumnMappings.Add("Fuente de postulación", "fuentepostulacion");
                    s.ColumnMappings.Add("Modalidad", "modalidad");
                    s.ColumnMappings.Add("Tipo de contrato", "tipocontrato");
                    s.ColumnMappings.Add("Vacantes", "vacantes");
                    s.ColumnMappings.Add("Evaluados", "cantevaluados");
                    s.ColumnMappings.Add("Est Proceso", "estproceso");
                    s.ColumnMappings.Add("Prioridad", "prioridad");
                    s.ColumnMappings.Add("Proveedor", "proveedor");
                    s.ColumnMappings.Add("Estatus ocupación", "estatusocupacion");
                    s.ColumnMappings.Add("Nro Long List Enviados", "nrolong");
                    s.ColumnMappings.Add("Satisfacción del proceso (al cliente interno)", "satisfaccion");
                    s.ColumnMappings.Add("Comentarios", "comentarios");
                    s.ColumnMappings.Add("Carta oferta", "cartaoferta");
                    s.ColumnMappings.Add("Induccion", "induccion");
                    s.ColumnMappings.Add("Inicio de proceso", "feinicioproceso");
                    s.ColumnMappings.Add("Fecha Fin Estimada", "feestimacion");
                    s.ColumnMappings.Add("Fecha cierre ", "fecierre");
                    s.ColumnMappings.Add("Fecha incorporación", "feincorporacion");
                    s.ColumnMappings.Add("Días", "Totaldias");

                    //definimos la tabla a cargar
                    s.DestinationTableName = "gestiontalento";


                    s.BulkCopyTimeout = 1500;
                    try
                    {
                        s.WriteToServer(tbData);
                    }
                    catch (Exception e)
                    {
                        string st = e.Message;
                        resultado = false;
                    }


                }
            }

            return resultado;
        }

    }
}
