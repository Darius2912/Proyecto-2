using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using DataAccess.DAO;
using Entities_DTOs;

namespace DataAccess.CRUD
{
    //Clase madre/padre abstracta de los crud
    //define como se hacen y comportan los crud en la arquitectura
    public abstract class CrudFactory
    {
        protected SqlDAO SqlDAO;

        //Definir los metodos que forman parte del contrato
        //Create
        //retrive
        //update 
        //delete

        

        public abstract void Create(BaseDTO baseDTO);

        public abstract void Update(BaseDTO baseDTO);

        public abstract void Delete(BaseDTO baseDTO);

        public abstract T RetrieveById<T>(int id);


        public abstract List<T> RetrieveAll<T>();


      
    }
}
