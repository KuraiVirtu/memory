using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Media;
using System.IO;

namespace Memoria
{
    public partial class FormMemoria : Form
    {
        bool guardado;//Para verificar se o Recorde já foi guardado.

        Carta[] grid; //Array com as referencias para todas as cartas
        int gridSize;//Tamanho da grid (numero de cartas)
        string dificuldade;//Dificuldade do Jogo
        string jogador;//Nome do Jogador
        int pontos;//A Pontuação
        int nivel;//Nivel Actual        
        public int tentativas;//Quantas vezes se pode falhar
        DateTime horaInicio;//Momento em que o Form foi criado
        DateTime ultimoPar;//Momento em que foi descoberto o último par
        TimeSpan tempo;//Tempo decorrido desde inicio do jogo
        Recordes rec;//O Recorde que vai ser guardado(ou passado para o prócimo nível)
        Replay rep;//O Replay onde são guardados todos os movimentos
        bool bloqueado;//Determina se se pode clicar nas cartas
        

        public FormMemoria(Recordes r,Replay re)
        {
            InitializeComponent();
            horaInicio = r.Data;

            //Inicia Recorde e Replay
            rec = r;
            rep = re;

            guardado = false;
            bloqueado = true;
            dificuldade = r.Dificuldade;
            jogador = r.NomeJogador;
            pontos = r.Pontuacao;
            txtPontos.Text = pontos.ToString();
            CalcGrid();//Calcula o tamanho do array de Cartas bem como o intervalo do flipTimer
            flipTimerGlobal.Interval = flipTimer.Interval * 3;
            grid = new Carta[gridSize];
            DrawGrid();//Desenha a grelha de cartas
            Carta.pares = gridSize / 2;
            Carta.viradas = 0;
            nivel = r.Nivel+1;
            rep.addNivel(nivel);//Coloca informação do nível no Replay
            label5.Text = nivel.ToString();

            //Calcula numero de tentativas 
            tentativas = (int)Math.Round((0.15 * Math.Pow(nivel, 2) - 3.26 * nivel + 23));
            if (tentativas < 5)
                tentativas = 5;          
            DesenhaTentativas();
            tempo = r.Tempo;
            label1.Text = string.Format("{0:mm\\:ss}", tempo);
        }

//Eventos
        private void FormMemoria_Load(object sender, EventArgs e)
        {
            this.BackgroundImage = Tema.BackGround;
        }

        private void FormMemoria_FormClosing(object sender, FormClosingEventArgs e)
        {
            guarda();
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
            bloqueado = false;//Desbloqueia as cartas
            
        }

        private void clockTimer_Tick(object sender, EventArgs e)
        {
            //Incrementa em um segundo o tempo, depois de ser iniciado o jogo
            tempo=tempo.Add(new TimeSpan(0,0,1));
            label1.Text = string.Format("{0:mm\\:ss}", tempo);
        }

    //Voltar
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult diagRes = MessageBox.Show("Tens a certeza que queres desistir?", "Desistir?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (diagRes == DialogResult.Yes)
                this.Close();
        }

    //Click das Cartas
        private void Carta_Click(object sender, EventArgs e)
        {
            //Verifica qual a instancia de carta associada ao botao
            Button b = (Button)sender;
            Carta c = (Carta)b.Tag;

            //Só regista click se carta não estiver bloqeuada e bloquado==false
            if (!c.Bloqueada && !bloqueado)
            {
                if (Carta.viradas <= 1)//Não permite virar mais que 2 cartas ao mesmo tempo
                {
                    //Som de carta a virar
                    string flip = "Memoria.Resources.flip.wav";
                    Stream s = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(flip);
                    SoundPlayer snd = new SoundPlayer(s);
                    snd.Play();
                    s.Close();
                    //Vira a carta
                    c.MostraCarta();

                    for (int i = 0; i < gridSize; i++)//Encontra posição da carta no array de Cartas
                    {
                        if (grid[i] == c)
                            rep.addMovimento(i);//Adiciona posição da carta ao Replay
                    }
                }

                if (Carta.viradas == 2)
                {
                    Teste();//Verifica a lógica do jogo
                }
            }
        }

