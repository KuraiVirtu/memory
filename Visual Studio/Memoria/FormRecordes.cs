using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZedGraph;



namespace Memoria
{
    public partial class FormRecordes : Form
    {

        Recordes[] records; //Array que irá conter todos os recordes, ordenados por pontos
        string[] jogadores; //Array que irá conter o nome dos jogadores presentes nas comboboxes (Tab3)
        Color[] cores; //Array que define algumas cores para serem apresentadas nos gráficos (Tab3)

        public FormRecordes()
        {
            InitializeComponent();
            this.BackgroundImage = Tema.BackGround;
            //Inicializa os 3 arrays
            records = Stats.Sort();
            jogadores = new string[5];
            cores = new Color[5];
            
        }

        private void FormRecordes_Load(object sender, EventArgs e)
        {
            if (Recordes.alterado) //Só faz UpdateJogadores() se Recordes.lista foi alterado desde a última vez
                Stats.UpdateStatsJogadores();

            foreach (Jogador j in Jogador.lista.Values)
            {//Adiciona o nome dos jogadores a todas as ComboBoxes onde estes são necessários
                comboBox2.Items.Add(j.Nome);

                comboBox10.Items.Add(j.Nome);

                comboBox4.Items.Add(j.Nome);
                comboBox5.Items.Add(j.Nome);
                comboBox6.Items.Add(j.Nome);
                comboBox7.Items.Add(j.Nome);
                comboBox8.Items.Add(j.Nome);
            }
            
            //Selecciona os itens seleccionados de cada ComboBox do index desejado
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;

            comboBox9.SelectedIndex = 0;
            comboBox10.SelectedIndex = 0;

            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 1;
            try
            {
                comboBox5.SelectedIndex = 2;
                comboBox6.SelectedIndex = 3;
                comboBox7.SelectedIndex = 4;
                comboBox8.SelectedIndex = 5;
            }
            catch { }

            //Preenche os arrays com valores (Tab3)
            updateArrays(null, null);             
        }

//Tab1       
        /// <summary>
        /// Fecha o Formulário
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Actualiza a listView1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            update_ListBox1();
        }

        /// <summary>
        /// Determina os filtros seleccionados pelo utilizador e chama o método de listagem correspondente
        /// </summary>
        private void update_ListBox1()
        {
            if (comboBox1.Text != null)
            {
                if (comboBox2.Text != null)
                {
                    if (comboBox1.Text == "Todas" && comboBox2.Text == "Todos")
                    { lista_Todas_Vit(); }
                    else if (comboBox1.Text == "Todas")
                        lista_Jogador_Vit(comboBox2.Text);
                    else if (comboBox2.Text == "Todos")
                        lista_Dificuldade_Vit(comboBox1.Text);
                    else
                        lista_Jogador_Dificuldade_Vit(comboBox2.Text, comboBox1.Text);
                }
                else if (comboBox1.Text == "Todas")
                { lista_Todas_Vit();  }
                else
                { lista_Dificuldade_Vit(comboBox1.Text); }
            }
            else if (comboBox2.Text != null)
            {
                if (comboBox2.Text == "Todos")
                    lista_Todas_Vit();
                else
                    lista_Jogador_Vit(comboBox2.Text);
            }
        }

    //Listagens
        /// <summary>
        /// Lista todas as vitórias dos jogadores à listView1
        /// </summary>
        private void lista_Todas_Vit()
        {
            listView1.Items.Clear();
            foreach (Recordes r in records)
            {
                if (r.Vitoria)
                {
                    DateTime t = new DateTime(r.Tempo.Ticks);
                    string[] s = new string[4];
                    s[0] = r.NomeJogador; s[1] = r.Pontuacao.ToString(); s[2] = t.ToString("mm:ss"); s[3] = r.Dificuldade;
                    ListViewItem item = new ListViewItem(s);
                    listView1.Items.Add(item);
                }
            }
        }

