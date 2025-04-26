using static jdelgadoT3.Views.vRegistro;

namespace jdelgadoT3.Views;

public partial class vDatos : ContentPage
{
    private Contacto _contacto;

    public vDatos(Contacto contacto)
    {
        InitializeComponent();
        _contacto = contacto;
        MostrarDatos();
    }

    private void MostrarDatos()
    {
        lblTipoId.Text = $"Tipo de Identificación: {_contacto.TipoIdentificacion}";
        lblIdentificacion.Text = $"Identificación: {_contacto.Identificacion}";
        lblNombres.Text = $"Nombres: {_contacto.Nombres}";
        lblApellidos.Text = $"Apellidos: {_contacto.Apellidos}";
        lblFecha.Text = $"Fecha de Nacimiento: {_contacto.FechaNacimiento:dd/MM/yyyy}";
        lblCorreo.Text = $"Correo: {_contacto.Correo}";
        lblSalario.Text = $"Salario: {_contacto.Salario:C}";

        // Aporte IESS 9.45% en Ecuador
        decimal aporte = _contacto.Salario * 0.0945m;
        lblAporteIESS.Text = $"Aporte al IESS: {aporte:C}";
    }
    private async void btnExportar_Clicked(object sender, EventArgs e)
    {
        try
        {
            string contenido = $"Tipo Identificación: {_contacto.TipoIdentificacion}\n" +
                               $"Identificación: {_contacto.Identificacion}\n" +
                               $"Nombres: {_contacto.Nombres}\n" +
                               $"Apellidos: {_contacto.Apellidos}\n" +
                               $"Fecha de Nacimiento: {_contacto.FechaNacimiento:dd/MM/yyyy}\n" +
                               $"Correo: {_contacto.Correo}\n" +
                               $"Salario: {_contacto.Salario:C}\n" +
                               $"Aporte al IESS: {_contacto.Salario * 0.0945m:C}";

            string ruta = Path.Combine(FileSystem.AppDataDirectory, "Contacto.txt");
            File.WriteAllText(ruta, contenido);

            await DisplayAlert("Éxito", $"Archivo guardado en:\n{ruta}", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudo exportar: {ex.Message}", "OK");
        }
    }
}