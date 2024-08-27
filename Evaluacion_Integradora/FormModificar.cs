using MisClases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Evaluacion_Integradora
{
    public partial class FormModificar : Form
    {
        public Personaje miPersonaje;

        //propiedad
        public Personaje MiPersonaje { get => miPersonaje; set => miPersonaje = value; }

        public FormModificar()
        {
            InitializeComponent();
        }

        //constructor
        public FormModificar(Personaje personaje) : this()
        {
            this.miPersonaje = personaje;
        }

        //evento Load
        private void FormModificar_Load(object sender, EventArgs e)
        {
            #region NO MODIFICAR
            cmb_tipoPersonaje.DataSource = new List<string>() { "Heroe", "Villano" };
            this.lst_lugarOrigen.DataSource = new List<string>() { "Asgard", "Midgard", "Xandar", "Hala", "Skrullos", "Titan", "Tierra-616" };

            #endregion


            //num_id.Value = this.miPersonaje.Id;
            //textBox1.Text = this.miPersonaje.NombrePersonaje;
            //textBox2.Text = this.miPersonaje.NombreReal;
            //lst_lugarOrigen.SelectedItem = miPersonaje.LugarOrigen;


            //foreach (CheckBox cb in grp_habilidades.Controls)
            //{
            //    cb.Checked = false;

            //    if (this.miPersonaje.Habilidades.Contains(cb.Text))
            //    {
            //        cb.Checked = true;
            //    }
            //}


            //if (this.miPersonaje is Villano)
            //{
            //    Villano villano = (Villano)this.miPersonaje;
            //    rtx_planMalvado.Text = villano.PlanMalvado;
            //    cmb_tipoPersonaje.SelectedItem = "Villano";
            //}

            //if (this.miPersonaje is Heroe)
            //{
            //    Heroe heroe = (Heroe)this.miPersonaje;
            //    foreach (RadioButton rdb in grp_alianza.Controls)
            //    {
            //        if (rdb.Text == heroe.Alianza)
            //        {
            //            rdb.Checked = true;
            //            break;
            //        }
            //    }
            //    cmb_tipoPersonaje.SelectedItem = "Heroe";
            //}
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


        //boton modificar
        private void btn_modificar_Click(object sender, EventArgs e)
        {
            string nombreReal = this.textBox2.Text;
            string nombrePersonaje = this.textBox1.Text;
            string lugarOrigen = lst_lugarOrigen.SelectedItem!.ToString()!;
            int id = (int)this.num_id.Value;

            bool listaConValores = false;
            bool control = false;


            if (cmb_tipoPersonaje.SelectedItem!.ToString() == "Heroe")
            {
                string alianza = string.Empty;

                foreach (RadioButton rdb in this.grp_alianza.Controls)
                {
                    if (rdb.Checked)
                    {
                        alianza = rdb.Text;
                        break;
                    }
                }
                
                this.miPersonaje = new Heroe(nombreReal, nombrePersonaje, lugarOrigen, alianza);

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

                this.miPersonaje = new Villano(nombreReal, nombrePersonaje, lugarOrigen, planMalvado);

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
