namespace jdelgadoT3.Views;

public partial class vRegistro : ContentPage
{
    public vRegistro()
    {
        InitializeComponent();
    }

    private void txtIdentificación_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (pckTipoIden.SelectedItem == null)
            return; // No se ha seleccionado todavía

        string tipoId = pckTipoIden.SelectedItem.ToString();
        int maxLength = 0;

        if (tipoId == "CI")
            maxLength = 10;
        else if (tipoId == "RUC")
            maxLength = 13;
        else if (tipoId == "Pasaporte")
            maxLength = int.MaxValue;

        var entry = sender as Entry;

        // Solo ajustar si realmente supera el máximo
        if (!string.IsNullOrEmpty(entry.Text) && entry.Text.Length > maxLength)
        {
            string correctedText = entry.Text.Substring(0, maxLength);
            entry.Text = correctedText;
            entry.CursorPosition = correctedText.Length; // Mantener cursor al final
        }
    }
    private async void btnRegistrar_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtIdentificación.Text) ||
                string.IsNullOrWhiteSpace(txtNombres.Text) ||
                string.IsNullOrWhiteSpace(txtApellidos.Text) ||
                string.IsNullOrWhiteSpace(txtCorreo.Text) ||
                string.IsNullOrWhiteSpace(txtSalario.Text) ||
                pckTipoIden.SelectedIndex == -1)
        {
            await DisplayAlert("Error", "Por favor llene todos los campos.", "OK");
            return;
        }
        // Validación según el tipo de identificación
        string tipoId = pckTipoIden.SelectedItem.ToString();
        string identificacion = txtIdentificación.Text;

        if (tipoId == "CI" && identificacion.Length != 10)
        {
            await DisplayAlert("Error", "La CI debe tener exactamente 10 números.", "OK");
            return;
        }
        else if (tipoId == "RUC" && identificacion.Length != 13)
        {
            await DisplayAlert("Error", "El RUC debe tener exactamente 13 números.", "OK");
            return;
        }
        else if ((tipoId == "CI" || tipoId == "RUC") && !identificacion.All(char.IsDigit))
        {
            await DisplayAlert("Error", "La identificación debe contener solo números.", "OK");
            return;
        }
        var datos = new Contacto
        {
            TipoIdentificacion = tipoId,
            Identificacion = identificacion,
            Nombres = txtNombres.Text,
            Apellidos = txtApellidos.Text,
            FechaNacimiento = dtpckDate.Date,
            Correo = txtCorreo.Text,
            Salario = Convert.ToDecimal(txtSalario.Text)
        };

        await Navigation.PushAsync(new Views.vDatos(datos));
    }
    public class Contacto
    {
        public string TipoIdentificacion { get; set; }
        public string Identificacion { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Correo { get; set; }
        public decimal Salario { get; set; }
    }
}