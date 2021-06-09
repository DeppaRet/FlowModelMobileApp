using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlowModelMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResearcherPage : ContentPage
    {
        public List<Prop> Props { get; set; }

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
                Props = new List<Prop>();
                Props.Add(new Prop { Name = "Плотность", Unit = "кг/м^3", Value = "1380" });
                Props.Add(new Prop { Name = "Удельная теплоемкость", Unit = "Дж/(кг·°С)", Value = "2500" });
                Props.Add(new Prop { Name = "Температура плавления", Unit = "°С", Value = "145" });
                Props.Add(new Prop { Name = "Эмперические коэффиценты", Unit = "Единица измерения", Value = "Значение" });
                Props.Add(new Prop { Name = "Температура приведения", Unit = "°С", Value = "165" });
                Props.Add(new Prop { Name = "Коэффициент консистенции приведения", Unit = "Па·с^n", Value = "12000" });
                Props.Add(new Prop { Name = "Температурный коэффициент вязкости", Unit = "1/°С", Value = "0,05" });
                Props.Add(new Prop { Name = "Индекс течения", Unit = "-", Value = "0,28" });
                Props.Add(new Prop { Name = "Коэффициент теплоотдачи крышки", Unit = "Вт/(м^2·°С)", Value = "400" });
                PropsGrid.ItemsSource = Props;
            }
            else
            {
                Props.Clear();
                PropsGrid.Refresh();
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

        public class Prop
        {
            public string Name { get; set; }
            public string Unit { get; set; }
            public string Value { get; set; }
        }
    }
}