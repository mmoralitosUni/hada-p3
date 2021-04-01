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
            Messages.Text = "Mission acomplished!<br/>Name: "+en.nombre+"<br>Nif: "+en.nif+"<br/>Edad: "+en.edad+"<br/>";
            if (Messages.Visible == false) Messages.Visible = true;
            nifTB.Text = en.nif;
            nombreTB.Text = en.nombre;
            edadTB.Text = en.edad.ToString();
        }

        private ENUsuario createUsuario() {
            if (nifTB.Text == "") throw new Exception("No has introducido el nif...");
            if (nombreTB.Text == "") throw new Exception("No has introducido el nombre...");
            if (edadTB.Text == "") throw new Exception("No has introducido la edad...");
            int ueadad = 0;
            if (int.TryParse(edadTB.Text, out ueadad) == false) throw new Exception("La edad tiene que ser un número...");
            return new ENUsuario(nombreTB.Text, nifTB.Text, ueadad);
        }

        protected void ButtonLeer_Click(object sender, EventArgs e) // leer usuario
        {
            try {
                if (nifTB.Text == "") throw new Exception("No has introducido el nif...");
                ENUsuario u = new ENUsuario(nifTB.Text,"",0); ;
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

                ENUsuario u = new ENUsuario(); // esto lo tienes que hacer siempre
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

                ENUsuario u = createUsuario(); // esto lo tienes que hacer siempre
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
                ENUsuario u = createUsuario(); // esto lo tienes que hacer siempre
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

                ENUsuario u = createUsuario(); // esto lo tienes que hacer siempre
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
                ENUsuario u = createUsuario(); // esto lo tienes que hacer siempre
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
                ENUsuario u = createUsuario(); // esto lo tienes que hacer siempre
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