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
                PartidaXadrez partida = new PartidaXadrez();

                while (!partida.tab.PartidaTerminada)
                {
                    try
                    {
                        Console.Clear();
                        Tela.imprimirPartida(partida);

                        Console.Write("Origem:");
                        Posicao origem = Tela.LerPosicao().ToPosicao();
                        partida.validarPosicaoOrigem(origem);

                        bool[,] posicoesPossiveis = partida.tab.peca(origem).MovimentosPossiveis();

                        Console.Clear();
                        Tela.imprimirTabuleiro(partida.tab, posicoesPossiveis);

                        Console.Write("Destino:");
                        Posicao destino = Tela.LerPosicao().ToPosicao();
                        partida.ValidarPosicaoDestino(origem, destino);
                        partida.RealizaJogada(origem, destino);
                    }catch(TabuleiroException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                        
                    }
                }

            }
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
                
            }

        }
    }
}
