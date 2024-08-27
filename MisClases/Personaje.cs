using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Drawing;
using System.Runtime.ConstrainedExecution;
using System;

namespace MisClases
{
    public abstract class Personaje
    {
        //atributos
        //Todos deben ser privados para el exterior pero las clases derivadas deben poder verlos, aplicar la visibilidad correspondiente.
        protected int id;
        protected string nombreReal;
        protected string nombrePersonaje;
        protected string lugarOrigen;
        protected List<string> habilidades;

        //propiedades
        public int Id { get => id; set => id = value; }
        public string NombreReal { get => nombreReal; set => nombreReal = value; }

        public string NombrePersonaje { get => nombrePersonaje; set => nombrePersonaje = value; }

        public string LugarOrigen { get => lugarOrigen; }

        public string Tipo { get => this.GetType().Name; }

        //Descripcion: retorna el resultado del método MostrarDescripcion.
        public string Descripcion { get => this.MostrarDescripcion(); }

        //Habilidades: debe retornar un solo string con todas las habilidades concatenadas y separadas por una coma.
        public string Habilidades {
            get
            {
                string valores = string.Empty;
                for (int i = 0; i < this.habilidades.Count; i++) 
                {
                    valores += this.habilidades[i];
                    if (i < this.habilidades.Count -1)
                    {
                        valores += ", ";
                    }

                } 
                return valores;
            }
        }
       
        //Constructor privado: solo se encarga de crear una nueva instancia de la lista y no recibe parámetros.
        private Personaje()
        {
           habilidades = new List<string>(); 
        }

        //Constructor público: recibe parámetros y se encarga de darle valores a todos los atributos MENOS A LA LISTA,
        //no olvidar llamar al constructor privado. (No poner el id en el constructor)
        public Personaje(string nombreReal, string nombrePersonaje, string lugarOrigen) 
            :this()
        {
            this.nombreReal = nombreReal;
            this.nombrePersonaje = nombrePersonaje;
            this.lugarOrigen = lugarOrigen;
        }

        //metodos
        //SetHabilidad: no retorna nada, es público y recibe un parámetro del tipo string que debe agregar a la lista.
        public void SetHabilidad(string habilidad)
        {
            habilidades.Add(habilidad);
        }

        //MostrarDescripcion: método abstracto, no visible para el exterior pero si en las clases derivadas,
        //debe retornar un string.
        protected abstract string MostrarDescripcion();


        //Ejercicio:
        //En la clase personaje: Sobreescribir el método equals() y dos personajes solo serán iguales si
        //coincide su nombreReal y nombrePersonaje.

        //Casteo: Se utiliza "as" para intentar convertir el objeto recibido a un tipo Personaje.
        //Si no es posible (por ejemplo, si se pasa un objeto de otro tipo), comparar será null.

        public override bool Equals(object? obj)
        {

            Personaje comparar = obj as Personaje;

            return comparar is not null &&
                this.nombreReal == comparar.nombreReal &&
                this.nombrePersonaje == comparar.nombrePersonaje;


            //Forma mas larga:

            //bool esIgual = false;

            //if (personaje is not null && personaje.GetType().Name == typeof(Personaje).Name) 
            //{
            //    Personaje unPersonaje = (Personaje)personaje;

            //    esIgual = this.nombreReal == unPersonaje.nombreReal &&   
            //              this.nombrePersonaje == unPersonaje.nombrePersonaje; 
            //}
            //return esIgual;
        }

        public void CargarHabilidadesDesdeString(string habilidades)
        {
            this.habilidades.AddRange(habilidades.Split(", "));
        }

    }
}
