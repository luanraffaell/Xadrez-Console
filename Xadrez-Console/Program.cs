using System;
using tabuleiro;
using Xadrez;

namespace Xadrez_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Tabuleiro tab = new Tabuleiro(8, 8);
                tab.ColocarPeca(new Torre(tab, Cor.Preta), new Posicao(0, 0));
                tab.ColocarPeca(new Torre(tab, Cor.Preta), new Posicao(1, 3));
                tab.ColocarPeca(new Rei(tab, Cor.Preta), new Posicao(0, 6));

                Tela.imprimirTabuleiro(tab);

            }
            catch(TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }
            PosicaoXadrez pos = new PosicaoXadrez('c',3);
            Console.WriteLine(pos.ToPosicao());
            Console.WriteLine('a'-'y');
        }
    }
}
