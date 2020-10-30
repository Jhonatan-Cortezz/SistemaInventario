using SistemaInventario.AccesoDatos.Data;
using SistemaInventario.SistemaInventario.AccesoDatos.Repositorio.IRepositorio;
using SistemaInventario.SistemaInventario.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaInventario.SistemaInventario.AccesoDatos.Repositorio
{
    public class BodegaRepositorio : Repositorio<Bodega>, IBodegaRepositorio
    {
        //como en el constructor Repositorio requiere el dbcotext debemos enviarle el paramtro
        private readonly ApplicationDbContext _db;

        public BodegaRepositorio(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }

        public void actualizar(Bodega bodega)
        {
            var bodegaDb = _db.Bodegas.FirstOrDefault(b => b.Id == bodega.Id);
            if(bodegaDb != null)
            {
                bodegaDb.Nombre = bodega.Nombre;
                bodegaDb.Descripcion = bodega.Descripcion;
                bodegaDb.Estado = bodega.Estado;

                _db.SaveChanges();
            }
        }
    }
}
