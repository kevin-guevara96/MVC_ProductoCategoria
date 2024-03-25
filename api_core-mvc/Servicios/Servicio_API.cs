using api_core_mvc.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using System.Collections.Generic;

namespace api_core_mvc.Servicios
{
    public class Servicio_API : IServicio_API
    {
        private static string _usuario;
        private static string _clave;
        private static string _baseurl;
        private static string _token;

        public Servicio_API()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            _usuario = builder.GetSection("ApiSettings:nombre").Value;
            _clave = builder.GetSection("ApiSettings:clave").Value;
            _baseurl = builder.GetSection("ApiSettings:baseURL").Value;
        }

        public async Task Autenticar()
        {
            var client = new HttpClient();

            client.BaseAddress = new Uri(_baseurl);

            var usuario = new Usuario() { Nombre = _usuario, Clave = _clave };

            var content = new StringContent(JsonConvert.SerializeObject(usuario), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/Autenticacion/Validar",content);

            var json_respuesta = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ResultadoToken>(json_respuesta);

            _token = result.token;
        }

        public async Task<List<Producto>> Lista()
        {
            List<Producto> lista = new List<Producto>();

            await Autenticar();

            var client = new HttpClient();

            client.BaseAddress = new Uri(_baseurl);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var response = await client.GetAsync("api/Producto/Listar");

            if (response.IsSuccessStatusCode)
            {
                var json_Result = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<ResultadoApi>(json_Result);

                lista = result.lresponse;
            }

            return lista;
        }

        public async Task<Producto> Obtener(int id)
        {
            Producto obj = new Producto();

            await Autenticar();

            var client = new HttpClient();

            client.BaseAddress = new Uri(_baseurl);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var response = await client.GetAsync($"api/Producto/Obtener/{id}");

            if (response.IsSuccessStatusCode)
            {
                var json_Result = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<ResultadoApi>(json_Result);

                obj = result.oresponse;
            }

            return obj;
        }

        public async Task<bool> Guardar(Producto obj)
        {
            bool respuesta = false;

            await Autenticar();

            var client = new HttpClient();

            client.BaseAddress = new Uri(_baseurl);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/Producto/Guardar",content);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }

            return respuesta;
        }

        public async Task<bool> Editar(Producto obj)
        {
            bool respuesta = false;

            await Autenticar();

            var client = new HttpClient();

            client.BaseAddress = new Uri(_baseurl);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

            var response = await client.PutAsync("api/Producto/Editar", content);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }

            return respuesta;
        }

        public async Task<bool> Eliminar(int id)
        {
            bool respuesta = false;

            await Autenticar();

            var client = new HttpClient();

            client.BaseAddress = new Uri(_baseurl);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var response = await client.DeleteAsync($"api/Producto/Eliminar/{id}");

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }

            return respuesta;
        }        
       
    }
}
