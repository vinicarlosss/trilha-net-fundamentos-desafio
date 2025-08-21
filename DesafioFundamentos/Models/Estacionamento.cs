using System.Text.RegularExpressions;

namespace DesafioFundamentos.Models
{
    public class Estacionamento
    {
        private decimal precoInicial = 0;
        private decimal precoPorHora = 0;
        private List<string> veiculos = new List<string>();

        public Estacionamento(decimal precoInicial, decimal precoPorHora)
        {
            this.precoInicial = precoInicial;
            this.precoPorHora = precoPorHora;
        }

        public void AdicionarVeiculo()
        {
            bool placaValida = false;
            while (!placaValida)
            {
                Console.WriteLine("Digite a placa do veículo para estacionar: (Modelo antigo: ABC1234 || Modelo Mercosul: BRA2E19)");
                string placa = Console.ReadLine();
                if (checarPlaca(placa))
                {
                    veiculos.Add(placa);
                    Console.WriteLine("Veículo estacionado com sucesso!");
                    placaValida = true;
                }
                else
                {
                    Console.WriteLine("Digite uma placa de veículo em formato válido");
                }
            }
        }

        public void RemoverVeiculo()
        {
            Console.WriteLine("Digite a placa do veículo para remover: (Modelo antigo: ABC1234 || Modelo Mercosul: BRA2E19)");
            string placa = Console.ReadLine();
            // Verifica se o veículo existe
            if (veiculos.Any(x => x.ToUpper() == placa.ToUpper()))
            {
                bool horaValida = false;
                int horas = 0;
                while (!horaValida)
                {
                    Console.WriteLine("Digite a quantidade de horas que o veículo permaneceu estacionado:");
                    if (int.TryParse(Console.ReadLine(), out horas))
                    {
                        horaValida = !horaValida;
                    }
                    else
                    {
                        Console.WriteLine("Valor inválido!");
                    }
                }
                decimal valorTotal = precoInicial + precoPorHora * horas;
                veiculos.Remove(placa);
                Console.WriteLine($"O veículo {placa} foi removido e o preço total foi de: R$ {valorTotal}");
            }
            else
            {
                Console.WriteLine("Desculpe, esse veículo não está estacionado aqui. Confira se digitou a placa corretamente");
            }
        }

        public void ListarVeiculos()
        {
            // Verifica se há veículos no estacionamento
            if (veiculos.Any())
            {
                Console.WriteLine("Os veículos estacionados são:");
                foreach (string placa in veiculos)
                {
                    Console.WriteLine(placa);
                }
            }
            else
            {
                Console.WriteLine("Não há veículos estacionados.");
            }
        }

        //verifica se a placa informada é válida para o terrirório brasileiro
        public bool checarPlaca(string placa)
        {
            if (string.IsNullOrWhiteSpace(placa))
                return false;

            placa = placa.ToUpper();
            //Regex para checar modelo da placa do Mercosul e modelo antigo
            string padraoAntigo = @"^[A-Z]{3}[0-9]{4}$";
            string padraoMercosul = @"^[A-Z]{3}[0-9][A-Z][0-9]{2}$";
            return Regex.IsMatch(placa, padraoAntigo) || Regex.IsMatch(placa, padraoMercosul);
        }
    }
}
