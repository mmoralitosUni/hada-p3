using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using library;

namespace usuWeb
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Messages.Visible = false;
        }

        private void send_msg(Exception msg) {
            Messages.Text = msg.Message;
            if (Messages.Visible == false) Messages.Visible = true;
        }
        private void send_msg(ENUsuario en)
        {
            Messages.Text = "Mission acomplished";
            if (Messages.Visible == false) Messages.Visible = true;
            nifTB.Text = en.nif;
            nombreTB.Text = en.nombre;
            edadTB.Text = en.edad.ToString();
        }

        protected void ButtonLeer_Click(object sender, EventArgs e) // leer usuario
        {
            try {
                
                ENUsuario u = new ENUsuario(nombreTB.Text, nifTB.Text, Convert.ToInt32(edadTB.Text)); // esto lo tienes que hacer siempre
                u.readUsuario();
                send_msg(u);
            }
            catch (Exception ex) {
                send_msg(ex);
            }
        }

        protected void ButtonLeerPrimero_Click(object sender, EventArgs e)
        {
            try
            {

                ENUsuario u = new ENUsuario(nombreTB.Text, nifTB.Text, Convert.ToInt32(edadTB.Text)); // esto lo tienes que hacer siempre
                u.readFirstUsuario();
                send_msg(u);
            }
            catch (Exception ex)
            {
                send_msg(ex);
            }
        }

        protected void ButtonLeerAnterior_Click(object sender, EventArgs e)
        {
            try
            {

                ENUsuario u = new ENUsuario(nombreTB.Text, nifTB.Text, Convert.ToInt32(edadTB.Text)); // esto lo tienes que hacer siempre
                u.readPrevUsuario();
                send_msg(u);
            }
            catch (Exception ex)
            {
                send_msg(ex);
            }
        }

        protected void ButtonLeerSiguiente_Click(object sender, EventArgs e)
        {
            try
            {

                ENUsuario u = new ENUsuario(nombreTB.Text, nifTB.Text, Convert.ToInt32(edadTB.Text)); // esto lo tienes que hacer siempre
                u.readNextUsuario();
                send_msg(u);
            }
            catch (Exception ex)
            {
                send_msg(ex);
            }
        }

        protected void ButtonCrear_Click(object sender, EventArgs e)
        {
            try
            {

                ENUsuario u = new ENUsuario(nombreTB.Text, nifTB.Text, Convert.ToInt32(edadTB.Text)); // esto lo tienes que hacer siempre
                u.createUsuario();
                send_msg(u);
            }
            catch (Exception ex)
            {
                send_msg(ex);
            }
            
        }

        protected void ButtonActualizar_Click(object sender, EventArgs e)
        {
            try
            {

                ENUsuario u = new ENUsuario(nombreTB.Text, nifTB.Text, Convert.ToInt32(edadTB.Text)); // esto lo tienes que hacer siempre
                u.updateUsuario();
                send_msg(u);
            }
            catch (Exception ex)
            {
                send_msg(ex);
            }
        }

        protected void ButtonBorrar_Click(object sender, EventArgs e)
        {
            try
            {

                ENUsuario u = new ENUsuario(nombreTB.Text, nifTB.Text, Convert.ToInt32(edadTB.Text)); // esto lo tienes que hacer siempre
                u.deleteUsuario();
                send_msg(u);
            }
            catch (Exception ex)
            {
                send_msg(ex);
            }
        }
    }
}