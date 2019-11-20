using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OracleClient;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private const string V = "Data Source=iutdb;User ID=mms13;Password=02084649";

        public Form1()
        {
            InitializeComponent();
            OracleConnection _connex = new OracleConnection
            {
                ConnectionString = "Data Source=iutdb;User ID=mms13;Password=02084649"
            };
            _connex.Open();

            OracleCommand req = new OracleCommand();

            req.Connection = _connex;
            req.CommandText = "SELECT * FROM Pays";

            OracleDataReader res = req.ExecuteReader();

            while (res.Read())
            {
                comboBox6.Items.Add(res["nom"]);
            }
        }

        class CPays
        {
            int numero;
            String nom;

            public String Nom { get { return this.nom; } set { this.nom = value; } }
            public int Numero { get { return this.numero; } set { this.numero = value; } }

            public CPays(int num, String nom)
            {
                this.numero = num;
                this.nom = nom;
            }

            public override string ToString()
            {
                return this.nom;
            }
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            OracleConnection _connex = new OracleConnection
            {
                ConnectionString = V
            };
            _connex.Open();


            OracleCommand req3 = new OracleCommand
            {
                Connection = _connex,
                CommandText = "SELECT * FROM pays WHERE nom = " + comboBox6.SelectedValue
            };
            OracleDataReader res3 = req3.ExecuteReader();
            
            OracleCommand req2 = new OracleCommand
            {
                Connection = _connex,
                CommandText = "SELECT * FROM region WHERE id_pays = " + res3["id"]
            };
            OracleDataReader res2 = req2.ExecuteReader();

            while (res2.Read())
            {
                comboBox7.Items.Add(res2["nom"]);
            }
        }
    }
}
