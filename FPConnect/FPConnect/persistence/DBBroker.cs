using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using MySql.Data.MySqlClient;
using System.Data;
using System.Collections.Generic;

namespace FPConnect.persitence
{
    public class DBBroker
    {
        private static DBBroker _instancia;
        private static MySqlConnection conexion;
        private const String cadenaConexion = "server=localhost;database=mydb;uid=root;pwd=toor";

        // Constructor privado - patrón Singleton
        private DBBroker()
        {
            DBBroker.conexion = new MySqlConnection(DBBroker.cadenaConexion);
        }

        // Método para obtener la instancia única
        public static DBBroker obtenerAgente()
        {
            if (DBBroker._instancia == null)
            {
                DBBroker._instancia = new DBBroker();
            }
            return DBBroker._instancia;
        }

        // Método de lectura que devuelve una ObservableCollection
        public ObservableCollection<ObservableCollection<object>> leer(String sql)
        {
            ObservableCollection<ObservableCollection<object>> resultado = new ObservableCollection<ObservableCollection<object>>();
            ObservableCollection<object> fila;
            int i;
            MySqlDataReader reader;
            MySqlCommand com = new MySqlCommand(sql, DBBroker.conexion);

            conectar();
            reader = com.ExecuteReader();

            while (reader.Read())
            {
                fila = new ObservableCollection<object>();
                for (i = 0; i < reader.FieldCount; i++)
                {
                    // Convertir DBNull a null o el valor adecuado
                    fila.Add(reader[i] == DBNull.Value ? null : reader[i].ToString());
                }
                resultado.Add(fila);
            }

            desconectar();
            return resultado;
        }

        // Sobrecarga para obtener resultados tipados
        public ObservableCollection<T> leer<T>(string sql, Func<ObservableCollection<object>, T> convertidor)
        {
            ObservableCollection<T> resultadoTipado = new ObservableCollection<T>();
            ObservableCollection<ObservableCollection<object>> resultadoRaw = leer(sql);

            foreach (var fila in resultadoRaw)
            {
                resultadoTipado.Add(convertidor(fila));
            }

            return resultadoTipado;
        }

        // Método para ejecutar operaciones de modificación (INSERT, UPDATE, DELETE)
        public int modificar(String sql)
        {
            MySqlCommand com = new MySqlCommand(sql, DBBroker.conexion);
            int resultado;

            conectar();
            resultado = com.ExecuteNonQuery();
            desconectar();

            return resultado;
        }

        // Método para ejecutar operaciones con parámetros (previene SQL Injection)
        public int modificarConParametros(string sql, Dictionary<string, object> parametros)
        {
            MySqlCommand com = new MySqlCommand(sql, DBBroker.conexion);

            foreach (var param in parametros)
            {
                com.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
            }

            int resultado;
            conectar();
            resultado = com.ExecuteNonQuery();
            desconectar();

            return resultado;
        }

        // Método para obtener un valor único
        public object obtenerValorUnico(string sql)
        {
            MySqlCommand com = new MySqlCommand(sql, DBBroker.conexion);
            object resultado;

            conectar();
            resultado = com.ExecuteScalar();
            desconectar();

            return resultado == DBNull.Value ? null : resultado;
        }

        // Métodos privados para manejar la conexión
        private void conectar()
        {
            if (DBBroker.conexion.State == ConnectionState.Closed)
            {
                DBBroker.conexion.Open();
            }
        }

        private void desconectar()
        {
            if (DBBroker.conexion.State == ConnectionState.Open)
            {
                DBBroker.conexion.Close();
            }
        }
    }
}
