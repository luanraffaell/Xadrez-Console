using tabuleiro;

namespace Xadrez
{

    class Cavalo : Peca
    {

        public Cavalo(Tabuleiro tab, Cor cor) : base(cor,tab)
        {
        }

        public override string ToString()
        {
            return "C";
        }

        private bool podeMover(Posicao pos)
        {
            Peca p = tab.peca(pos);
            return p == null || p.cor != cor;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[tab.linhas, tab.colunas];

            Posicao pos = new Posicao(0, 0);

            pos.DefinirValores(posicao.Linha - 1, posicao.Coluna - 2);
            if (tab.PosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            pos.DefinirValores(posicao.Linha - 2, posicao.Coluna - 1);
            if (tab.PosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            pos.DefinirValores(posicao.Linha - 2, posicao.Coluna + 1);
            if (tab.PosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            pos.DefinirValores(posicao.Linha - 1, posicao.Coluna + 2);
            if (tab.PosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            pos.DefinirValores(posicao.Linha + 1, posicao.Coluna + 2);
            if (tab.PosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            pos.DefinirValores(posicao.Linha + 2, posicao.Coluna + 1);
            if (tab.PosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            pos.DefinirValores(posicao.Linha + 2, posicao.Coluna - 1);
            if (tab.PosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            pos.DefinirValores(posicao.Linha + 1, posicao.Coluna - 2);
            if (tab.PosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            return mat;
        }
    }
}