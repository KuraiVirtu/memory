using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
//using System.Drawing;

namespace Memoria
{
    public partial class FormTemas : Form
    {

        Principal parent;//Referencia para o form principal

        public FormTemas()
        {
            InitializeComponent();
            this.Tag = Tema.Nome;//Tema a aplicar quando fecha o form
        }

//Eventos
        private void FormTemas_Load(object sender, EventArgs e)
        {
            updateListBox();
            parent = (Principal)this.MdiParent;
        }

        private void FormTemas_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Abre o tema presente na tag
            string s = (string)this.Tag;
            if (s != "Defeito")
                Tema.AbreTema(s);
            else
                Tema.TemaDefeito();
        }

    //Aplicar Tema
        private void button4_Click(object sender, EventArgs e)
        {
            Principal parent = (Principal)this.MdiParent;
            string s = listBox2.SelectedItem.ToString();
            if (s != null && s != "Defeito")//Se um tema que não o Defeito estiver seleccionado
            {
                if (s != Tema.Nome)
                    Tema.AbreTema(s);//Abre o tema seleccionado
            }
            else
            {
                Tema.TemaDefeito();//Abre o tema Defeito
            }
            this.Tag = Tema.Nome;//Tag do Form guarda tema a abrir quando fecha o form
            parent.updateBackgrounds();//Actualiza imagem de fundo de todos os forms abertos
            MessageBox.Show("Tema alterado com sucesso");
        }

    //Cancelar/Sair
        private void button5_Click(object sender, EventArgs e)
        {
            if ((string)this.Tag != Tema.Nome)//Se for mudar de tema, actualiza player
                parent.Player.Ctlcontrols.stop();
            //Senão continua a tocar
            this.Close();
        }

    //Seleccionar Tema da ListBox
        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex != -1)//Se alguma coisa seleccionada
            {
                Cursor = Cursors.WaitCursor;
                string s = listBox2.SelectedItem.ToString();
                if ( s != "Defeito")
                {
                    //"pre"visualização do tema
                    Tema.AbreTema(s);
                    this.BackgroundImage = Tema.BackGround;
                    DesenhaCartas();
                    parent.Player.Ctlcontrols.stop();//Actualiza musica

                }
                else if (s == "Defeito")
                {
                    //"pre"visualização do tema
                    Tema.TemaDefeito();
                    this.BackgroundImage = Tema.BackGround;
                    DesenhaCartas();
                    parent.Player.Ctlcontrols.stop();//Actualiza musica

                }
                Cursor = Cursors.Default;
            }
        }        

    //Click das cartas
        private void Carta_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            Carta c = (Carta)b.Tag;
            if (c.Virada)
                c.EscondeCarta();
            else
                c.MostraCarta();
        }

    //Criar Novo
        private void button6_Click(object sender, EventArgs e)
        {
            //mostra form NovoTema
            FormNovoTema f = new FormNovoTema(this);
            f.MdiParent = this.MdiParent;
            f.Dock = DockStyle.Fill;
            this.Hide();
            f.Show();            
        }

    //Remover
        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex != -1)//Se algum tema seleccionado
            {
                string n = (string)listBox2.SelectedItem;
                if (n == "Defeito")
                {
                    MessageBox.Show("Este tema não pode ser apagado");
                }
                else
                {
                    listBox2.SelectedIndex = 0;
                    //Remove tema da lista
                    Tema.lista.Remove(n);
                    //Adiciona na lista um tema com o nome >>[n]
                    Tema.Nome = ">>" + n;
                    Tema.lista.Add(Tema.Nome);
                    //Depois de carregados, não é possivel remover os ficheiros.
                    //Marcam-se, assim para serem removidos no proximo inicio da aplicação
                    Tema.TemaDefeito();
                    updateListBox();
                    MessageBox.Show("Tema removido com sucesso!\nEsta alteração terá efeito quando reiniciar o jogo.","Sucesso", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
        }

//Metodos
        /// <summary>
        /// Actualiza a listBox2
        /// </summary>
        public void updateListBox()
        {
            listBox2.Items.Clear();
            listBox2.Items.Add("Defeito");
            foreach (string s in Tema.lista)
            {
                //Temas cujo nome começa com ">>" estão marcados para só serem removidos da lista depois de remover os ficheiros do disco
                if (!s.StartsWith(">>"))
                {
                    if (s.StartsWith(">"))//O tema marcados com ">" é o tema que será aberto na inicialização do programa
                    {
                        listBox2.Items.Add(s.Remove(0, 1));//apresenta tema sem ">"
                    }
                    else
                        listBox2.Items.Add(s);
                }//Temas com nome ">>" não aparecem na listBox
            }
        }

        /// <summary>
        /// Desenha grelha de cartas para "previsualização"
        /// </summary>
        private void DesenhaCartas()
        {
            panel1.Controls.Clear();

            int x = 0, incrX = 70;
            int y = 0, incrY = 100;
            int ctrl = 0;
            //70,100
            for (int i = 0; i < 13; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (ctrl == 13)
                        break;
                    Carta c = new Carta(ctrl, new Rectangle(x + incrX * j, y + incrY * i, 70, 100));
                    c.MostraCarta();
                    c.Desenha(panel1);
                    c.Butao.Click += new EventHandler(Carta_Click);
                    ctrl++;
                }
            }
        }
    }
}
