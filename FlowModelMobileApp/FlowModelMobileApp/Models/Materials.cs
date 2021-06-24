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

   [Table("properties")]
   public class Properties
   {
      [PrimaryKey, AutoIncrement]
      public int PropertyId { get; set; }
      public string PropertyName { get; set; }
      public string PropertyUnit { get; set; }
      public string PropertyType { get; set; }

   }

   [Table("material_has_properties")]
   public class Material_has_Properties
   {
      Materials MaterialId { get; set; }
      Properties PropertyId { get; set; }
      public double Value { get; set; }
   }
}
