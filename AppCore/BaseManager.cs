namespace AppCore
{
    public class BaseManager
    {
        //todas las exepciones en las clases de app core deben ser enviadas a esta funcion para guardar datos de la exepcion
        protected void ManagerExeption(Exception exception) {

            //pendiente en esta parte enviar las exepciones a un archivo de texto

            throw exception;
        }


    }
}
