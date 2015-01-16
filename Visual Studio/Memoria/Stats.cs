using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Memoria
{
    class Stats : Recordes
    {

        public Stats(DateTime Data, string Jogador, string Dificuldade, TimeSpan Tempo, int Pontos, int Nivel, bool Vit):
            base(Data,Jogador,Dificuldade,Tempo,Pontos,Nivel,Vit)
        {
           
        }

//Métodos
        /// <summary>
        /// Utiliza o algoritmo de ordenação ShellSort, para devolver um array de Recordes
        /// com estes ordenado por pontos.
        /// </summary>
        /// <returns></returns>
        public static Recordes[] Sort()
        {
            //Array que será devolvido
            Recordes[] rec=new Recordes [lista.Count];
            int tamanho=lista.Count;//Tamanho da lista de Recordes

            //Copia todos os Recordes da lista para o array rec
            for(int i=0;i<tamanho;i++)
            {
                rec[i] = lista.Values[i];
            }

            int limEsq;
            Recordes temp;//Referencia para o recorde que está a ser ordenado        
            int h = 1;

            while (h <= tamanho / 3)
                h = h * 3 + 1;//Limite direito

            while (h > 0)
            {
                for (int i = h; i < tamanho ; i++)
                {
                    temp = rec[i];
                    limEsq = i;
                    while ((limEsq > h - 1) && rec[limEsq - h].Pontuacao <= temp.Pontuacao)
                    {
                        rec[limEsq] = rec[limEsq - h];
                        limEsq -= h;
                    }
                    rec[limEsq] = temp;
                }
                h = (h - 1) / 3;
            }
            return rec;
        }

        /// <summary>
        /// Calcula os vários valores necessários para o cálculo de estatisticas e
        /// guarda-os na instancia de jogador correspondente
        /// </summary>
        public static  void UpdateStatsJogadores()
        {
            
            int i;
            foreach (Jogador j in Jogador.lista.Values)//FOR!!
                j.ReiniciaDados();
            
            foreach (Recordes r in Recordes.lista.Values)
            {                
                switch (r.Dificuldade.ToLower())
                {
                    case "facil":
                    case "fácil":
                        i = 1;
                        break;
                    case "dificil":
                    case "difícil":
                        i = 3;
                        break;
                    case "media":
                    case "média":
                        i = 2;
                        break;
                    default:
                        i = 0;
                        break;
                }
                Jogador j = Jogador.lista[r.NomeJogador.ToUpper()];
                
                if (r.Nivel > j.MaximoNivel[i]) j.MaximoNivel[i] = r.Nivel;
                if (r.Nivel > j.MaximoNivel[0]) j.MaximoNivel[0] = r.Nivel;

                if (r.Pontuacao > j.MaximoPontos[i]) j.MaximoPontos[i] = r.Pontuacao;
                if (r.Pontuacao > j.MaximoPontos[0]) j.MaximoPontos[0] = r.Pontuacao;

                if (r.Tempo > j.MaximoTempo[i]) j.MaximoTempo[i] = r.Tempo;
                if (r.Tempo > j.MaximoTempo[0]) j.MaximoTempo[0] = r.Tempo;

                if (r.Vitoria) { j.Vitorias[i]++; j.Vitorias[0]++; }
                else { j.Derrotas[i]++; j.Derrotas[0]++; }

                j.TotPontos[i] += r.Pontuacao;
                j.TotPontos[0] += r.Pontuacao;

                j.TotNiveis[i] += r.Nivel;
                j.TotNiveis[0] += r.Nivel;

                j.TempoJogado[i] += r.Tempo;
                j.TempoJogado[0] += r.Tempo;
            }
            alterado = false;
        }

    //Estatisticas
        /// <summary>
        /// Calcula a média de pontos em todos os jogos
        /// </summary>
        /// <returns></returns>
        public static double CalcMediaPontos()
        {
            int n = 0;
            int tot = 0;
            foreach (Recordes r in lista.Values)
            {
                tot += r.Pontuacao;
                n++;
            }

            return tot / n;
        }

        /// <summary>
        /// Calcula a média de Pontos dos jogos de um ou vários jogadores, com aa dficuldades especificadas.<para>
        /// Devolve -1 se não conseguiu calcular a média.</para>
        /// </summary>
        /// <param name="Jogador">Designação do jogador cuja média de pontos se quer calcular.
        /// <para>Passar string vazia ou "Todos" se não quiser filtrar jogador.</para></param>
        /// <param name="Dificuldade">Dificuldade dos jogos a considerar para a média.<para>
        /// Passar string vazia ou "Todas" se não quiser filtrar Dificuldade.</para></param>
        /// <returns></returns>
        public static double CalcMediaPontos(string Jogador, string Dificuldade)
        {
            int n = 0;
            int tot = 0;

            //Se Jogador Especificado
            if (Jogador != "" && Jogador != "Todos")
            {
                //E Dificuldade Especificada
                if (Dificuldade != "" && Dificuldade != "Todas")
                {
                    foreach (Recordes r in lista.Values)
                    {
                        if (r.NomeJogador == Jogador.ToUpper() && r.Dificuldade == Dificuldade)
                        {
                            tot += r.Pontuacao;
                            n++;
                        }
                    }
                }

                //E Dificuldade não Especificada
                else
                {
                    foreach (Recordes r in lista.Values)
                    {
                        if (r.NomeJogador == Jogador.ToUpper())
                        {
                            tot += r.Pontuacao;
                            n++;
                        }
                    }
                }
            }
            //Se Jogador não Especificado
            else
            {
                //E Dificuldade Especificada
                if (Dificuldade != "" && Dificuldade != "Todas")
                {
                    foreach (Recordes r in lista.Values)
                    {
                        if (r.Dificuldade == Dificuldade)
                        {
                            tot += r.Pontuacao;
                            n++;
                        }
                    }
                }

                //E Dificuldade não Especificada
                else
                    return CalcMediaPontos();
            }
            if (n > 0)
                return tot / n;
            else
                return -1;
        }


    }
}