    //Começar
        private void button2_Click(object sender, EventArgs e)
        {
            bloqueado = true;//Bloqueia toas as cartas
            int[] pos = new int[gridSize];//Inicia array para passar a flipTimerGlobal com a posição de todas as cartas para esconder
            for (int i = 0; i < gridSize; i++)//Mostra toas as cartas
            {
                grid[i].MostraCarta();
                pos[i] = i;
            }
            flipTimerGlobal.Tag = pos;
            clockTimer.Start();//Inicia Relogio
            flipTimerGlobal.Start();
            rep.restartTime();
            button2.Hide();
        }

    //Continuar
        private void button3_Click(object sender, EventArgs e)
        {
            //Cria um novo FormMemoria no nivel seguinte
            FormMemoria m = new FormMemoria(rec, rep);
            guardado = true;
            m.MdiParent = this.MdiParent;
            m.Dock = DockStyle.Fill;
            m.Show();
            this.Close();
        }

    //Novo Jogo
        private void button4_Click(object sender, EventArgs e)
        {
            guarda();//guarda o recorde
            //Inicia novo FormMemoria com novo jogo
            Recordes r = new Recordes(DateTime.Now, jogador, dificuldade, TimeSpan.Zero, 0, 0, false);
            FormMemoria m = new FormMemoria(r, new Replay(r));
            m.MdiParent = this.MdiParent;
            m.Dock = DockStyle.Fill;
            m.Show();
            this.Close();

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
            int c = 0;//Variavel de controle para o ciclo for            

            //Array de valores aleatórios que irão defenir a imagem de cada carta
            int[] array = Distribui(gridSize);

            for (int i = 0; i < gridSize / 4; i++)
            {
                for (int j = 0; j < 4; j++)//Apenas permite 4 colunas
                {
                    grid[c]=new Carta(array[c],new Rectangle(i*70,j*100,70,100));
                    grid[c].Desenha(panel2);
                    grid[c].Butao.Click+=new EventHandler(Carta_Click);
                    c++;
                }
            }
            rep.addSequencia(array);//Coloca sequencia de cartas no Replay
        }

        /// <summary>
        /// Lógica principal do jogo.
        /// <para>Verifica se as cartas são iguaise e se jogo foi ganho ou perdido </para>
        /// </summary>
        public void Teste()
        {
            if (!bloqueado)
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
                    Carta.pares -= 1;//Remove um valor do contador de pares
                    if (Carta.pares == 0)//Se já não houver mais pares
                    {
                        Vitoria();//acaba o jogo em vitória
                    }
                    Carta.viradas = 0;
                }

