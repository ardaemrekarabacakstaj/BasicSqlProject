using MySql.Data.MySqlClient;
using System;
using MaterialSkin;
using MaterialSkin.Controls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BasicSqlProject
{
    public partial class Form1 : MaterialForm
    {        
        public Form1()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.DeepPurple500, Primary.DeepPurple700,
                Primary.DeepPurple100, Accent.Purple200,
                TextShade.WHITE
            );
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=containers-us-west-132.railway.app;Port=7978;Database=railway;User Id=root;Password=k7it7kcObPOn5MdESGIt;";
            MySqlConnection con = new MySqlConnection(connectionString);

            string kitapİd = materialSingleLineTextField1.Text.ToString();
            string kitapAdi = materialSingleLineTextField2.Text.ToString();
            string tur = comboBox1.Text.ToString();
            string yazarAdi = materialSingleLineTextField3.Text.ToString();
            string yayınEvi = materialSingleLineTextField4.Text.ToString();
            int stokMiktari = int.Parse(materialSingleLineTextField5.Text);
            string eklenmeTarihi = dateTimePicker1.Text.ToString();


            try
            {
                con.Open();

                string insertQuery = "INSERT INTO kitap (kitapid, kitapadi, yazaradi, yayinevi, tur, stokmiktari, eklenmetarihi) VALUES (@kitapid, @kitapadi,@yazaradi,@yayinevi,@tur,@stokmiktari,@eklenmetarihi)";

                MySqlCommand cmd = new MySqlCommand(insertQuery, con);
                cmd.Parameters.AddWithValue("@kitapid", kitapİd);
                cmd.Parameters.AddWithValue("@kitapadi", kitapAdi);
                cmd.Parameters.AddWithValue("@yazaradi", yazarAdi);
                cmd.Parameters.AddWithValue("@yayinevi", yayınEvi);
                cmd.Parameters.AddWithValue("@tur", tur);
                cmd.Parameters.AddWithValue("@stokmiktari", stokMiktari.ToString());
                cmd.Parameters.AddWithValue("@eklenmetarihi", eklenmeTarihi);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Veri başarıyla eklendi.");
                    TableView();
                }
                else
                {
                    MessageBox.Show("Veri eklenirken bir hata oluştu.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        private void TableView()
        {
            string connectionString = "Server=containers-us-west-132.railway.app;Port=7978;Database=railway;User Id=root;Password=k7it7kcObPOn5MdESGIt;";
            MySqlConnection con = new MySqlConnection(connectionString);
        
            try
            {
                con.Open();

                string viewQuery = "select * from kitap";
                MySqlCommand cmd = new MySqlCommand(viewQuery, con);
                MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                MyAdapter.SelectCommand = cmd;
                DataTable dataTable = new DataTable();

                MyAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir Hata Oluştu...");
                throw;
            }
            finally {
                con.Close();

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TableView();
            dataGridView1.Columns["kitapid"].Visible = false;

        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                materialSingleLineTextField1.Text = selectedRow.Cells["kitapid"].Value.ToString();
                materialSingleLineTextField2.Text = selectedRow.Cells["kitapadi"].Value.ToString();
                materialSingleLineTextField3.Text = selectedRow.Cells["yazaradi"].Value.ToString();
                materialSingleLineTextField4.Text = selectedRow.Cells["yayinevi"].Value.ToString();
                comboBox1.Text = selectedRow.Cells["tur"].Value.ToString();
                materialSingleLineTextField5.Text = selectedRow.Cells["stokmiktari"].Value.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=containers-us-west-132.railway.app;Port=7978;Database=railway;User Id=root;Password=k7it7kcObPOn5MdESGIt;";
            MySqlConnection con = new MySqlConnection(connectionString);

            string deleteQuery = "delete from kitap where kitapid='" + this.materialSingleLineTextField1.Text + "';";
            MySqlCommand cmd = new MySqlCommand(deleteQuery, con);
            MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
            MySqlDataReader MyReader;

            try
            {
                con.Open();

                MyReader = cmd.ExecuteReader();
                MessageBox.Show("Veriniz Silindi...");
                TableView();

                while (MyReader.Read()) { }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir Hata Oluştu...");
                throw;
            }
            finally
            {
                con.Close();

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=containers-us-west-132.railway.app;Port=7978;Database=railway;User Id=root;Password=k7it7kcObPOn5MdESGIt;";
            MySqlConnection con = new MySqlConnection(connectionString);

            string updateQuery = "UPDATE kitap SET kitapadi=@kitapadi, yazaradi=@yazaradi, yayinevi=@yayinevi, tur=@tur, stokmiktari=@stokmiktari WHERE kitapid=@kitapid";
            MySqlCommand cmd = new MySqlCommand(updateQuery, con);

            cmd.Parameters.AddWithValue("@kitapid", materialSingleLineTextField1.Text);
            cmd.Parameters.AddWithValue("@kitapadi", materialSingleLineTextField2.Text);
            cmd.Parameters.AddWithValue("@yazaradi", materialSingleLineTextField3.Text);
            cmd.Parameters.AddWithValue("@yayinevi", materialSingleLineTextField4.Text);
            cmd.Parameters.AddWithValue("@tur", comboBox1.Text);
            cmd.Parameters.AddWithValue("@stokmiktari", int.Parse(materialSingleLineTextField5.Text));

            try
            {
                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Veri başarıyla güncellendi.");
                    TableView();
                }
                else
                {
                    MessageBox.Show("Veri güncellenirken bir hata oluştu.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

    }
}

