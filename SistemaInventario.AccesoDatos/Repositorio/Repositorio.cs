using Microsoft.EntityFrameworkCore;
using SistemaInventario.AccesoDatos.Data;
using SistemaInventario.SistemaInventario.AccesoDatos.Repositorio.IRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SistemaInventario.SistemaInventario.AccesoDatos.Repositorio
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {

        //propiedad solo de lectura para el dbcontext
        private readonly ApplicationDbContext _db;
        internal DbSet<T> bdSet;

        //constructor
        public Repositorio(ApplicationDbContext db)
        {
            _db = db;
            this.bdSet = _db.Set<T>();
        }



        public void Agregar(T entidad)
        {
            bdSet.Add(entidad); //insert into Table
        }

        public T Obtener(int id)
        {
            return bdSet.Find(id);
        }

        public T ObtenerPrimero(Expression<Func<T, bool>> filter = null, string incluirPropiedades = null)
        {
            IQueryable<T> query = bdSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (incluirPropiedades != null)
            {
                foreach (var incluirProp in incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(incluirProp);
                }
            }


            return query.FirstOrDefault();
        }

        public IEnumerable<T> ObtenerTodos(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string incluirPropiedades = null)
        {
            IQueryable<T> query = bdSet;

            if(filter != null)
            {
                query = query.Where(filter);
            }

            if(incluirPropiedades != null)
            {
                foreach (var incluirProp in incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(incluirProp);
                }
            }

            if(orderBy != null)
            {
                return orderBy(query).ToList();
            }

            return query.ToList();
        }

        public void Remover(int id)
        {
            T entidad = bdSet.Find(id);
            Remover(entidad);
        }

        public void Remover(T entidad)
        {
            bdSet.Remove(entidad);
        }

        public void RemoverRango(IEnumerable<T> entidad)
        {
            bdSet.RemoveRange(entidad);
        }
    }
}
