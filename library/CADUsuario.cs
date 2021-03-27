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
    class CADUsuario
    {
        private string constring = ConfigurationManager.ConnectionStrings["DefaultDB"].ToString();
        SqlConnection sqlc;
        private int count(SqlConnection s, int i = -1) {
            try {
                s.Open();
                SqlCommand sc = new SqlCommand("select count(id) from Usuarios;", s);
                SqlDataReader dr = sc.ExecuteReader();
                i = dr.GetInt32(0);
                s.Close();
            } catch (Exception e) {
                error(s, e);
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
                s.Close();
            }
            catch (Exception e)
            {
                error(s, e);
            }
            return i;
        }
        private bool updateTable(SqlConnection s, string table, ENUsuario en, bool i = false, short opt = 0) {
            try 
            {
                s.Open();
                SqlCommand sc = new SqlCommand("",s);
                switch (opt) {
                    case 0:
                        sc.CommandText = "insert into Usuarios(nombre, nif, edad) values(\"" + en.nombre + "\",\"" + en.nif + "\"," + en.edad + ")";
                        break;
                    case 1:
                        sc.CommandText = "update Usuarios set nombre=" + en.nombre + ",nif=" + en.nif + ",edad=" + en.edad + " where nombre=\"" + en.nombre + "\" and nif=\"" + en.nif + "\"and edad="+en.edad + ";";
                        break;
                    case 2:
                        sc.CommandText = "delete from Usuarios where nombre=\"" + en.nombre + "\" and nif=\"" + en.nif + "\"and edad=" +en.edad + ";"; 
                        break;
                    default:
                        sc.CommandText = "select * from Usuarios where nombre=\"" + en.nombre + "\" and nif=\"" + en.nif + "\"and edad="+en.edad + ";";
                        break;
                }
                sc.ExecuteNonQuery();
                i = true;
                s.Close();
            }
            catch(Exception e) 
            {
                error(s, e);
            }
            return i;
        }
        private bool error(SqlConnection s, Exception e) {
            s.Close();
            Console.WriteLine("User operation has failed. Error: {0}",e.Message);
            return false;
        }
        public CADUsuario() {
            sqlc = new SqlConnection(constring);
        }
        public bool createUsuario(ENUsuario en) {
            bool ret = false;
            try{
                int total = count(sqlc);
                if ((total == -1) ^ (find(sqlc, en) == false))
                {
                    ret = updateTable(sqlc, "Usuarios", en);
                }
            }
            catch(Exception e){
                ret = error(sqlc, e);
            }
            return ret;
        }
        public bool readUsuario(ENUsuario en)
        {
            bool ret = false;
            try
            {
                sqlc.Open();
                SqlCommand sc = new SqlCommand("", sqlc);
                sc.CommandText = "select * from Usuarios where nombre=\"" + en.nombre + "\" and nif=\"" + en.nif + "\"and edad=" + en.edad + ";";
                SqlDataReader dr = sc.ExecuteReader();
                if (dr["id"] != null) {
                    en.nombre = dr["nombre"]; en.nif = dr["nif"]; en.edad = dr["edad"];
                    ret = true;
                }
                sqlc.Close();
            }
            catch (Exception e)
            {
               ret = error(sqlc, e);
            }
            return ret;
        }
        public bool readFirstUsuario(ENUsuario en)
        {
            bool ret = false;
            try
            {
                sqlc.Open();
                SqlCommand sc = new SqlCommand("", sqlc);
                sc.CommandText = "select * from Usuarios where id<>null and id == 0;";
                SqlDataReader dr = sc.ExecuteReader();
                if (dr["id"] != null)
                {
                    en.nombre = dr["nombre"]; en.nif = dr["nif"]; en.edad = dr["edad"];
                    ret = true;
                }
                sqlc.Close();
            }
            catch (Exception e)
            {
                ret = error(sqlc, e);
            }
            return ret;
        }
        public bool readNextUsuario(ENUsuario en)
        {
            bool ret = false;
            try
            {
                sqlc.Open();
                SqlCommand sc = new SqlCommand("", sqlc);
                sc.CommandText = "select * from Usuarios where nombre=\"" + en.nombre + "\" and nif=\"" + en.nif + "\"and edad=" + en.edad + ";";
                SqlDataReader dr = sc.ExecuteReader();
                if (dr["id"]!=null)
                {
                    int id = Convert.ToInt32(dr["id"].ToString());
                    if (dr.Read() == true) {
                        id += 1;
                        sc.CommandText = "select * from Usuario where id=" + id;
                        dr = sc.ExecuteReader();
                        en.nombre = dr["nombre"]; en.nif = dr["nif"]; en.edad = Convert.ToInt32(dr["edad"].ToString());
                        ret = true;
                    }
                    else {
                        ret = false; // noo quedan usuarios
                    }
                }
                sqlc.Close();
            }
            catch (Exception e)
            {
                ret = error(sqlc, e);
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
                    if (id>0)
                    {
                        id -= 1;
                        sc.CommandText = "select * from Usuario where id=" + id;
                        dr = sc.ExecuteReader();
                        en.nombre = dr["nombre"]; en.nif = dr["nif"]; en.edad = Convert.ToInt32(dr["edad"].ToString());
                        ret = true;
                    }
                    else
                    {
                        ret = false; // noo quedan usuarios
                    }
                }
                sqlc.Close();
            }
            catch (Exception e)
            {
                ret = error(sqlc, e);
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
            }
            return ret;
        }
    }
}
