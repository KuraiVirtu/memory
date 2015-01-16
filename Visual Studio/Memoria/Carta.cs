using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Memoria
{
    class Carta
    {        
        //public static SortedList<int, string> listaImagens = new SortedList<int, string>(); 
        
        public static int pares; //guarda os pares que falta associar.
        public static int viradas = 0; //guarda o número de cartas com a mostrar a imagem

        Image img; //Imagem desta carta
        Image  capa; //Imagem para a "parte de trás" desta carta
        bool mostrada; // se a carta está a mostrar a imagem
        bool bloqueada; // se a carta está bloqueada e não se pode fazer click
        Button b = new Button(); //a interação com as cartas baseia-se neste botão        
        Rectangle dimensao; // tamanho e posição da carta
        int codImagem;//O Código que identidica a imagem da carta(para comparação)

        /// <summary>
        /// Cria uma nova instância de Carta e prepara-a para ser desenhada no local defenido.
        /// </summary>
        /// <param name="CodigoImagem">Posição da imagem, na lista de imagens do tema aberto</param>
        /// <param name="Dim">Posição e dimensão do butão representante da carta</param>
        public Carta(int CodigoImagem,Rectangle Dim)
        {
            this.codImagem = CodigoImagem;

            //Capa e Imagem são retiradas de Tema
            this.img = Tema.Cartas[codImagem];
            capa = Tema.Capa;
            this.dimensao = Dim;            
            this.bloqueada = false;
            this.mostrada = false;

            //propriedades do botao            
            this.b.Size = dimensao.Size;
            this.b.Location = dimensao.Location;
            this.b.BackgroundImage = capa;
            this.b.BackgroundImageLayout = ImageLayout.Stretch;
            b.Cursor = Cursors.Hand; //Cursor do butão é a mão indicando que esta pode ser clicada.
            b.Tag = this;//Guarda na Tag do butão a referencia para esta instancia
        }

       /// <summary>
        /// Cria uma nova instância de Carta e prepara-a para ser desenhada no local defenido.
       /// </summary>
       /// <param name="tempImagem">Imagem a ser apresentada na carta</param>
       /// <param name="tempCapa">Capa a ser apresentada na carta</param>
        /// <param name="Dim">Posição e dimensão do butão representante da carta<</param>
        public Carta(Image tempImagem, Image tempCapa, Rectangle Dim)
        {
            this.img =tempImagem;
            this.capa = tempCapa;
            this.dimensao = Dim;
            this.bloqueada = false;
            this.mostrada = false;

            //propriedades do botao            
            this.b.Size = dimensao.Size;
            this.b.Location = dimensao.Location;
            this.b.BackgroundImage = capa;
            this.b.BackgroundImageLayout = ImageLayout.Stretch;
            b.Cursor = Cursors.Hand; //Cursor do butão é a mão indicando que esta pode ser clicada.
            b.Tag = this;
        }

//Propriedades
        public bool Virada { get { return mostrada; } }

        public bool Bloqueada { get { return bloqueada; } }

        public int Codigo { get { return codImagem; } }

        public Button Butao { get { return b; } }

        public Image Imagem { get { return img; } set { img = value;  } }

        public Image Capa { get { return capa; } set { capa = value; } }
        
//Métodos
        /// <summary>
        /// Se a carta estiver escondida, mostra a imagem que lhe está associada
        /// </summary>
        public void MostraCarta()
        {            
            if (!mostrada)
            {
                b.BackgroundImage = img;                
                mostrada = true;                
                b.Cursor = Cursors.Default;                 
                viradas++;                
            }
            
        }

        /// <summary>
        /// Se a carta estiver virada, mostra a capa que lhe está associada
        /// </summary>
        public void EscondeCarta()
        {
            if (mostrada)
            {
                b.BackgroundImage = capa;
                mostrada = false;
            }
        }

        public void Bloqueia()
        {
            bloqueada = true;
        }        

        /// <summary>
        /// Desenha o botão da carta num controlo
        /// </summary>
        /// <param name="controlo">controlo em que se quer desenhar a carta</param>
        public void Desenha (Control controlo)
        {
            controlo.Controls.Add(b);
        }
        
    }
}
