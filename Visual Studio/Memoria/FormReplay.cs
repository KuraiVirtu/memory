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
    public partial class FormReplay :Form
    {
        SortedList<TimeSpan, int> Movimentos = new SortedList<TimeSpan, int>();//Lista com os moviemtntos do Replay
        StreamReader sr;
        string dificuldade;//Dificuldade do recorde
        int gridSize;//numero de cartas
        int tentativas;
        int nivel;
        int pontos;
        Carta[] grid;//Array com as referencias para todas as cartas
        DateTime inicio;
        TimeSpan intervalo;//tempo entre cliques
        DateTime ultimoPar;//momento em que o ultimo par foi encontrado

        public FormReplay()
        {           
            InitializeComponent();
        }

        public FormReplay(string Path)
        {
            //Inicializa o form com um replay já carregado
            InitializeComponent();
            textBox1.Text = Path;
            button2.Enabled = true;
            button3.Enabled = true;
            button5.Enabled = true;
            inicializa(Path);//prepara o replay para reprodução
        }

//Eventos
        private void FormReplay_Load(object sender, EventArgs e)
        {

        }

        private void flipTimer_Tick(object sender, EventArgs e)
        {
            //Tag do Timer tem um array int[] com as posições das cartas que são para esconder
            Timer t = (Timer)sender;
            int[] pos = (int[])t.Tag;

            //Esconde as cartas nessas posições
            for (int i = 0; i < pos.Length; i++)
            {
                grid[pos[i]].EscondeCarta();
            }
            t.Stop();
            Carta.viradas = 0;
        }

    //Replay (...)
        private void button4_Click(object sender, EventArgs e)
        {
            timer1.Stop();//Para a reprodução

            //Procura um novo replay com o dialogo OpenFileDialog
            OpenFileDialog abrir = new OpenFileDialog();
            abrir.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\My Games\Memoria\Replays";
            abrir.Filter = "Texto (.txt)|*.txt";
            DialogResult res = abrir.ShowDialog();
            if (res == DialogResult.OK)//Se carregou ficheiro
            {
                panel1.Controls.Clear();
                panel2.Controls.Clear();
                button2.Enabled = true;//ativa butao começar
                button3.Enabled = true;//ativa butao proximo nivel
                button5.Enabled = true;//ativa butao reiniciar
                inicializa(abrir.FileName);//Prepara reprodução 
            }
            else button5_Click(null, null);//Reinicia a reproduçao
        }

    //Começar
        private void button2_Click(object sender, EventArgs e)
        {
            if (Movimentos.Count > 0)//So começa se houver elementos na lista
            {
                int[] pos = new int[gridSize];//Inicia array para passar a flipTimerGlobal com a posição de todas as cartas para esconder
                //Mostra toas as cartas
                for (int i = 0; i < gridSize; i++)
                {
                    grid[i].MostraCarta();
                    pos[i] = i;
                }
                flipTimerGlobal.Tag = pos;
                flipTimerGlobal.Start();
                intervalo = Movimentos.Keys[0];//Proximo clique devera ser feito no tempo do primeiro movimento
                inicio = DateTime.Now;
                timer1.Start();//Inicia Relogio e reproduçao dos movimentos
                button2.Enabled = false;//Destiva o botao começar
            }
        }

    //Reprodução
        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan temp = DateTime.Now - inicio;//Tempo passado desde inicio da reprodução
            label1.Text = string.Format("{0:mm\\:ss}", temp);
            if (temp >= intervalo)//Se já chegou o tempo de reproduzir o proximo movimento
            {
                Carta_Click(Movimentos.Values[0]);//Reproduz proximo movimento
                Movimentos.RemoveAt(0);//Remove movimento reproduzido
                if (Movimentos.Count > 0)//Se ainda houver movimentos
                    intervalo = Movimentos.Keys[0];//Proximo movimento sera efectuado neste momento
                else timer1.Stop();//para reprodução
            }
        }

    //Sair
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    //Proximo Nivel
        private void button3_Click(object sender, EventArgs e)
        {
            nivel++;//aumenta nivel
            timer1.Stop();//para reprodução
            Movimentos.Clear();//Limpa lista Movimentos
            panel2.Controls.Clear();//Limpa cartas
            panel1.Controls.Clear();//Limpa tentativas
            button2.Enabled = true;//Activa botao começar
            novo(textBox1.Text);//Inicia novo nivel

        }

    //Reinicia
        private void button5_Click(object sender, EventArgs e)
        {
            timer1.Stop();//Para reprodução
            Movimentos.Clear();//limpa lista movimentos
            panel2.Controls.Clear();//limpa cartas
            panel1.Controls.Clear();//limpa tentativas
            button2.Enabled = true;//activa butao comçar
            button3.Enabled = true;//activa butao proximo nivel
            inicializa(textBox1.Text);//reinicia reprodução          
        }

