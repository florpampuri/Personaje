using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

using MisClases;
using System.Text.RegularExpressions;

namespace Evaluacion_Integradora
{
    public partial class FormCrear : Form
    {
        public Personaje miPersonaje;
       
        public FormCrear()
        {
            InitializeComponent();
        }

        //propiedad
        public Personaje MiPersonaje { get => miPersonaje; }

        //evento load
        private void FormCrear_Load(object sender, EventArgs e)
        {
            #region NO MODIFICAR

            cmb_tipoPersonaje.DataSource = new List<string>() { "Heroe", "Villano" };
            this.lst_lugarOrigen.DataSource = new List<string>() { "Asgard", "Midgard", "Xandar", "Hala", "Skrullos", "Titan", "Tierra-616" };


            #endregion
        }

        //evento que cambia entre "alianza" y "plan malvado"
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region NO MODIFICAR

            if (cmb_tipoPersonaje.SelectedItem!.ToString() == "Heroe")
            {
                grp_alianza.Enabled = true;
                grp_planMalvado.Enabled = false;
            }
            else if (cmb_tipoPersonaje.SelectedItem.ToString() == "Villano")
            {
                grp_alianza.Enabled = false;
                grp_planMalvado.Enabled = true;
            }

            #endregion
        }


        //boton crear
        private void btn_Crear_Click(object sender, EventArgs e)
        {
            string nombreReal = this.textBox2.Text;
            string nombrePersonaje = this.textBox1.Text;
            string lugarOrigen = lst_lugarOrigen.SelectedItem!.ToString()!;

            bool listaConValores = false;
            bool control = false;
            int id = (int)this.num_id.Value;


            if (cmb_tipoPersonaje.SelectedItem!.ToString() == "Heroe")
            {
                string alianza = string.Empty;

                foreach (RadioButton rdb in grp_alianza.Controls)
                {
                    if (rdb.Checked)
                    {
                        alianza = rdb.Text;
                        break;
                    }
                }

                this.miPersonaje = new Heroe(id, nombreReal, nombrePersonaje, lugarOrigen, alianza);

                foreach (CheckBox item in grp_habilidades.Controls)
                {
                    if (item.Checked)
                    {
                        this.miPersonaje.SetHabilidad(item.Text);
                        listaConValores = true;
                    }
                }

                if (!string.IsNullOrEmpty(nombreReal) && !string.IsNullOrEmpty(nombrePersonaje) &&
                !string.IsNullOrEmpty(lugarOrigen) && !string.IsNullOrEmpty(alianza) && listaConValores)
                {
                    control = true;
                }

            }
            else if (cmb_tipoPersonaje.SelectedItem.ToString() == "Villano")
            {
                string planMalvado = rtx_planMalvado.Text;

                this.miPersonaje = new Villano(id, nombreReal, nombrePersonaje, lugarOrigen, planMalvado);

                foreach (CheckBox item in grp_habilidades.Controls)
                {
                    if (item.Checked) 
                    {
                        this.miPersonaje.SetHabilidad(item.Text);
                        listaConValores = true;
                    }
                }

                if (!string.IsNullOrEmpty(nombreReal) && !string.IsNullOrEmpty(nombrePersonaje) &&
                !string.IsNullOrEmpty(lugarOrigen) && !string.IsNullOrEmpty(planMalvado) && listaConValores)
                {
                    control = true;
                }
            }
           
            if (control)
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Debe seleccionar al menos un elemento de cada categoria", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //El botón cancelar debe finalizar la ejecución del FormCrear.
        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

    }
}
