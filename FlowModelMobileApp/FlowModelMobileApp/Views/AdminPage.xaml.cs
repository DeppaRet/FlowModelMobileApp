using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlowModelMobileApp.Views
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class AdminPage : ContentPage
   {
      string selectedRole;
      public AdminPage()
      {
         InitializeComponent();
      }

     

      private void AddUserButtonClick(object sender, EventArgs e)
      {
         Navigation.PushAsync(new AddUser());
      }
   }
}