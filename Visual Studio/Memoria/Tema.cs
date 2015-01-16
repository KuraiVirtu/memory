using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
//using S

namespace Memoria
{
    static class Tema
    {
        //Lista com o nome de todos os Temas
        //Temas cujo nome começa em ">>" estão marcados para terem os ficheiros 
        public static List<string> lista=new List<string>();

        static Image[] cartas = new Image[13];//Array com todas as imagens a serem usadas nas cartas
        static Image capa;//Imagem a ser usada na capa das cartas
        static Image backGround;//Imagem de fundo para os formulários
        static List<string> musicas = new List<string>();//lista com o caminho paraas músicas do tema
        static string nome;//Nome do tema

//Propriedades
        public static Image[] Cartas { get { return cartas; } }

        public static List<string> Musicas { get { return musicas; } }

        public static Image Capa { get { return capa; } }

        public static Image BackGround { get { return backGround; } }

        public static string Nome { get { return nome; } set { nome = value; } }

//Métodos

        /// <summary>
        /// Abre o tema por defeito
        /// </summary>
        public static void TemaDefeito()
        {
            limpaTema();
            capa = Properties.Resources.Capa;
            backGround = null;
            cartas[0] = Properties.Resources._1;
            cartas[1] = Properties.Resources._2;
            cartas[2] = Properties.Resources._3;
            cartas[3] = Properties.Resources._4;
            cartas[4] = Properties.Resources._5;
            cartas[5] = Properties.Resources._6;
            cartas[6] = Properties.Resources._7;
            cartas[7] = Properties.Resources._8;
            cartas[8] = Properties.Resources._9;
            cartas[9] = Properties.Resources._10;
            cartas[10] = Properties.Resources._11;
            cartas[11] = Properties.Resources._12;
            cartas[12] = Properties.Resources._13;
            musicas.Clear();
            musicas.Add(@"Resources\MUS1.mp3");
            musicas.Add(@"Resources\MUS2.mp3");
            musicas.Add(@"Resources\MUS3.mp3");
            nome = "Defeito";
        }

        /// <summary>
        /// Tenta remover todos os ficheiros relativos ao tema do disco
        /// </summary>
        /// <param name="nomeTema">Nome do tema a remover</param>
        public static void ApagaTema(string nomeTema)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\My Games\Memoria\" + nomeTema;
            if (Directory.Exists(path))
            {
                try
                {
                    if (File.Exists(path + @"\capa"))
                    {
                        File.Delete(path + @"\capa");
                    }
                    if (File.Exists(path + @"\bkgrd"))
                    {
                        File.Delete(path + @"\bkgrd");
                    }

                    for (int i = 0; i < 13; i++)
                    {
                        if (File.Exists(path + string.Format(@"\{0}", i + 1)))
                            File.Delete(path + string.Format(@"\{0}", i + 1));
                    }
                    if (Directory.Exists(path + @"\Musica"))
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            if (File.Exists(path + @"\Musica\mus" + i + ".wav"))
                                File.Delete(path + @"\Musica\mus" + i + ".wav");
                        }
                    }
                    if (!Directory.EnumerateFiles(path + @"\Musica").Any())//Se a pasta das musicas estiver vazia
                    {
                        Directory.Delete(path + @"\Musica");
                        if (!Directory.EnumerateFiles(path).Any())//Se a pasta do tema estiver vazia
                            Directory.Delete(path);
                    }
                }
                catch 
                {
                    //Se não conseguiu remover o tema, coloca-se novamente na lista,
                    //marcado para remover mais tarde
                    lista.Add(">>" + nomeTema);
                }

            }
        }

        /// <summary>
        /// Prepara para abrir um novo tema
        /// </summary>
        private static void limpaTema()
        {
            for (int i = 0; i < 13; i++)
            {
                cartas[i] = null;                
            }
            capa = null;
            backGround = null;
            musicas.Clear();

        }

        /// <summary>
        /// Tenta abrir um tema especificado
        /// </summary>
        /// <param name="nomeTema">nome do tema a abrir</param>
        public static void AbreTema(string nomeTema)
        {
            limpaTema();
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\My Games\Memoria\" + nomeTema;
            try
            {
                if (File.Exists(path + @"\capa"))
                {
                    capa = Image.FromFile(path + @"\capa");
                }
                else
                    capa = Properties.Resources.Capa;

                if (File.Exists(path + @"\bkgrd"))
                {
                    backGround = Image.FromFile(path + @"\bkgrd");
                }
                else
                    backGround = null;

                for (int i = 0; i < 13; i++)
                {
                    cartas[i] = Image.FromFile(path + string.Format(@"\{0}", i + 1));

                }
                for (int i = 0; i < 5; i++)
                {
                    if (File.Exists(path + @"\Musica\mus" + i + ".wav"))
                        musicas.Add(path + @"\Musica\mus" + i + ".wav");
                }
                nome = nomeTema;
            }
            catch
            {
                TemaDefeito();
                System.Windows.Forms.MessageBox.Show("Erro a abrir o Tema!", "Erro", 
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Grava o nome dos temas no ficheiro de texto
        /// </summary>
        /// <param name="path">Ficheiro onde os nomes serão guardados</param>
        public static void GravaLista(string path)
        {
            StreamWriter sr = new StreamWriter(new FileStream(path + @"\temas", FileMode.Create));
            foreach (string s in lista)
            {
                if (Tema.nome == s)
                    sr.WriteLine(">" + s);//O tema actual é marcado com ">" para ser aberto na próxima execução
                else
                    sr.WriteLine(s);
            
            }
            sr.Close();
        }

        /// <summary>
        /// Carrega a lista de temas de um ficheiro de texto e
        /// devolve o nome do tema com que a aplicação foi fechada
        /// </summary>
        /// <param name="path">Caminho para o directorio do Ficheiro</param>
        /// <returns></returns>
        public static string CarregaLista(string path)
        {
            string retorno="";
            path += @"\temas";
            StreamReader sr = new StreamReader(new FileStream(path, FileMode.OpenOrCreate));
            while (sr.Peek() != -1)
            {
                string s = sr.ReadLine();
                if (s.StartsWith(">>"))//Se tema marcado com ">>" tenta remov-lo do disco              
                    ApagaTema(s.Remove(0, 2));
                else if (s.StartsWith(">"))//Se começa com ">", guarda nome do tema para devolver
                { retorno = s.Remove(0,1);lista.Add(retorno); }//coloca-o na lista sem o ">"
                else
                    lista.Add(s);
            }            
            sr.Close();
            return retorno;
        }

        

        

    }
}
