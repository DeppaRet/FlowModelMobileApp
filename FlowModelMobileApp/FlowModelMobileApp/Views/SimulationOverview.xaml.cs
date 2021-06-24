using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using obj = FlowModelMobileApp.Objects.SimulationObject;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Syncfusion.SfChart;
using System.Collections.ObjectModel;
using Syncfusion.SfChart.XForms;
using Syncfusion.SfDataGrid.XForms.Exporting;
using System.IO;


namespace FlowModelMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SimulationOverview : ContentPage
    {
        public double q_ch, eta_prod, t_prod;
        public List<double> eta_ch;
        public List<double> t_ch;
        public List<CanalPoint> MaterialCondition { get; set; }
        public SimulationOverview()
        {
            InitializeComponent();
            Simulation();
        }
        public void Simulation()
        {

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            double gamma, ae, F, q_gamma, q_alpha;
            F = 0.125 * Math.Pow(obj.Canal.Height / obj.Canal.Width, 2) - 0.625 * (obj.Canal.Height / obj.Canal.Width) + 1.0;
            q_ch = F * obj.Canal.Width * obj.Canal.Cap.Vu * obj.Canal.Height / 2.0;
            gamma = obj.Canal.Cap.Vu / obj.Canal.Height;
            q_gamma = obj.Canal.Width * obj.Canal.Height * obj.Material.Mu0 * Math.Pow(gamma, obj.Material.N + 1.0);
            q_alpha = obj.Canal.Width * obj.Material.Alpha_u * (1.0 / obj.Material.B + obj.Material.Tr - obj.Canal.Cap.Tu);
            eta_ch = new List<double>();
            t_ch = new List<double>();
            MaterialCondition = new List<CanalPoint>();
            CanalPoint canalPoint;
            for (double z = 0.0; Math.Round(z, GetDecimalDigitsCount(obj.Step)) < obj.Canal.Length; z += obj.Step)
            {
                z = Math.Round(z, GetDecimalDigitsCount(obj.Step));
                ae = ((obj.Material.B * q_gamma + obj.Canal.Width * obj.Material.Alpha_u) / (obj.Material.B * q_alpha)) *
                     (1.0 - Math.Exp(-(z * obj.Material.B * q_alpha / (obj.Material.Ro * obj.Material.C * q_ch)))) +
                     Math.Exp(obj.Material.B * (obj.Material.T0 - obj.Material.Tr -
                                                (z * q_alpha / (obj.Material.Ro * obj.Material.C * q_ch))));
                double t_temp1 = obj.Material.Tr + 1.0 * Math.Log(ae) / obj.Material.B;
                t_ch.Add(Math.Round(t_temp1, 2));
                eta_ch.Add(Math.Round(
                   Math.Pow(gamma, obj.Material.N - 1) * obj.Material.Mu0 *
                   Math.Exp(-obj.Material.B * (t_temp1 - obj.Material.Tr)), 1));
                canalPoint = new CanalPoint();
                canalPoint.x = z;
                canalPoint.t = t_ch[t_ch.Count - 1];
                canalPoint.eta = eta_ch[eta_ch.Count - 1];
                MaterialCondition.Add(canalPoint);
            }
            ae = ((obj.Material.B * q_gamma + obj.Canal.Width * obj.Material.Alpha_u) / (obj.Material.B * q_alpha)) *
                 (1.0 - Math.Exp(
                    -(obj.Canal.Length * obj.Material.B * q_alpha / (obj.Material.Ro * obj.Material.C * q_ch)))) +
                 Math.Exp(obj.Material.B * (obj.Material.T0 - obj.Material.Tr -
                                            (obj.Canal.Length * q_alpha / (obj.Material.Ro * obj.Material.C * q_ch))));
            double t_temp = obj.Material.Tr + 1.0 * Math.Log(ae) / obj.Material.B;
            t_ch.Add(Math.Round(t_temp, 2));
            eta_ch.Add(Math.Round(Math.Pow(gamma, obj.Material.N - 1) * obj.Material.Mu0 *
            Math.Exp(-obj.Material.B * (t_temp - obj.Material.Tr)), 1));
            eta_prod = eta_ch[eta_ch.Count - 1];
            t_prod = t_ch[t_ch.Count - 1];
            canalPoint = new CanalPoint();
            canalPoint.x = obj.Canal.Length;
            canalPoint.t = t_ch[t_ch.Count - 1];
            canalPoint.eta = eta_ch[eta_ch.Count - 1];
            MaterialCondition.Add(canalPoint);
            PerfomanceLabel.Text = "Производительность (кг/ч): " + Math.Round(q_ch * 3600 * 1380).ToString();
            TemperatureLabel.Text = "Температура продукта(°C): " + t_prod.ToString();
            ViscosityLabel.Text = "Вязкость продукта(Па*с): " + eta_prod.ToString();
            stopwatch.Stop();

            T_Chart.ItemsSource = MaterialCondition;
            V_Chart.ItemsSource = MaterialCondition;
            ResultsGrid.ItemsSource = MaterialCondition;
            CalcTime.Text = "Затрачено времени: " + stopwatch.ElapsedMilliseconds.ToString() + " мс";
            CalcMem.Text = "Затрачено памяти: " + Math.Round((Process.GetCurrentProcess().PeakWorkingSet64 / Math.Pow(1024, 2)), 2).ToString() + " Мб";

        }

        private void SaveReport_Clicked(object sender, EventArgs e)
        {
            DataGridExcelExportingController excelExport = new DataGridExcelExportingController();
            var excelEngine = excelExport.ExportToExcel(this.ResultsGrid);
            var workbook = excelEngine.Excel.Workbooks[0];
            MemoryStream stream = new MemoryStream();
            workbook.SaveAs(stream);
            workbook.Close();
            excelEngine.Dispose();

            Xamarin.Forms.DependencyService.Get<ISave>().Save("DataGrid.xlsx", "application/msexcel", stream);
        }
        
        static int GetDecimalDigitsCount(double value)
        {
            string[] str = value.ToString(new System.Globalization.NumberFormatInfo() { NumberDecimalSeparator = "." })
               .Split('.');
            return str.Length == 2 ? str[1].Length : 0;
        }

        public class CanalPoint
        {
            public double x { get; set; }
            public double t { get; set; }
            public double eta { get; set; }
        }

    }
}