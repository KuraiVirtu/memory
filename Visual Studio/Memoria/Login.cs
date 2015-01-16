using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Memoria
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            this.BackgroundImage=Tema.BackGround;
        }

//Eventos
        private void Login_Load(object sender, EventArgs e)
        {
            //Coloca ambos os paineis no centro do Formulários
            panel1.Location = new Point(this.MdiParent.Width / 2 - panel1.Width / 2, this.MdiParent.Height / 2 - panel1.Height / 2);
            panel2.Location = new Point(this.MdiParent.Width / 2 - panel2.Width / 2, this.MdiParent.Height / 2 - panel2.Height / 2);
            
            //Apresenta painel Novo Jogador se a lista de Jogadores estiver vazia
            if (Jogador.lista.Count < 1)
            {
                AcceptButton = button1;
                panel1.Show();
            }

            //Apresenta painel Seleccionar Perfil se a lista de Jogadores tiver elementos
            else
            {
                updateListBox();
                AcceptButton = button4;
                panel2.Show();
            }

        }

    //Cria Novo Jogador (OK)
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                if (Jogador.lista.Keys.Contains(textBox1.Text.ToUpper()))
                {
                    MessageBox.Show("Utilizador já existe com esse nome!");
                    textBox1.Text = "";
                }
                else
                {
                    //Cria Perfil e inicia FormInicial
                    new Jogador(textBox1.Text);
                    MessageBox.Show("Bem vindo " + textBox1.Text);
                    FormInicial ini = new FormInicial(textBox1.Text);
                    ini.MdiParent = this.MdiParent;
                    ini.Dock = DockStyle.Fill;
                    ini.Show();
                    Jogador.GravaJogadores(@"Data\");
                    this.Close();
                }
            }
        }

    //Escolhe Jogador da Lista (OK)
        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count != 0)
            {                
                MessageBox.Show("Bem vindo " + listBox1.SelectedItem.ToString());
                FormInicial ini = new FormInicial(listBox1.SelectedItem.ToString());
                ini.MdiParent = this.MdiParent;
                ini.Dock = DockStyle.Fill;
                ini.Show();
                this.Close();
            }
        }

    //Adiciona Novo Jogador (Novo Jogador)
        private void button5_Click(object sender, EventArgs e)
        {           
            panel2.Hide();
            panel1.Show();
            textBox1.Focus();
            AcceptButton = button1;
        }

    //Cancelar Novo Jogador
        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Hide();
            panel2.Show();
            AcceptButton = button4;
        }

    //Elimina Jogador
        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count != 0)
            {
                string jogador = listBox1.SelectedItem.ToString();
                DialogResult d = MessageBox.Show("Tem a certeza que quer eliminar " + jogador + " e todos os seus dados?", "Confirmação",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (d == DialogResult.Yes)
                { Jogador.lista[jogador.ToUpper()].EliminaJogador(); updateListBox(); }
            }            
            Jogador.GravaJogadores(@"Data\");
            Jogador.GravaAchievs(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\My Games\Memoria");
            Recordes.GravaRecordes(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\My Games\Memoria");
        }

    //Update ListBox
        private void updateListBox()
        {
            listBox1.Items.Clear();
            foreach (Jogador j in Jogador.lista.Values)
            {
                listBox1.Items.Add(j.Nome);
            }
        }

        
    }
}
