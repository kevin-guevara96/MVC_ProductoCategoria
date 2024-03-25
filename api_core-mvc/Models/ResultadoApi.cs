namespace api_core_mvc.Models
{
    public class ResultadoApi
    {
        public string? message { get; set; }
        public List<Producto>? lresponse { get; set; }
        public Producto? oresponse { get; set; }
    }
}
