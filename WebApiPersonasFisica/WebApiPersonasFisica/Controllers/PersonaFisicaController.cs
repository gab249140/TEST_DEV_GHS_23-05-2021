using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using WebApiPersonasFisica.Models;
using System.Web.Http.Description;
using System.Data.SqlClient;
using System.Data;

namespace WebApiPersonasFisica.Controllers
{
    public class PersonaFisicaController : ApiController
    {
        private TEST_DEV_TOKAEntities db = new TEST_DEV_TOKAEntities();

        //GetALL
        [HttpGet]
        public IQueryable<Tb_PersonasFisicas> GetPersonasFisicas()
        {
            return db.Tb_PersonasFisicas;
        }

        //GetById
        [HttpGet]
        [ResponseType(typeof(Tb_PersonasFisicas))]
        public IHttpActionResult GetPersonaFisica(int id)
        {
            Tb_PersonasFisicas persona = db.Tb_PersonasFisicas.Find(id);
            if (persona == null)
            {
                return BadRequest(ModelState);
            }
            return Ok(persona);
        }

        //SaveNew
        [HttpPost]
        [ResponseType(typeof(Tb_PersonasFisicas))]
        public IHttpActionResult PostSaveNewPersonaFisica(Tb_PersonasFisicas persona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            else
            {
                try
                {
                    var resultado = db.sp_AgregarPersonaFisica(persona.Nombre, persona.ApellidoPaterno, persona.ApellidoMaterno, persona.RFC, persona.FechaNacimiento, persona.UsuarioAgrega);    
                    if (resultado != null)
                    {

                        List<ResponseStored> response = new List<ResponseStored>();
                        foreach (var item in resultado)
                        {
                            response.Add(new ResponseStored
                            {
                                ERROR = Int32.Parse(item.ERROR.ToString()),
                                MENSAJEERROR = item.MENSAJEERROR.ToString()
                            });
                        }

                        return Ok(response);
                    }
                    else
                    {
                        return BadRequest(ModelState);
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ModelState);
                }

            }            
        }

        //UpdateData
        [HttpPut]
        [ResponseType(typeof(Tb_PersonasFisicas))]
        public IHttpActionResult PutUpdatePersonaFisica(Tb_PersonasFisicas persona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            else
            {
                try
                {
                    var resultado = db.sp_ActualizarPersonaFisica(persona.IdPersonaFisica, persona.Nombre, persona.ApellidoPaterno, persona.ApellidoMaterno, persona.RFC, persona.FechaNacimiento, persona.UsuarioAgrega);
                    if (resultado != null)
                    {

                        List<ResponseStored> response = new List<ResponseStored>();
                        foreach (var item in resultado)
                        {
                            response.Add(new ResponseStored
                            {
                                ERROR = Int32.Parse(item.ERROR.ToString()),
                                MENSAJEERROR = item.MENSAJEERROR.ToString()
                            });
                        }

                        return Ok(response);
                    }
                    else
                    {
                        return BadRequest(ModelState);
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ModelState);
                }

            }
        }

        //Delete
        [HttpDelete]
        [ResponseType(typeof(Tb_PersonasFisicas))]
        public IHttpActionResult DeletePersonaFisica(int id)
        {
            Tb_PersonasFisicas persona = db.Tb_PersonasFisicas.Find(id);
            if (persona == null)
            {
                return BadRequest(ModelState);
            }

            else
            {
                try
                {
                    db.sp_EliminarPersonaFisica(id);
                    return Ok("Registro Eliminado");
                }
                catch (Exception ex)
                {
                    return BadRequest(ModelState);
                }

            }
        }



    }
}
