using System;
using System.Collections.Generic;
using System.Text;

/*cada peça do tabuleiro se encontra em uma posição e tenho que representar isso.*/

namespace tabuleiro
{
    class Posicao
    {
        public int Linha { get; set; } //uma posição é dizer em qual linha e coluna a peça está
        public int Coluna { get; set; }

        public Posicao(int linha, int coluna)
        {
            Linha = linha;
            Coluna = coluna;
        }
        public override string ToString()
        {
            return Linha + ", "+Coluna;
        }
    }
}
