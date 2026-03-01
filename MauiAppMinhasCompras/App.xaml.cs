using Microsoft.Extensions.DependencyInjection;

namespace MauiAppMinhasCompras
{
    public partial class App : Application
    {
        public App()
        {

            //MainPage = new AppShell();
            MainPage = new NavigationPage(new Views.ListaProduto());
        }
    }
}