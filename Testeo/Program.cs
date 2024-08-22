using MisClases;

namespace Testeo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Personaje pjUno = new Heroe(9,"Bruce Wayne", "Batman", "Ciudad Gotica", "La liga de la Justicia");
            Personaje pjDos = new Heroe(8,"Bruce Wayne", "Batman", "Ciudad Gotica", "La liga de la Justicia");
            Personaje pjTres = pjUno;

            pjUno.SetHabilidad("inteligencia");


            //Comparacion de memoria
            Console.WriteLine($"La comparacion entre pj 1 y pj 2: {pjUno == pjDos}");
            Console.WriteLine($"La comparacion entre pj 1 y pj 3: {pjUno == pjTres}");
            Console.WriteLine($"La comparacion entre pj 2 y pj 3: {pjDos == pjTres}");

            
            //Comparacion de elemento 
            Console.WriteLine($"La comparacion entre pj 1 y pj 2: {pjUno.NombreReal == pjDos.NombreReal}");
            Console.WriteLine($"La comparacion entre pj 1 y pj 3: {pjUno.NombreReal == pjTres.NombreReal}");
            Console.WriteLine($"La comparacion entre pj 2 y pj 3: {pjDos.NombreReal == pjTres.NombreReal}");

            Console.WriteLine($"La comparacion entre pj 1 y pj 2: {pjUno.Equals(pjDos)}");
            Console.WriteLine($"La comparacion entre pj 1 y pj 3: {pjUno.Equals(pjTres)}");
            Console.WriteLine($"La comparacion entre pj 2 y pj 3: {pjDos.Equals(pjTres)}");


            if (Personaje_Ado.Guardar(pjUno))
            {
                Console.WriteLine("Guardado con éxito");
            }
            else
            {
                Console.WriteLine("Error al intentar conectarse a la base de datos");
            }


            List<Personaje> misPersonajes = Personaje_Ado.LeerTodos();

            foreach (Personaje item in misPersonajes)
            {
                Console.WriteLine($"id: {item.Id} - nombre de personaje: {item.NombrePersonaje}");

            }


            Personaje pj4 = Personaje_Ado.LeerUnoSolo(4);
            Console.WriteLine($"id: {pj4.Id} - nombre de personaje: {pj4.NombrePersonaje}");


            if (Personaje_Ado.EliminarUno(4))
            {
                Console.WriteLine("Eliminado con éxito");
            }
            else
            {
                Console.WriteLine("Error al intentar conectarse a la base de datos");
            }


            if (pj4 is not null)
            {
                Console.WriteLine("El personaje se encuentra en la Base de Datos");
            }
            else
            {
                Console.WriteLine("El personaje NO se encuentra en la Base de Datos");
            }


            if (Personaje_Ado.Modificar(pj4))
            {
                Console.WriteLine("Modificado con Exito");
            }
            else
            {
                Console.WriteLine("Error al intentar conectarse a la base de datos");
            }


        }
    }
}
