using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Курсовая
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Проверим количество выбранных строк
            if (dataGridView1.SelectedRows.Count != 1)
            {
                MessageBox.Show("Выберите одну строку!", "Внимание!");
                return;
            }

            //Запомним выбранную строку
            int index = dataGridView1.SelectedRows[0].Index;

            //Проверим данные в таблицы
            if (dataGridView1.Rows[index].Cells[0].Value == null ||
                dataGridView1.Rows[index].Cells[1].Value == null ||
                dataGridView1.Rows[index].Cells[2].Value == null ||
                dataGridView1.Rows[index].Cells[3].Value == null||
                dataGridView1.Rows[index].Cells[3].Value == null)
            {
                MessageBox.Show("Не все данные введены!", "Внимание!");
                return;
            }

            //Считаем данные
            string BookName = dataGridView1.Rows[index].Cells[0].Value.ToString();
            string AuthorName = dataGridView1.Rows[index].Cells[1].Value.ToString();
            string Yearofpublishing = dataGridView1.Rows[index].Cells[2].Value.ToString();
            string Publisher = dataGridView1.Rows[index].Cells[3].Value.ToString();
            string Genre = dataGridView1.Rows[index].Cells[4].Value.ToString();

            //Создаем соеденение
            string connectionString = "provider=Microsoft.Jet.OLEDB.4.0;Data Source=Database.mdb";//строка соеденения
            OleDbConnection dbConnection = new OleDbConnection(connectionString);//создаем соеденение

            //Выполянем запрос к БД
            dbConnection.Open();//открываем соеденение
            string query = "INSERT INTO Book VALUES (" + BookName + ", '" + AuthorName + "', " + Yearofpublishing + ", " + Publisher + ","+ Genre +")";//строка запроса
            OleDbCommand dbCommand = new OleDbCommand(query, dbConnection);//команда

            //Выполняем запрос
            if (dbCommand.ExecuteNonQuery() != 1)
                MessageBox.Show("Ошибка выполнения запроса!", "Ошибка!");
            else
                MessageBox.Show("Данные добавлены!", "Внимание!");

            //Закрываем соеденение с БД
            dbConnection.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void оРазработчикеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Денис Сиразов БПИ-20-1");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Создаем соеденение
            string connectionString = "provider=Microsoft.Jet.OLEDB.4.0;Data Source=Database.mdb";//строка соеденения
            OleDbConnection dbConnection = new OleDbConnection(connectionString);//создаем соеденение

            //Выполянем запрос к БД
            dbConnection.Open();//открываем соеденение
            string query = "SELECT * FROM Book";//строка запроса
            OleDbCommand dbCommand = new OleDbCommand(query, dbConnection);//команда
            OleDbDataReader dbReader = dbCommand.ExecuteReader();//считываем данные

            //Проверяем данные
            if (dbReader.HasRows == false)
            {
                MessageBox.Show("Данные не найдены!", "Ошибка!");
            }
            else
            {
                //Запишем данные в таблицу формы
                while (dbReader.Read())
                {
                    //Выводим данные
                    dataGridView1.Rows.Add(dbReader["BookName"], dbReader["AuthorName"], dbReader["Yearofpublishing"], dbReader["Publisher"], dbReader["Genre"]);
                }
            }

            //Закрываем соеденение с БД
            dbReader.Close();
            dbConnection.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
