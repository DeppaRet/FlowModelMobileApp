using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace FlowModelMobileApp
{
   public class Users
   {
      [PrimaryKey, AutoIncrement]
      public int UserId { get; set; }
      public string Login { get; set; }
      public string Password { get; set; }
      public string Role { get; set; }
   }
}

