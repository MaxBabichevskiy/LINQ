using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace LINQ
{
    public partial class Form1 : Form
    {
        private DataContext db;

        public Form1()
        {
            InitializeComponent();

            // Подключение к БД
            string connectionString = "Data Source=KOMPUTER\\;Initial Catalog=adonetdb;Integrated Security=True";
            db = new DataContext(connectionString);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Получение всех пользователей
            var users = db.GetTable<User>().ToList();

            // Отображение пользователей в DataGridView
            dataGridView1.DataSource = users;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            // Создание нового пользователя
            User user = new User { Name = nameTextBox.Text, Age = int.Parse(ageTextBox.Text) };

            // Добавление пользователя в БД
            db.GetTable<User>().InsertOnSubmit(user);
            db.SubmitChanges();

            // Обновление DataGridView
            dataGridView1.DataSource = db.GetTable<User>().ToList();
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            // Получение выбранного пользователя из DataGridView
            User selectedUser = (User)dataGridView1.SelectedRows[0].DataBoundItem;

            // Обновление имени и возраста пользователя
            selectedUser.Name = nameTextBox.Text;
            selectedUser.Age = int.Parse(ageTextBox.Text);

            // Обновление пользователя в БД
            db.SubmitChanges();

            // Обновление DataGridView
            dataGridView1.DataSource = db.GetTable<User>().ToList();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            // Получение выбранного пользователя из DataGridView
            User selectedUser = (User)dataGridView1.SelectedRows[0].DataBoundItem;

            // Удаление пользователя из БД
            db.GetTable<User>().DeleteOnSubmit(selectedUser);
            db.SubmitChanges();

            // Обновление DataGridView
            dataGridView1.DataSource = db.GetTable<User>().ToList();
        }
    }

    [Table(Name = "User")]
    public class User
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [Column]
        public string Name { get; set; }

        [Column]
        public int Age { get; set; }
    }
}
