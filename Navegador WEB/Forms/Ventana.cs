using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using NavegadorWEB.Forms;
using NavegadorWEB.Clases;

namespace NavegadorWEB.Forms
{
    public partial class Ventana : Form
    {
        public Ventana()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

            Thread actualizarVentanaConstantemente = new Thread(EsperaQueSeTermineDeCargarLaVentana);
            actualizarVentanaConstantemente.SetApartmentState(ApartmentState.STA);
            actualizarVentanaConstantemente.Start();
        }
        public Thread HiloPestanna
        {
            get;
            set;
        }

        private void ToolStripButtonAtras_Click(object sender, EventArgs e)
        {
            WebBrowserPrincipal.GoBack();
        }

        private void ToolStripButtonAdelante_Click(object sender, EventArgs e)
        {
            WebBrowserPrincipal.GoForward();
        }

        private void ToolStripButtonRecargar_Click(object sender, EventArgs e)
        {
            WebBrowserPrincipal.Refresh();
        }

        private void ToolStripButtonBuscar_Click(object sender, EventArgs e)
        {
            HacerBuesqueda();
        }

        private void ToolStripButtonNuevaPestanna_Click(object sender, EventArgs e)
        {
            Mutex mutex = new Mutex();
            mutex.WaitOne();
            Thread.Sleep(50);
            Contenedor.AbrirPestanna = true;
            Thread.Sleep(50);
            mutex.ReleaseMutex();
        }
        private void Inicio_Load(object sender, EventArgs e)
        {
            //Esta linea permite ejecutar varios hilos diferentes al hilo principal
            CheckForIllegalCrossThreadCalls = false;
        }
        private void ToolStripButtonCerrar_Click(object sender, EventArgs e)
        {
            Mutex mutex = new Mutex();
            mutex.WaitOne();
            Thread.Sleep(50);
            Contenedor.CerrarPestanna = true;
            WebBrowserPrincipal.Dispose();
            HiloPestanna.Abort();
            Thread.Sleep(50);
            mutex.ReleaseMutex();
        }

        private void ToolStripMenuItemHistorial_Click(object sender, EventArgs e)
        {
            Mutex mutex = new Mutex();
            mutex.WaitOne();
            Thread.Sleep(50);
            Contenedor.AbrirHistorial = true;
            Thread.Sleep(50);
            mutex.ReleaseMutex();
        }

        private void ToolStripMenuItemCache_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Desea borrar el cache de la aplicación?", "Confirmacion", MessageBoxButtons.YesNoCancel);
            if (resultado == DialogResult.Yes)
            {

            }
        }
        private void HacerBuesqueda()
        {
            if (ToolStripTextBoxURL.TextLength > 2)
            {
                try
                {
                    WebBrowserPrincipal.Navigate(ToolStripTextBoxURL.Text);
                    Mutex mutex = new Mutex();
                    mutex.WaitOne();
                    Thread.Sleep(50);
                    Contenedor.Busqueda = ToolStripTextBoxURL.Text;
                    Contenedor.BusquedaEcha = true;
                    Thread.Sleep(50);
                    mutex.ReleaseMutex();
                }
                catch (Exception ex)
                {

                }
            }
        }
        private void EsperaQueSeTermineDeCargarLaVentana()
        {
            Thread.Sleep(4000);
            ToolStripTextBoxURL.Text = Contenedor.Busqueda;
            HacerBuesqueda();
            Thread.CurrentThread.Abort();
        }
    }
}
