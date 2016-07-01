﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowsFormsApplication1.ConexionBD;

namespace WindowsFormsApplication1.ABM_Usuario
{
    public partial class btn_crearCliente : Form
    {
        public btn_crearCliente()
        {
            InitializeComponent();
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void CrearCliente_Load(object sender, EventArgs e)
        {
            BaseDeDatos bd = new BaseDeDatos();
            var spObtenerTiposDoc = bd.obtenerStoredProcedure("ObtenerTiposDocumento");
            var reader = spObtenerTiposDoc.ExecuteReader();
            while (reader.Read())
            {
                string tipoDoc = reader[1].ToString();
                comboBox1.Items.Add(tipoDoc);
            }
            ;
     
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                BaseDeDatos bd = new BaseDeDatos();
                int si = comboBox1.SelectedIndex;
                var spcrearUsuarioYCliente = bd.obtenerStoredProcedure("crearUsuarioYCliente");
                spcrearUsuarioYCliente.Parameters.Add("@Username", SqlDbType.VarChar).Value = txt_username.Text;
                spcrearUsuarioYCliente.Parameters.Add("@Contraseña", SqlDbType.VarChar).Value = SHA256.GetSHA256(txt_password.Text);
                spcrearUsuarioYCliente.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = txt_nombre.Text;
                spcrearUsuarioYCliente.Parameters.Add("@Apellido", SqlDbType.VarChar).Value = txt_apellido.Text;
                spcrearUsuarioYCliente.Parameters.Add("@NroDocumento", SqlDbType.Int).Value = Convert.ToInt32(txt_nroDoc.Text);
                spcrearUsuarioYCliente.Parameters.Add("@Mail", SqlDbType.VarChar).Value = txt_mail.Text;
                spcrearUsuarioYCliente.Parameters.Add("@Telefono", SqlDbType.Int).Value = Convert.ToInt32(txt_telefono.Text);
                spcrearUsuarioYCliente.Parameters.Add("@Domicilio_Calle", SqlDbType.VarChar).Value = txt_calle.Text;
                spcrearUsuarioYCliente.Parameters.Add("@Nro_Calle", SqlDbType.Int).Value = Convert.ToInt32(txt_nroCalle.Text);
                spcrearUsuarioYCliente.Parameters.Add("@Piso", SqlDbType.Int).Value = Convert.ToInt32(txt_nroPiso.Text);
                spcrearUsuarioYCliente.Parameters.Add("@Localidad", SqlDbType.VarChar).Value = txt_localidad.Text;
                spcrearUsuarioYCliente.Parameters.Add("@Departamento", SqlDbType.VarChar).Value = txt_depto.Text;
                spcrearUsuarioYCliente.Parameters.Add("@Cod_Postal", SqlDbType.Int).Value = Convert.ToInt32(txt_codPostal.Text);
                spcrearUsuarioYCliente.Parameters.Add("@Nombre_Tipo_Doc", SqlDbType.VarChar).Value = (string)comboBox1.Items[si];
                spcrearUsuarioYCliente.Parameters.Add("@Nacimiento", SqlDbType.DateTime).Value = dateTimePicker1.Value;
                var reader = spcrearUsuarioYCliente.ExecuteReader();
                reader.Read();
                //spcrearUsuarioYCliente.ExecuteNonQuery();
                spcrearUsuarioYCliente.Connection.Close();
                MessageBox.Show("Nuevo cliente cargado con éxito");
            }

            catch (SqlException excepcion)
            {
                MessageBox.Show("Hubo un error en la base: " + excepcion.Message);
            }
        
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

    }
}