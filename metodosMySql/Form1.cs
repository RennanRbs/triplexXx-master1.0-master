﻿using System;
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
            bool m =false, t=false, n=false ,ifce = false,outra = false,remunerado = false,voluntario = false;
            if (manha.Checked) { m = true; }
            if (tarde.Checked) { t = true; }
            if (noite.Checked) { n = true; }
            if (radioifce.Checked) { ifce = true; }
            if (radiooutra.Checked) { outra = true; }
            if (radiovoluntario.Checked) { voluntario = true; }
            if (radioremunerado.Checked) { remunerado = true; }
            try
            {

                MySqlDataReader reader;
                conectar.Open();


                MySqlCommand comandoPessoa = new MySqlCommand("INSERT INTO Pessoa(nome,email,cpf,celular,cod_lit) VALUES('" + entradaNome.Text + "','" + entradaEmail.Text + "','" + entradaCpf.Text + "','" + entradaCelular.Text + "', '" + entradaIDLit.Text + "' )", conectar);
                MySqlCommand comandoselect = new MySqlCommand(" select id from pessoa order by id DESC limit 1", conectar);
                MySqlCommand comandoselectprofessor = new MySqlCommand("SELECT id FROM bolsista order by id desc limit 1",conectar);
                MySqlCommand comandoselectremunerado = new MySqlCommand("SELECT id FROM bolsista order by id desc limit 1",conectar);

                comandoPessoa.ExecuteNonQuery();

                reader = comandoselect.ExecuteReader();

                if (reader.Read())
                {

                    MySqlCommand comandoBolsista = new MySqlCommand("INSERT INTO Bolsista(pessoa_id,endereco,bairro,rg,telefone,curso,matricula,instituicaodeensino,semestre,datadenascimento,cep,manha,tarde,noite,radioifce,radiooutra,radioremunerado,radiovoluntario,obs)"+
                        " VALUES(" + reader.GetString("id") + " , '" + entradaEndereço.Text + "','" + entradaBairro.Text + "','" + entradaRg.Text + "','" + entradaTelefone.Text + "','" + entradaCurso.Text + "','" + entradaMatriula.Text + "','" + entradaINstituiçao.Text + "'"+
                        " ,'" + entradaSemestre.Text + "','" + entradaDataDeNascimento.Text + "','"+entradaCep.Text+"',"+m+","+t+","+n+","+ifce+","+outra+", "+remunerado+", "+voluntario+" , '"+entradaOBS.Text+"')", conectar);

                    MySqlCommand comandoprofessor = new MySqlCommand("INSERT INTO professor(pessoa_id,projeto)VALUES(" + reader.GetString("id") + ", '" + entradaProjeto.Text + "')", conectar);

                    reader.Close();
                    comandoBolsista.ExecuteNonQuery();
                    comandoprofessor.ExecuteNonQuery();
                    
                }

                reader = comandoselectremunerado.ExecuteReader();

                if (reader.Read()) {
                    MySqlCommand comandoremunerado = new MySqlCommand("INSERT INTO remunerado(bolsista_id,agencia,conta,orientador,fonte_bolsa,banco)VALUES (" + reader.GetString("id") + ", '"+entradaAgencia.Text+"', '"+entradaConta.Text+"', '"+entradaOrientador.Text+"','"+entradaFonteDaBolsa.Text+"', '"+entradaBanco.Text+"')", conectar);
                    reader.Close();
                    comandoremunerado.ExecuteNonQuery();
                    MessageBox.Show("Salvo com sucesso, que Demais!");
                }





                conectar.Close();
            }

            


            catch (Exception error)
            {
                MessageBox.Show("error.." + error.Message + "   Contate o suporte");
                conectar.Close();
            }
            
                    }
 // ******************************************************* botao para buscar aluno ************************************************//
        private void button2_Click(object sender, EventArgs e)
        {
            if (entradaIDLit.Text != "")
            {
                try
                {

                    MySqlCommand comando;
                    MySqlDataReader reader;
                    conectar.Open();

                    string selecionarpessoa = "select  bolsista.obs,professor.projeto,remunerado.banco ,remunerado.agencia,remunerado.conta,remunerado.orientador,remunerado.fonte_bolsa,"+
                                              "bolsista.semestre, bolsista.endereco, bolsista.bairro, bolsista.rg, bolsista.instituicaodeensino,bolsista.cep ," +
                                              "bolsista.matricula, bolsista.curso, bolsista.telefone, pessoa.cod_lit, pessoa.celular,bolsista.datadenascimento" +
                                              " ,pessoa.email, pessoa.cpf, pessoa.nome, bolsista.manha,bolsista.tarde,bolsista.noite,bolsista.radioifce,bolsista.radiooutra,"+
                                              "bolsista.radioremunerado,bolsista.radiovoluntario " +
                                              " from bolsista ,pessoa ,professor ,remunerado where bolsista.pessoa_id =  pessoa.id and pessoa.id = professor.pessoa_id and bolsista.id = remunerado.bolsista_id and  pessoa.cod_lit = " + entradaIDLit.Text + "; ";



                    comando = new MySqlCommand(selecionarpessoa, conectar);

                    reader = comando.ExecuteReader();


                    if (reader.Read())
                    {
                        entradaMatriula.Text = reader.GetString("matricula");
                        entradaCelular.Text = reader.GetString("celular");
                        entradaCpf.Text = reader.GetString("cpf");
                        entradaEmail.Text = reader.GetString("email");
                        entradaCep.Text = reader.GetString("cep");
                        entradaEndereço.Text = reader.GetString("endereco");
                        entradaNome.Text = reader.GetString("nome");
                        entradaSemestre.Text = reader.GetString("semestre");
                        entradaBairro.Text = reader.GetString("bairro");
                        entradaRg.Text = reader.GetString("rg");
                        entradaINstituiçao.Text = reader.GetString("instituicaodeensino");
                        entradaTelefone.Text = reader.GetString("telefone");
                        entradaDataDeNascimento.Text = reader.GetString("datadenascimento");
                        entradaCurso.Text = reader.GetString("curso");
                        entradaProjeto.Text = reader.GetString("projeto");
                        entradaAgencia.Text = reader.GetString("agencia");
                        entradaConta.Text = reader.GetString("conta");
                        entradaOrientador.Text = reader.GetString("orientador");
                        entradaFonteDaBolsa.Text = reader.GetString("fonte_bolsa");
                        entradaBanco.Text = reader.GetString("banco");
                        entradaOBS.Text = reader.GetString("obs");
                        
                        if (reader.GetBoolean("manha")) { manha.Checked = true; }
                        if (reader.GetBoolean("tarde") ) { tarde.Checked = true; }
                        if (reader.GetBoolean("noite")) { noite.Checked= true; }
                        if (reader.GetBoolean("radioifce")) { radioifce.Checked = true; }
                        if (reader.GetBoolean("radiooutra")) { radiooutra.Checked = true; }
                        if (reader.GetBoolean("radioremunerado")) { radioremunerado.Checked = true; }
                        if (reader.GetBoolean("radiovoluntario")) { radiovoluntario.Checked = true; }



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
            else { MessageBox.Show("Campo  em Branco"); }

           
        }
    //****************************botao para dar update no aluno ********************************************//
        private void button3_Click_1(object sender, EventArgs e)

        {
            try
            {
                  string UpdatePessoa = "UPDATE pessoa,bolsista,professor,remunerado SET  obs = '"+entradaOBS.Text+"',fonte_bolsa = '"+entradaFonteDaBolsa.Text+"',orientador = '"+entradaOrientador.Text+"',banco = '"+entradaBanco.Text+"',conta = '"+entradaConta.Text+"',agencia = '"+entradaAgencia.Text+"',projeto = '"+entradaProjeto.Text+"' ,rg = '"+entradaRg.Text+"',radioremunerado = "+radioremunerado.Checked+",radiovoluntario = "+radiovoluntario.Checked+",radiooutra = "+radiooutra.Checked+",radioifce = "+ radioifce.Checked+",manha = "+manha.Checked+",tarde ="+tarde.Checked+",noite = "+noite.Checked+"  ,email = '"+entradaEmail.Text+"', cep= '"+entradaCep.Text+"' ,cpf = '"+entradaCpf.Text+"',bairro = '"+entradaBairro.Text+"',datadenascimento = '"+entradaDataDeNascimento.Text+"', telefone = '"+entradaTelefone.Text+"',instituicaodeensino = '"+entradaINstituiçao.Text+"',matricula = '"+entradaMatriula.Text+"',semestre = '"+entradaSemestre.Text+"', celular = '"+entradaCelular.Text+ "',curso = '"+entradaCurso.Text+"',  nome= '" + entradaNome.Text + "', endereco = '"+ entradaEndereço.Text+ "'  where bolsista.pessoa_id =  pessoa.id and bolsista.id = remunerado.bolsista_id and pessoa.id = professor.pessoa_id and pessoa.cod_lit = " + entradaIDLit.Text + " ";
                  conectar.Open();
                 MySqlCommand comando = new MySqlCommand(UpdatePessoa,conectar);
                
                  if ( comando.ExecuteNonQuery()== 4) { MessageBox.Show("dados atualizados"); } else { MessageBox.Show("nao atualizado"); }
                  conectar.Close();
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
                manha.Checked = false;
                tarde.Checked = false;
                noite.Checked = false;
                radiooutra.Checked = false;
                radioifce.Checked = false;
            }
            else if (dialogResult == DialogResult.No) { }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form2 outroform = new Form2();
            outroform.Show();
        }
    }
}
