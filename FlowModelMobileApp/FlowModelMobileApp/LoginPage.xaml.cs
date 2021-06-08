using FlowModelMobileApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FlowModelMobileApp
{
   public partial class MainPage : ContentPage
   {
      public MainPage()
      {
         InitializeComponent();
      }

      private void LoginClicked(object sender, EventArgs eventArgs)
      {
            if (LoginText.Text == "1")
            {
                Navigation.PushAsync(new AdminPage());
            }
            else if (LoginText.Text == "2") {
                Navigation.PushAsync(new ResearcherPage());
            }
         

      }
   }
}