        /// <summary>
        /// Lista todas as vitórias dos jogadores, cuja dificuldade é a especificada, à listView1.
        /// </summary>
        /// <param name="dif">A string que identifica a dificuldade especificada.</param>
        private void lista_Dificuldade_Vit(string dif)
        {
            listView1.Items.Clear();
            foreach (Recordes r in records)
            {
                if (r.Vitoria == true && r.Dificuldade == dif)
                {
                    DateTime t = new DateTime(r.Tempo.Ticks);
                    string[] s = new string[4];
                    s[0] =r.NomeJogador; s[1] = r.Pontuacao.ToString(); s[2] = t.ToString("mm:ss"); s[3] = r.Dificuldade;
                    ListViewItem item = new ListViewItem(s);
                    listView1.Items.Add(item);
                }
            }
        }

        /// <summary>
        /// Lista todas as vitórias do jogador especificado à listView1.
        /// </summary>
        /// <param name="jogador">A string que identifica o jogador especificado.</param>
        private void lista_Jogador_Vit(string jogador)
        {
            listView1.Items.Clear();
            foreach (Recordes r in records)
            {
                if (r.Vitoria == true && r.NomeJogador == jogador)
                {
                    DateTime t = new DateTime(r.Tempo.Ticks);
                    string[] s = new string[4];
                    s[0] = r.NomeJogador; s[1] = r.Pontuacao.ToString(); s[2] = t.ToString("mm:ss"); s[3] = r.Dificuldade;
                    ListViewItem item = new ListViewItem(s);
                    listView1.Items.Add(item);
                }
            }
        }

