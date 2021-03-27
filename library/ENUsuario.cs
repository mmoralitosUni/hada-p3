using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    class ENUsuario
    {
        internal string nifUsuario;
        internal string nombreUsuario;
        internal int edadUsuario;
        public string nif {
            get { return nifUsuario; }
            private set { }
        }
        public string nombre {
            get { return nombreUsuario; }
            private set { }
        }
        public int edad {
            get { return edadUsuario; }
            private set { }
        }

        public ENUsuario() {
            this.nif = "";
            this.nombre = "";
            this.edad = 0;
        }
        public ENUsuario( string nom, string nif, int edad){
            this.edad = edad;
            this.nombre = sanetize(nom);
            this.nif = sanetize(nif);
        }

        public bool createUsuario() {
            bool ret = false;
            try {
                CADUsuario c = new CADUsuario();
                ret = c.createUsuario(this);
            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }
            return ret;
        }

        public bool readUsuario() {
            bool ret = false;
            try
            {
                CADUsuario c = new CADUsuario();
                ret = c.readUsuario(this);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return ret;
        }
        public bool readFirstUsuario()
        {
            bool ret = false;
            try
            {
                CADUsuario c = new CADUsuario();
                ret = c.readFirstUsuario(this);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return ret;
        }
        public bool readNextUsuario()
        {
            bool ret = false;
            try
            {
                CADUsuario c = new CADUsuario();
                ret = c.readNextUsuario(this);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return ret;
        }
        public bool readPrevUsuario()
        {
            bool ret = false;
            try
            {
                CADUsuario c = new CADUsuario();
                ret = c.readPrevUsuario(this);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return ret;
        }
        public bool updateUsuario()
        {
            bool ret = false;
            try
            {
                CADUsuario c = new CADUsuario();
                ret = c.updateUsuario(this);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return ret;
        }
        public bool deleteUsuario()
        {
            bool ret = false;
            try
            {
                CADUsuario c = new CADUsuario();
                ret = c.deleteUsuario(this);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return ret;
        }
        public class InvalidInputException : Exception {
            internal string msg { get; private set; }
            public InvalidInputException() {}
            public InvalidInputException(string s) {
                this.msg = s;
            }
            public string GetMessage() {
                return " -- Caused at "+msg+" because cannot be used as name since its a reserved word for SQL";
            }
        }
        private string sanetize(string str)
        {
            string[] sql_code = { "\"", "\'", "DROP", "OR", "=", "TABLE", "ALTER", "INSERT", ";", "--" };    // completar
            foreach (string s in str.Split(' ')) {
                for (short i = 0; i < sql_code.Length; i++) {
                    if (s.ToUpper() == sql_code[i]) {
                        str = "";
                        throw new InvalidInputException(s);
                    }
                }
            }
            return str;
        }
    }
}
