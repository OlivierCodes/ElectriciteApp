using ElectriciteApp.Views;

namespace ElectriciteApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ListeDesLocatairesItemPage), typeof(ListeDesLocatairesItemPage));
        }
    }
}
