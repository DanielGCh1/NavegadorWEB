using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NavegadorWEB.Forms
{
    public partial class HistorialMenu : Form
    {
        public HistorialMenu()
        {
            InitializeComponent();
            Mutex mutex = new Mutex();
            mutex.WaitOne();
            Thread.Sleep(50);
            CargarHistorial();
            Thread.Sleep(50);
            mutex.ReleaseMutex();
        }

        private void ButtonBorrarHistorial_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Desea borrar el historial de la aplicación?", "Confirmacion", MessageBoxButtons.YesNoCancel);
            if (resultado == DialogResult.Yes)
            {
                Mutex mutex = new Mutex();
                mutex.WaitOne();
                Thread.Sleep(50);
                Contenedor.ListaHistorial.Clear();
                Thread.Sleep(50);
                mutex.ReleaseMutex();
                DataGridViewInformacion.Rows.Clear();
            }
        }
        private void CargarHistorial()
        {
            for (int i = 0; i < Contenedor.ListaHistorial.Count(); i++)
            {
                DataGridViewInformacion.Rows.Add(Contenedor.ListaHistorial[i].Fecha, Contenedor.ListaHistorial[i].DirecionWEB);
            }
        }
    }
}
