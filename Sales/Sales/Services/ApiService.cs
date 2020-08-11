namespace Sales.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Common.Models;
    using Newtonsoft.Json;

    public class ApiService
    {
        //Metodo que consume de cualquier servicio API, cualquier tipo de Lista
        //async Task<Response> GetList<T>: Un metodo async tiene la funcion de que una petición puede
        //esperarla n cantodad de tiempo, async Task<Response> devuelve una tarea en este caso la tarea
        //es la clase Response, la clase Response tiene la tarea de devolver atributos dependiendo del tipo de respuesta
        //que se necesite, GetList<T> con el metodo que devuelve una tarea la T representa el objeto a llamar, 
        //puede ser cualquier objeto

        public async Task<Response> GetList<T>(string urlBase, string prefix, string controller)
        {
            try
            {
                //
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = $"{prefix}{controller}";
                var response = await client.GetAsync(url);
                var answer = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = answer
                    };
                }
                //Convierte un string de json en una lista de objeto
                var list = JsonConvert.DeserializeObject<List<T>>(answer);
                return new Response
                {
                    IsSuccess = true,
                    Result = list
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }
    }
}
