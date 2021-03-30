using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;


namespace library
{
    public class CADUsuario
    {
        private string constring;
        internal SqlConnection sqlc;
        private int count(SqlConnection s, int i = -1) {
            try {
                s.Open();
                SqlCommand sc = new SqlCommand("select count(id) from Usuarios;", s);
                SqlDataReader dr = sc.ExecuteReader();
                i = dr.GetInt32(0);
            } catch (Exception e) {
                error(s, e);
                throw new Exception("Database doesn't contains data...");
            }
            finally
            {
                s.Close();
            }
            return i;
        }
        private bool find(SqlConnection s, ENUsuario en, bool i = false)
        {
            try
            {
                s.Open();
                SqlCommand sc = new SqlCommand("",s);
                sc.CommandText = "select id from Usuarios where nombre=\"" + en.nombre + "\" and nif=\"" + en.nif + "\"and edad=" + en.edad + ";";
                SqlDataReader dr = sc.ExecuteReader();
                while (dr.Read()) { i = true; }
            }
            catch (Exception e)
            {
                error(s, e);
                throw new Exception("Couldn't find the specified user...");
            }
            finally
            {
                s.Close();
            }
            return i;
        }

        /// <summary>
        /// Funcion para actualizar los valores de las tablas
        /// </summary>
        /// <param name="s"> conexion sql </param>
        /// <param name="table"> tabla objetivo </param>
        /// <param name="en"> ENUsuario que representa al usuario </param>
        /// <param name="i"> resultado de la opreacion </param>
        /// <param name="opt"> opcion a hacer {insertar,  actualizar, borrar}</param>
        /// <returns></returns>
        private bool updateTable(SqlConnection s, string table, ENUsuario en, bool i = false, short opt = 0) {
            try 
            {
                s.Open();
                SqlCommand sc = new SqlCommand("",s);
                switch (opt) {
                    case 0:
                        // crea un usuario
                        sc.CommandText = "insert into Usuarios(nombre, nif, edad) values(\"" + en.nombre + "\",\"" + en.nif + "\"," + en.edad + ")";
                        break;
                    case 1:
                        // cambia los valores de un usuario
                        sc.CommandText = "update Usuarios set nombre=" + en.nombre + ",nif=" + en.nif + ",edad=" + en.edad + " where nombre=\"" + en.nombre + "\" and nif=\"" + en.nif + "\"and edad="+en.edad + ";";
                        break;
                    case 2:
                        // borra el usuario
                        sc.CommandText = "delete from Usuarios where nombre=\"" + en.nombre + "\" and nif=\"" + en.nif + "\"and edad=" +en.edad + ";"; 
                        break;
                    default:
                        // si la lio, devuelve el usuario que se pasa por parametro
                        sc.CommandText = "select * from Usuarios where nombre=\"" + en.nombre + "\" and nif=\"" + en.nif + "\"and edad="+en.edad + ";";
                        break;
                }
                sc.ExecuteNonQuery();
                i = true;
            }
            catch(Exception e) 
            {
                error(s, e);
                throw new Exception("Couldn't finish the operations due to: " + e.Message);
            }
            finally
            {
                s.Close();
            }
            return i;
        }
        private bool error(SqlConnection s, Exception e) {
            s.Close();
            Console.WriteLine("User operation has failed. Error: {0}",e.Message);
            return false;
        }
        public CADUsuario() {
           this.constring = ConfigurationManager.ConnectionStrings["DefaultDB"].ToString();
        }
        
