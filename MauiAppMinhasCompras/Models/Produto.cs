using SQLite;

namespace MauiAppMinhasCompras.Models
{
    public class Produto
    {
        string _descricao;
        double _quantidade; // Mudei para double para aceitar KG ou frações
        double _preco;
        string _categoria; // Campo novo necessário!

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Descricao
        {
            get => _descricao;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("Por favor, preencha a descrição.");
                _descricao = value;
            }
        }

        public double Quantidade
        {
            get => _quantidade;
            set
            {
                if (value <= 0)
                    throw new Exception("A quantidade deve ser maior que zero.");
                _quantidade = value;
            }
        }

        public double Preco
        {
            get => _preco;
            set
            {
                if (value <= 0)
                    throw new Exception("O preço deve ser maior que zero.");
                _preco = value;
            }
        }

        // --- INFORMAÇÕES QUE DEVEM SER INSERIDAS ---

        public string Categoria
        {
            get => _categoria;
            set => _categoria = value;
        }

        // Propriedade calculada para a ListView (Binding Total)
        [Ignore] // O SQLite ignora isso, serve apenas para a tela
        public double Total => Quantidade * Preco;
    }
}