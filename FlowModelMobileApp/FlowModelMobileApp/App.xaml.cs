using System;
using System.IO;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlowModelMobileApp
{
   public partial class App : Application
   { 
      public App()
      {
         InitializeComponent();
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

      //https://metanit.com/sharp/xamarin/7.3.php гайд про базу
      //https://itvdn.com/ru/channel/video/it-mobile-dev-android-sqlite-grouping-sorting-filtering
      public const string DATABASE_NAME = "users.db";
      public static UserAsyncRepository database;
      public static UserAsyncRepository Database
      {
         get
         {
            if (database == null)
            {
               // путь, по которому будет находиться база данных
               string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME);
               // если база данных не существует (еще не скопирована)
               if (!File.Exists(dbPath))
               {
                  // получаем текущую сборку
                  var assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
                  // берем из нее ресурс базы данных и создаем из него поток
                  using (Stream stream = assembly.GetManifestResourceStream($"HelloApp.{DATABASE_NAME}"))
                  {
                     using (FileStream fs = new FileStream(dbPath, FileMode.OpenOrCreate))
                     {
                        stream.CopyTo(fs);  // копируем файл базы данных в нужное нам место
                        fs.Flush();
                     }
                  }
               }
               database = new UserAsyncRepository(dbPath);
            }
            return database;
         }
      }
   }
}
