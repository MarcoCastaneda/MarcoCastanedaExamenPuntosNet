using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {


        public static ML.Result GetAll()

        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnection()))
                {
                    string query = "UsuarioGetAll";

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.StoredProcedure;

                        DataTable tableMateria = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        da.Fill(tableMateria);

                        if (tableMateria.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();
                            foreach (DataRow row in tableMateria.Rows)
                            {
                                ML.Usuario usuario = new ML.Usuario();
                                usuario.IdUsuario = int.Parse(row[0].ToString());
                                usuario.Username = row[1].ToString();
                                usuario.Nombre = row[2].ToString();
                                usuario.ApellidoPaterno = row[3].ToString();
                                usuario.ApellidoMaterno = row[4].ToString();
                                usuario.Email = row[5].ToString();
                                usuario.Password = row[6].ToString();
                                

                                result.Objects.Add(usuario);

                            }
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }




        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnection()))
                {
                    string query = "UsuarioAdd"; 
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter[] collection = new SqlParameter[6];

                        collection[0] = new SqlParameter("Nombre", SqlDbType.VarChar);
                        collection[0].Value = usuario.Nombre;
                        
                        collection[1] = new SqlParameter("UserName", SqlDbType.VarChar);
                        collection[1].Value = usuario.Username;


                        collection[2] = new SqlParameter("ApellidoPaterno", SqlDbType.VarChar);
                        collection[2].Value = usuario.ApellidoPaterno;


                        collection[3] = new SqlParameter("ApellidoMaterno", SqlDbType.VarChar);
                        collection[3].Value = usuario.ApellidoMaterno;

                        collection[4] = new SqlParameter("Email", SqlDbType.VarChar);
                        collection[4].Value = usuario.Email;

                        collection[5] = new SqlParameter("Password", SqlDbType.VarChar);
                        collection[5].Value = usuario.Password;

                        cmd.Parameters.AddRange(collection);
                        cmd.Connection.Open();
                        int RowsAffected = cmd.ExecuteNonQuery();

                        if (RowsAffected > 0)
                        {
                            result.Correct = true;
                        }

                        else
                        {
                            result.Correct = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }


        public static ML.Result GetById(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnection()))
                {
                    string query = "UsuarioGetById";

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter[] collection = new SqlParameter[1];

                        collection[0] = new SqlParameter("IdUsuario", SqlDbType.Int);
                        collection[0].Value = IdUsuario;

                        cmd.Parameters.AddRange(collection);
                        cmd.Connection.Open();

                        DataTable tablaUsuario = new DataTable();

                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        da.Fill(tablaUsuario);

                        if (tablaUsuario.Rows.Count > 0)
                        {
                            DataRow row = tablaUsuario.Rows[0];

                            ML.Usuario usuario = new ML.Usuario();
                            usuario.IdUsuario = int.Parse(row[0].ToString());
                            usuario.Username = row[1].ToString();
                            usuario.Nombre = row[2].ToString();
                            usuario.ApellidoPaterno = row[3].ToString();
                            usuario.ApellidoMaterno = row[4].ToString();
                            usuario.Email = row[5].ToString();
                            usuario.Password = row[6].ToString();


                            result.Object = usuario;
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result GetByUsername(string email)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnection()))
                {
                    string query = "UsuarioGetByEmail";

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter[] collection = new SqlParameter[1];

                        collection[0] = new SqlParameter("Email", SqlDbType.VarChar);
                        collection[0].Value = email;

                        cmd.Parameters.AddRange(collection);
                        cmd.Connection.Open();

                        DataTable tablaUsuario = new DataTable();

                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        da.Fill(tablaUsuario);

                        if (tablaUsuario.Rows.Count > 0)
                        {
                            DataRow row = tablaUsuario.Rows[0];

                            ML.Usuario usuario = new ML.Usuario();
                           
                            usuario.Email = row[0].ToString();
                            usuario.Password = row[1].ToString();
                            usuario.IdUsuario = int.Parse(row[2].ToString());


                            result.Object = usuario;
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result Delete(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnection()))
                {
                    String query = "UsuarioDelete";

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = query;
                    cmd.Connection = context;
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] collection = new SqlParameter[1];
                    collection[0] = new SqlParameter("@IdUsuario", SqlDbType.Int);
                    collection[0].Value = usuario.IdUsuario;

                    cmd.Parameters.AddRange(collection);
                    cmd.Connection.Open();

                    int RowsAffected = cmd.ExecuteNonQuery();
                    if (RowsAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {

                        result.Correct = false;
                    }





                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
        public static ML.Result Update(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnection()))
                {
                    String query = "UsuarioUpdate";
                    SqlCommand cmd = new SqlCommand();
                    
                        cmd.Connection = context;
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter[] collection = new SqlParameter[6];

                        collection[0] = new SqlParameter("@IdUsuario", SqlDbType.Int);
                        collection[0].Value = usuario.IdUsuario;

                        collection[1] = new SqlParameter("Nombre", SqlDbType.VarChar);
                        collection[1].Value = usuario.Nombre;

                        collection[2] = new SqlParameter("ApellidoPaterno", SqlDbType.VarChar);
                        collection[2].Value = usuario.ApellidoPaterno;

                        collection[3] = new SqlParameter("ApellidoMaterno", SqlDbType.VarChar);
                        collection[3].Value = usuario.ApellidoMaterno;

                        collection[4] = new SqlParameter("Email", SqlDbType.VarChar);
                        collection[4].Value = usuario.Email;

                        collection[5] = new SqlParameter("Password", SqlDbType.VarChar);
                        collection[5].Value = usuario.Password;

                    cmd.Parameters.AddRange(collection);

                        cmd.Connection.Open();

                        int RowsAffected = cmd.ExecuteNonQuery();

                        cmd.Connection.Close();

                        if (RowsAffected > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                        }


                    
                }
            }

            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        public static ML.Result UpdatePassword(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnection()))
                {
                    String query = "PasswordUpdate";
                    SqlCommand cmd = new SqlCommand();

                    cmd.Connection = context;
                    cmd.CommandText = query;
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] collection = new SqlParameter[2];

                    collection[0] = new SqlParameter("@IdUsuario", SqlDbType.Int);
                    collection[0].Value = usuario.IdUsuario;

                    collection[1] = new SqlParameter("Password", SqlDbType.VarChar);
                    collection[1].Value = usuario.Password;

                    cmd.Parameters.AddRange(collection);

                    cmd.Connection.Open();

                    int RowsAffected = cmd.ExecuteNonQuery();

                    cmd.Connection.Close();

                    if (RowsAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }



                }
            }

            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }



    }
}