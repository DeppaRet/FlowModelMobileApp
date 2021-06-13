using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Syncfusion.SfDataGrid.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlowModelMobileApp.Views
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class AdminPage : ContentPage
   {
      string currentTable;
      public List<UsersTable> AuthList { get; set; }

      public class UsersTable
      {
         public string Login { get; set; }
         public string Password { get; set; }
         public string Role { get; set; }
      }

      public AdminPage()
      {
         InitializeComponent();
         ChoosenTable.Items.Insert(0, "Пользователи");
         ChoosenTable.Items.Insert(1, "Материалы");
         ChoosenTable.Items.Insert(2, "Свойства");
         ChoosenTable.Items.Insert(3, "Таблица связи");
      }

     

      private void AddUserButtonClick(object sender, EventArgs e)
      {
         Navigation.PushAsync(new AddUser());
      }

      private void ChoosenTableChanged(object sender, EventArgs e)
      {
         if (ChoosenTable.SelectedIndex == 0)
         {
            currentTable = "Users";
            ResultTable.Columns.Clear();
            ResultTable.Columns.Add(new GridTextColumn() { HeaderText = "Login", MappingName = "Login" });
            ResultTable.Columns.Add(new GridTextColumn() { HeaderText = "Password", MappingName = "Password" });
            ResultTable.Columns.Add(new GridTextColumn() { HeaderText = "Role", MappingName = "Role" });
            
            selectFromTableUsers();
         }
         else if (ChoosenTable.SelectedIndex == 1)
         {
            currentTable = "Material";
         }
      }

      public void selectFromTableUsers()
      {
         using (SQLiteConnection conn = new SQLiteConnection(App.usersFilePath))
         {
            conn.CreateTable<Users>();
            var data = conn.Table<Users>();
            var users = data.ToList();
            AuthList = new List<UsersTable>();
            for(int i = 0; i < users.Count; i++)
            {
               AuthList.Add(new UsersTable { Login = users[i].Login, Password = users[i].Password, Role = users[i].Role });
            }

            ResultTable.ItemsSource = AuthList;
         }
         var height = (ResultTable.View.Records.Count * ResultTable.RowHeight) + ResultTable.HeaderRowHeight;
         this.ResultTable.HeightRequest = (double)height;
         ResultTable.Refresh();
      }
   }
}