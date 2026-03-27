using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore
{
    public class BaseManager
    {
        protected void ManegerException(Exception exception)
        {
            //TO DO: Escribir las excepciones en un archivo o en base de datos
            if (exception != null)
            {

<<<<<<< HEAD
                //jb
=======

>>>>>>> BF_Dario
                throw exception;
            }
        }
    }
}