using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace FlowModelMobileApp
{
   [Table("materials")]
   public class Materials
   {
      [PrimaryKey, AutoIncrement]
      public int MaterialId { get; set; }
      public string MaterialName { get; set; }
   }
}
