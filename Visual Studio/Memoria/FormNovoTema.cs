using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using AxWMPLib;
using WMPLib;

namespace Memoria
{
    public partial class FormNovoTema : Form
    {
        string[] paths;//Caminhos para os ficheiros das cartas
        string[] musicas;//Caminho spara os ficheiros das Musicas
        string capa;//Caminho para o ficheiro das capa
        string background;//Caminho para o ficheiro da imagem de fundo
        int numCartas;//Variavel de controle com o numero de cartas carregadas
        int numMusicas;//Variavel de controle com o numero de cartas carregadas
        FormTemas parent;//Referencia para a form FormTema
        Carta carta;//A carta que será apresentada na previsualização

        public FormNovoTema(FormTemas f)
        {
            paths = new string[13];
            musicas = new string[5];
            numCartas = 0;
            numMusicas = 0;
            parent = f;            
            InitializeComponent();
            carta = new Carta(null,Properties.Resources.Capa, new Rectangle(panel1.Location, panel1.Size));//Cria a carta 
            carta.Desenha(panel1);//Desenha a carta
            carta.Butao.Dock = DockStyle.Fill;
            carta.MostraCarta();//mostra a imgagem (null) da carta
            carta.Butao.Click += new EventHandler(this.btnCarta_Click);//Adiciona evento click ao butao da carta
        }
        
//Cartas
    //Procura Cartas (...)
        private void button1_Click(object sender, EventArgs e)
        {
            if (numCartas < 13)//Não permite adicionar mais de 13 cartas
            {
                bool sucesso = true;

                OpenFileDialog abrir = new OpenFileDialog();
                abrir.Multiselect = true;//Permite seleccionar vários ficheiros duma vez
                abrir.Filter = "JPEG (*.jpeg)|*.jpeg|PNG (*.png)|*.png|JPG (*.jpg)|*.jpg|GIF (*.gif)|*.gif";
                DialogResult res = abrir.ShowDialog();
                if (res == DialogResult.OK)
                {
                    foreach (string s in abrir.FileNames)
                    {
                        if (numCartas < 13)//Não permite adicionar mais de 13 cartas
                        {
                            if (verificaImagem(s))//Verifica se a imagem é permitida
                            {
                                paths[numCartas] = s;//Adiciona a imagem
                                numCartas++;
                            }
                            else
                            sucesso = false;
                        }
                        
                    }
                    if(!sucesso)
                        MessageBox.Show("Erro a adiconar imagem!\nVerifique que ficheiro é uma imagem e que não é maior que 1MB", "Capa Inválida",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    UpdateListBox1(0);
                }
                
            }
        }

    //Remove Capa
        private void button8_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            carta.Capa = Properties.Resources.Capa;
            UpdateCarta();//Actualiza a imagem/capa da pre-visualização
        }

    //Procura Capa (...)
        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog abrir = new OpenFileDialog();
            abrir.Filter = "JPEG (*.jpeg)|*.jpeg|PNG (*.png)|*.png|JPG (*.jpg)|*.jpg|GIF (*.gif)|*.gif";
            if (abrir.ShowDialog() == DialogResult.OK)
            {
                if (verificaImagem(abrir.FileName))//Verifica se imagem é permitida
                {
                    capa = abrir.FileName;
                    carta.Capa = Image.FromFile(capa);
                    UpdateCarta();//Actualiza a imagem/capa da pre-visualização
                    textBox2.Text = capa;
                }
                else
                {
                    MessageBox.Show("Erro a adiconar imagem!\nVerifique que ficheiro é uma imagem e que não é maior que 1MB", "Capa Inválida",
                     MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

    //Remove Carta
        private void button12_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                int temp = listBox1.SelectedIndex;//guarda o indice da carta a remover
                //Desloca os valores do array uma posição para o indice da carta removida
                for (int i = temp; i < 12; i++)
                {
                    paths[i] = paths[i + 1];
                }
                paths[12] = null;
                numCartas--;
                UpdateListBox1(temp - 1);
            }
        }
        
    //Seleciona Imagem para Pre-Visualizar (da ListBox)
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            carta.Imagem = Image.FromFile(paths[listBox1.SelectedIndex]);
            UpdateCarta(); //Actualiza imagem do butao da previsualização
            
        }

    //Alterna Pre-visualização de imagem e capa
        private void btnCarta_Click(object sender, EventArgs e)
        {
            if (!carta.Virada)
            {
                carta.MostraCarta();
            }
            else
            {
                carta.EscondeCarta();
            }
        }    