        /// <summary>
        /// Lista todas as vitórias do jogador, cuja dificuldade é a especificada, à listView1.
        /// </summary>
        /// <param name="jogador">A string que identifica o jogador.</param>
        /// <param name="dif">A string que identifica a dificuldade especificada.</param>
        private void lista_Jogador_Dificuldade_Vit(string jogador, string dif)
        {
            listView1.Items.Clear();
            foreach (Recordes r in records)
            {
                if (r.Vitoria==true && r.NomeJogador == jogador && r.Dificuldade == dif)
                {
                    DateTime t = new DateTime(r.Tempo.Ticks);
                    string[] s = new string[4];
                    s[0] = r.NomeJogador; s[1] = r.Pontuacao.ToString(); s[2] = t.ToString("mm:ss"); s[3] = r.Dificuldade;
                    ListViewItem item = new ListViewItem(s);
                    listView1.Items.Add(item);
                }
            }
        }

//Tab2
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            update_ListBox2();
        }

        /// <summary>
        /// Determina os filtros seleccionados pelo utilizador e chama o método de listagem correspondente
        /// </summary>
        private void update_ListBox2()
        {
            
            if (comboBox9.Text != null)
            {
                if (comboBox10.Text != null)
                {
                    if (comboBox9.Text == "Todas" && comboBox10.Text == "Todos")
                    { lista_Todas_Der(); }
                    else if (comboBox9.Text == "Todas")
                        lista_Jogador_Der(comboBox10.Text);
                    else if (comboBox10.Text == "Todos")
                        lista_Dificuldade_Der(comboBox9.Text);
                    else
                        lista_Jogador_Dificuldade_Der(comboBox10.Text, comboBox9.Text);
                }
                else if (comboBox9.Text == "Todas")
                { lista_Todas_Der(); }
                else
                { lista_Dificuldade_Der(comboBox9.Text); }
            }
            else if (comboBox10.Text != null)
            {
                if (comboBox10.Text == "Todos")
                    lista_Todas_Der();
                else
                    lista_Jogador_Der(comboBox10.Text);
            }
        }

    //Listagens
        /// <summary>
        /// Lista todas as vitórias dos jogadores à listView1
        /// </summary>
        private void lista_Todas_Der()
        {
            listView2.Items.Clear();
            foreach (Recordes r in records)
            {
                if (!r.Vitoria)
                {
                    DateTime t = new DateTime(r.Tempo.Ticks);
                    string[] s = new string[4];
                    s[0] = r.NomeJogador; s[1] = r.Pontuacao.ToString(); s[2] = t.ToString("mm:ss"); s[3] = r.Dificuldade;
                    ListViewItem item = new ListViewItem(s);
                    listView2.Items.Add(item);

                    
                }
            }
        }

        /// <summary>
        /// Lista todas as vitórias dos jogadores, cuja dificuldade é a especificada, à listView1.
        /// </summary>
        /// <param name="dif">A string que identifica a dificuldade especificada.</param>
        private void lista_Dificuldade_Der(string dif)
        {
            listView2.Items.Clear();
            foreach (Recordes r in records)
            {
                if (!r.Vitoria && r.Dificuldade == dif)
                {
                    DateTime t = new DateTime(r.Tempo.Ticks);
                    string[] s = new string[4];
                    s[0] = r.NomeJogador; s[1] = r.Pontuacao.ToString(); s[2] = t.ToString("mm:ss"); s[3] = r.Dificuldade;
                    ListViewItem item = new ListViewItem(s);
                    listView2.Items.Add(item);
                }
            }
        }

        /// <summary>
        /// Lista todas as vitórias do jogador especificado à listView1.
        /// </summary>
        /// <param name="jogador">A string que identifica o jogador especificado.</param>
        private void lista_Jogador_Der(string jogador)
        {
            listView2.Items.Clear();
            foreach (Recordes r in records)
            {
                if (!r.Vitoria && r.NomeJogador == jogador)
                {
                    DateTime t = new DateTime(r.Tempo.Ticks);
                    string[] s = new string[4];
                    s[0] = r.NomeJogador; s[1] = r.Pontuacao.ToString(); s[2] = t.ToString("mm:ss"); s[3] = r.Dificuldade;
                    ListViewItem item = new ListViewItem(s);
                    listView2.Items.Add(item);
                }
            }
        }

        /// <summary>
        /// Lista todas as vitórias do jogador, cuja dificuldade é a especificada, à listView1.
        /// </summary>
        /// <param name="jogador">A string que identifica o jogador.</param>
        /// <param name="dif">A string que identifica a dificuldade especificada.</param>
        private void lista_Jogador_Dificuldade_Der(string jogador, string dif)
        {
            listView2.Items.Clear();
            foreach (Recordes r in records)
            {
                if (!r.Vitoria && r.NomeJogador == jogador && r.Dificuldade == dif)
                {
                    DateTime t = new DateTime(r.Tempo.Ticks);
                    string[] s = new string[4];
                    s[0] = r.NomeJogador; s[1] = r.Pontuacao.ToString(); s[2] = t.ToString("mm:ss"); s[3] = r.Dificuldade;
                    ListViewItem item = new ListViewItem(s);
                    listView2.Items.Add(item);
                }
            }
        }        

