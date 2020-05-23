using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace automation_of_drug_accounting_WF_csharp_
{
    public partial class Form1 : Form
    {

        private SqlConnection sqlConnection = null;
        private SqlCommandBuilder sqlBuilder = null;
        private SqlDataAdapter sqlDataAdapter = null;
        private DataSet dataSet = null;
        private bool newRowAdding = false;


        public Form1()
        {
            InitializeComponent();
        }

        private void DataLoad()
        {
            // загрузка таблицы
            try
            {
                sqlDataAdapter = new SqlDataAdapter("SELECT *, 'Delete' AS [DELETE] FROM [Medicines]", sqlConnection); // выбор всей таблицы

                sqlBuilder = new SqlCommandBuilder(sqlDataAdapter);
                
                sqlBuilder.GetInsertCommand(); // комманды удаления, изменения, ввода
                sqlBuilder.GetUpdateCommand();
                sqlBuilder.GetDeleteCommand();

                dataSet = new DataSet();

                sqlDataAdapter.Fill(dataSet, "Medicines");

                dataGridView1.DataSource = dataSet.Tables["Medicines"];

                // последний столбец команд
                for (int i=0; i<dataGridView1.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                    dataGridView1[11, i] = linkCell;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DataUpdate()
        {
            // обновление таблицы
            try
            {
                dataSet.Tables["Medicines"].Clear();

                sqlDataAdapter.Fill(dataSet, "Medicines");

                dataGridView1.DataSource = dataSet.Tables["Medicines"];

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                    dataGridView1[11, i] = linkCell;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // загрузка формы и подключение таблицы
            sqlConnection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; Initial Catalog = mydatabase; AttachDbFilename = |DataDirectory|\Database.mdf; Integrated Security = True");

            sqlConnection.Open();

            DataLoad();

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            DataUpdate();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //code Sardanov
            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
        }

        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            // изменения удаления на инсерт

            //code Sardanov

        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // изменение удаления на апдейт

            //code Sardanov

        }


        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column_KeyPress);
            // указание ячеек для проверки
            if (dataGridView1.CurrentCell.ColumnIndex == 3)
            {
                TextBox textBox = e.Control as TextBox;
                if (textBox != null)
                {
                    textBox.KeyPress += new KeyPressEventHandler(Column_KeyPress);
                }
            }

            if (dataGridView1.CurrentCell.ColumnIndex == 4)
            {
                TextBox textBox = e.Control as TextBox;
                if (textBox != null)
                {
                    textBox.KeyPress += new KeyPressEventHandler(Column_KeyPress);
                }
            }

            if (dataGridView1.CurrentCell.ColumnIndex == 6)
            {
                TextBox textBox = e.Control as TextBox;
                if (textBox != null)
                {
                    textBox.KeyPress += new KeyPressEventHandler(Column_KeyPress);
                }
            }

            if (dataGridView1.CurrentCell.ColumnIndex == 7)
            {
                TextBox textBox = e.Control as TextBox;
                if (textBox != null)
                {
                    textBox.KeyPress += new KeyPressEventHandler(Column_KeyPress);
                }
            }

        }

        private void Column_KeyPress(object sender, KeyPressEventArgs e)
        {
            //проверка на симолы (цифра, пунктуаци)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && !char.IsPunctuation(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // кнопка выхода
            Form2 s = new Form2();
            s.Show();
            this.Hide();
        }

        private void помощьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // кнопка помощи
            Form4 s = new Form4();
            s.Show();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            // закрытие всех окон
            Application.Exit();
        }
    }
}
