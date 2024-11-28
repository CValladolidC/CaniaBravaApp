using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace CaniaBrava
{
    public partial class ui_guardarimangen : Form
    {
        public ui_guardarimangen()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog Abrir = new OpenFileDialog();
            Abrir.Title = "Seleccionar imagen";
            Abrir.Filter = "JPG(*.jpg)|*.jpg|PNG(*.png)|*.png|GIF(*.gif)|*.gif|Todos(*.Jpg, *.Png, *.Gif, *.Tiff, *.Jpeg, *.Bmp)|*.Jpg; *.Png; *.Gif; *.Tiff; *.Jpeg; *.Bmp";
            Abrir.FilterIndex = 4;
            Abrir.RestoreDirectory = true;

            if (Abrir.ShowDialog() == DialogResult.OK)
            {
                string Dir = Abrir.FileName;
                txtRuta.Text = Dir;
                Bitmap Picture = new Bitmap(Dir);
                pictureBoxImg.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBoxImg.Image = (Image)Picture;
            }
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            string ruta = txtRuta.Text.Trim();
            ConvertirImagen convertirimagen = new ConvertirImagen();
            convertirimagen.guardarFoto(ruta);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ConvertirImagen convertirimagen = new ConvertirImagen();
            convertirimagen.leerFoto(pictureBoxMostrar);

        }

        private void pictureBoxImg_Click(object sender, EventArgs e)
        {

        }
    }
}