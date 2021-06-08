using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlowModelMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResearcherPage : ContentPage
    {
        public List<prop> props { get; set; }
        public ResearcherPage()
        {
            InitializeComponent();
            MaterialPicker.Items.Insert(0, "Поливинилхлорид");
            MaterialPicker.Items.Insert(1, "Вода");
        }

        private void MaterialPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MaterialPicker.SelectedIndex == 0)
            {
                props = new List<prop>();
                props.Add(new prop { Name = "Плотность", Unit = "кг/м^3", Value = "1380" });
                props.Add(new prop { Name = "Удельная теплоемкость", Unit = "Дж/(кг·°С)", Value = "2500" });
                PropsView.ItemsSource = props;
            }
            //dataGridView1.Rows.Add("Плотность", "кг/м^3", "1380");
            //dataGridView1.Rows.Add("Удельная теплоемкость", "Дж/(кг·°С)", "2500");
            //dataGridView1.Rows.Add("Температура плавления", "°С", "145");
            //dataGridView1.Rows.Add("Эмперические коэффиценты", "Единица измерения", "Значение");
            //dataGridView1.Rows[dataGridView1.Rows.Count - 2].MinimumHeight = 50;
            //dataGridView1.Rows[dataGridView1.Rows.Count - 2].Cells[2].ReadOnly = true;
            //dataGridView1.Rows.Add("Температура приведения", "°С", "165");
            //dataGridView1.Rows.Add("Коэффициент консистенции приведения", "Па·с^n", "12000");
            //dataGridView1.Rows.Add("Температурный коэффициент вязкости", "1/°С", "0,05");
            //dataGridView1.Rows.Add("Индекс течения", "-", "0,28");
            //dataGridView1.Rows.Add("Коэффициент теплоотдачи крышки", "Вт/(м^2·°С)", "400");
        }
    }
    public class prop
    {
        public string Name { get; set; }
        public string Unit { get; set; }
        public string Value { get; set; }
    }
}