using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using PruebaWebApp.Models;
using System.Threading.Tasks;
using System.Net;

namespace PruebaWebApp.Controllers
{
    public class PersonaFisicaController : Controller
    {
        private string DireccionPersonaFisica = WebConfigurationManager.AppSettings["WebApiPersonaFisica"];
        // GET: PersonaFisica
        public ActionResult Index()
        {
            //IEnumerable<PersonaFisica> listaPaersonas = null;
            List<PersonaFisica> listaPesonasFisicas = new List<PersonaFisica>();
            try
            {
                using (var cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri(DireccionPersonaFisica);
                    var response = cliente.GetAsync("PersonaFisica");
                    response.Wait();
                    var result = response.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<IList<PersonaFisica>>();
                        readTask.Wait();
                        foreach (var item in readTask.Result)
                        {
                            if (item.Activo == true)
                            {
                                listaPesonasFisicas.Add(
                                    new PersonaFisica { IdPersonaFisica = item.IdPersonaFisica
                                                        ,Nombre= item.Nombre
                                                        ,ApellidoPaterno = item.ApellidoPaterno
                                                        ,ApellidoMaterno = item.ApellidoMaterno
                                                        ,RFC = item.RFC
                                                        ,FechaNacimiento = item.FechaNacimiento
                                                        ,UsuarioAgrega = item.UsuarioAgrega
                                                        ,Activo = item.Activo
                                                        ,FechaRegistro = item.FechaRegistro
                                                        ,FechaActualizacion = item.FechaActualizacion
                                    });
                            }

                        }
                        //listaPesonasFisicas = readTask.Result;
                    }
                    else
                    {
                        listaPesonasFisicas = null;
                        ModelState.AddModelError(string.Empty, "Server error try after some time.");
                    }
                }

                return View(listaPesonasFisicas);

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            
        }

        public ActionResult Nuevo()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Nuevo(PersonaFisica persona)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var cliente = new HttpClient())
                    {
                        cliente.BaseAddress = new Uri(DireccionPersonaFisica);
                        var response = await cliente.PostAsJsonAsync("PersonaFisica", persona);
                        if (response.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Server error try after some time.");
                            return RedirectToAction("Nuevo");
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                    return RedirectToAction("Nuevo");
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            
        }

        public async Task<ActionResult> Editar(int Id)
        {
            PersonaFisica persona = new PersonaFisica();
            if (Id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                try
                {
                    using(var cliente = new HttpClient())
                    {
                        cliente.BaseAddress = new Uri(DireccionPersonaFisica);
                        var result = await cliente.GetAsync($"PersonaFisica/{Id}");
                        if (result.IsSuccessStatusCode)
                        {
                            persona = await result.Content.ReadAsAsync<PersonaFisica>();
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Server error try after some time.");
                        }
                    }
                    return View(persona);
                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }
            }
        }

        [HttpPost]
        public async Task<ActionResult> Editar(PersonaFisica persona)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var cliente = new HttpClient())
                    {
                        cliente.BaseAddress = new Uri(DireccionPersonaFisica);
                        var response = await cliente.PutAsJsonAsync("PersonaFisica", persona);
                        if (response.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Server error try after some time.");
                            return RedirectToAction("Editar");
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                    return RedirectToAction("Editar");
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


        [HttpGet]
        public async Task<ActionResult> Eliminar(int Id)
        {
            try
            {
                if (Id != 0)
                {
                    using (var cliente = new HttpClient())
                    {
                        cliente.BaseAddress = new Uri(DireccionPersonaFisica);
                        //var response = await cliente.PutAsJsonAsync("PersonaFisica", persona);
                        var response = await cliente.DeleteAsync($"PersonaFisica/{Id}");
                        if (response.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Server error try after some time.");
                            return RedirectToAction("Index");
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


    }
}