    //Drag Drop Cartas (Aceitar ficheiros por arrastamento)
        private void listBox1_DragDrop(object sender, DragEventArgs e)
        {
            //Carrega string[] p com os caminhos dos ficheiros arrastados
            string[] p = (string[])e.Data.GetData(DataFormats.FileDrop);
            bool sucesso = true; ;

            foreach (string s in p)
            {
                if (numCartas < 13)//Não permite mais de 13 cartas
                {
                    if (verificaImagem(s))//Verifica se a imagem é válida
                    {
                        paths[numCartas] = s;//Adiciona imagem
                        numCartas++;
                    }
                    else
                        sucesso = false;
                }
                
            }
            if (!sucesso)
                MessageBox.Show("Erro a adiconar imagens!\nVerifique que os ficheiros são imagens e que têm menos de 1MB.", "Cartas Inválidas",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            UpdateListBox1(0);
        }
        
        private void listBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy; 
            else
                e.Effect = DragDropEffects.None; 
        }

    //Drag Drop Capa (Aceitar ficheiros por arrastamento)
        private void textBox2_DragDrop(object sender, DragEventArgs e)
        {
            string[] p = (string[])e.Data.GetData(DataFormats.FileDrop);
            if(verificaImagem(p[0]))//Verifica se a imagem é válida
            {
                capa = p[0];
                carta.Capa = Image.FromFile(capa);//Capa da pre-visualização passa a ser a adicionada
                textBox2.Text = p[0];
                UpdateCarta();
            }
            else
              MessageBox.Show("Erro a adiconar imagem!\nVerifique que ficheiro é uma imagem e que não é maior que 1MB", "Capa Inválida",
                  MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            
        }

        private void textBox2_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

//BackGround
    //Drag Drop Background
        private void textBox3_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void textBox3_DragDrop(object sender, DragEventArgs e)
        {
            string[] p = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (verificaImagem(p[0]))//Verifica se a imagem é válida
            {
                background = p[0];
                textBox3.Text = p[0];
                this.BackgroundImage = Image.FromFile(background);//Altera imagem de fundo para pre-visualizaçao
            }
            else
                MessageBox.Show("Erro a adiconar imagem!\nVerifique que ficheiro é uma imagem e que não tem mais de 1MB", "Background Inválido",
                  MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            
        }

    //Procura BackGround (...)
        private void button7_Click(object sender, EventArgs e)
        {
            OpenFileDialog abrir = new OpenFileDialog();
            abrir.Filter = "JPEG (*.jpeg)|*.jpeg|PNG (*.png)|*.png|JPG (*.jpg)|*.jpg|GIF (*.gif)|*.gif";
            if (abrir.ShowDialog() == DialogResult.OK)
            {
                if (verificaImagem(abrir.FileName))
                {
                    background = abrir.FileName;
                    this.BackgroundImage = Image.FromFile(background);
                    textBox3.Text = background;
                }
                else
                    MessageBox.Show("Erro a adiconar imagem!\nVerifique que ficheiro é uma imagem e que não tem mais de 1MB", "Background Inválido",
                  MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

    //Remove BackGround
        private void button9_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
            background = null;
            BackgroundImage = null;
        }

//Musica
    //Procurar Musica (...)
        private void button4_Click(object sender, EventArgs e)
        {
            if (numMusicas < 5)//Não permite mais de 5 musicas
            {
                OpenFileDialog abrir = new OpenFileDialog();
                abrir.Multiselect = true;
                abrir.Filter = "MP3 (*.mp3)|*.mp3|wav (*.wav)|*.wav|wma (*.wma)|*.wma";
                DialogResult res = abrir.ShowDialog();
                if (res == DialogResult.OK)
                {
                    foreach (string s in abrir.FileNames)
                    {
                        if (numMusicas < 5)
                        {
                            musicas[numMusicas] = s;
                            numMusicas++;
                        }
                    }
                    UpdateListBox2(0);
                }
            }
        }

    //Drag Drop Musicas
        private void listBox2_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void listBox2_DragDrop(object sender, DragEventArgs e)
        {
            string[] p = (string[])e.Data.GetData(DataFormats.FileDrop);
            bool sucesso = true;

            Principal parent = (Principal)this.MdiParent;
            foreach (string s in p)
            {
                if (numMusicas < 5 )
                {
                    if (s.EndsWith(".mp3") || s.EndsWith(".wav") || s.EndsWith(".wma"))//Verifica se é ficheiro com extensão audio                        
                    {
                        listBox2.Items.Add(s);
                        musicas[numMusicas] = s;
                        numMusicas++;
                    }
                    else
                        sucesso = false;
                }
                else
                    break;
            }
            if (!sucesso)
                MessageBox.Show("Erro a adiconar ficheiros de som!\nVerifique que os ficheiros de som são válidos", "Ficheiros de Som Inválidos",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            UpdateListBox1(0);
        }

    //Remove Musicas
        private void button13_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex != -1)
            {
                int temp = listBox2.SelectedIndex;
                for (int i = temp; i < 4; i++)
                {
                    musicas[i] = musicas[i + 1];
                }
                musicas[4] = null;
                numMusicas--;
                UpdateListBox2(temp - 1);
            }
        }

    //Ouvir Musica
        private void button6_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex != -1)
            {
                int temp = listBox2.SelectedIndex;

                //Carrega referencia para form principal, para aceder ao Player e alterar a musica
                Principal prinnny = (Principal)this.MdiParent; 
                prinnny.Player.URL = musicas[temp];
                prinnny.Player.Ctlcontrols.play();
                button14.Enabled = true; //Permite parar a musica
            }
        }

    //Parar Musica
        private void button14_Click(object sender, EventArgs e)
        {
            //Carrega referencia para form principal, para aceder ao Player e alterar a musica
            Principal prinny = (Principal)this.MdiParent;
            prinny.Player.Ctlcontrols.stop();
            button14.Enabled = false; 
        }

    //Passar Musica para Cima
        private void button11_Click(object sender, EventArgs e)
        {
            //Altera a posição da musica na lista de musicas
            if (listBox2.SelectedIndex > 0)
            {
                int i = listBox2.SelectedIndex;
                string temp = musicas[i];
                musicas[i] = musicas[i - 1];
                musicas[i - 1] = temp;                          
                

                UpdateListBox2(i-1);
            }
        }

    //Passar Musica para Baixo
        private void button10_Click(object sender, EventArgs e)
        {
            //Altera a posição da musica na lista de musicas
            if (listBox2.SelectedIndex != -1 && listBox2.SelectedIndex<5)
            {
                int i = listBox2.SelectedIndex;
                string temp = musicas[i];
                musicas[i] = musicas[i + 1];
                musicas[i + 1] = temp;
                UpdateListBox2(i + 1);
            }
        }
        
//Formulário
    //Sair
        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
            parent.Show();
            parent.updateListBox();
        }

