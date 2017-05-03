using JogoDaVelha.Domain;
using JogoDaVelha.Domain.Enums;
using System;
using System.Linq;

namespace JogoDaVelha
{
    class Program
    {
        static void Main(string[] args)
        {
            var jogo = new Jogo();

            while (jogo.SituacaoJogo == SituacaoJogoRetorno.EmAndamento)
            {
                Console.WriteLine("  a b c");
                Console.WriteLine($"1 {(jogo.Jogadas.Any(x => x.Posicao == Posicao.A1) ? jogo.Jogadas.FirstOrDefault(x => x.Posicao == Posicao.A1).Jogador.ToString() : " ") }|{(jogo.Jogadas.Any(x => x.Posicao == Posicao.B1) ? jogo.Jogadas.FirstOrDefault(x => x.Posicao == Posicao.B1).Jogador.ToString() : " ") }|{(jogo.Jogadas.Any(x => x.Posicao == Posicao.C1) ? jogo.Jogadas.FirstOrDefault(x => x.Posicao == Posicao.C1).Jogador.ToString() : " ") }");
                Console.WriteLine("  -----");
                Console.WriteLine($"2 {(jogo.Jogadas.Any(x => x.Posicao == Posicao.A2) ? jogo.Jogadas.FirstOrDefault(x => x.Posicao == Posicao.A2).Jogador.ToString() : " ") }|{(jogo.Jogadas.Any(x => x.Posicao == Posicao.B2) ? jogo.Jogadas.FirstOrDefault(x => x.Posicao == Posicao.B2).Jogador.ToString() : " ") }|{(jogo.Jogadas.Any(x => x.Posicao == Posicao.C2) ? jogo.Jogadas.FirstOrDefault(x => x.Posicao == Posicao.C2).Jogador.ToString() : " ") }");
                Console.WriteLine("  -----");
                Console.WriteLine($"3 {(jogo.Jogadas.Any(x => x.Posicao == Posicao.A3) ? jogo.Jogadas.FirstOrDefault(x => x.Posicao == Posicao.A3).Jogador.ToString() : " ") }|{(jogo.Jogadas.Any(x => x.Posicao == Posicao.B3) ? jogo.Jogadas.FirstOrDefault(x => x.Posicao == Posicao.B3).Jogador.ToString() : " ") }|{(jogo.Jogadas.Any(x => x.Posicao == Posicao.C3) ? jogo.Jogadas.FirstOrDefault(x => x.Posicao == Posicao.C3).Jogador.ToString() : " ") }");
                Console.WriteLine("");
                Console.WriteLine($"Vez do jogador { jogo.JogadorDaVez }: ");

                var jogada = Console.ReadLine();
                Console.Clear();

                switch (jogo.Jogar(jogada))
                {
                    case JogarRetorno.Ok: break;
                    case JogarRetorno.Invalida: Console.WriteLine("Jogada invalida"); break;
                    case JogarRetorno.JaRealizada: Console.WriteLine("Posicao nao disponivel"); break;
                }
            }

            Console.Write($"Fim jogo - ");

            switch (jogo.SituacaoJogo)
            {
                case SituacaoJogoRetorno.XVenceu:
                    Console.Write($"X venceu!");
                    break;
                case SituacaoJogoRetorno.OVenceu:
                    Console.Write($"O venceu!");
                    break;
                case SituacaoJogoRetorno.Empate:
                    Console.Write($"Empate!");
                    break;
            }

            Console.ReadKey();
        }
    }
}