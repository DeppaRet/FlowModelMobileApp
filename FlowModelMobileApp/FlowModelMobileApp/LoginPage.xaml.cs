using FlowModelMobileApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;

namespace FlowModelMobileApp
{
   public partial class MainPage : ContentPage
   {
      public MainPage()
      {
         InitializeComponent();
      }

      public void check()
      {
         using (SQLiteConnection conn = new SQLiteConnection(App.usersFilePath))
         {
            conn.CreateTable<Users>();
            var data = conn.Table<Users>();
            var size = data.ToList();
            int i = size.Count;
            if (i == 0)
            {
               Users users = new Users()
               {
                  Login = "admin",
                  Password = "admin",
                  Role = "admin"
               };
               conn.CreateTable<Users>();
               int rowsAdded = conn.Insert(users);
               users = new Users()
               {
                  Login = "user",
                  Password = "user",
                  Role = "research"
               };
               conn.CreateTable<Users>();
               rowsAdded = conn.Insert(users);

            }
         }

      }

      private void LoginClicked(object sender, EventArgs eventArgs)
      {
         check();
         string currentRole;
         using (SQLiteConnection conn = new SQLiteConnection(App.usersFilePath))
         {
            conn.CreateTable<Users>();
            var data = conn.Table<Users>();
            var users = data.ToList();
            var d1 = (from user in users
               where user.Login == LoginText.Text && user.Password == PasswordText.Text
               select user.Role);
            currentRole = d1.FirstOrDefault();
         }

         LoginText.Text = "";
         PasswordText.Text = "";

         if (currentRole == "admin")
         {
            Navigation.PushAsync(new AdminPage());
         }
         else if (currentRole == "research")
         {
            Navigation.PushAsync(new ResearcherPage());
         }

      }



   }
}
