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
            dataGridView1.Rows[index].Cells[3].Value == null ||
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
            string Description = textBox2.Text;//добавил это

            //Создаем соеденение
            string connectionString = "provider=Microsoft.Jet.OLEDB.4.0;Data Source=Database.mdb";//строка соеденения
            OleDbConnection dbConnection = new OleDbConnection(connectionString);//создаем соеденение

            //Выполянем запрос к БД
            dbConnection.Open();//открываем соеденение       ИЗМЕНИЛ СНИЗУ
            string query = "INSERT INTO Book (BookName, AuthorName, Yearofpublishing, Publisher, Genre, Description) VALUES ('" + BookName + "', '" + AuthorName + "', " + Yearofpublishing + ", '" + Publisher + "', '" + Genre + "', '" + Description + "')";//строка запроса
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
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Selected = false;

                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {

                    if (dataGridView1.Rows[i].Cells[j].Value != null)
                    {

                        if (dataGridView1.Rows[i].Cells[j].Value.ToString().Contains(textBox1.Text))
                        {
                            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;

                            dataGridView1.Rows[i].Selected = true;
                            break;
                        }

                    }
                }
            }
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
                //Очистим таблицу перед обновлением
                dataGridView1.Rows.Clear();
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

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 1)
            {
                MessageBox.Show("Выберите одну строку!", "Внимание!");
                return;
            }

            //Запомним выбранную строку
            int index = dataGridView1.SelectedRows[0].Index;

            //Считаем данные
            string BookName = dataGridView1.Rows[index].Cells[0].Value.ToString();

            //Создаем соеденение
            string connectionString = "provider=Microsoft.Jet.OLEDB.4.0;Data Source=Database.mdb";//строка соеденения
            OleDbConnection dbConnection = new OleDbConnection(connectionString);//создаем соеденение

            //Выполянем запрос к БД
            dbConnection.Open();//открываем соеденение
            string query = "SELECT Description FROM Book WHERE BookName = '" + BookName + "';";//строка запроса
            OleDbCommand dbCommand = new OleDbCommand(query, dbConnection);//команда
            OleDbDataReader reader = dbCommand.ExecuteReader();
            while (reader.Read())
            {
                string valueour = reader.GetString(0);
                textBox2.Text = valueour;
            }
            reader.Close();
            //Закрываем соеденение с БД
            dbConnection.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 1)
            {
                MessageBox.Show("Выберите одну строку!", "Внимание!");
                return;
            }

            //Запомним выбранную строку
            int index = dataGridView1.SelectedRows[0].Index;

            //Проверим данные в таблицы
            if (dataGridView1.Rows[index].Cells[0].Value == null)
            {
                MessageBox.Show("Не все данные введены!", "Внимание!");
                return;
            }

            //Считаем данные
            string BookName = dataGridView1.Rows[index].Cells[0].Value.ToString();

            //Создаем соеденение
            string connectionString = "provider=Microsoft.Jet.OLEDB.4.0;Data Source=Database.mdb";//строка соеденения
            OleDbConnection dbConnection = new OleDbConnection(connectionString);//создаем соеденение

            //Выполянем запрос к БД
            dbConnection.Open();//открываем соеденение
            string query = "DELETE FROM Book WHERE BookName = '" + BookName + "';";//строка запроса
            OleDbCommand dbCommand = new OleDbCommand(query, dbConnection);//команда

            //Выполняем запрос
            if (dbCommand.ExecuteNonQuery() != 1)
                MessageBox.Show("Ошибка выполнения запроса!", "Ошибка!");
            else
            {
                MessageBox.Show("Данные удалены!", "Внимание!");
                //Удаляем данные из таблицы в форме
                dataGridView1.Rows.RemoveAt(index);
            }

            //Закрываем соеденение с БД
            dbConnection.Close();
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Программа представляет собой приложение, целью которого является учёт книг домашней библиотеки. Пользователю будут предложены возможности просмотра данных хранимых книг (название, автор, год издания, издательство, жанр), пополнения библиотеки книг, поиск книг путём использования фильтра по данным каждого произведения (название, автор, год издания, издательство, жанр), поиск книг через поисковую строку по их названию, автору, жанру или году издания.", "Справка");
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 1)
            {
                MessageBox.Show("Выберите одну строку!", "Внимание!");
                return;
            }

            //Запомним выбранную строку
            int index = dataGridView1.SelectedRows[0].Index;

            //Считаем данные
            string BookName = dataGridView1.Rows[index].Cells[0].Value.ToString();

            //Создаем соеденение
            string connectionString = "provider=Microsoft.Jet.OLEDB.4.0;Data Source=Database.mdb";//строка соеденения
            OleDbConnection dbConnection = new OleDbConnection(connectionString);//создаем соеденение

            //Выполянем запрос к БД
            dbConnection.Open();//открываем соеденение
            string query = "SELECT Description FROM Book WHERE BookName = '" + BookName + "';";//строка запроса
            OleDbCommand dbCommand = new OleDbCommand(query, dbConnection);//команда
            OleDbDataReader reader = dbCommand.ExecuteReader();
            while (reader.Read())
            {
                string valueour = reader.GetString(0);
                textBox2.Text = valueour;
            }
            reader.Close();
            //Закрываем соеденение с БД
            dbConnection.Close();
        }
    }
}