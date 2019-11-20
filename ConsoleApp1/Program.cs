using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OracleClient;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            OracleConnection _connex = new OracleConnection();
            _connex.ConnectionString = "Data Source=iutdb;User ID=mms13;Password=02084649";
            _connex.Open();

            Console.WriteLine("connexion : " + _connex.State);

            OracleCommand req = new OracleCommand();

            req.Connection = _connex;
            req.CommandText = "SELECT * FROM v_ville";

            OracleDataReader res = req.ExecuteReader();

            while(res.Read())
            {
                Console.WriteLine(res[0] + " - " + res["nom_ville"]);
            }

            OracleCommand reqSP = new OracleCommand();
            reqSP.Connection = _connex;
            reqSP.CommandType = System.Data.CommandType.StoredProcedure;
            reqSP.CommandText = "createCommande";

            OracleParameter p_numCli = new OracleParameter("numeroClient", OracleType.Number);
            p_numCli.Value = 48;

            OracleParameter p_return = new OracleParameter("return", OracleType.Number);
            p_return.Direction = System.Data.ParameterDirection.ReturnValue;

            reqSP.Parameters.Add(p_numCli);
            reqSP.Parameters.Add(p_return);

            reqSP.ExecuteNonQuery();

            Console.WriteLine("Commande n°" + p_return.Value + " créée");

            chercheClient(12,_connex);


            _connex.Close();
            System.Threading.Thread.Sleep(100000);
        }

        static void chercheClient(int numeroClient, OracleConnection _connex)
        {
            OracleCommand req2 = new OracleCommand();
            req2.Connection = _connex;
            req2.CommandText = "SELECT * FROM client WHERE numero = :numCli";

            OracleParameter p_numCli = new OracleParameter("numeroClient", OracleType.Number);
            p_numCli.Value = numeroClient;

            req2.Parameters.Add(p_numCli);

            OracleDataReader res2 = req2.ExecuteReader();

            if (res2.HasRows)
            {
                res2.Read();
                for (int i = 0; i<10; i++)
                {
                    Console.WriteLine(res2[i]);
                }
            }

        }
    }
}
