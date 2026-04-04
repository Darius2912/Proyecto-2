using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore
{
    public class BaseManager
    {
<<<<<<< HEAD
        //todas las exepciones en las clases de app core deben ser enviadas a esta funcion para guardar datos de la exepcion
        protected void ManagerExeption(Exception exception)
        {

            //jb
            throw exception;
        }
    }

}


=======
        protected void ManegerException(Exception exception)
        {
            //TO DO: Escribir las excepciones en un archivo o en base de datos
            if (exception != null)
            {


                throw exception;
            }
        }
    }
}
>>>>>>> adb841ee777f14b12b6621b45f091600613b66d1
