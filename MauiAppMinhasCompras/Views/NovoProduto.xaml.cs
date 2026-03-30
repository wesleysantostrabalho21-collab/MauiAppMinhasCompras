using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views; // Certifique-se que o namespace está correto

public partial class NovoProduto : ContentPage
{
    public NovoProduto()
    {
        InitializeComponent();
        // Preenchendo o picker de categorias
        categoriaPicker.ItemsSource = new List<string> { "Alimentos", "Higiene", "Limpeza" };
    }

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            // 1. Validação da Categoria
            string selectedCategory = categoriaPicker.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selectedCategory))
            {
                await DisplayAlert("Erro", "Por favor, selecione uma categoria.", "OK");
                return;
            }

            // 2. Validação de Campos Vazios
            // Verifique se o nome txt_descriao está IGUAL ao x:Name no seu XAML
            if (string.IsNullOrEmpty(txt_descricao.Text) ||
                string.IsNullOrEmpty(txt_quantidade.Text) ||
                string.IsNullOrEmpty(txt_preco.Text))
            {
                await DisplayAlert("Erro", "Preencha todos os campos!", "OK");
                return;
            }

            // 3. Montagem do Objeto (Verifique se sua Model Produto tem o campo Categoria)
            Produto p = new Produto
            {
                Descricao = txt_descricao.Text,
                Quantidade = Convert.ToDouble(txt_quantidade.Text),
                Preco = Convert.ToDouble(txt_preco.Text),
                Categoria = selectedCategory
            };

            // 4. Persistência (App.Db precisa estar configurado no App.xaml.cs)
            await App.Db.Insert(p);

            await DisplayAlert("Sucesso!", "Produto adicionado!", "OK");

            // 5. Retorno
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            // Se cair aqui, a mensagem vai dizer se o erro é no banco ou na conversão
            await DisplayAlert("Ops", "Erro ao salvar: " + ex.Message, "OK");
        }
    }
}