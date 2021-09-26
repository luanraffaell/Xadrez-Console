using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;

namespace Xadrez
{
    class PartidaXadrez
    {
        public Tabuleiro tab { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public int Turno { get; private set; }
        public bool terminada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;
        public bool xeque { get; private set; }
        public Peca vulneravelEnPassant { get; private set; }

        public PartidaXadrez()
        {
            tab = new Tabuleiro(8, 8);
            terminada = false;
            JogadorAtual = Cor.Branca;
            Turno = 1;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            ColocarPecas();
        }
        public bool testeXequemate(Cor cor)
        {
            if (!estaEmCheque(cor))
            {
                return false;
            }
            foreach(Peca x in PecasEmJogo(cor))
            {
                bool[,] mat = x.MovimentosPossiveis();
                for(int i = 0; i < tab.linhas; i++)
                {
                    for(int j=0; j < tab.colunas; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao origem = x.posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca capturada = MoverPecas(origem, destino);
                            bool testeXeque = estaEmCheque(cor);
                            desfazMovimento(origem, destino, capturada);
                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true; // está em xeque
        }
        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            pecas.Add(peca);
        }
        public void ColocarPecas()
        { 
            ColocarNovaPeca('a', 1, new Torre(tab, Cor.Branca));
            ColocarNovaPeca('b', 1, new Cavalo(tab, Cor.Branca));
            ColocarNovaPeca('c', 1, new Bispo(tab, Cor.Branca));
            ColocarNovaPeca('d', 1, new Dama(tab, Cor.Branca));
            ColocarNovaPeca('e', 1, new Rei(tab, Cor.Branca));
            ColocarNovaPeca('f', 1, new Bispo(tab, Cor.Branca));
            ColocarNovaPeca('g', 1, new Cavalo(tab, Cor.Branca));
            ColocarNovaPeca('h', 1, new Torre(tab, Cor.Branca));
            ColocarNovaPeca('a', 2, new Peao(tab, Cor.Branca));
            ColocarNovaPeca('b', 2, new Peao(tab, Cor.Branca));
            ColocarNovaPeca('c', 2, new Peao(tab, Cor.Branca));
            ColocarNovaPeca('d', 2, new Peao(tab, Cor.Branca));
            ColocarNovaPeca('e', 2, new Peao(tab, Cor.Branca));
            ColocarNovaPeca('f', 2, new Peao(tab, Cor.Branca));
            ColocarNovaPeca('g', 2, new Peao(tab, Cor.Branca));
            ColocarNovaPeca('h', 2, new Peao(tab, Cor.Branca));

            ColocarNovaPeca('a', 8, new Torre(tab, Cor.Preta));
            ColocarNovaPeca('b', 8, new Cavalo(tab, Cor.Preta));
            ColocarNovaPeca('c', 8, new Bispo(tab, Cor.Preta));
            ColocarNovaPeca('d', 8, new Dama(tab, Cor.Preta));
            ColocarNovaPeca('e', 8, new Rei(tab, Cor.Preta));
            ColocarNovaPeca('f', 8, new Bispo(tab, Cor.Preta));
            ColocarNovaPeca('g', 8, new Cavalo(tab, Cor.Preta));
            ColocarNovaPeca('h', 8, new Torre(tab, Cor.Preta));
            ColocarNovaPeca('a', 7, new Peao(tab, Cor.Preta));
            ColocarNovaPeca('b', 7, new Peao(tab, Cor.Preta));
            ColocarNovaPeca('c', 7, new Peao(tab, Cor.Preta));
            ColocarNovaPeca('d', 7, new Peao(tab, Cor.Preta));
            ColocarNovaPeca('e', 7, new Peao(tab, Cor.Preta));
            ColocarNovaPeca('f', 7, new Peao(tab, Cor.Preta));
            ColocarNovaPeca('g', 7, new Peao(tab, Cor.Preta));
            ColocarNovaPeca('h', 7, new Peao(tab, Cor.Preta));
        }
        public Peca MoverPecas(Posicao origem,Posicao destino)
        {
            Peca p = tab.RemoverPeca(origem);
            p.posicao = null;
            p.IncrementarMov();
            Peca pecaCapturada = tab.RemoverPeca(destino);
            tab.ColocarPeca(p,destino);
            if(pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }
            return pecaCapturada;
        }
        public void desfazMovimento(Posicao origem, Posicao destino, Peca capturada)
        {
            Peca p = tab.RemoverPeca(destino);
            p.DecrementarMov();
            if(capturada != null)
            {
                tab.ColocarPeca(capturada, destino);
                capturadas.Remove(capturada);
            }
            tab.ColocarPeca(p, origem);
        }
        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            Peca capturada = MoverPecas(origem, destino);
            if (estaEmCheque(JogadorAtual))
            {
                desfazMovimento(origem, destino, capturada);
                throw new TabuleiroException("Voçê não pode se colocar em cheque");
            }
            if (estaEmCheque(adversaria(JogadorAtual)))
            {
                xeque = true;
            }
            else
            {
                xeque = false;
            }
            if (testeXequemate(adversaria(JogadorAtual)))
            {
                terminada = true;
            }
            else
            {
                Turno++;
                MudaJogador();
            }

        }

        public void MudaJogador()
        {
            if(JogadorAtual == Cor.Branca)
            {
                JogadorAtual = Cor.Preta;
            }
            else
            {
                JogadorAtual = Cor.Branca;
            }
        }
        public void validarPosicaoOrigem(Posicao pos)
        {
            if(tab.peca(pos) == null)
            {
                throw new TabuleiroException("Não existe peca na posição de origem");
            }
            if(JogadorAtual != tab.peca(pos).cor)
            {
                throw new TabuleiroException("A peça de origem escolhida não é sua");
            }
            if (!tab.peca(pos).ExisteMovimentosPossiveis())
            {
                throw new TabuleiroException("Não há movimentos possiveis para a peça de origem escolhida");
            }
        }
        public void ValidarPosicaoDestino(Posicao origem, Posicao destino)
        {
            if (!tab.peca(origem).podeMoverPara(destino))
            {
                throw new TabuleiroException("Posição de destino invalida");
            }
        }
        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach(Peca x in capturadas)
            {
                if(x.cor == cor)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas)
            {
                if (x.cor == cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(PecasCapturadas(cor));
            return aux;
        }
        private Cor adversaria(Cor cor)
        {
            if(cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            else
            {
                return Cor.Branca;
            }
        }
        private Peca rei(Cor cor)
        {
            foreach(Peca c in PecasEmJogo(cor))
            {
                if(c is Rei)
                {
                    return c;
                }
            }
            return null;
        }
        public bool estaEmCheque(Cor cor)
        {
            Peca R = rei(cor);
            if(R == null)
            {
                throw new TabuleiroException("Não há rei no tabuleiro");
            }
            foreach(Peca x in PecasEmJogo(adversaria(cor)))
            {
                bool[,] mat = x.MovimentosPossiveis();
                if (mat[R.posicao.Linha, R.posicao.Coluna])
                {
                    return true;
                }
            }
            return false;
        }
    }
}
