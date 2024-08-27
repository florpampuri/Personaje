
using MisClases;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using System.Windows.Forms;

namespace Evaluacion_Integradora
{
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InitializeComponent();
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            dataGridView1.RowHeadersVisible = false;
            ActualizarDataGrid();
        }

        private void btn_agregar_Click(object sender, EventArgs e)
        {
            FormCrear formCrear = new FormCrear();
            formCrear.ShowDialog();

            if (formCrear.DialogResult == DialogResult.OK)
            {
                Personaje_Ado.Guardar(formCrear.miPersonaje);
                ActualizarDataGrid();
            }
        }

        private void btn_eliminar_Click(object sender, EventArgs e)
        {
            Personaje pjEliminar = dataGridView1.CurrentRow.DataBoundItem as Personaje;

            DialogResult rpta = MessageBox.Show($"¿Está seguro de que desea eliminar el Personaje {pjEliminar.NombreReal}?\n" +
                $"ESTA ACCION ES IRREVERSIBLE", "ELIMINAR", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (rpta == DialogResult.OK)
            {
                if (Personaje_Ado.EliminarUno(pjEliminar.Id))
                {
                    MessageBox.Show($"Se eliminó el Personaje {pjEliminar.NombreReal}", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Falló la conexion con la base de datos");
                }
            }

            ActualizarDataGrid();
        }

        private void btn_modificar_Click(object sender, EventArgs e)
        {
            Personaje pjModificar = dataGridView1.CurrentRow.DataBoundItem as Personaje;

            FormModificar formModificar = new FormModificar();
            formModificar.ShowDialog();

            if (formModificar.DialogResult == DialogResult.OK)
            {
                Personaje_Ado.Modificar(formModificar.miPersonaje);
                ActualizarDataGrid();
            }
        }

        
        //Metodo para actualizar el data grid con la base de datos
        private void ActualizarDataGrid()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = Personaje_Ado.LeerTodos();
        }
    }  
}
