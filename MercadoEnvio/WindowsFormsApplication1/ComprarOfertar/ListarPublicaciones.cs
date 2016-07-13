using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowsFormsApplication1.ConexionBD;

namespace WindowsFormsApplication1.ComprarOfertar
{
    public partial class ListarPublicaciones : FormMaestro
    {
        private int idUserActual;

        public ListarPublicaciones(int idUserActual)
        {
            InitializeComponent();
            this.idUserActual = idUserActual;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3 && (e.RowIndex != -1))
            {
                int idPublicacion = (Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString()));
                if (dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString() == "Compra Inmediata")
                {
                    MostrarCompra mc = new MostrarCompra(idPublicacion, idUserActual);
                    mc.Show();
                }
                if (dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString() == "Subasta")
                {

                    MostrarSubasta ms = new MostrarSubasta(idPublicacion, idUserActual);
                    ms.Show();
                }
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        

        protected override void interactuar()
        {
            bool poseeUnRubro = false;
            foreach (DataGridViewRow item in dataGridView2.Rows)
            {
                if (bool.Parse(item.Cells[0].Value.ToString()))
                {
                    poseeUnRubro = true;
                }
            }

            if (poseeUnRubro == false)
            {
                MessageBox.Show("Debe seleccionar como minimo un rubro");
                return;
            }

            try
            {
                dataGridView1.Rows.Clear();
                cb_pags.Items.Clear();
                BaseDeDatos bd = new BaseDeDatos();
                DataTable dbdataset2 = new DataTable();
                foreach (DataGridViewRow item in dataGridView2.Rows)
                {
                    if (bool.Parse(item.Cells[0].Value.ToString()))
                    {
                        var spObtenerPublicaciones = bd.obtenerStoredProcedure("obtenerPublicaciones");
                        spObtenerPublicaciones.Parameters.Add("@Rubro", SqlDbType.VarChar).Value = item.Cells[1].Value.ToString();
                        spObtenerPublicaciones.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = txt_descripcion.Text;
                        spObtenerPublicaciones.Parameters.Add("@Id_User", SqlDbType.Int).Value = idUserActual;
                        //var reader = spObtenerPublicaciones.ExecuteReader();
                        //reader.Read();

                        SqlDataAdapter sda2 = new SqlDataAdapter();
                        sda2.SelectCommand = spObtenerPublicaciones;
                        
                        sda2.Fill(dbdataset2);
                    }
                }
            

                

                foreach (DataRow item2 in dbdataset2.Rows)
                    {
                        int n = dataGridView1.Rows.Add();
                        dataGridView1.Rows[n].Cells[0].Value = item2["Descripcion"].ToString();
                        dataGridView1.Rows[n].Cells[1].Value = item2["Monto"].ToString();
                        dataGridView1.Rows[n].Cells[2].Value = item2["tipo_public"].ToString();
                        dataGridView1.Rows[n].Cells[4].Value = item2["Id_Publicacion"].ToString();
                        dataGridView1.Rows[n].Cells[3].Value = "Ver";
                        dataGridView1.Rows[n].Cells[5].Value = item2["Id_Visibilidad"].ToString();
                    }

                    dataGridView1.Sort(dataGridView1.Columns[5], ListSortDirection.Ascending);
                    this.RemoveDuplicate(dataGridView1);

                    double rows = ((dataGridView1.Rows.Count));
                    double paginas = Math.Ceiling(rows / 10);
                    if (rows > 0)
                    {
                        for (int j = 0; j < rows; j++)
                        {
                            dataGridView1.Rows[j].Visible = false;
                        }
                    }

                    if (paginas > 0)
                    {
                        for (int i = 1; i <= paginas; i++)
                        {
                            cb_pags.Items.Add(i);
                        }
                        cb_pags.SelectedItem = cb_pags.Items[0] ;
                    }

                    
                
                    
   
            }
                
            
            catch (SqlException excepcion)
            {
                MessageBox.Show(excepcion.Message);
            }
        }

        private void ListarPublicaciones_Load(object sender, EventArgs e)
        {
            validador.textBoxsNoVacios(new List<TextBox>(new[] {
                txt_descripcion
            }));

            BaseDeDatos bd = new BaseDeDatos();
            var spObtenerRubros = bd.obtenerStoredProcedure("obtenerRubros");

            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = spObtenerRubros;
            DataTable dbdataset = new DataTable();
            sda.Fill(dbdataset);
            foreach (DataRow item in dbdataset.Rows)
            {
                int n = dataGridView2.Rows.Add();
                dataGridView2.Rows[n].Cells[0].Value = "false";
                dataGridView2.Rows[n].Cells[1].Value = item["Desc_Corta"].ToString();
            }
            spObtenerRubros.Connection.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btn_buscar_Click(object sender, EventArgs e)
        {
            this.submitir();
        }

        private void cb_pags_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rows = ((dataGridView1.Rows.Count));

            if (rows > 0)
            {
                for (int j = 0; j < rows; j++)
                {
                    dataGridView1.Rows[j].Visible = false;
                }
            }

            int pag = (((Convert.ToInt32(cb_pags.SelectedIndex)) + 1)*10);
            for (int i = pag - 10; i < pag ; i++)
            {
                if (dataGridView1.Rows.Count > i)
                {
                    dataGridView1.Rows[i].Visible = true;
                }
            }

        }
    }
}
