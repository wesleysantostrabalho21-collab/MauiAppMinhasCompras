using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;

namespace MauiAppMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{
	ObservableCollection<Produto> lista = new ObservableCollection<Produto>();
	public ListaProduto()
	{
		InitializeComponent();

		lst_produtos.ItemsSource = lista;
	}

	protected async override void OnAppearing()
	{
		try
		{

            lista.Clear();

			List<Produto> tmp = await App.Db.GetAll();

			tmp.ForEach(i => lista.Add(i));
		}
		catch (Exception ex)
		{
            await DisplayAlert("ops", ex.Message, "OK");
		}
	}

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
		try
		{
			Navigation.PushAsync(new Views.NovoProduto());
		} catch (Exception ex)
		{
			DisplayAlert("ops", ex.Message, "OK");
		}
    }

    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
		try
		{
			string q = e.NewTextValue;

            lst_produtos.IsRefreshing = true;

            lista.Clear();

			List<Produto> tmp = await App.Db.Search(q);

			tmp.ForEach(i => lista.Add(i));
		} 
		catch (Exception ex)
		{
            await DisplayAlert("ops", ex.Message, "OK");
        }
        finally
        {
            lst_produtos.IsRefreshing = false;
        }
    }

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
		double soma = lista.Sum(i => i.Total);

		string msg = $"O total é {soma:C}";

		DisplayAlert("Total dos Produtos", msg, "OK");
    }

	private async void MenuItem_Clicked(object sender, EventArgs e)
	{
		try
		{
			MenuItem selecionado = sender as MenuItem;

			Produto p = selecionado.BindingContext as Produto;

			bool confirm = await DisplayAlert(
				"Tem certeza?", $"Remover {p.Descricao}?", "Sim", "Não");

			if (confirm)
			{
				await App.Db.Delete(p.Id);
				lista.Remove(p);
			}
         }
		catch (Exception ex)
		{
            await DisplayAlert("ops", ex.Message, "OK");
        }
	}

	private async void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
	{
		try
		{

			Produto p = e.SelectedItem as Produto;

			Navigation.PushAsync(new Views.EditarProduto
			{
				BindingContext = p,
			});
		}

		catch (Exception ex)
		{
			DisplayAlert("ops", ex.Message, "OK");
		}
	}

    private async void lst_produtos_Refreshing(object sender, EventArgs e)
    {
        try
        {
            lista.Clear();

            List<Produto> tmp = await App.Db.GetAll();

            tmp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
        {
            await DisplayAlert("ops", ex.Message, "OK");

        } finally
		{
			lst_produtos.IsRefreshing = false;
		}
    }

	private async void categoriaPicker_SelectedIndexChanged(object sender, EventArgs e)
	{
		string selectedCategory = categoriaPicker.SelectedItem?.ToString();
		lista.Clear();
		 List<Produto> tmp = await App.Db.GetAll();

        if (selectedCategory == "Todos" || string.IsNullOrEmpty(selectedCategory))
        {
			tmp.ForEach(i => lista.Add(i));
		}
		else
		{
            var filtrados = tmp.Where(p => p.Categoria == selectedCategory).ToList();
            filtrados.ForEach(i => lista.Add(i));
        }
	}
    private async void GerarRelatorio_Clicked(object sender, EventArgs e)
    {
		var relatorio = await App.Db.GetRelatorioPorCategoria();
		string mensagemRelatorio = "";
		foreach (var item in relatorio)
		{
			mensagemRelatorio += $"{item.Categoria}: {item.TotalGasto:C}\n";
		}
		await DisplayAlert("Relatório De Gastos por Categoria", mensagemRelatorio, "OK");
    }
}