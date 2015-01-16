using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Memoria
{
    class chievsNivel : Chievs
    {
        int nivel;//O valor que é necessário atingir para desbloquear esta conquista

        public chievsNivel(string cod, string nome, int Nivel, string img)
            : base(cod, nome, img)
        {
            nivel = Nivel;
            requisitos = "Ganha um jogo no nível " + nivel;//string a ser apresentada contendo os objectivos
        }

        //Métodos
        /// <summary>
        /// Verifica se os objectivos da conquista foram atingidos.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cond">O parâmetro a testar(Nivel)</param>
        /// <returns></returns>
        public override bool Condicao<T>(T cond)
        {
            if (cond is int)//Só faz verificação se cond for um int
            {
                string s = cond.ToString();
                int nivelAtingidido = int.Parse(s);
                if (nivelAtingidido >= this.nivel)//Se nivel foi atingido/Ultrapassado
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
            return codigo + '|' + nome + '|' + nivel.ToString() + '|' + imagem;
        }

    }
}

