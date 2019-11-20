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

namespace addRubrique
{
    public partial class Form1 : Form
    {


        OracleConnection _connex = new OracleConnection
        {
            ConnectionString = "Data Source=iutdb; User ID=mms13; Password=02084649"
        };

        public Form1()
        {
            InitializeComponent();

            _connex.Open();

            getRubriques(treeView2.Nodes.Add("Root"), null);

            this.dataGridView1.ColumnCount = 7;

            dataGridView1.Columns[0].Name = "IDENTIFIANT";
            dataGridView1.Columns[1].Name = "NOM";
            dataGridView1.Columns[2].Name = "DESCRIPTION";
            dataGridView1.Columns[3].Name = "ID_RUBRIQUE";
            dataGridView1.Columns[4].Name = "POIDS";
            dataGridView1.Columns[5].Name = "PRIX UNITAIRE";
            dataGridView1.Columns[6].Name = "STOCK";

            getArticles(null);
        }

        public void getRubriques(TreeNode node, Nullable<int> idRubrique)
        {

            OracleCommand req = new OracleCommand();
            req.Connection = _connex;

            if (idRubrique == null)
            {
                req.CommandText = "SELECT * FROM rubrique WHERE id_parent is null";
            }
            else
            {
                req.CommandText = "SELECT * FROM rubrique WHERE id_parent = " + idRubrique;
            }
            
            OracleDataReader res = req.ExecuteReader();

            while (res.Read())
            {
                TreeNode NoeudFils = new TreeNode(res[1].ToString());
                NoeudFils.Tag = int.Parse(res[0].ToString());
                node.Nodes.Add(NoeudFils);
                getRubriques(NoeudFils, int.Parse((res[0]).ToString()));
            }
        }

        private void getArticles(Nullable<int> idRubrique)
        {
            OracleCommand req = new OracleCommand();
            req.Connection = _connex;

            if (idRubrique == null)
            {
                req.CommandText = "SELECT * FROM article";
            }
            else
            {
                req.CommandText = "SELECT * FROM article WHERE id_rubrique = " + idRubrique;
            }
            
            OracleDataReader res = req.ExecuteReader();

            while (res.Read())
            {
                this.dataGridView1.Rows.Add(res[0].ToString(), res[1].ToString(), res[2].ToString(),
                           res[3].ToString(), res[4].ToString(), res[5].ToString(), res[6].ToString());
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _connex.Close();
        }
        
        //private void treeView2_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        //{
        //    if (e.Node != null)
        //    {
        //        if (e.Node.Tag != null)
        //        {
        //            getArticles(e.Node.Tag.ToString());
        //        }
        //    }
            
        //}

        private void button1_Click(object sender, EventArgs e)
        {
            OracleCommand req = new OracleCommand();
            req.Connection = _connex;
            if (textBox1.Text != "" )
            {
                req.CommandText = "INSERT INTO rubrique(id,nom,descritption,id_parent) VALUES (,textBox1.Text,,)";
            }
        }

        private void treeView2_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }
    }
}
