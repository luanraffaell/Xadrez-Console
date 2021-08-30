using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;

namespace Xadrez
{
    class PartidaXadrez
    {
        public Tabuleiro tab { get; private set; }
        public Cor JogadorAtual { get; set; }
        public int Momento { get; set; }

        public PartidaXadrez()
        {
            tab = new Tabuleiro(8, 8);
            JogadorAtual = Cor.Branca;
            Momento = 1;
            ColocarPecas();
        }
        public void ColocarPecas()
        {
            tab.ColocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('b', 7).ToPosicao());
            tab.ColocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('a', 1).ToPosicao());
            tab.ColocarPeca(new Rei(tab, Cor.Preta), new PosicaoXadrez('c', 2).ToPosicao());

            tab.ColocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('b', 1).ToPosicao());
            tab.ColocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('e', 2).ToPosicao());
            tab.ColocarPeca(new Rei(tab, Cor.Branca), new PosicaoXadrez('d', 4).ToPosicao());
        }
        public Peca MoverPecas(Posicao origem,Posicao destino)
        {
            Peca p = tab.RemoverPeca(origem);
            p.posicao = null;
            p.IncrementarMov();
            Peca capturada = tab.RemoverPeca(destino);
            tab.ColocarPeca(p,destino);
            return capturada;
        }
    }
}
