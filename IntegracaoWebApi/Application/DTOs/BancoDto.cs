namespace IntegracaoWebApi.Application.DTOs
{
    public class BancoDto
    {
        public int? Codigo { get; set; }
        public string? Nome { get; set; }
        public string? Ispb { get; set; }

        // Campo adicional que pode ser calculado ou transformado
        public string NomeFormatado => $"{Codigo} - {Nome}";
    }
}
