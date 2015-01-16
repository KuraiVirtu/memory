using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Memoria
{
    public class Replay : Recordes
    {
        List<string> log;//strings com toda a informação que será guardad em ficheiro de Texto
        DateTime Inicio;//Momento em que o nivel corrente começou
        
        public Replay(Recordes r)
            :base(r.Data,r.NomeJogador,r.Dificuldade,r.Tempo,r.Pontuacao,r.Nivel,r.Vitoria)
        {
            log=new List<string>();
            log.Add(Data.ToString());
            log.Add(NomeJogador);
            log.Add(Dificuldade);
        }

        /// <summary>
        /// Adiciona ao log, o movimento que foi efectuado
        /// </summary>
        /// <param name="pos">Posição da carta que foi virada</param>
        public void addMovimento(int pos)
        {
            TimeSpan tempo = DateTime.Now - Inicio;
            log.Add(pos.ToString() + "|" + tempo.ToString());
        }

        /// <summary>
        /// Adiciona ao log a sequencia de cartas gerda pelo método distribui
        /// </summary>
        /// <param name="sequencia">arrray com a sequencia</param>
        public void addSequencia(int[]sequencia)
        {
            string s = "";
            s += sequencia[0].ToString();
            for (int i = 1; i < sequencia.Length; i++)
            {
                s += ",";
                s += sequencia[i].ToString();
            }
            log.Add("[Novo]");
            log.Add(s);
        }

        /// <summary>
        /// Adiciona ao log qual o nivel corrente
        /// </summary>
        /// <param name="nivel">o nivel actual do jogo</param>
        public void addNivel(int nivel)
        {
            log.Add(nivel.ToString());
        }

        /// <summary>
        /// Grava, num ficheiro de texto, o log, para ser reproduzido posteriormente
        /// </summary>
        public void gravaReplay()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\My Games\Memoria";
            if (!Directory.Exists(path + @"\Replays"))
                Directory.CreateDirectory(path + @"\Replays");
            path+=@"\Replays\";

            StreamWriter sw = new StreamWriter(new FileStream(path + Data.ToString("yy-MM-dd-HH-mm-ss")+".txt", FileMode.Create));
            foreach (string s in log)
            {
                sw.WriteLine(s);
            }
            sw.Close();
        }

        /// <summary>
        /// Maraca o inicio do novo nivel
        /// </summary>
        public void restartTime()
        {
            Inicio = DateTime.Now;
        }
    }
}
