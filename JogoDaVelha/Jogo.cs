using ElemarJR.FunctionalCSharp;
using JogoDaVelha.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JogoDaVelha
{
    public class Jogo
    {
        public Jogador JogadorDaVez { get; private set; }
        public List<Jogada> Jogadas { get; private set; }
        public Jogador? FimJogo => JogoAcabou();

        public Jogo()
        {
            JogadorDaVez = Jogador.X;
            Jogadas = new List<Jogada>();
        }

        public JogarRetorno Jogar(string posicao)
        {
            var posicaoNormalizada = new Posicao();

            if (!Enum.TryParse(posicao.ToUpper(), out posicaoNormalizada))
                return JogarRetorno.Invalida;

            if (Jogadas.Any(x => x.Posicao == posicaoNormalizada))
                return JogarRetorno.JaRealizada;

            Jogadas.Add(new Jogada
            {
                Id = Jogadas.Count > 0 ? Jogadas.Max(x => x.Id) + 1 : 1,
                Posicao = posicaoNormalizada,
                Jogador = JogadorDaVez
            });

            JogadorDaVez = JogadorDaVez == Jogador.O ? Jogador.X : Jogador.O;

            return JogarRetorno.Ok;
        }

        private Jogador? JogoAcabou()
        {
            var jogadasJogadorX = Jogadas.Where(x => x.Jogador == Jogador.X);
            var jogadasJogadorO = Jogadas.Where(x => x.Jogador == Jogador.O);

            if (jogadasJogadorX.Count(x => x.Posicao == Posicao.A1 || x.Posicao == Posicao.A2 || x.Posicao == Posicao.A3) == 3
                || jogadasJogadorX.Count(x => x.Posicao == Posicao.B1 || x.Posicao == Posicao.B2 || x.Posicao == Posicao.B3) == 3
                || jogadasJogadorX.Count(x => x.Posicao == Posicao.C1 || x.Posicao == Posicao.C2 || x.Posicao == Posicao.C3) == 3
                || jogadasJogadorX.Count(x => x.Posicao == Posicao.A1 || x.Posicao == Posicao.B1 || x.Posicao == Posicao.C1) == 3
                || jogadasJogadorX.Count(x => x.Posicao == Posicao.A2 || x.Posicao == Posicao.B2 || x.Posicao == Posicao.C2) == 3
                || jogadasJogadorX.Count(x => x.Posicao == Posicao.A3 || x.Posicao == Posicao.B3 || x.Posicao == Posicao.C3) == 3
                || jogadasJogadorX.Count(x => x.Posicao == Posicao.A1 || x.Posicao == Posicao.B2 || x.Posicao == Posicao.C3) == 3
                || jogadasJogadorX.Count(x => x.Posicao == Posicao.A3 || x.Posicao == Posicao.B2 || x.Posicao == Posicao.C1) == 3)
            {
                return Jogador.X;
            }

            if (jogadasJogadorO.Count(x => x.Posicao == Posicao.A1 || x.Posicao == Posicao.A2 || x.Posicao == Posicao.A3) == 3
                || jogadasJogadorO.Count(x => x.Posicao == Posicao.B1 || x.Posicao == Posicao.B2 || x.Posicao == Posicao.B3) == 3
                || jogadasJogadorO.Count(x => x.Posicao == Posicao.C1 || x.Posicao == Posicao.C2 || x.Posicao == Posicao.C3) == 3
                || jogadasJogadorO.Count(x => x.Posicao == Posicao.A1 || x.Posicao == Posicao.B1 || x.Posicao == Posicao.C1) == 3
                || jogadasJogadorO.Count(x => x.Posicao == Posicao.A2 || x.Posicao == Posicao.B2 || x.Posicao == Posicao.C2) == 3
                || jogadasJogadorO.Count(x => x.Posicao == Posicao.A3 || x.Posicao == Posicao.B3 || x.Posicao == Posicao.C3) == 3
                || jogadasJogadorO.Count(x => x.Posicao == Posicao.A1 || x.Posicao == Posicao.B2 || x.Posicao == Posicao.C3) == 3
                || jogadasJogadorO.Count(x => x.Posicao == Posicao.A3 || x.Posicao == Posicao.B2 || x.Posicao == Posicao.C1) == 3)
            {
                return Jogador.O;
            }

            return null;
        }
    }
}
