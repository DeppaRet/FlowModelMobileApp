using System;
using System.Collections.Generic;
using System.Text;

namespace FlowModelMobileApp.Models
{
   public interface ILocalFileHelper
   {
      string GetLocalFilePath(string fileName);
   }
}
