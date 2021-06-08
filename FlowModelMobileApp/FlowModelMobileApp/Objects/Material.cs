using System;
using System.Collections.Generic;
using System.Text;

namespace FlowModelMobileApp.Objects
{
    public class Material
    {
        public string material_name { get; set; }
        public double t0 { get; set; }
        public double b { get; set; }
        public double tr { get; set; }
        public double n { get; set; }
        public double alpha_u { get; set; }
        public double ro { get; set; }
        public double c { get; set; }
        public double mu0 { get; set; }
    }
}
