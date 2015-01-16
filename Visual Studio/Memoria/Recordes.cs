using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Memoria
{
    public class Recordes
    {
        //Lista onde são guardados todos os recordes
        public static SortedList<DateTime, Recordes> lista = new SortedList<DateTime, Recordes>();

        public static bool alterado = true;//Se foi adicionado algum recorde dede a ultima vez que se calcularam os valores para estatisticas

        string jogador;//Nome do Jogador a que o recorde se refere
        string dificuldade;//Dificuldade do jogo a que o recorde se refere
        TimeSpan tempo;//Duração do jogo a que o recorde se refere
        DateTime data;//Momento em que o recorde foi criado (serve de código)
        int pontos;//pontos adquiridos no jogo a que o recorde se refere        
        int nivel;//nivel atingido no jogo a que o recorde se refere
        bool vitoria;//Se o jogo foi perdido no primeiro nivel (vitoria=false)


        public Recordes(DateTime Data, string Jogador, string Dificuldade, TimeSpan Tempo, int Pontos, int Nivel, bool Vit)
        {
            this.jogador = Jogador;
            this.dificuldade = Dificuldade;
            this.tempo = Tempo;
            this.pontos = Pontos;
            this.data = Data;
            this.nivel = Nivel;
            this.vitoria = Vit;
        }

//Propriedades
        public string NomeJogador { get { return jogador; } }

        public string Dificuldade { get { return dificuldade; } }

        public TimeSpan Tempo { get { return tempo; } }

        public DateTime Data { get { return data; } }

        public int Pontuacao { get { return pontos; } }

        public int Nivel { get { return nivel; } }

        public bool Vitoria { get { return vitoria; } }

        /// <summary>
        /// Adiciona o Recorde ao ficheiro binário dos Recordes.
        /// </summary>
        /// <param name="path">Caminho para a pasta onde vai ser guardado o ficheiro.</param>
        public void GravaRecorde(string path)
        {
            path += @"\Recs";
            BinaryWriter br = new BinaryWriter(new FileStream(path, FileMode.Append));
            br.Write(data.ToString());
            br.Write(jogador);            
            br.Write(dificuldade);
            br.Write(tempo.ToString());            
            br.Write(pontos);
            br.Write(Nivel);
            br.Write(Vitoria);
            br.Close();
        }

        /// <summary>
        /// Grava todos os Recordes no ficheiro binário dos Recordes.
        /// </summary>
        /// <param name="path">Caminho para a pasta onde vai ser guardado o ficheiro.</param>
        public static void GravaRecordes(string path)
        {
            path += @"\Recs";
            BinaryWriter br = new BinaryWriter(new FileStream(path, FileMode.Create));
            foreach (Recordes r in lista.Values)
            {
                br.Write(r.data.ToString());
                br.Write(r.jogador);
                br.Write(r.dificuldade);
                br.Write(r.tempo.ToString());
                br.Write(r.pontos);
                br.Write(r.Nivel);
                br.Write(r.Vitoria);
            }
            br.Close();
        }

        /// <summary>
        /// Carrega todos os Recordes do ficheiro binário dos Recordes.
        /// </summary>
        /// <param name="path">Caminho para a pasta onde foi guardado o ficheiro.</param>
        public static void CarregaRecordes(string path)
        {
            path += @"\Recs";
            DateTime rdata;
            int rnivel;
            string rjogador, rdificuldade;
            TimeSpan rtempo; int rpontos;
            bool rvitoria;

            BinaryReader br = new BinaryReader(new FileStream(path, FileMode.OpenOrCreate));
            while (br.PeekChar() != -1)
            {
                rdata = DateTime.Parse(br.ReadString());                
                rjogador = br.ReadString();
                rdificuldade = br.ReadString();
                rtempo = TimeSpan.Parse(br.ReadString());
                rpontos = br.ReadInt32();
                rnivel = br.ReadInt32();
                rvitoria = br.ReadBoolean();
                lista.Add(rdata, new Recordes(rdata,rjogador, rdificuldade, rtempo, rpontos,rnivel,rvitoria));
            }

            br.Close();
        }
    }
}
