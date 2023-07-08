using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

namespace TDMPW_412_3P_PR02;

public partial class MainPage : ContentPage, INotifyPropertyChanged
{
    ObservableCollection<Tarea> tasks = new ObservableCollection<Tarea>
    {
        new Tarea {Id=1, Nombre = "Estudiar para italiano", Prioridad=0, Selected=false},
        new Tarea {Id=2, Nombre = "Terminar app de tareas", Prioridad=0, Selected=false},
        new Tarea {Id=3, Nombre = "Preparar examen de redes", Prioridad=0, Selected=false}
    };

    public ObservableCollection<Tarea> Tareas
    {
        get => tasks;
        set
        {
            tasks = value;
            OnPropertyChanged();
        }
    }


    public MainPage()
	{
		InitializeComponent();
        BindingContext = this;
        

        listaTareas.ItemsSource = tasks;
        //string selectedOption = (string)picker.SelectedItem;
    }

    void btnAgregarTarea_Clicked(System.Object sender, System.EventArgs e)
    {
        if (txtTarea.Text.Trim() != "")
        {
            if (tareaNoSeRepite())
            {
                var contadorProvisional = 0;
                txtTarea.PlaceholderColor = Color.FromRgb(100, 100, 100);
                txtTarea.Placeholder = "Ingresa la tarea";
                Tarea t = new Tarea
                {
                    Id = tasks.Count() + 1,
                    Nombre = txtTarea.Text,
                    Prioridad = picker.SelectedIndex,
                    Selected = false
                };

                if (picker.SelectedIndex == 0)
                {
                    tasks.Add(t);
                }
                else if (picker.SelectedIndex == 1)
                {
                    tasks.Insert(calcularPosicion(), t);
                }
                else if (picker.SelectedIndex == 2)
                {
                    tasks.Insert(0, t);
                }

                foreach (var task in tasks)
                {
                    //Console.WriteLine(task.Nombre);
                    task.Id = contadorProvisional + 1;
                    contadorProvisional++;
                }

                listaTareas.ItemsSource = null;
                listaTareas.ItemsSource = tasks;

                txtTarea.Text = string.Empty;
                picker.SelectedItem = "Normal";
            }
            else
            {
                txtTarea.Text = string.Empty;
                txtTarea.PlaceholderColor = Color.FromRgb(100, 0, 0);
                txtTarea.Placeholder = "¡Esa tarea ya existe!";
            }
        }
        else
        {
            txtTarea.PlaceholderColor = Color.FromRgb(100,0,0);
            txtTarea.Placeholder = "¡No dejes el campo vacío!";
        } 
    }

    public int calcularPosicion()
    {
        Console.WriteLine("entro a caclular posicion");
        int contador = 0;
        foreach (var task in tasks)
        {
            Console.WriteLine("entro a foreach ");
            if (task.Prioridad == 2)
            {
                Console.WriteLine("entro a if");
                contador++;
            }
            else if (task.Prioridad == 1)
            {
                Console.WriteLine("q verga retorno?"+contador);
                return contador;
            }
        }
        Console.WriteLine("no debería retornar aquí");
        return 0;
    }

    public bool tareaNoSeRepite()
    {
        bool boolProvisional = false;
        if (tasks.Count() != 0)
        {
            foreach (var task in tasks)
            {
                Console.WriteLine("txt" + txtTarea.Text.ToLower());
                Console.WriteLine("task existente" + task.Nombre.ToLower());
                if (txtTarea.Text.ToLower().Trim() == task.Nombre.ToLower().Trim())
                {

                    boolProvisional = false;
                }
                else
                {
                    boolProvisional = true;
                }
            }
        }
        else
        {
            boolProvisional = true;
        }
        

        return boolProvisional;

    }

    void btnActualizarLista_Clicked(System.Object sender, System.EventArgs e)
    {
        txtTarea.PlaceholderColor = Color.FromRgb(100, 100, 100);
        txtTarea.Placeholder = "Ingresa la tarea";

        var contadorProvisional = 0;

        List<Tarea> tareasEliminadas = tasks.Where(t => t.Selected).ToList();
        foreach (Tarea tarea in tareasEliminadas)
        {
            tasks.Remove(tarea);
            Console.WriteLine("tarea eliminada"+ tarea.Nombre);
        }

        foreach (var task in tasks)
        {
            Console.WriteLine(task.Nombre);
            task.Id = contadorProvisional + 1;
            contadorProvisional++;
        }

        listaTareas.ItemsSource = null;
        listaTareas.ItemsSource = tasks;

        foreach (var task in tasks)
        {
            Console.WriteLine(task.Nombre);
        }
    }

    void CheckBox_CheckedChanged(System.Object sender, Microsoft.Maui.Controls.CheckedChangedEventArgs e)
    {
        foreach (var task in tasks)
        {
            if (task.Selected)
            {
                btnActualizarLista.IsEnabled = true;
            }
        }
        
    }
}


