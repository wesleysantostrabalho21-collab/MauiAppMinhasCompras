using SQLite;

namespace MauiAppMinhasCompras.Models
{
    public class Produto
    {
        string _descricao;
        int _quantidade;
        double _preco;

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Descricao

        
        {
            get => _descricao;
            set
            {
                if (value == null)
                {
                    throw new Exception("Por favor, preencha a descrição.");
                }
                _descricao = value;
            }
        }
        
        public double Quantidade
        {
            get => _quantidade;
            set
            {
                if (value <= 0) 
                {
                    throw new Exception("Por favor, Preencha a Quantidade.");
                }

                _quantidade = (int)value;
            }

        }
        public double Preco 
        {
            get => _preco;
            set
            {
                if (value <= 0)
                {
                    throw new Exception("Por favor, Preço precisa de um valor diferente de 0.");
                }

                _preco = value;
            }       
        }


        public double Total { get => Quantidade * Preco; }
    }
}
