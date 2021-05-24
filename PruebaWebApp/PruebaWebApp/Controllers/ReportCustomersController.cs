using Newtonsoft.Json;
using PruebaWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;

namespace PruebaWebApp.Controllers
{
    public class ReportCustomersController : Controller
    {
        private string DireccionCustomer = WebConfigurationManager.AppSettings["ReportClientes"];
        private string UserApi = WebConfigurationManager.AppSettings["User"];
        private string PassApi = WebConfigurationManager.AppSettings["Pass"];
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ActionName("ObterneListaCustomer")]
        public JsonResult ObterneListaCustomer()
        {
            List<Customer> listacustomers = new List<Customer>();
            try
            {
                listacustomers = ListaCustomer();


                return Json(new { success = true, error = false, lista = listacustomers, message = "Lista Customer" });
            }
            catch (Exception ex )
            {

                return Json(new { success = false, error = ex, message = "Error al generar Lista" });
            }
            
        }
        public ActionResult ExportToExcel()
        {
            var gv = new GridView();
            gv.DataSource = ListaCustomer();
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=DemoExcel.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
            gv.RenderControl(objHtmlTextWriter);
            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();
            return View("Index");
        }


        private async Task<List<Customer>> CustomerList()
        {
            var jsonSerializerOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
            List<Customer> Listcustomers = new List<Customer>();
            try
            {
                using (var cliente = new HttpClient())
                {
                    var token = ObterToken();
                    cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    cliente.BaseAddress = new Uri(DireccionCustomer);
                    var response = cliente.GetAsync($"customers");
                    response.Wait();
                    var result = response.Result;

                    if (result.IsSuccessStatusCode)
                    {

                        var modeljson = JsonConvert.DeserializeObject<ResponseCustomer>(await result.Content.ReadAsStringAsync());

                        foreach (var item in modeljson.Data)
                        {
                            Listcustomers.Add(
                                new Customer
                                {
                                    IdCliente = item.IdCliente
                                        ,FechaRegistroEmpresa = item.FechaRegistroEmpresa
                                        ,RazonSocial = item.RazonSocial
                                        ,RFC = item.RFC
                                        ,Sucursal = item.Sucursal
                                        ,IdEmpleado = item.IdEmpleado
                                        ,Nombre = item.Nombre
                                        ,Paterno = item.Paterno
                                        ,Materno = item.Materno
                                        ,IdViaje = item.IdViaje
                                });
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return Listcustomers;
        }

        private string ObterToken()        
        {
            string token = null;
            UsuarioReportes usuario = new UsuarioReportes();
            usuario.Username = UserApi;
            usuario.Password = PassApi;
            try
            {
                using (var cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri(DireccionCustomer);
                    var response = cliente.PostAsJsonAsync("login/authenticate", usuario);
                    response.Wait();
                    var result = response.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var reedTask = result.Content.ReadAsAsync<ResponseToken>();
                        reedTask.Wait();
                        var tokeObtenido = reedTask.Result;
                        token = tokeObtenido.Data.ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            
            return token;

        }

        public List<Customer> ListaCustomer()
        {
            List<Customer> listacustomers = new List<Customer>();

            try
            {
                var response = CustomerList();
                foreach (var item in response.Result)
                    {
                        listacustomers.Add(
                            new Customer
                            {
                                IdCliente = item.IdCliente
                                    ,FechaRegistroEmpresa = item.FechaRegistroEmpresa
                                    ,RazonSocial = item.RazonSocial
                                    ,RFC = item.RFC
                                    ,Sucursal = item.Sucursal
                                    ,IdEmpleado = item.IdEmpleado
                                    ,Nombre = item.Nombre
                                    ,Paterno = item.Paterno
                                    ,Materno = item.Materno
                                    ,IdViaje = item.IdViaje
                            });
                    }
                return listacustomers;
            }
            catch (Exception ex)
            {
                return listacustomers;
            }
        }

    }
}