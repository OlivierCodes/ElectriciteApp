
using ElectriciteApp.Data;
using ElectriciteApp.Models;
using System.Security.Cryptography.X509Certificates;


namespace ElectriciteApp.Views;

[QueryProperty("Item", "Item")]
public partial class ListeDesLocatairesItemPage : ContentPage
{

	private readonly Database database;

	public Locataire Item
	{
		get => BindingContext as Locataire;
		set => BindingContext  = value;

	}

    protected override void OnAppearing()
    {
		if(Item.Id == 0)
		{
			Title = "Ajouter un Locataire";
		}
        base.OnAppearing();
    }


    public ListeDesLocatairesItemPage(Database database)
	{
		InitializeComponent();

		this.database = database;

	}

    private async void OnSave(object sender, EventArgs e)
    {
		if (string.IsNullOrWhiteSpace(Item.Nom))
		{
			await DisplayAlert("Nom obligatoire", "Veuillez entrer le nom", "Ok");
			return;
		}

		await database.SaveItemAsync(Item);
		await Shell.Current.GoToAsync("..",true);
		
    }

    private async void OnCancel(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..", true);

    }

    private async void OnDelete(object sender, EventArgs e)
    {
		if (Item.Id == 0)
			return;
		var answer = await DisplayAlert("Alerte!!!", "Êtes-vous sûr de vouloir supprimer ce locataire ?", "Oui", "Non");


		if (answer)
		{
            await database.DeleteItemAsync(Item);
            await Shell.Current.GoToAsync("..", true);
        }

    }


}