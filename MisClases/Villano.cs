using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace MisClases
{
    public class Villano : Personaje
    {
        //planMalvado: atributo propio, es del tipo string, no debe ser visible al exterior.
        private string planMalvado;


        //Tiene un único constructor público, parametrizado y de instancia que da valores a todos los atributos.
        public Villano(int id, string nombreReal, string nombrePersonaje, string lugarOrigen, string planMalvado)
            : base (id, nombreReal, nombrePersonaje, lugarOrigen)
        {
            this.planMalvado = planMalvado;
        }
        
        public string PlanMalvado 
        { 
            get => planMalvado; 
            set => planMalvado = value; 
        }

        //MostrarDescripcion: debe implementar dicho método y retornar un string con la siguiente leyenda
        protected override string MostrarDescripcion()
        {
            return $"{nombrePersonaje} es un villano con un plan malvado de: {planMalvado}.";
        }

    }
}