                else//Se o código das cartas for diferente
                {
                    DescontaTentativa();//Retira uma tentativa
                    if (tentativas < 1)//Se não há mais tentativas
                    {
                        Derrota();//Perde o Jogo
                    }
                    else//Se ainda há tentativas
                    {
                        bloqueado = true;//Bloqueia todas as cartas
                        flipTimer.Tag = pos;//Coloca na Tag de flipTimer, a posição das cartas a serem escondidas
                        flipTimer.Start();
                    }
                }
            }
        }

        /// <summary>
        /// Adiciona pontos tendo em conta a última vez que o método foi chamado
        /// </summary>
        public void Pontua()
        {
            TimeSpan intervalo = DateTime.Now - ultimoPar;//Há quanto tempo o método foi chamado
            int tempPontos = (int)(500 * (1 / intervalo.TotalSeconds));//Calcula os pontos com base no intervalo
            if (tempPontos < 100)//Não permite que sejam adicionados menos de 100 pontos
                pontos += 100;
            else
                pontos += tempPontos;
            ultimoPar = DateTime.Now;
            txtPontos.Text = pontos.ToString();
        }

        /// <summary>
        /// Para o relogio e prepara o próximo nivel
        /// </summary>
        private void Vitoria()
        {
            bloqueado=true;//Bloqueia todas as cartas
            clockTimer.Stop();//Para o relogio
            rec = new Recordes(horaInicio, jogador, dificuldade, tempo, pontos, nivel, true);//prepara o recorde para ser guardado ou enviado para o proximo nivel
            Achievements();//Verifica se desbloqueou alguma conquista
            //mostra butoes Novo Jogo e Continuar
            button3.Show();
            button4.Show();            
            
        }

        /// <summary>
        /// Para o relogio, mostra mensagem de derrota e prepara o recodrde para ser guardado.
        /// </summary>
        private void Derrota()
        {
            bloqueado = true;//Bloqueia todas as cartas
            clockTimer.Stop();//para o relogio
            button4.Show();//Mostra butao Novo Jogo
            //Se derrota foi no nivel 1, conta como derrota (Para classificações)
            bool vit = false;
            if (nivel > 1)
                vit = true;
            rec = new Recordes(horaInicio, jogador, dificuldade, tempo, pontos, nivel, vit);//Prepara recorde para ser gravado
            MessageBox.Show("Perdeu!");            
        }

        /// <summary>
        /// Remove uma tentativa
        /// </summary>
        private void DescontaTentativa()
        {  
            //Remove gráfico de coração do painel1
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
        /// Gera uma sequência de valores aleatórios aos pares, com n elementos.
        /// </summary>
        /// <param name="n">Dimensão do array a devolver.</param>
        /// <returns></returns>
        private int[] Distribui(int n)
        {
            //string x = "";
            List<int> lista=new List<int>();
            for(int i=0;i<n;i++)
            {
                lista.Add(i/2);
                //Cria lista com pares até n/2
                //ex: {0,0,1,1,2,2,3,3} para n=8
                //x += lista[i].ToString();
            }

            int[] array = new int[n];//array com a distribuição de valores final
            byte[] b = new byte[n];//array de numeros aleatórios
            RNGCryptoServiceProvider random = new RNGCryptoServiceProvider();
            random.GetBytes(b);//popula array b com bytes aleatórios
            for (int i = 0; i < n; i++)
            {
                array[i] = b[i] % (n / 2);//coloca na posição i de array um valor entre 0 e n/2, Baseado no numero aleatório de b
                if(lista.Contains(array[i]))
                {// se este número ainda se encontra disponível na lista, o valor matem-se no array e remove-se da lista
                    lista.Remove(array[i]);
                }
                else
                {//se o número já não estiver na lista(já foi usado 2 vezes), coloca nesta posição o ultimo valor da lista
                    array[i]=lista[lista.Count-1];
                    lista.RemoveAt(lista.Count - 1);
                }
            }            
            return array;

        }

        /// <summary>
        /// Verifica se requesitos de alguma conquista foram cumpridos
        /// </summary>
        public void Achievements()
        {
            Jogador j = Jogador.lista[jogador.ToUpper()];
            bool conquistas = false;//Se foram encontradas conquistas ou não

            foreach (Chievs c in Chievs.lista.Values)
            {
                //Só verifica requesitos se o jogador ainda não desbloqueou conquista
                if (!j.listaChievs.Keys.Contains(c.Codigo))
                {
                    //Verifica para os 3 tipos de Conquista
                    if (c.Codigo.StartsWith("PNT") && c.Condicao(pontos))//Só verifica condição com pontos se chiev se referir aos pontos (StartsWith("PNT")
                    {
                        j.listaChievs.Add(c.Codigo, rec.Data);//Cria relação Jogador/Conquista/Recorde      
                        conquistas = true;
                    }
                    else if (c.Codigo.StartsWith("NIV") && c.Condicao(nivel))
                    {
                        j.listaChievs.Add(c.Codigo, rec.Data);
                        conquistas = true;
                    }

                    if (c.Codigo.StartsWith("TMP") && nivel > 4 && c.Condicao(tempo))//Conquista relativa ao tempo só se verifica no nivel 5
                    {
                        j.listaChievs.Add(c.Codigo, rec.Data);
                        conquistas = true;
                    }
                }
            }
            if (conquistas)
                MessageBox.Show("Conquistas desbloqueadas!");


        }

        /// <summary>
        /// Guarda Recorde na lista e Grava para o ficheiro de Texto
        /// </summary>
        private void guarda()
        {
            if (!guardado && rec.Nivel > 0)//Se ainda não tiver guardado e houver dados para gravar(nivel>0)
            {
                Recordes.lista.Add(rec.Data, rec);//Adiciona recorde á lista
                rec.GravaRecorde(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\My Games\Memoria");
                Recordes.alterado = true;
                MessageBox.Show("Recorde Guardado com Sucesso");
                rep.gravaReplay();//Guarda Replay
                guardado = true;
            }
        }
    }
}
