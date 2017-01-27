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

                MySqlDataReader reader;
                conectar.Open();


                MySqlCommand comandoPessoa = new MySqlCommand("INSERT INTO Pessoa(nome,email,cpf,celular,cod_lit) VALUES('" + entradaNome.Text + "','" + entradaEmail.Text + "','" + entradaCpf.Text + "','" + entradaCelular.Text + "', '" + entradaIDLit.Text + "' )", conectar);
                MySqlCommand comandoselect = new MySqlCommand(" select id from pessoa order by id DESC limit 1", conectar);

                comandoPessoa.ExecuteNonQuery();

                reader = comandoselect.ExecuteReader();

                if (reader.Read())
                {


                    MySqlCommand comandoBolsista = new MySqlCommand("INSERT INTO Bolsista(pessoa_id,endereco,bairro,rg,telefone,curso,matricula,instituicaodeensino,semestre,datadenascimento) VALUES(" + reader.GetString("id") + " , '" + entradaEndereço.Text + "','" + entradaBairro.Text + "','" + entradaRg.Text + "','" + entradaTelefone.Text + "','" + entradaCurso.Text + "','" + entradaMatriula.Text + "','" + entradaINstituiçao.Text + "' ,'" + entradaSemestre.Text + "','" + entradaDataDeNascimento.Text + "')", conectar);
                    // MySqlCommand comandoremunerado = new MySqlCommand("INSERT INTO Remunerado(agencia,conta,orientador,fonte_bolsa) VALUES('" + entradaAgencia.Text + "','" + entradaConta.Text + "','" + entradaOrientador.Text + "','" + entradaFonteDaBolsa.Text + "' )", conectar);
                    // MySqlCommand comandoprofessor = new MySqlCommand("INSERT INTO Professor(projeto) VALUES('" + entradaProjeto.Text + "' )", conectar);
                    reader.Close();
                    comandoBolsista.ExecuteNonQuery();
                    // comandoremunerado.ExecuteNonQuery();
                    //comandoprofessor.ExecuteNonQuery();
                    MessageBox.Show("Salvo com sucesso");
                }
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
            try{

                MySqlCommand comando;
                MySqlDataReader reader;
                conectar.Open();
                
                string selecionarpessoa = "select bolsista.semestre, bolsista.endereco, bolsista.bairro, bolsista.rg, bolsista.instituicaodeensino,"+
                                          "bolsista.matricula, bolsista.curso, bolsista.telefone, pessoa.cod_lit, pessoa.celular,bolsista.datadenascimento" +
                                          " ,pessoa.email, pessoa.cpf, pessoa.nome"+
                                          " from bolsista, pessoa where bolsista.pessoa_id = pessoa.id and pessoa.cod_lit = "+entradaIDLit.Text+"; ";
                


                comando = new MySqlCommand(selecionarpessoa, conectar);

                reader = comando.ExecuteReader();


                if (reader.Read())
                {
                    entradaCelular.Text = reader.GetString("celular");
                    entradaCpf.Text = reader.GetString("cpf");
                    entradaEmail.Text = reader.GetString("email");
                    entradaEndereço.Text = reader.GetString("endereco");
                    entradaNome.Text = reader.GetString("nome");
                    entradaSemestre.Text = reader.GetString("semestre");
                    entradaBairro.Text = reader.GetString("bairro");
                    entradaRg.Text = reader.GetString("rg");
                    entradaINstituiçao.Text = reader.GetString("instituicaodeensino");
                    entradaTelefone.Text = reader.GetString("telefone");
                    entradaDataDeNascimento.Text=reader.GetString("datadenascimento");


                }
                else { MessageBox.Show("sem dados com esse nome"); }




                conectar.Close();
                reader.Close();

            }




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
