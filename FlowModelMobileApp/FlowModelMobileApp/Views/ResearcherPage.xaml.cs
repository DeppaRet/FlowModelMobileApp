using FlowModelMobileApp.Objects;
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
        delegate bool IsEqual(string x);
        public Dictionary<string, double> props;

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
                Props.Add(new Prop { Name = "Температурный коэффициент вязкости", Unit = "1/°С", Value = "0.05" });
                Props.Add(new Prop { Name = "Индекс течения", Unit = "-", Value = "0.28" });
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

        private void StartSimulation_Clicked(object sender, EventArgs e)
        {
            props = new Dictionary<string, double>();
            foreach ( Prop prop in Props)
            {
                if (prop.Value != "Значение")
                {
                    props.Add(prop.Name, Convert.ToDouble(prop.Value));
                }
            }
            SimulationObject.Material = new Material();
            SimulationObject.Material.Alpha_u = ThisProp(x => x == "Коэффициент теплоотдачи крышки");
            SimulationObject.Material.B = ThisProp(x => x == "Температурный коэффициент вязкости");
            SimulationObject.Material.C = ThisProp(x => x == "Удельная теплоемкость");
            SimulationObject.Material.Material_name = MaterialPicker.SelectedItem.ToString() ;
            SimulationObject.Material.Mu0 = ThisProp(x => x == "Коэффициент консистенции приведения");
            SimulationObject.Material.N = ThisProp(x => x == "Индекс течения");
            SimulationObject.Material.Ro = ThisProp(x => x == "Плотность");
            SimulationObject.Material.T0 = ThisProp(x => x == "Температура плавления");
            SimulationObject.Material.Tr = ThisProp(x => x == "Температура приведения");
            SimulationObject.Canal = new Canal();
            SimulationObject.Canal.Width = Convert.ToDouble(G_Width.Text);
            SimulationObject.Canal.Height = Convert.ToDouble(G_Depth.Text);
            SimulationObject.Canal.Length = Convert.ToDouble(G_Lenght.Text);
            SimulationObject.Canal.Cap = new Cap();
            SimulationObject.Canal.Cap.Tu = Convert.ToDouble(Cap_Temp.Text);
            SimulationObject.Canal.Cap.Vu = Convert.ToDouble(Cap_Speed.Text);
            SimulationObject.Step = Convert.ToDouble(Step.Text);
            Navigation.PushAsync(new SimulationOverview());
        }
        double ThisProp(IsEqual func)
        {
            foreach (var prop in props)
            {
                if (func(prop.Key))
                {
                    return prop.Value;
                }
            }
            return 0;
        }
    }
}