    //Load
        private void FormNovoTema_Load(object sender, EventArgs e)
        {
            this.BackgroundImage = Tema.BackGround;
        }

    //Verifica se tema está pronto a ser criado
        private void button2_Click(object sender, EventArgs e)
        {
            //Realiza várias verificações antes de criar o tema
            if (VerificaNome())
            {
                if (VerificaCartas())
                {
                    if (VerificaMusicas())
                    {
                        if (VerificaCapa())
                        {
                            if (VerificaBackGround())
                            {
                                CriaTema();//Cria o tema
                            }
                        }                       
                    }                   
                }               
            }            
        }

//Métodos
        /// <summary>
        /// Represente alterações da lista de paths na listbox.
        /// </summary>
        /// <param name="selected">O indice que ficará seleccionado depois do update</param>
        private void UpdateListBox1(int selected)
        {
            listBox1.Items.Clear();
            for (int i = 0; i < 13; i++)
            {
                string s = paths[i];
                if (s != null)
                    listBox1.Items.Add(s);
            }

            if (selected < 0)
                selected = 0;
            if (listBox1.Items.Count > 0)
                listBox1.SetSelected(selected, true);
        }

        /// <summary>
        /// Represente alterações da lista de musicas na listbox.
        /// </summary>
        /// <param name="selected">O indice que ficará seleccionado depois do update</param>
        private void UpdateListBox2(int selected)
        {
            listBox2.Items.Clear();
            for (int i = 0; i < 5; i++)
            {
                string s = musicas[i];
                if (s != null)
                    listBox2.Items.Add(s);
            }
            if (selected < 0)
                selected = 0;
            if (listBox2.Items.Count > 0)
                listBox2.SetSelected(selected, true);
        }

        /// <summary>
        /// Actualiza a imagem do botão de pre-visualização
        /// </summary>
        private void UpdateCarta()
        {
            if (carta.Virada)
            {
                carta.Butao.BackgroundImage = carta.Imagem;
            }
            else
            {
                carta.Butao.BackgroundImage = carta.Capa;
            }
        }

