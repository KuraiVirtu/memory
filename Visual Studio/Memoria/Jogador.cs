using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Memoria
{
    class Jogador
    {
        //a lista de jogadores
        static public SortedList<string,Jogador> lista = new SortedList<string, Jogador>();

        //Lista de conquistas desbloqueadas por este jogador, associadas a um recorde (DateTime é o código do recorde)
        public SortedList<string, DateTime> listaChievs = new SortedList<string, DateTime>();

        string nome;//Nome do Jogador. 

        //arrays usados para armazenar alguma informação relativa a estatisticas,
        //tornando o processo de as calcular mais rápido
        int[] maximoPTS;
        int[] maximoNIV;
        int[] totPTS;
        int[] totNIV;
        int[] vitorias, derrotas;
        TimeSpan[] tempo;
        TimeSpan[] maximoTMP;


        public Jogador(string Nome)
        {
            nome = Nome;
            lista.Add(nome.ToUpper(), this);

            maximoPTS = new int[4];//A primeira posição armazena os pontos máximos em todas as dificuldades
            maximoNIV = new int[4];//A segunda posição armazena o Nivel máximo na dificuldade fácil
            totPTS = new int[4];//A terceira posição armazena os pontos totais dos jogos na dificuldade média
            totNIV = new int[4];//A quarta posição armazena o total de niveis atingidos na dificuldade dificil (para média)
            vitorias = new int[4];//Estes valores não são guardados entre sessões 
            derrotas = new int[4];//Estes valores são recalculdados depois de ser adicionado um novo Recorde à lista
            tempo = new TimeSpan[4];
            maximoTMP = new TimeSpan[4];
        }

        //Atributos
        public string Nome { get { return nome; } }

        public int[] MaximoPontos { get { return maximoPTS; } set { maximoPTS = value; } }

        public int[] MaximoNivel { get { return maximoNIV; } set { maximoNIV = value; } }

        public int[] Vitorias { get { return vitorias; } set { vitorias = value; } }

        public int[] Derrotas { get { return derrotas; } set { derrotas = value; } }

        public int[] TotPontos { get { return totPTS; } set { totPTS = value; } }

        public int[] TotNiveis { get { return totNIV; } set { totNIV = value; } }

        public TimeSpan[] TempoJogado { get { return tempo; } set { tempo = value; } }

        public TimeSpan[] MaximoTempo { get { return maximoTMP; } set { maximoTMP = value; } }


//Métodos
        /// <summary>
        /// Apaga os valores de todos os arrays, para serem recalculados
        /// </summary>
        public void ReiniciaDados()
        {
            for (int i = 0; i < 4; i++)
            {
                maximoPTS[i] = 0; 
                maximoNIV[i] = 0;
                totPTS[i] = 0;
                totPTS[i] = 0;
                vitorias[i] = 0;
                derrotas[i] = 0;
                tempo[i] = TimeSpan.Zero;
                maximoTMP[i] = TimeSpan.Zero;
            }
        }

        /// <summary>
        /// Remove o Jogador da lista, bem como todos os recordes a ele associado
        /// </summary>
        public void EliminaJogador()
        {
            int t = Recordes.lista.Count;
            for (int i = t-1; i >=0 ; i--)
            {
                if (Recordes.lista.Values[i].NomeJogador == nome)
                {
                    Recordes.lista.RemoveAt(i);
                }
            }
            lista.Remove(nome.ToUpper());            
        }

    //Ficheiros
        /// <summary>
        /// Grava, para o ficheiro ...\perfis.txt, o nome de todos os jogadores
        /// </summary>
        /// <param name="Path">Directório do ficheiro (não incluindo o ficheiro)</param>
        public static void GravaJogadores(string Path)
        {
            Path += @"\perfis.txt";
            StreamWriter sw=new StreamWriter(new FileStream(Path,FileMode.Create));
            foreach (Jogador j in lista.Values)
            {
                sw.WriteLine(j.Nome);
            }
            sw.Close();
        }

        /// <summary>
        /// Carrega, do ficheiro ...\perfis.txt, o nome de todos os jogadores
        /// </summary>
        /// <param name="Path">Directório do ficheiro (não incluindo o ficheiro)</param>
        public static void CarregaJogadores(string Path)
        {
            Path += @"\perfis.txt";
            StreamReader sr = new StreamReader(new FileStream(Path,FileMode.OpenOrCreate));
            while (sr.Peek() != -1)
            {
                string s = sr.ReadLine();
                new Jogador(s);
            }
            sr.Close();
        }

        /// <summary>
        /// Grava, para o ficheiro ...\relacaoCJ, os valores da listaChievs de cada jogador
        /// </summary>
        /// <param name="path">Directório do ficheiro (não incluindo o ficheiro)</param>
        public static void GravaAchievs(string path)
        {
            path += @"\relacaoCJ";
            BinaryWriter bw = new BinaryWriter(new FileStream(path, FileMode.Create));
            foreach (Jogador j in Jogador.lista.Values)
            {
                bw.Write(j.nome.ToUpper());//Grava nome do Jogador
                foreach (string s in j.listaChievs.Keys)//E de seguida os elementos da lista que lhe estão associados
                {
                    bw.Write(s);
                    bw.Write(j.listaChievs[s].ToString());
                }
                bw.Write("Sep");//Separa os registos dos vários jogadores
            }
            bw.Close();
        }

        /// <summary>
        /// Carrega, do ficheiro ...\relacaoCJ,os valores da listaChievs de cada jogador
        /// </summary>
        /// <param name="path">Directório do ficheiro (não incluindo o ficheiro)</param>
        public static void CarregaAchievs(string path)
        {
            path += @"\relacaoCJ";
            BinaryReader br = new BinaryReader(new FileStream(path, FileMode.OpenOrCreate));
            string j = "";//string onde se encontrará o nome do Jogador
            string s = "";//string onde se encontrará o valor da lista
            while (br.PeekChar() != -1)
            {
                j = br.ReadString();
                if (j != "Sep" )
                {
                    s = br.ReadString();
                    if (s != "Sep")
                    {
                        do//Enquanto não atingir o próximo "Sep"
                        {
                            //Adiciona á lista o código da conquista e o Datetime do Recorde
                            Jogador.lista[j].listaChievs.Add(s, DateTime.Parse(br.ReadString()));
                            s = br.ReadString();
                        } while (s != "Sep");
                    }
                }
            }
            br.Close();
        }
    }

}
