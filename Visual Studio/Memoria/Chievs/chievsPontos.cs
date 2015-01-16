using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Memoria
{
    class chievPontos : Chievs
    {
        int pontos;//O valor que é necessário atingir para desbloquear esta conquista

        public chievPontos(string cod, string nome, int Ponts, string img)
            : base(cod, nome, img)
        {
            pontos = Ponts;
            requisitos = "Ganha um jogo com mais de " + pontos + " pontos.";//string a ser apresentada contendo os objectivos
        }

        //Métodos
        /// <summary>
        /// Verifica se os objectivos da conquista foram atingidos.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cond">O parâmetro a testar(Pontos)</param>
        /// <returns></returns>
        public override bool Condicao<T>(T cond)
        {
            if (cond is int)//Só faz verificação se cond for um int
            {
                    string s = cond.ToString();
                    int pontsObtidos = int.Parse(s);
                    if (pontsObtidos >= this.pontos)//Se Pontos foram atingidos/Ultrapassados
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
            return codigo + '|' + nome + '|' + pontos.ToString() + '|' + imagem;
        }

    }
}
