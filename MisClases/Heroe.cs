using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace MisClases
{
    public class Heroe : Personaje
    {
        private string alianza;


        //Tiene un único constructor público, parametrizado y de instancia que da valores a todos los atributos.
        public Heroe(string nombreReal, string nombrePersonaje, string lugarOrigen, string alianza)
            : base (nombreReal, nombrePersonaje, lugarOrigen) //Llamo al constructor de la clase padre
        {
            this.alianza = alianza;
        }

        //Property
        public string Alianza
        {
            get => alianza;
            set => alianza = value;
        }

        //MostrarDescripcion: debe implementar dicho método y retornar un string con la siguiente leyenda
        protected override string MostrarDescripcion()
        {
            return $"{nombrePersonaje} es un héroe de la alianza { Alianza }.";
        }
        
      


    }
}
