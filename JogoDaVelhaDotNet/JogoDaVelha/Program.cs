using System;
using System.Collections.Generic;
using System.Linq;

namespace JogoDaVelha
{
    class Program
    {
        static void Main(string[] args)
        {
            JogoDaVelha jogoDaVelha;

            while (true)
            {
                Console.Write("Quem começa (X/O): ");
                var jogadorDaVez = Console.ReadLine().ToUpper();

                if (new List<string> { "X", "O" }.Contains(jogadorDaVez))
                {
                    jogoDaVelha = new JogoDaVelha((Jogador)Enum.Parse(typeof(Jogador), jogadorDaVez));
                    break;
                }

                Console.WriteLine("Jogador invalido");
                Console.WriteLine();

                Console.Clear();
            }

            Console.Clear();

            while (true)
            {
                DesenharJogo(jogoDaVelha);

                Console.Write($"Vez do jogador { jogoDaVelha.JogadorDaVez.ToString() } (ex. B2): ");
                var posicao = Console.ReadLine().ToUpper().Trim();

                var jogada = jogoDaVelha.AdicionarJogada(posicao);
                if (!jogada.Item1)
                {
                    Console.WriteLine(jogada.Item2);
                    continue;
                }

                var fimJogo = jogoDaVelha.FimDoJogo();
                if (fimJogo.Item1)
                {
                    Console.Clear();

                    DesenharJogo(jogoDaVelha);

                    if (fimJogo.Item2 == null)
                    {
                        Console.WriteLine("Empate!");
                        break;
                    }

                    Console.WriteLine($"Jogador {fimJogo.Item2.ToString()} venceu!");
                    break;
                }
                
                Console.Clear();
            }

            Console.ReadLine();
        }

        private static void DesenharJogo(JogoDaVelha jogoDaVelha)
        {
            Console.Write("  ");

            jogoDaVelha.Colunas.ForEach(coluna => Console.Write($" {coluna}"));
            Console.WriteLine();

            jogoDaVelha.Linhas.ForEach(linha =>
            {
                Console.Write($" {linha} ");

                for (int coluna = 0; coluna < jogoDaVelha.Colunas.Count; coluna++)
                {
                    if (jogoDaVelha.Jogo.Any(x => x.Posicao == jogoDaVelha.Colunas[coluna] + linha))
                        Console.Write(jogoDaVelha.Jogo.FirstOrDefault(x => x.Posicao == jogoDaVelha.Colunas[coluna] + linha).Jogador);
                    else
                        Console.Write(" ");

                    if (jogoDaVelha.Colunas.Count - 1 == coluna)
                        Console.WriteLine();
                    else
                        Console.Write("|");
                }

                Console.WriteLine("   -----");
            });
        }
    }
}
