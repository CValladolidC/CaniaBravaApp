using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using System.Data;


namespace CaniaBrava
{
    class ConvertirImagen
    {
        public static byte[] Image2Bytes(Image img)
        {
            string sTemp = Path.GetTempFileName();
            FileStream fs = new FileStream(sTemp, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            img.Save(fs, System.Drawing.Imaging.ImageFormat.Png);
            fs.Position = 0;
            //
            int imgLength = Convert.ToInt32(fs.Length);
            byte[] bytes = new byte[imgLength];
            fs.Read(bytes, 0, imgLength);
            fs.Close();
            return bytes;
        }

        /*public static byte[] Image2Bytes(Image pImagen)
        {
            byte[] mImage = null;
            try
            {
                if (pImagen != null)
                {
                    using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                    {
                        pImagen.Save(ms, pImagen.RawFormat);
                        mImage = ms.GetBuffer();
                        ms.Close();
                    }
                }
                else { mImage = null; }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return mImage;
        }
        */
        public static Image Bytes2Image(byte[] bytes)
        {
            if (bytes == null) return null;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                Bitmap bm = null;
                try
                {
                    bm = new Bitmap(ms);
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                return bm;
            }
        }

        public void guardarFoto(string vPathFoto)
        {
            string squery;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            
            byte[] Blob = ConvertirImagen.Image2Bytes(Image.FromFile(String.Format("{0}",vPathFoto)));

            squery = "INSERT INTO image (image) VALUES ('" + @Blob + "');";

            try
            {
                SqlCommand myCommand = new SqlCommand(squery, conexion);
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();

            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }
            conexion.Close();
                       
        }

        public void leerFoto(PictureBox pb)
        {
            
            try
            {
                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();

                SqlCommand cmd = new SqlCommand("SELECT image FROM image;", conexion);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "BLOB");
                int c = ds.Tables["BLOB"].Rows.Count;
                if (c > 0)
                {
                    System.Byte[] byteBLOBData = new System.Byte[0];
                    byteBLOBData = (Byte[])(ds.Tables["BLOB"].Rows[c - 1]["image"]);
                    System.IO.MemoryStream stmBLOBData = new System.IO.MemoryStream(byteBLOBData);
                    pb.Image = Image.FromStream(stmBLOBData);

                }
                conexion.Close();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }



        }
    }
}