//Tab3
        /// <summary>
        /// Carrega Cores[] e jogadores[] com os valores necessários.
        /// </summary>
        private void updateArrays(object sender, EventArgs e)
        {
            jogadores[0] = (string)comboBox4.SelectedItem;
            jogadores[1] = (string)comboBox5.SelectedItem;
            jogadores[2] = (string)comboBox6.SelectedItem;
            jogadores[3] = (string)comboBox7.SelectedItem;
            jogadores[4] = (string)comboBox8.SelectedItem;
            cores[0] = Color.Cyan;
            cores[1] = Color.LightGreen;
            cores[2] = Color.IndianRed;
            cores[3] = Color.GreenYellow;
            cores[4] = Color.Blue;
            escolheCalculo(null,null);            
        }        
        
        /// <summary>
        /// Bloqueia os radioButtons cujas opções não devem estar disponiveis.
        /// </summary>        
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((string)comboBox3.SelectedItem == "Níveis")
            {
                rdOrgTot.Enabled = false;
                rdOrgMed.Enabled = true;
                rdOrgMax.Enabled = true;
                rdOrgMed.Checked = true;                
            }
            else if ((string)comboBox3.SelectedItem == "Nº Jogos")
            {
                rdOrgTot.Enabled = true;
                rdOrgMed.Enabled = false;
                rdOrgMax.Enabled = false;
                rdOrgTot.Checked = true;
            }
            else
            {
                rdOrgTot.Enabled = true;
                rdOrgMed.Enabled = true;
                rdOrgMax.Enabled = true;
                rdOrgTot.Checked = true; 
            }
            escolheCalculo(null, null);
        }

        /// <summary>
        /// Determina qual o método de cálculo que deve ser chamado para o desenho do gráfico.
        /// </summary>
        private void escolheCalculo(object sender, EventArgs e)
        {
            int org; int dif; int gra;
            if (rdOrgMax.Checked)
                org = int.Parse((string)rdOrgMax.Tag);
            else if (rdOrgMed.Checked)
                org = int.Parse((string)rdOrgMed.Tag);
            else
                org = int.Parse((string)rdOrgTot.Tag);

            if (rdDifDif.Checked)
                dif = int.Parse((string)rdDifDif.Tag);
            else if (rdDifMed.Checked)
                dif = int.Parse((string)rdDifMed.Tag);
            else if (rdDifFac.Checked)
                dif = int.Parse((string)rdDifFac.Tag);
            else
                dif = int.Parse((string)rdDifTod.Tag);

            if (rdGraBar.Checked)
                gra = int.Parse((string)rdGraBar.Tag);
            else
                gra = int.Parse((string)rdGraCir.Tag);

            ComboBox c=comboBox3;
            if (c.SelectedItem == c.Items[0])
                calcPontos(org, dif, gra);
            else if (c.SelectedItem == c.Items[1])
                calcNiveis(org, dif, gra);
            else if (c.SelectedItem == c.Items[2])
                calcTempo(org, dif, gra);
            else
                calcNumJogos( dif, gra);
        }

    //Cálculos
        /// <summary>
        /// De acordo com os parâmetros passados, calcula média, total ou máximo de pontos e chama o método de
        /// desenho de gráficos apropriado.
        /// </summary>
        /// <param name="org">Determina se calcula Total(1), Média(2) ou Máximo(3).</param>
        /// <param name="dif">Determina a dificuldade a ser considerada nos dados.
        /// <para>Todas - 0; Fácil - 1; Média - 2; Difícil - 3;</para></param>
        /// <param name="gra">Determina se é para desenhar gráfico de barras(1) ou circular(2)</param>
        private void calcPontos(int org, int dif,int gra)
        {
            double[] valores=new double[5];
            string titulo="";
            switch (org)
            {
                case 1:
                    for (int i = 0; i < 5; i++)
                    {
                        if (jogadores[i] != null && jogadores[i] != "")
                        {
                            valores[i] = Jogador.lista[jogadores[i].ToUpper()].TotPontos[dif];
                        }
                    }
                    titulo+="Total";
                    break;
                case 2:
                    for(int i=0;i<5;i++)
                    {
                        if (jogadores[i] != null && jogadores[i] != "")
                        {
                            Jogador j = Jogador.lista[jogadores[i].ToUpper()];
                            int tot = j.Vitorias[dif] + j.Derrotas[dif];
                            if (tot < 1) tot = 1; //impede divisão por 0
                            valores[i] = j.TotPontos[dif] / tot;
                        }
                    }
                    titulo+="Média";
                    break;
                case 3:
                    for (int i = 0; i < 5; i++)
                    {
                        if (jogadores[i] != null && jogadores[i] != "")
                        {
                            valores[i] = Jogador.lista[jogadores[i].ToUpper()].MaximoPontos[dif];
                        }
                    }
                    titulo+="Máximo";
                    break;                   
            }

            titulo += " de Pontos por Jogador\n";
            if (dif == 0)
                titulo += "em Todas as Dificuldades";
            else if (dif == 1)
                titulo += "na Dificuldade Fácil";
            else if(dif==2)
                titulo += "na Dificuldade Média";
            else if(dif==3)
                titulo += "na Dificuldade Difícil";

            if (gra == 1)
                desenhaBarGraph(valores,titulo);
            else
                desenhaPieGraph(valores, titulo);
        }

        /// <summary>
        /// De acordo com os parâmetros passados, calcula média ou máximo de níveis atingidos e chama o método de
        /// desenho de gráficos apropriado.
        /// </summary>
        /// <param name="org">Determina se calcula Média(2) ou Máximo(3).</param>
        /// <param name="dif">Determina a dificuldade a ser considerada nos dados.
        /// <para>Todas - 0; Fácil - 1; Média - 2; Difícil - 3;</para></param>
        /// <param name="gra">Determina se é para desenhar gráfico de barras(1) ou circular(2)</param>
        private void calcNiveis(int org, int dif, int gra)
        {
            double[] valores = new double[5];
            string titulo = "";
            switch (org)
            {
                
                case 2:
                    for (int i = 0; i < 5; i++)
                    {
                        if (jogadores[i] != null && jogadores[i] != "")
                        {
                            Jogador j = Jogador.lista[jogadores[i].ToUpper()];
                            int tot = j.Vitorias[dif] + j.Derrotas[dif];
                            if (tot < 1) tot = 1; //impede divisão por 0
                            valores[i] = j.TotNiveis[dif] / tot;                            
                        }
                    }
                    titulo += "Média";
                    break;
                case 3:
                    for (int i = 0; i < 5; i++)
                    {
                        if (jogadores[i] != null && jogadores[i] != "")
                        {
                            valores[i] = Jogador.lista[jogadores[i].ToUpper()].MaximoNivel[dif];
                        }
                    }
                    titulo += "Máximo";
                    break;
            }

            titulo += " de Nível Atingido por Jogador\n";
            if (dif == 0)
                titulo += "em Todas as Dificuldades";
            else if (dif == 1)
                titulo += "na Dificuldade Fácil";
            if (dif == 2)
                titulo += "na Dificuldade Média";
            else if (dif == 3)
                titulo += "na Dificuldade Difícil";
                
            if (gra == 1)
                desenhaBarGraph(valores, titulo);
            else
                desenhaPieGraph(valores, titulo);
        }

        /// <summary>
        /// De acordo com os parâmetros passados, calcula média, total ou máximo de tempo de jogo e chama o método de
        /// desenho de gráficos apropriado.
        /// </summary>
        /// <param name="org">Determina se calcula Total(1), Média(2) ou Máximo(3).</param>
        /// <param name="dif">Determina a dificuldade a ser considerada nos dados.
        /// <para>Todas - 0; Fácil - 1; Média - 2; Difícil - 3;</para></param>
        /// <param name="gra">Determina se é para desenhar gráfico de barras(1) ou circular(2)</param>
        private void calcTempo(int org, int dif, int gra)
        {
            double[] valores = new double[5];
            string titulo = "";
            switch (org)
            {
                case 1:
                    for (int i = 0; i < 5; i++)
                    {
                        if (jogadores[i] != null && jogadores[i] != "")
                        {
                            Jogador j = Jogador.lista[jogadores[i].ToUpper()];
                            valores[i] = (double)j.TempoJogado[dif].TotalMinutes;
                        }
                    }
                    titulo += "Total";
                    break;
                case 2:
                    for (int i = 0; i < 5; i++)
                    {
                        if (jogadores[i] != null && jogadores[i] != "")
                        {
                            Jogador j = Jogador.lista[jogadores[i].ToUpper()];
                            int tot = j.Vitorias[dif] + j.Derrotas[dif];
                            if (tot < 1) tot = 1; //impede divisão por 0
                            valores[i] = j.TempoJogado[dif].TotalMinutes / tot;
                        }
                    }
                    titulo += "Média";
                    break;
                case 3:
                    for (int i = 0; i < 5; i++)
                    {
                        if (jogadores[i] != null && jogadores[i] != "")
                        {
                            valores[i] = Jogador.lista[jogadores[i].ToUpper()].MaximoTempo[dif].TotalMinutes;
                        }
                    }
                    titulo += "Máximo";
                    break;
            }

            titulo += " de Tempo Jogado por Jogador\n";
            if (dif == 0)
                titulo += "em Todas as Dificuldades";
            else if (dif == 1)
                titulo += "na Dificuldade Fácil";
            else if (dif == 2)
                titulo += "na Dificuldade Média";
            else if (dif == 3)
                titulo += "na Dificuldade Difícil";

            if (gra == 1)
                desenhaBarGraph(valores, titulo);
            else
                desenhaPieGraph(valores, titulo);
        }

        /// <summary>
        /// De acordo com os parâmetros passados, total de jogos efectuados e chama o método de
        /// desenho de gráficos apropriado.
        /// </summary>
        /// <param name="dif">Determina a dificuldade a ser considerada nos dados.
        /// <para>Todas - 0; Fácil - 1; Média - 2; Difícil - 3;</para></param>
        /// <param name="gra">Determina se é para desenhar gráfico de barras(1) ou circular(2)</param>
        private void calcNumJogos(int dif, int gra)
        {
            double[] valores = new double[5];
            string titulo = "";
            
            for (int i = 0; i < 5; i++)
            {
                if (jogadores[i] != null && jogadores[i] != "")
                {
                    Jogador j = Jogador.lista[jogadores[i].ToUpper()];
                    valores[i] = j.Vitorias[dif] + j.Derrotas[dif];
                }
            }
            titulo += "Total";


            titulo += " de Jogos Efectuados por Jogador\n";
            if(dif==0)
                titulo += "em Todas as Dificuldades";
            else if (dif == 1)
                titulo += "na Dificuldade Fácil";
            else if (dif == 2)
                titulo += "na Dificuldade Média";
            else if (dif == 3)
                titulo += "na Dificuldade Difícil";

            if (gra == 1)
                desenhaBarGraph(valores, titulo);
            else
                desenhaPieGraph(valores, titulo);
        }

    //Desenho dos Gráficos
        /// <summary>
        /// Desenha um gráfico circular de acordo com os dados passados.
        /// </summary>
        /// <param name="valores">Um array double[] com os valores a serem representados no gráfico </param>
        /// <param name="titulo">Titulo a ser apresentado no gráfico</param>
        private void desenhaPieGraph(double[] valores, string titulo)
        {
            GraphPane g = new GraphPane(new RectangleF(5, 5, 450, 450), titulo, "", "");
            MasterPane m = zedGraphControl1.MasterPane;
            m.PaneList.Clear();

            int t = valores.Length;
            for (int i = 0; i < t; i++)
            {
                if (jogadores[i] != null && jogadores[i] != "")
                {
                    PieItem pie = g.AddPieSlice(valores[i], cores[i], 0, jogadores[i]);
                    pie.LabelType = PieLabelType.Name_Value_Percent;
                }
            }

            g.Legend.IsHStack = false;
            g.AxisChange();

            m.Add(g);
            m.SetLayout(this.CreateGraphics(), PaneLayout.ForceSquare);
            m.AxisChange();
            zedGraphControl1.Invalidate();


        }

        /// <summary>
        /// Desenha um gráfico de barras de acordo com os dados passados.
        /// </summary>
        /// <param name="valores">Um array double[] com os valores a serem representados no gráfico </param>
        /// <param name="titulo">Titulo a ser apresentado no gráfico</param>
        private void desenhaBarGraph(double[] valores, string titulo)
        {
            GraphPane g = new GraphPane(new RectangleF(5, 5, 450, 450), titulo, "", "");
            MasterPane m = zedGraphControl1.MasterPane;
            m.PaneList.Clear();

            int t = valores.Length;
            for (int i = 0; i < t; i++)
            {
                if (jogadores[i] != null && jogadores[i] != "")
                {
                    PointPairList p = new PointPairList();
                    p.Add(i, valores[i]);
                    BarItem b = g.AddBar(jogadores[i], p, cores[i]);


                }
            }
            g.XAxis.Type = AxisType.Text;

            g.Legend.IsHStack = false;
            g.AxisChange();

            m.Add(g);
            m.SetLayout(this.CreateGraphics(), PaneLayout.ForceSquare);
            m.AxisChange();
            zedGraphControl1.Invalidate();

        }

       
        

        

        
    }
}
