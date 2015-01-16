using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Memoria
{
    public partial class FormChiev : Form
    {
        string jogador;

        public FormChiev(string j)
        {
            InitializeComponent();
            jogador = j;
            this.BackgroundImage = Tema.BackGround;
        }

        private void FormChiev_Load(object sender, EventArgs e)
        {
            //Variáveis que indicam posição inicial e incremento, para o desenho da grelha
            int x = 0, y =0, i = 0, j = 0;

            //Desenha a imagem correspondente a cada Conquista(Chievs)
            foreach (Chievs c in Chievs.lista.Values)
            {
                //Define uma Instância de Jogador, para verificar as conquistas por ele desbloqueadas
                Jogador jo = Jogador.lista[jogador.ToUpper()];

                if (jo.listaChievs.Keys.Contains(c.Codigo))//Se conquista == desbloqueada
                {
                    c.Enabled = true;
                    //c.Enabled determina a imagem que é desenhada pelo
                    //método c.DesenhaImagem()
                }
                else
                    c.Enabled = false;

                //Cria uma nova PictureBox e atribui-lhe os eventos necessários
                PictureBox p= c.DesenhaImagem(new Rectangle(x,y,110,110)); 
                p.MouseEnter += new EventHandler(Picture_MouseEnter);
                p.MouseLeave += new EventHandler(Picture_MouseLeave);
                if (c.Enabled)//Só atribui a capacidade de Click se Conquista == desbloqueada (Enabled==true)
                { p.Cursor = Cursors.Hand; p.Click += new EventHandler(Picture_Click); }

                panel1.Controls.Add(p);//Adiciona a PictureBox ao painel
                //Prepara a posição de desenho da próxima PictureBox
                if (i < 3)
                { y += 130; i++; }
                else
                {
                    y = 0;
                    i = 0;
                    x += 130;
                    j++;
                }

            }
        }

    //Picture Boxes
        private void Picture_MouseEnter(object sender, EventArgs e)
        {
            //A Tag da PictureBox contém a referencia para a Conquista(Chiev) que lhe corresponde
            PictureBox p = (PictureBox)sender;
            Chievs c = (Chievs)p.Tag;

            if (c.Enabled)//Se Conquista==Desbloqeada
            {
                //Inicializa i com o valor DateTime associado ao Chiev c, presente na listaChievs do Jogador
                DateTime i = Jogador.lista[jogador.ToUpper()].listaChievs[c.Codigo];
                //Esta Data é a Key da lista de Recordes, correspondente ao jogo em que a conquista foi desbloquada
                Recordes r = Recordes.lista[i];

                string s = string.Format("{0}\nDificuldade: {1}\nPontos: {2}\nNível: {3}\nTempo: {4}\nData: {5}", c.Nome, r.Dificuldade, r.Pontuacao, r.Nivel, r.Tempo.ToString("mm\\:ss"), r.Data.ToShortDateString());
                toolTip1.Show(s, p.Parent, p.Location.X, p.Location.Y + p.Height);
                //Ao passar o rato na PictureBox, aparece uma ToolTip com informações sobre este Recorde
            }
            else
            {
                //Se ainda não foi desbloquada, aparece uma tooltip com os requesitos necessários para a desbloquear
                toolTip1.Show(c.Nome + "\n" + c.Requisitos, p.Parent, p.Location.X, p.Location.Y + p.Height);
            }
        }

        private void Picture_MouseLeave(object sender, EventArgs e)
        {
            //Esconde as tooltips, quando o rato não se encontra sobre uma PictureBox
            toolTip1.Hide(panel1);
        }

        private void Picture_Click(object sender, EventArgs e)
        {
            //Ao Carregar na PictureBox, Adiciona informações sobre o Recorde
            //no qual a conquista foi desbloqueada, á ListBox
            listBox1.Items.Clear();//Limpa a ListBox

            //Identifica a Conquista associada á PictureBox
            PictureBox p = (PictureBox)sender;
            Chievs c = (Chievs)p.Tag;

            //Inicializa i com o valor DateTime associado ao Chiev c, presente na listaChievs do Jogador
            DateTime i = Jogador.lista[jogador.ToUpper()].listaChievs[c.Codigo];
            //Esta Data é a Key da lista de Recordes, correspondente ao jogo em que a conquista foi desbloquada
            Recordes r = Recordes.lista[i];
            
            //Coloca uma referencia para o recorde na Tag da listbox, para ser usada no botão Ver Replay
            listBox1.Tag = r;

            //Adiciona as informações á ListBox
            listBox1.Items.Add("Conquista: " + c.Nome);
            listBox1.Items.Add("Objectivo:");
            listBox1.Items.Add(c.Requisitos);
            listBox1.Items.Add("");
            listBox1.Items.Add("Jogador: " + r.NomeJogador);
            listBox1.Items.Add("Data: " + r.Data.ToShortDateString());            
            listBox1.Items.Add("Dificuldade: " + r.Dificuldade );
            listBox1.Items.Add("Pontos: " + r.Pontuacao);
            listBox1.Items.Add("Tempo: " + r.Tempo);
            listBox1.Items.Add("Nível Atingido " + r.Nivel);

            //Liga o Botão Ver Replay
            button1.Enabled = true;
            
        }

    //Sair
        private void button2_Click(object sender, EventArgs e)
        {
            //Fecha o Formulário
            this.Close();
        }

    //Ver Replay
        private void button1_Click(object sender, EventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\My Games\Memoria\Replays\";

            //Analisa a referencia do recorde representado na ListBox
            Recordes c = (Recordes)listBox1.Tag;

            string s = c.Data.ToString("yy-MM-dd-HH-mm-ss");
            if (File.Exists(path + s + ".txt"))//Verifica se o Replay do Recorde se encontra no local onde devia ter sido criado
            {
                //Inicializa o FormReplay com o caminho para o Replay do Recorde
                FormReplay f = new FormReplay(path + s + ".txt");
                f.MdiParent = this.MdiParent;
                f.Dock = DockStyle.Fill;
                f.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Replay não encontrado");
            }
        }
    }
}
