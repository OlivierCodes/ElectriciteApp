using ClosedXML.Excel;
using ElectriciteApp.Data;
using ElectriciteApp.Models;
using System.Collections.ObjectModel;

namespace ElectriciteApp.Views
{
    public partial class ListeDesLocataires : ContentPage
    {

        private readonly Database database;
        public ObservableCollection<Locataire> Items { get; set; } = new();

        public ListeDesLocataires(Database database)
        {
            InitializeComponent();

            this.database = database;
            BindingContext = this;
        }

        protected override async void OnNavigatedTo(NavigatedToEventArgs args)
        {
            base.OnNavigatedTo(args);

            var items = await database.GetItemsAsync();

            MainThread.BeginInvokeOnMainThread(() =>
            {
                Items.Clear();

                foreach(var item in items)
                    Items.Add(item);
            });
        }


        private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (e.CurrentSelection.FirstOrDefault() is not Locataire item)
                return;


            await Shell.Current.GoToAsync(nameof(ListeDesLocatairesItemPage), true, new Dictionary<string, object>
            {
                ["Item"] = item

            });


        }

        private async void OnItemAdd(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(ListeDesLocatairesItemPage), true, new Dictionary<string, object>
            {
                ["Item"] = new Locataire()

            }) ;
        }


        private void OnGenerateExcelButtonClicked(object sender, EventArgs e)
        {
            var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "data.xlsx");


            // Créer le fichier Excel
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Sheet1");
                worksheet.Cell(1, 1).Value = "N°";
                worksheet.Cell(1, 2).Value = "NOM";
                worksheet.Cell(1, 3).Value = "Anc";
                worksheet.Cell(1, 4).Value = "Nou";
                worksheet.Cell(1, 5).Value = "Conso";
                worksheet.Cell(1, 6).Value = "PU";
                worksheet.Cell(1, 7).Value = "Montant";
                worksheet.Cell(1, 8).Value = "TF";
                worksheet.Cell(1, 9).Value = "Poubelle";
                worksheet.Cell(1, 10).Value = "D";
                worksheet.Cell(1, 11).Value = "Retse + dépCEET + Balais";
                worksheet.Cell(1, 12).Value = "Total";
                // Ajoutez plus de colonnes si nécessaire

                for (int i = 0; i < Items.Count; i++)
                {
                    worksheet.Cell(i + 2, 1).Value = Items[i].NumChambre;
                    worksheet.Cell(i + 2, 2).Value = Items[i].Nom;
                    worksheet.Cell(i + 2, 3).Value = Items[i].AncienKilowatts;
                    worksheet.Cell(i + 2, 4).Value = Items[i].NouveauKilowatts;
                    worksheet.Cell(i + 2, 5).Value = Items[i].Consommation;
                    worksheet.Cell(i + 2, 6).Value = Items[i].PrixUnitaire;
                    worksheet.Cell(i + 2, 7).Value = Items[i].Montant;
                    worksheet.Cell(i + 2, 8).Value = Items[i].TauxForfaitaire;
                    worksheet.Cell(i + 2, 9).Value = Items[i].PrixPoubelle;
                    worksheet.Cell(i + 2, 10).Value = Items[i].Don;
                    worksheet.Cell(i + 2, 11).Value = Items[i].PrixBalais;
                    worksheet.Cell(i + 2, 12).Value = Items[i].Total;
                    // Ajoutez plus de colonnes si nécessaire
                }

                workbook.SaveAs(filePath);
            }

            // Ouvrir le fichier Excel
            try
            {
                Launcher.OpenAsync(new OpenFileRequest
                {
                    File = new ReadOnlyFile(filePath)
                });
            }
            catch (Exception ex)
            {
                DisplayAlert("Erreur", $"Impossible d'ouvrir le fichier Excel : {ex.Message}", "OK");
            }
        }
    }
}


