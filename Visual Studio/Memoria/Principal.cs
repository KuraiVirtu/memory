using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZedGraph;
using System.IO;
using WMPLib;
using AxWMPLib;


namespace Memoria
{
    public partial class Principal : Form
    {
        string DataPath;//Caminho  para a pasta \Data        
        string myGamesPath;//Caminho para a pasta \My Games\Memoria
        int ctrl;//Controle da posição da lista de musica (Tema.Musicas[ctrl])

        public Principal()
        {
            DataPath = @"Data\";
            myGamesPath = DataPath;//Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\My Games\Memoria";

            //Cria as pastas \Data e \My Games\Memoria se ainda não existirem
            if(!Directory.Exists(myGamesPath))
                Directory.CreateDirectory(myGamesPath);
            if (!Directory.Exists(DataPath))
                Directory.CreateDirectory(DataPath);

            //Carrega as classes com os dados dos ficheiros respectivos
            Recordes.CarregaRecordes(myGamesPath);
            Jogador.CarregaJogadores(DataPath);
            Jogador.CarregaAchievs(myGamesPath);
            Chievs.CarregaChievs(DataPath);

            //Carrega o tema usado na ultima utilização ou o tema Defeito
            string tema=Tema.CarregaLista(myGamesPath);
            if (tema == "")
                Tema.TemaDefeito();
            else
                Tema.AbreTema(tema);

            InitializeComponent();
            //Inicia reprodução de musica se houver músicas na lista do tema
            if (Tema.Musicas.Count > 0)
            {
                ctrl = 0;
                Player.URL = Tema.Musicas[ctrl];
                ctrl++;
                Player.Ctlcontrols.play();
            }
            timer1.Start();//Verifica se a música está parada e eavança a lista
        }
        
        private void Principal_Load(object sender, EventArgs e)
        {
            //Mostra a janela de login
            Login ini = new Login();
            ini.MdiParent = this;
            ini.Dock = DockStyle.Fill;

            ini.Show();

        }

        private void Principal_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Grava as classes, quando a aplicação fecha
            Recordes.GravaRecordes(myGamesPath);
            Jogador.GravaAchievs(myGamesPath);
            Tema.GravaLista(myGamesPath);
            Jogador.GravaJogadores(DataPath);
            Chievs.GravaChievs(DataPath);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //Se o WMPlayer estiver parado(Stopped) ou recebeu um ficheiro que não consegue reproduzir(Ready),
            //avança a posição da lista de Musica (caso esta não esteja vazia)
            if (Player.playState == WMPPlayState.wmppsStopped || Player.playState == WMPPlayState.wmppsReady)
            {
                if (Tema.Musicas.Count > 0)
                {
                    if (ctrl >= Tema.Musicas.Count)//Caso a lista tenha atingido o fim
                        ctrl = 0;//Coloca o controle da lista na posição inicial
                    Player.URL = Tema.Musicas[ctrl];
                    ctrl++;
                }               
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {//PictureBox presente em toda a aplicação para desligar e ligar a música
            if (!Player.settings.mute)
            {
                Player.settings.mute = true;
                pictureBox1.Image = Properties.Resources.Sound_Off;
            }
            else
            {
                Player.settings.mute = false;
                pictureBox1.Image = Properties.Resources.Sound_On;
            }


        }

        /// <summary>
        /// Altera a imagem de fundo de todos os formulários.
        /// </summary>
        public void updateBackgrounds()
        {
            foreach(Form f in this.MdiChildren)
            {
                f.BackgroundImage = Tema.BackGround;
            }
        }

        
        
    }
}
