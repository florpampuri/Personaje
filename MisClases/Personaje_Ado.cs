using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MisClases
{
    //Conexion con la base de datos

    public static class Personaje_Ado
    {
        private static string connectionString;
        private static MySqlConnection connection;
        private static MySqlCommand command;

        //connectionstring.com/mysql --> para ver que necesitamos para realizar las conexiones

        static Personaje_Ado()
        {
            connectionString = "Server=localhost;Database=personajes;Uid=root;Pwd=;";

            connection = new MySqlConnection(connectionString);

            command = new MySqlCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.Connection = connection;

        }


        //Manejo de excepciones usando Try/Catch/Finally

        //Metodo Guardar
        public static bool Guardar(Personaje pj)
        {
            bool exito = true;

            try
            {
                string planMalvado = string.Empty;
                string alianza = string.Empty;

                if (pj.GetType().Name == typeof(Heroe).Name)
                {
                    alianza = ((Heroe)pj).Alianza;
                }
                else
                {
                    planMalvado = ((Villano)pj).PlanMalvado;
                }

                OpenConnection("INSERT INTO personajes " +
                    "(NombreReal, NombrePersonaje, LugarOrigen, Habilidades, Alianza, PlanMalvado) " +
                    "VALUES (@NombreReal, @NombrePersonaje, @LugarOrigen, @Habilidades, @alianza, @planMalvado)");
                
                command.Parameters.AddWithValue("@NombreReal", pj.NombreReal);
                command.Parameters.AddWithValue("@NombrePersonaje", pj.NombrePersonaje);
                command.Parameters.AddWithValue("@LugarOrigen", pj.LugarOrigen);
                command.Parameters.AddWithValue("@Habilidades", pj.Habilidades);
                command.Parameters.AddWithValue("@alianza", alianza);
                command.Parameters.AddWithValue("@planMalvado", planMalvado);

                command.ExecuteNonQuery();

            }
            catch (Exception)
            {
                exito = false;
            }
            finally
            {
                CloseConnection();
            }

            return exito;
        }


        //Crear una nueva instancia de los objetos por cada fila recibida en mysql 

        //Metodo Leer Todos
        public static List<Personaje> LeerTodos()
        {
            List<Personaje> list = new List<Personaje>();

            try
            {
                string query = "SELECT * FROM personajes";

                connection.Open();
                command.CommandText = query;

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read()) //Se puede usar cualquiera de estas opciones:
                    {
                        int Id = reader.GetInt32(0);
                        string NombreReal = reader["NombreReal"].ToString();
                        string NombrePersonaje = reader["NombrePersonaje"].ToString();
                        string LugarOrigen = reader.GetString(3); //Se encuentra en la posicion 3 de la tabla

                        string Habilidades = string.Empty;
                        string Alianza = string.Empty;
                        string PlanMalvado = string.Empty;

                        //Si el elemento puede llegar a ser nulo hay que hacer lo siguiente:
                        if (!reader.IsDBNull(reader.GetOrdinal("Habilidades")))
                        {
                            Habilidades = reader.GetString(4); ;
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("Alianza")))
                        {
                            Alianza = reader.GetString(5); ;
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("PlanMalvado")))
                        {
                            PlanMalvado = reader.GetString(6); ;
                        }

                        if (string.IsNullOrEmpty(Alianza))
                        {
                            Villano v = new Villano(NombreReal, NombrePersonaje, LugarOrigen, PlanMalvado);
                            v.Id = Id;
                            v.CargarHabilidadesDesdeString(Habilidades);
                            list.Add(v);
                        }
                        else
                        {
                            Heroe h = new Heroe(NombreReal, NombrePersonaje, LugarOrigen, Alianza);
                            h.Id = Id;
                            h.CargarHabilidadesDesdeString(Habilidades);
                            list.Add(h);
                        }
                    }
                }

            }
            catch (Exception)
            {
                list = null;
            }
            finally
            {
                if (connection is not null && connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return list;

        }


        //Metodo Leer uno solo
        public static Personaje LeerUnoSolo(int idBuscar)
        {
            Personaje pj = null;
            string query = string.Empty;

            try
            {
                query = "SELECT * FROM personajes WHERE id = @personajeBuscado";

                connection.Open();
                command.CommandText = query;

                command.Parameters.Clear();
                command.Parameters.AddWithValue("personajeBuscado", idBuscar);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    reader.Read(); //Importante

                    int Id = reader.GetInt32(0);
                    string NombreReal = reader["NombreReal"].ToString();
                    string NombrePersonaje = reader["NombrePersonaje"].ToString();
                    string LugarOrigen = reader.GetString(3); //Se encuentra en la posicion 3 de la tabla

                    string Habilidades = string.Empty;
                    string Alianza = string.Empty;
                    string PlanMalvado = string.Empty;

                    //Si el elemento puede llegar a ser nulo hay que hacer lo siguiente:
                    if (!reader.IsDBNull(reader.GetOrdinal("Habilidades")))
                    {
                        Habilidades = reader.GetString(4); ;
                    }

                    if (!reader.IsDBNull(reader.GetOrdinal("Alianza")))
                    {
                        Alianza = reader.GetString(5); ;
                    }

                    if (!reader.IsDBNull(reader.GetOrdinal("PlanMalvado")))
                    {
                        PlanMalvado = reader.GetString(6); ;
                    }

                    if (string.IsNullOrEmpty(Alianza))
                    {
                        Villano v = new Villano(NombreReal, NombrePersonaje, LugarOrigen, PlanMalvado);
                        v.Id = Id;
                        v.CargarHabilidadesDesdeString(Habilidades);
                        pj = v;
                    }
                    else
                    {
                        Heroe h = new Heroe(NombreReal, NombrePersonaje, LugarOrigen, Alianza);
                        h.Id = Id;
                        h.CargarHabilidadesDesdeString(Habilidades);
                        pj = h;
                    }
                }
            }
            catch (Exception)
            {
                pj = null;
            }
            finally
            {
                if (connection is not null && connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return pj;

        }


        //Metodo Eliminar
        public static bool EliminarUno(int idEliminar)
        {
            bool exito = true;

            try
            {
                OpenConnection("DELETE FROM personajes WHERE id = @personajeBuscado");
                command.Parameters.AddWithValue("@personajeBuscado", idEliminar);
                command.ExecuteNonQuery();

            }
            catch (Exception)
            {
                exito = false;
            }
            finally
            {
                CloseConnection();
            }
            return exito;
        }


        //Metodo Modificar 
        public static bool Modificar(Personaje pjModificar)
        {
            bool exito = true;

            try
            {
                string planMalvado = string.Empty;
                string alianza = string.Empty;

                if (pjModificar.GetType().Name == typeof(Heroe).Name)
                {
                    alianza = ((Heroe)pjModificar).Alianza;
                }
                else
                {
                    planMalvado = ((Villano)pjModificar).PlanMalvado;
                }


                OpenConnection("UPDATE personajes SET NombreReal = @NombreReal," +
                "NombrePersonaje = @NombrePersonaje, LugarOrigen = @LugarOrigen, Habilidades = @Habilidades," +
                "Alianza = @Alianza, PlanMalvado = @PlanMalvado WHERE Id = @Id");

                command.Parameters.AddWithValue("@Id", pjModificar.Id);
                command.Parameters.AddWithValue("@NombreReal", pjModificar.NombreReal);
                command.Parameters.AddWithValue("@NombrePersonaje", pjModificar.NombrePersonaje);
                command.Parameters.AddWithValue("@LugarOrigen", pjModificar.LugarOrigen);
                command.Parameters.AddWithValue("@Habilidades", pjModificar.Habilidades);
                command.Parameters.AddWithValue("@alianza", alianza);
                command.Parameters.AddWithValue("@planMalvado", planMalvado);

                command.ExecuteNonQuery();

            }
            catch (Exception)
            {
                exito = false;
            }
            finally
            {
                CloseConnection();
            }
            return exito;
        }

        
        //Metodos para abrir y cerrar conexion
        private static void OpenConnection(string query)
        {
            command.Connection.Open();
            command.CommandText = query;

            command.Parameters.Clear();

        }

        private static void CloseConnection()
        {
            if (connection is not null && connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }


    }
}
