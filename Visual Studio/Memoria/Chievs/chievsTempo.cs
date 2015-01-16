using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Memoria
{
    class chievsTempo:Chievs
    {
        TimeSpan tempo;//O valor que é necessário atingir para desbloquear esta conquista

        public chievsTempo(string cod, string nome, TimeSpan Tempo, string img)
            : base(cod, nome, img)
        {
            tempo = Tempo;
            requisitos = "Atinge e ganha um jogo no nível 5 em menos de " + tempo.TotalSeconds.ToString() + " segundos";//string a ser apresentada contendo os objectivos
        }

        //Métodos
        /// <summary>
        /// Verifica se os objectivos da conquista foram atingidos.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cond">O parâmetro a testar(Tempo)</param>
        /// <returns></returns>
        public override bool Condicao<T>(T cond)
        {
            if (cond is TimeSpan)//Só faz verificação se cond for um TimeSpan
            {
                    string s = cond.ToString();                 
                    TimeSpan tempoUsado = TimeSpan.Parse(s);
                    if (tempoUsado < tempo)//Se tempo não foi atingido/Ultrapassado
                        return true;
                    else
                        return false;
            }
            return false;

        }

        /// <summary>
        /// String que devolve todos os dados da classe que devem ser guardados em ficheiro
        /// </summary>
        /// <returns></returns>
        public override string StringParaGravar()
        {
            return codigo + '|' + nome + '|' + tempo.ToString()+ '|' +imagem;
        }

    }
}