//Métodos
        /// <summary>
        /// Calcula com base na dificuldade, o numero de cartas e o intervali do flipTimer
        /// </summary>
        private void CalcGrid()
        {
            switch (dificuldade.ToLower())
            {
                case "facil":
                case "fácil":
                    gridSize = 16;
                    flipTimer.Interval = 800;
                    break;
                case "dificil":
                case "difícil":
                    gridSize = 24;
                    flipTimer.Interval = 500;
                    break;
                case "media":
                case "média":
                    gridSize = 20;
                    flipTimer.Interval = 650;
                    break;
                default:
                    gridSize = 8;
                    break;
            }
        }

        /// <summary>
        /// Desenha as cartas no painel2, de acordo com as posições defenidas pelo método Distribui().
        /// </summary>
        public void DrawGrid()
        {
            grid = new Carta[gridSize];

            int c = 0;//Variavel de controle para o ciclo for  

            string s = sr.ReadLine();//Le a sequencia das cartas do StreamReader
            string[] temp = s.Split(',');//Divide os numeros para um array de strings
            int[] array = new int[gridSize];//Array de valores aleatórios que irão defenir a imagem de cada carta

            //Coloca os valores de temp em array
            for (int i = 0; i < gridSize; i++)
            {
                array[i] = int.Parse(temp[i]);
            }
            //Desenha a grelha de cartas no painel2
            for (int i = 0; i < gridSize / 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    grid[c] = new Carta(array[c], new Rectangle(i * 70, j * 100, 70, 100));
                    grid[c].Desenha(panel2);
                    c++;
                }
            }
        }

        /// <summary>
        /// Remove uma tentativa
        /// </summary>
        private void DescontaTentativa()
        {
            panel1.Controls.RemoveAt(panel1.Controls.Count - 1);
            tentativas--;
        }

        /// <summary>
        /// Desenha no painel1, tantos corações como o numero de tentativas
        /// </summary>
        private void DesenhaTentativas()
        {
            int incrX = 40, incrY = 40;
            int ctrl = 0;
            for (int j = 0; j < 2; j++)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (ctrl == tentativas)
                        break;
                    PictureBox p = new PictureBox();
                    p.Image = Properties.Resources.Heart;
                    p.Size = p.Image.Size;
                    p.Location = new Point(5 + j * incrX, 5 + i * incrY);
                    panel1.Controls.Add(p);
                    ctrl++;
                }
            }
        }

        /// <summary>
        /// Prepara o formulario para reproduzir os varios niveis de um Replay
        /// </summary>
        /// <param name="filename">Diretorio do Replay</param>
        public void inicializa(string filename)
        {
            pontos = 0;//Reinicia os pontos
            txtPontos.Text = "0";
            Movimentos.Clear();//Reinicia a lista de Movimentos
            sr = new StreamReader(new FileStream(filename, FileMode.Open));
            textBox1.Text = filename;
            sr.ReadLine();//Consome a primeira linha (essa informação não está a ser usada)
            txtJogador.Text = sr.ReadLine();//Le o jogador do Replay
            dificuldade = sr.ReadLine();//Le a dificuldade do Replay
            sr.ReadLine();//Consome uma linha (essa informação não está a ser usada)
            sr.Close();
            nivel = 1;
            novo(filename);//Prepara a reproduçao do primeiro nivel
            Carta.viradas = 0;            
        }

        /// <summary>
        /// Prepara a reproduçao do primeiro nivel
        /// </summary>
        /// <param name="filename">Diretorio do Replay</param>
        public void novo(string filename)
        {
            int i=0;//Variavel de controle para encontrar a posição do nivel no ficheiro
            sr = new StreamReader(new FileStream(filename, FileMode.Open));
            while (i < nivel)
            {
                string s = sr.ReadLine();
                if (s == "[Novo]")//[Novo]Marca um novo nivel
                    i++;//Quando i==nivel, chegou ao ponto do ficheiro que procura (ao nivel correspondente)
            }

            ultimoPar = DateTime.Now;
            CalcGrid();//calcula numero de cartas e intervalo do flipTimer
            DrawGrid();//Desenha grelha de cartas
            flipTimerGlobal.Interval = flipTimer.Interval * 3;
            nivel = int.Parse(sr.ReadLine());
            label5.Text = nivel.ToString();
            //Calcula as tentativas
            tentativas = (int)Math.Round((0.15 * Math.Pow(nivel, 2) - 3.26 * nivel + 23));
            if (tentativas < 5)
                tentativas = 5;
            DesenhaTentativas();//Desenha as tentativas no painel1

            //Adiciona registos de movimentos á lista Movimentos, até atingir fim do ficheiro ou novo nivel ("[Novo]")
            while (sr.Peek() != -1)
            {
                string s = sr.ReadLine();
                if (s != "[Novo]")
                {
                    string[] temp = s.Split('|');
                    Movimentos.Add(TimeSpan.Parse(temp[1]), int.Parse(temp[0]));
                }
                else
                    break;
            }
            if (sr.Peek() == -1)//não há mais niveis, logo desactiva o botão proximo nivel
            { button3.Enabled = false; }
            sr.Close();
        }

        /// <summary>
        /// Mostra a carta na posição defenida, como se tivesse sido clicada.
        /// </summary>
        /// <param name="pos">A posição da carta no array grid</param>
        private void Carta_Click(int pos)
        {
            Carta c = grid[pos];

            if (Carta.viradas <= 1)
            {
                c.MostraCarta();
            }            

            if (Carta.viradas == 2)
            {
                Teste();
            }
        }

        /// <summary>
        /// Replica a lógica do jogo.
        /// <para>Verifica se as cartas são iguais</para>
        /// </summary>
        public void Teste()
        {
            int[] comparador = new int[2];//Array com o codigo das imagens(das cartas) a comparar
            int[] pos = new int[2];//Array com a posição das Cartas a comparar
            int ctrl = 0;//Controle da posição dos anteriores arrays onde se vão colocar os dados

            for (int i = 0; i < gridSize; i++)
            {
                if (grid[i].Virada == true && grid[i].Bloqueada == false)//Se a carta estiver virada e não fizer parte de um par descoberto(bloqueada==false)
                {
                    comparador[ctrl] = grid[i].Codigo;
                    pos[ctrl] = i;
                    ctrl++;
                }
            }

            if (comparador[0] == comparador[1])//Se o código das cartas for igual
            {
                //Bloqueia as duas cartas
                grid[pos[0]].Bloqueia();
                grid[pos[1]].Bloqueia();
                Pontua();//Calcula os pontos
                Carta.viradas = 0;
            }

            else
            {
                DescontaTentativa();//Retira uma tentativa
                flipTimer.Tag = pos;//Coloca na Tag de flipTimer, a posição das cartas a serem escondidas
                flipTimer.Start();
            }
        }

        /// <summary>
        /// Adiciona pontos tendo em conta a última vez que o método foi chamado
        /// </summary>
        public void Pontua()
        {
            TimeSpan intervalo = DateTime.Now - ultimoPar;
            pontos += (int)(500 * (1 / intervalo.TotalSeconds));
            if (pontos < 100)
                pontos = 100;
            ultimoPar = DateTime.Now;
            txtPontos.Text = pontos.ToString();
        }
        
    }
}