        public bool readUsuario(ENUsuario en) // leer usuario
        {
            bool ret = false;
            SqlCommand sc;
            SqlDataReader dr;
            try
            {
                sqlc = new SqlConnection(constring);
                sqlc.Open();

                sc = new SqlCommand("", sqlc);

                // comprueba si el usuario existe
                sc.CommandText = "select count(*) from Usuarios where nif = " + en.nif;
                dr = sc.ExecuteReader();
                if (dr.GetInt32(0) <= 0) throw new Exception("User's nif or id not found!");
                sc.CommandText = "select count(*) from Usuarios where nombre = " + en.nombre + "and nif = " + en.nif;
                dr = sc.ExecuteReader();
                if (dr.GetInt32(0) <= 0) throw new Exception("User's name not found!");
                sc.CommandText = "select * from Usuarios where nombre=\"" + en.nombre + "\" and nif=\"" + en.nif + "\"and edad=" + en.edad + ";";
                dr = sc.ExecuteReader();
                if (dr.GetInt32(0) <= 0) throw new Exception("User not found!");
                
                // si el usuario existe, lo devuelves
                sc.CommandText = "select * from Usuarios where nombre=\"" + en.nombre + "\" and nif=\"" + en.nif + "\"and edad=" + en.edad + ";";
                dr = sc.ExecuteReader();
                if (dr["id"] != null)
                {
                    en = new ENUsuario( dr["nombre"].ToString(),dr["nif"].ToString(),Convert.ToInt32(dr["edad"].ToString()));
                    ret = true;
                }
                
            }
            catch (Exception e)
            {
                ret = error(sqlc, e);
                throw new Exception(e.Message);
            }
            finally {
                sqlc.Close();
            }
            return ret;
        }
        public bool readFirstUsuario(ENUsuario en)
        {
            bool ret = false;
            SqlCommand sc;
            SqlDataReader dr;
            try
            {
                sqlc = new SqlConnection(constring);
                sqlc.Open();
                int i = -1;
                i = count(sqlc, i);
                sc = new SqlCommand("", sqlc);
                sc.CommandText = "select * from Usuarios where id == 0;";
                dr = sc.ExecuteReader();
                if (dr["id"] != null)
                {
                    en = new ENUsuario(dr["nombre"].ToString(), dr["nif"].ToString(), Convert.ToInt32(dr["edad"].ToString()));
                    ret = true;
                }
                sqlc.Close();
            }
            catch (Exception e)
            {
                ret = error(sqlc, e);
                throw new Exception(e.Message);
            }
            finally
            {
                sqlc.Close();
            }
            return ret;
        }
        public bool readNextUsuario(ENUsuario en)
        {
            bool ret = false;
            SqlCommand sc;
            SqlDataReader dr;
            try
            {
                sqlc = new SqlConnection(constring);
                sqlc.Open();
                sc = new SqlCommand("", sqlc);
                sc.CommandText = "select * from Usuarios where nombre=\"" + en.nombre + "\" and nif=\"" + en.nif + "\"and edad=" + en.edad + ";";
                dr = sc.ExecuteReader();
                if (dr["id"] != null)
                {
                    int id = Convert.ToInt32(dr["id"].ToString());
                    if (dr.Read() == true)
                    {
                        id += 1;
                        sc.CommandText = "select * from Usuario where id=" + id;
                        dr = sc.ExecuteReader();
                        en = new ENUsuario(dr["nombre"].ToString(), dr["nif"].ToString(), Convert.ToInt32(dr["edad"].ToString()));
                        ret = true;
                    }
                    else
                    {
                        ret = false; // noo quedan usuarios
                        sc.CommandText = "select * from Usuarios where id=0;";
                        dr = sc.ExecuteReader();
                        en = new ENUsuario(dr["nombre"].ToString(), dr["nif"].ToString(), Convert.ToInt32(dr["edad"].ToString()));
                        throw new Exception("No left users to read, showing first user...");
                    }
                }
            }
            catch (Exception e)
            {
                ret = error(sqlc, e);
                throw new Exception(e.Message);
            }
            finally
            {
                sqlc.Close();
            }
            return ret;
        }
        public bool readPrevUsuario(ENUsuario en)
        {
            bool ret = false;
            try
            {
                sqlc.Open();
                SqlCommand sc = new SqlCommand("", sqlc);
                sc.CommandText = "select * from Usuarios where nombre=\"" + en.nombre + "\" and nif=\"" + en.nif + "\"and edad=" + en.edad + ";";
                SqlDataReader dr = sc.ExecuteReader();
                if (dr["id"] != null)
                {
                    int id = Convert.ToInt32(dr["id"].ToString());
                    if (id > 0)
                    {
                        id -= 1;
                        sc.CommandText = "select * from Usuario where id=" + id;
                        dr = sc.ExecuteReader();
                        en = new ENUsuario(dr["nombre"].ToString(), dr["nif"].ToString(), Convert.ToInt32(dr["edad"].ToString()));
                        ret = true;
                    }
                    else
                    {
                        ret = false;
                        sc.CommandText = "select * from Usuarios where id=0;";
                        dr = sc.ExecuteReader();
                        en = new ENUsuario(dr["nombre"].ToString(), dr["nif"].ToString(), Convert.ToInt32(dr["edad"].ToString()));
                        throw new Exception("No left users to read, showing first user...");
                    }
                }
            }
            catch (Exception e)
            {
                ret = error(sqlc, e);
                throw new Exception(e.Message);
            }
            finally 
            {
                sqlc.Close();
            }
            return ret;
        }

        public bool createUsuario(ENUsuario en)
        {
            bool ret = false;
            try
            {
                sqlc = new SqlConnection(constring);
                try {
                    int total = count(sqlc);
                }catch(Exception e)
                {
                    try {
                        bool finded = find(sqlc, en);
                    }
                    catch (Exception ex) {
                        ret = updateTable(sqlc, "Usuarios", en);
                    }
                }
                if (ret == false) throw new Exception("Couldn't create user...");
            }
            catch (Exception e)
            {
                ret = error(sqlc, e);
                throw new Exception(e.Message);
            }
            return ret;
        }
        public bool updateUsuario(ENUsuario en)
        {
            bool ret = false;
            try
            {
                //if (find(sqlc, en) == false) throw new SqlNullValueException("Usuario no encontrado. Para modificar un usuario, creelo antes");
                int total = count(sqlc);
                if (total > -1)
                {
                    ret = updateTable(sqlc, "Usuarios", en, ret, 1);
                }
            }
            catch (Exception e)
            {
                ret = error(sqlc,e);
                throw new Exception(e.Message);
            }
            return ret;
        }
        public bool deleteUsuario(ENUsuario en)
        {
            bool ret = false;
            try
            {
                //if (find(sqlc, en) == false) throw new SqlNullValueException("Usuario no encontrado.");
                int total = count(sqlc);
                if (total > -1)
                {
                    ret = updateTable(sqlc, "Usuarios", en, ret, 2);
                }
            }
            catch (Exception e)
            {
                ret = error(sqlc, e);
                throw new Exception(e.Message);
            }
            return ret;
        }
    }
}
