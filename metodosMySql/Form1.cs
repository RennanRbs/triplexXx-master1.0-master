using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace metodosMySql
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        MySqlConnection conectar = new MySqlConnection("Server=lamp01ppgcc.ddns.net; Database=cadastro_bolsistas_lit; Uid=controlador_lit; Pwd=123qwe!@#");

   //***************************************************** salvar Bolsista *****************************************************************************//
        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                  conectar.Open();
                  MySqlCommand comandoPessoa = new MySqlCommand("INSERT INTO Pessoa(Nome,Email,Cpf,Celular,Id_Lit) VALUES('"+entradaNome.Text+"','"+entradaEmail.Text+"','"+entradaCpf.Text+"','"+entradaCelular.Text+ "', '" + entradaIDLit.Text + "' )", conectar);
                  MySqlCommand comandoBolsista = new MySqlCommand("INSERT INTO Bolsista(Endereço,Bairro,Rg,Telefone,Curso,Matricula,InstituicaoDeEnsino,pk_bolsista) VALUES('" + entradaEndereço.Text + "','" + entradaBairro.Text + "','" + entradaRg.Text + "','" + entradaTelefone.Text + "','" + entradaCurso.Text + "','" + entradaMatriula.Text + "','" + entradaINstituiçao.Text + "','" + entradaIDLit.Text + "' )", conectar);
                  MySqlCommand comandoremunerado = new MySqlCommand("INSERT INTO Remunerado(Agencia,Conta,Orientador,Fonte_bolsa) VALUES('" + entradaAgencia.Text + "','" + entradaConta.Text + "','" + entradaOrientador.Text + "','" + entradaFonteDaBolsa.Text + "' )", conectar);
                  MySqlCommand comandoprofessor = new MySqlCommand("INSERT INTO Professor(Projeto) VALUES('" + entradaProjeto.Text + "' )", conectar);


                  comandoprofessor.ExecuteNonQuery();
                  comandoremunerado.ExecuteNonQuery();
                  comandoPessoa.ExecuteNonQuery();
                  comandoBolsista.ExecuteNonQuery();
                  MessageBox.Show("Salvo com sucesso");
                  conectar.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show("error.." + error.Message + "Contate o suporte");
                conectar.Close();
            }

        }
 // ****************************** botao para buscar aluno ************************************************//
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                  MySqlCommand comando;
                  MySqlDataReader reader;
                  conectar.Open();
                  string selecionarpessoa = "SELECT * FROM PESSOA WHERE Id_Lit = '"+entradaIDLit.Text+"'";
                  string selecionarbolsista = "SELECT * FROM Bolsista WHERE pk_bolsista ='" + entradaIDLit.Text + "' ";

                
                  comando = new MySqlCommand(selecionarpessoa,conectar);

                  reader = comando.ExecuteReader();


                if (reader.Read())
                {
                    entradaCelular.Text = reader.GetString("Celular");
                    entradaCpf.Text = reader.GetString("Cpf");
                    entradaEmail.Text = reader.GetString("Email");
                    
                }
                else { MessageBox.Show("sem dados com esse nome"); }




                conectar.Close();
                reader.Close();}
            



            catch (Exception error)
            {
                MessageBox.Show("error.." + error.Message);
                conectar.Close();
            }


           
        }
    //****************************botao para dar update no aluno ********************************************//
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                //  string UpdatePessoa = "UPDATE Pessoa SET Email = '"+entradaemail.Text+"',Cpf = '"+entradacpf.Text+"', Celular = '"+entradacelular.Text+ "' WHERE NOME= '" + entradanome.Text + "'";
                //  conectar.Open();
                //  MySqlCommand comando = new MySqlCommand(UpdatePessoa,conectar);
                //  if (comando.ExecuteNonQuery() == 1) { MessageBox.Show("dados atualizados"); } else { MessageBox.Show("nao atualizado"); }
                //  conectar.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show("error.." + error.Message);
                conectar.Close();
            }
        }
 //*************************************************************limpar a tabela **********************************************//
        private void button4_Click(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("Desejar mesmo apagar o formulario?", "                            Lit 2017", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {

                foreach (Control ctrl in Controls)
                {

                    if (ctrl is TextBox)
                    {
                        ((TextBox)(ctrl)).Text = String.Empty;
                    }
                }
                entradaOBS.Text = "";
            }
            else if (dialogResult == DialogResult.No) { }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
