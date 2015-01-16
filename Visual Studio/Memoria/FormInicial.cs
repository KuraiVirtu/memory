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
    public partial class FormInicial : Form
    {
        string jogador;

        public FormInicial(string Jogdr)
        {
            InitializeComponent();
            this.BackgroundImage = Tema.BackGround;
            jogador = Jogdr;
            label1.Text =Jogdr;
        }

    //Novo Jogo
        private void btnNovoJogo_Click(object sender, EventArgs e)
        {
            //Mostra as Opçoes de Jogo
            btnFacil.Show();
            btnMedio.Show();
            btnDificil.Show();
        }

    //Jogo Fácil
        private void btnFacil_Click(object sender, EventArgs e)
        {
            IniciaJogo("Fácil");
        }

    //Jogo Médio
        private void btnMedio_Click(object sender, EventArgs e)
        {
            IniciaJogo("Média");
        }

    //Jogo Dificil
        private void btnDificil_Click(object sender, EventArgs e)
        {
            IniciaJogo("Difícil");
        }

        /// <summary>
        /// Inicializa um novo jogo, no FormMemoria, com a dificuldade Designada
        /// </summary>
        /// <param name="dificuldade">A dificuldade do jogo a iniciar</param>
        private void IniciaJogo(string dificuldade)
        {
            Recordes r = new Recordes(DateTime.Now, jogador, dificuldade, TimeSpan.Zero, 0, 0, false);
            FormMemoria x = new FormMemoria(r,new Replay(r));

            x.MdiParent = this.MdiParent;
            x.Dock = DockStyle.Fill;
            btnFacil.Hide();
            btnMedio.Hide();
            btnDificil.Hide();
            x.Show();
        }

    //Classificações
        private void button1_Click(object sender, EventArgs e)
        {
            FormRecordes rec = new FormRecordes();
            rec.MdiParent = this.MdiParent;
            rec.Dock = DockStyle.Fill;
            rec.Show();
        }

    //Mudar Jogador
        private void button2_Click(object sender, EventArgs e)
        {
            Login l = new Login();
            l.MdiParent = this.MdiParent;
            l.Dock = DockStyle.Fill;
            l.Show();
            this.Close();
        }

    //Conquistas
        private void button3_Click(object sender, EventArgs e)
        {
            FormChiev f = new FormChiev(jogador);
            f.MdiParent = this.MdiParent;
            f.Dock = DockStyle.Fill;            
            f.Show();
        }

    //Temas
        private void button4_Click(object sender, EventArgs e)
        {
            FormTemas f = new FormTemas();
            f.MdiParent = this.MdiParent;
            f.Dock = DockStyle.Fill;
            f.Show();
        }

    //Replays
        private void button5_Click(object sender, EventArgs e)
        {
            FormReplay f = new FormReplay();
            f.MdiParent = this.MdiParent;
            f.Dock= DockStyle.Fill;
            f.Show();
        }
    }
}
