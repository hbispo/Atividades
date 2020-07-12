using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SqlClient;

namespace Atividade3
{
    public partial class formMarca : Form
    {
        private SqlConnection conexao;
        private SqlCommand comando;
        private string sqlStr, strconexao;
        public formMarca()
        {
            InitializeComponent();
            txtMarca.Focus();
        }

        private void btPesquisar_Click(object sender, EventArgs e)
        {
            string marca = txtMarca.Text.Trim();
            if (marca.Length == 0)
            {
                MessageBox.Show("Por favor, informe o nome da marca!", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMarca.Focus();
                return;
            }

            strconexao = "Server=localhost; Database=AgenciaCarrosQBraQBra; Integrated Security=True;";
            conexao = new SqlConnection(strconexao);

            sqlStr = "SELECT * FROM Veiculos WHERE Marca LIKE @Marca";
            comando = new SqlCommand(sqlStr, conexao);

            comando.Parameters.Add("@Marca", SqlDbType.Text);
            comando.Parameters["@Marca"].Value = marca;

            try
            {
                conexao.Open();
                SqlDataAdapter consulta = new SqlDataAdapter(comando);
                DataSet dados = new DataSet();
                consulta.Fill(dados);

                tblVeiculos.DataSource = dados.Tables[0];
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