        /// <summary>
        /// Copia todos os ficheiros para a pasta onde o tema ficará guardado
        /// </summary>
        private void CriaTema()
        {
            string path;
            //Caminho para a pasta onde tudo ficará guardado
            path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\My Games\Memoria\" + textBox1.Text;
            Cursor = Cursors.WaitCursor;
            try
            {
                //Cria os directorios para onde os ficheiros vao ser copiados
                Directory.CreateDirectory(path);
                Directory.CreateDirectory(path + @"\Musica");

                //Copia cada ficheiro da origem para a pasta do tema
                if (capa != "" && capa != null)
                {
                    File.Copy(capa,path + @"\capa");    
                }
                if (background != "" && background != null)
                {
                    File.Copy(background, path + @"\bkgrd");                    
                }

                for (int i = 0; i < 13; i++)
                {
                    File.Copy(paths[i],path + string.Format(@"\{0}", i + 1));                    
                }

                for (int i = 0; i < numMusicas; i++)
                {
                    File.Copy(musicas[i],path+@"\Musica\mus"+i+".wav");
                }
                Cursor = Cursors.Default;
                Tema.lista.Add(textBox1.Text);
                MessageBox.Show("Tema criado com sucesso!");
                Tema.GravaLista("Data");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro a criar o Tema.\nO problema foi: "+ex.Message);
            }

        }

        /// <summary>
        /// Verifica se a imagem tem menos de 1MB e se é uma imagem válida
        /// </summary>
        /// <param name="ficheiro">Caminho para a imagem a testar</param>
        /// <returns></returns>
        private bool verificaImagem(string ficheiro)
        {
            try
            {
                if (new FileInfo(ficheiro).Length <= 1050000)//Verifica tamanho da imagem
                {
                    Image.FromFile(ficheiro);//Tenta converter para imagem
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }


        }

        /// <summary>
        /// Verifica se o nome do tema é válido
        /// </summary>
        /// <returns></returns>
        private bool VerificaNome()
        {
            string txt = textBox1.Text;
            if (textBox1.Text == "")
            {
                MessageBox.Show("Não se esqueça de adicionar um nome ao tema!", "Nome do Tema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBox1.Focus();
                return false;
            }
            //verifica se tema contem >>+txt, porque temas que não estão na lista, mas ainda existem no disco encontram-se assim marcados
            else if(Tema.lista.Contains(textBox1.Text)|| Tema.lista.Contains(">>"+txt)|| textBox1.Text=="Defeito")
            {
                MessageBox.Show("Um tema com esse nome já existe!", "Tema já Existente", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBox1.Focus();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Verifica se paths tem o numero de cartas todas
        /// </summary>
        /// <returns></returns>
        private bool VerificaCartas()
        {
            if (numCartas != 13)
            {
                MessageBox.Show("São necessárias 13 cartas para criar um tema!", "Adicione Mais Cartas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Verifica se Musicas[] se encontra vazio e se o utilizador quer que o tema não tenha musicas
        /// </summary>
        /// <returns></returns>
        private bool VerificaMusicas()
        {
            if (numMusicas == 0)
            {
                DialogResult res = MessageBox.Show("Não adicionou Musicas!\nQuer que o tema seja criado sem Musicas?", "Nenhuma Musica Seleccionada", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    numMusicas = -1;
                }
                else
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Verifica se capa se encontra vazio e se o utilizador quer utilizar a capa por defeito
        /// </summary>
        /// <returns></returns>
        private bool VerificaCapa()
        {
            if (textBox2.Text == "")
            {
                DialogResult res = MessageBox.Show("Não adicionou uma capa!\nQuer que o tema seja criado com a capa por defeito?", "Capa Não Seleccionada", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    capa = "";
                }
                else
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Verifica se capa se encontra vazio e se o utilizador não quer imagem de fundo
        /// </summary>
        /// <returns></returns>
        private bool VerificaBackGround()
        {
            if (textBox3.Text == "")
            {
                DialogResult res = MessageBox.Show("Não adicionou uma imagem de fundo!\nQuer que o tema seja criado sem imagem de fundo?", "Imagem de Fundo Não Seleccionada", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    background = "";
                }
                else
                    return false;
            }
            return true;
        }


        

        

        

        

        

       

        

        

        

        

        
    }
}
