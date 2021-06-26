using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlowModelMobileApp
{
   public partial class App : Application
   {

      public static string usersFilePath;
      public static string flowModelFilePath;
      public App()
      {
         InitializeComponent();
         MainPage = new NavigationPage(new MainPage());
      }

      public App(string usersPath, string flowModelPath)
      {
         InitializeComponent();
         CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US"); //ru-RU
         NumberFormatInfo numberFormatInfo = (NumberFormatInfo)CultureInfo.CurrentCulture.NumberFormat.Clone();
         usersFilePath = usersPath;
         flowModelFilePath = flowModelPath;
         MainPage = new NavigationPage(new MainPage());
      }

      protected override void OnStart()
      {
      }

      protected override void OnSleep()
      {
      }

      protected override void OnResume()
      {
      }

    
   }
}
