using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace Memoria
{
    class Chievs//(Conquistas)
    {
        //Lista de Conquistas
        static public SortedList<string, Chievs> lista = new SortedList<string, Chievs>();
        protected string codigo;//O código de cada conquista
        protected string nome;//Designação da conquista
        protected string requisitos;//String que explicita os requisitos para desbloquear a conquista
        protected string imagem;//Caminho da imagem a ser apresentada (neste caso vai-se referir a um dos recursos)
        protected bool enabled;//Determina se apresenta a imagem ou a imagem de bloqueado (Locked.png)

        public Chievs(string Cod, string Nom, string img)
        {
            codigo = Cod;
            nome = Nom;
            imagem = img;
        }

//Propriedades
        public string Codigo { get { return codigo; } }

        public string Nome { get { return nome; } }

        public string Requisitos { get { return requisitos; } }

        public bool Enabled { get { return enabled; } set { enabled = value; } }        

//Métodos
        /// <summary>
        /// Verifica se os objectivos da conquista foram atingidos.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cond">O parâmetro a testar(Nivel, Pontos ou tempo)</param>
        /// <returns></returns>
        public virtual bool Condicao<T>(T cond)
        {
            //Por defeito, se as classes filhas não implementarem este método, devolve falso.
            return false;
        }

        /// <summary>
        /// String que devolve todos os dados da classe que devem ser guardados em ficheiro
        /// </summary>
        /// <returns></returns>
        public virtual string StringParaGravar()
        {
            //Por defeito, se as classes filhas não implementarem este método, não devolve valor.
            return "";
        }

        /// <summary>
        /// Devolve uma pictureBox que irá representar a instancia da classe
        /// </summary>
        /// <param name="r">Dimensão e localização da PictureBox</param>
        /// <returns></returns>
        public virtual PictureBox DesenhaImagem(Rectangle r)
        {
            PictureBox pic = new PictureBox();
            pic.Tag = this;//Coloca a referencia desta intancia na Tag da PictureBox
            pic.BorderStyle = BorderStyle.FixedSingle;
            pic.Location = r.Location;
            pic.Size = r.Size;

            if (enabled)//Desenha a imagem correspondente á conquista
            {                
                Stream f=System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(imagem);
                Bitmap i = new Bitmap(System.Drawing.Image.FromStream(f), pic.Size);
                pic.Image = i;
                f.Close();
            }
            if (!enabled)//Desenha a imagem Locked.png
            {
                Bitmap i = new Bitmap(Properties.Resources.Locked, pic.Size);
                pic.Image = i;
            }
            return pic;
        }
        
    //Ficheiros
        /// <summary>
        /// //Lê e carrega, do ficheiro ...\Chievs, todas as conquistas nele guardados
        /// </summary>
        /// <param name="path">Directório do ficheiro (não incluindo o ficheiro)</param>
        public static void CarregaChievs(string path)
        {
            path += @"\Chievs";
            StreamReader sr = new StreamReader(new FileStream(path, FileMode.OpenOrCreate));

            while (sr.Peek() != -1)
            {
                string[] arr;//Array onde vão ser guardados os vários valores lidos do ficheiro
                string s = sr.ReadLine();

                if (s.StartsWith("TMP"))//Decide qual o tipo de Conquista é, e cria instancia na classe correspondente
                {
                    arr = s.Split('|');//Divide os valores lidos para o array
                    lista.Add(arr[0], new chievsTempo(arr[0], arr[1], TimeSpan.Parse(arr[2]),arr[3]));
                }
                else if (s.StartsWith("NIV"))
                {
                    arr = s.Split('|');
                    lista.Add(arr[0], new chievsNivel(arr[0], arr[1], int.Parse(arr[2]), arr[3]));
                }
                else if (s.StartsWith("PNT"))
                {
                    arr = s.Split('|');
                    lista.Add(arr[0], new chievPontos(arr[0], arr[1], int.Parse(arr[2]), arr[3]));
                }
            }
            sr.Close();
            
        }

        /// <summary>
        /// Grava, para o ficheiro ...\Chievs, todas as conquistas da lista
        /// </summary>
        /// <param name="path">Directório do ficheiro (não incluindo o ficheiro)</param>
        public static void GravaChievs(string path)
        {
            path += @"\Chievs";
            StreamWriter sw = new StreamWriter(new FileStream(path, FileMode.Create));
            foreach (Chievs c in Chievs.lista.Values)
            {
                sw.WriteLine(c.StringParaGravar());
            }
            sw.Close();
        }

        
    }
}
