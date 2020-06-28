using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Atividade1
{
    public partial class formcategoria : Form
    {
        private SqlConnection conexao;
        private SqlCommand comando;
        private string sqlStr, strconexao;

        private void btincluir_Click(object sender, EventArgs e)
        {
            string categoria = txtcategoria.Text.Trim();
            string descricao = txtdescricao.Text.Trim();
            if (categoria.Length == 0)
            {
                MessageBox.Show("Por favor, informe o nome da categoria!", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtcategoria.Focus();
                return;
            }

            strconexao = "Server=localhost; Database=Mercadinho; Integrated Security=True;";
            conexao = new SqlConnection(strconexao);

            sqlStr = "SELECT * FROM Categorias WHERE Categoria LIKE @categoria";
            comando = new SqlCommand(sqlStr, conexao);

            comando.Parameters.Add("@categoria", SqlDbType.Text);
            comando.Parameters["@categoria"].Value = categoria;

            try
            {
                conexao.Open();
                SqlDataAdapter consulta = new SqlDataAdapter(comando);
                DataSet dados = new DataSet();
                consulta.Fill(dados);

                if (dados.Tables[0].Rows.Count > 0)
                {
                    MessageBox.Show("Esta categoria já está cadastrada. Por favor, informe outro nome!", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtcategoria.Focus();
                    return;
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            sqlStr = "INSERT INTO Categorias (categoria, descricao) VALUES (@categoria, @descricao)";
            comando = new SqlCommand(sqlStr, conexao);

            comando.Parameters.Add("@categoria", SqlDbType.Text);
            comando.Parameters["@categoria"].Value = categoria;

            comando.Parameters.Add("@descricao", SqlDbType.Text);
            comando.Parameters["@descricao"].Value = descricao;

            try
            {
                if (comando.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Registro inserido com sucesso!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtcategoria.Clear();
                    txtdescricao.Clear();
                } else
                {
                    MessageBox.Show("Nenhum registro inserido!", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public formcategoria()
        {
            InitializeComponent();
        }
    }
}
