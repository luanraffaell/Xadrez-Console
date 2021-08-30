using System;
using System.Collections.Generic;
using System.Text;

namespace tabuleiro
{
    class Tabuleiro
    {
        public int linhas { get; set; }
        public int colunas { get; set; }
        private Peca[,] pecas;
        public bool PartidaTerminada { get; set; }

        public Tabuleiro(int linhas, int colunas)
        {
            this.linhas = linhas;
            this.colunas = colunas;
            pecas = new Peca[linhas, colunas];
            PartidaTerminada = false;
        }
        public Peca peca(int linha,int coluna)
        {
            return pecas[linha, coluna];
        }
        public Peca peca(Posicao pos)
        {
            return pecas[pos.Linha, pos.Coluna];
        }
        public bool ExistePeca(Posicao pos)
        {
            ValidarPosicao(pos);
            return peca(pos) != null;
        }
        public void ColocarPeca(Peca p, Posicao pos)
        {
            if (ExistePeca(pos))
            {
                throw new TabuleiroException("Já existe uma peça nesta posição");
            }
            pecas[pos.Linha, pos.Coluna] = p;
            p.posicao = pos;
        }
        //só posso colocar uma peça aonde não tenha uma peça
        public Peca RemoverPeca(Posicao pos)
        {
            if (!ExistePeca(pos))
            {
                return null;
            }
            Peca p = peca(pos);
            p.posicao = null;
            pecas[pos.Linha, pos.Coluna] = null;
            return p;
        }

       
        //retorna true ou false para caso a posição for valida ou não ex matriz 0,1,2 seleciono posição com numero 3
        public bool PosicaoValida(Posicao pos)
        {
            if(pos.Linha <0 || pos.Linha >= linhas ||pos.Coluna <0 || pos.Coluna >= colunas)
            {
                return false;
            }
            return true;
        }
        public void ValidarPosicao(Posicao pos)
        {
            if (!PosicaoValida(pos))
            {
                throw new TabuleiroException("Posição invalida!");
            }
        }
    }
}
