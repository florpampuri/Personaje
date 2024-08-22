
using MisClases;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using System.Windows.Forms;

namespace Evaluacion_Integradora
{
    public partial class FormPrincipal : Form
    {
        //atributos // iniciamos lista de personajes
        private List<Personaje> misPersonajes;

        public FormPrincipal()
        {
            InitializeComponent();
        }

        //precarga de lista de personajes vacia
        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            this.misPersonajes = new List<Personaje>();
        }

        private void btn_agregar_Click(object sender, EventArgs e)
        {
            FormCrear formCrear = new FormCrear();
            formCrear.ShowDialog();

            if (formCrear.DialogResult == DialogResult.OK)
            {
                misPersonajes.Add(formCrear.miPersonaje);
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = misPersonajes;
            }
        }

        private void btn_modificar_Click(object sender, EventArgs e)
        {
            Personaje pjEditar = dataGridView1.CurrentRow.DataBoundItem as Personaje;
            FormModificar formModificar = new FormModificar(pjEditar);

            formModificar.ShowDialog();

            if (formModificar.DialogResult == DialogResult.OK)
            {
                int index = -1;

                foreach (Personaje item in misPersonajes)
                {
                    if (item.Id == formModificar.MiPersonaje.Id)
                    {
                        index = misPersonajes.IndexOf(item);
                    }
                }

                if (index != -1)
                {
                    misPersonajes[index] = formModificar.MiPersonaje;
                }

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = misPersonajes;
            }
        }

        private void btn_eliminar_Click(object sender, EventArgs e)
        {
            Personaje pjEliminar = dataGridView1.CurrentRow.DataBoundItem as Personaje;

            DialogResult rpta = MessageBox.Show($"¿Está seguro de que desea eliminar el Personaje {pjEliminar.NombreReal}?\n" +
                $"ESTA ACCION ES IRREVERSIBLE", "ELIMINAR", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            //int index = -1;

            if (rpta == DialogResult.OK)
            {
                misPersonajes.Remove(pjEliminar);


                ////Si no modificamos el metodo Equals:

                //foreach (Personaje item in misPersonajes)
                //{
                //   if (item.NombreReal == pjEliminar.NombreReal)
                //   {
                //        index = misPersonajes.IndexOf(item);
                //   }
                //}

                //misPersonajes.RemoveAt(index);


                dataGridView1.DataSource = null;
                dataGridView1.DataSource = misPersonajes;
            }
        }
    }
}
