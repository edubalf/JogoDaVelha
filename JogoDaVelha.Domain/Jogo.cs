using JogoDaVelha.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JogoDaVelha.Domain
{
    public class Jogo
    {
        public Jogador JogadorDaVez { get; private set; }
        public List<Jogada> Jogadas { get; private set; }
        public SituacaoJogoRetorno SituacaoJogo
        {
            get
            {
                var jogadasJogadorX = Jogadas.Where(x => x.Jogador == Jogador.X).Select(x => x.Posicao).ToList();
                var jogadasJogadorO = Jogadas.Where(x => x.Jogador == Jogador.O).Select(x => x.Posicao).ToList();

                if (JogadorVenceu(jogadasJogadorX))
                {
                    return SituacaoJogoRetorno.XVenceu;
                }

                if (JogadorVenceu(jogadasJogadorO))
                {
                    return SituacaoJogoRetorno.OVenceu;
                }

                if (Jogadas.Count == 9)
                    return SituacaoJogoRetorno.Empate;

                return SituacaoJogoRetorno.EmAndamento;
            }
        }

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

        private bool JogadorVenceu(List<Posicao> jogadas)
        {
            foreach (var item in PosicoesFimJogo)
            {
                var count = 0;

                foreach (var posicao in jogadas)
                {
                    if (item.Contains(posicao))
                    {
                        count++;
                    }
                }

                if (count == 3)
                    return true;
            }

            return false;
        }

        private readonly List<List<Posicao>> PosicoesFimJogo = new List<List<Posicao>>
        {
            new List<Posicao>
            {
                Posicao.A1,
                Posicao.B1,
                Posicao.C1
            },
            new List<Posicao>
            {
                Posicao.A2,
                Posicao.B2,
                Posicao.C2
            },
            new List<Posicao>
            {
                Posicao.A3,
                Posicao.B3,
                Posicao.C3
            },
            new List<Posicao>
            {
                Posicao.A1,
                Posicao.A2,
                Posicao.A3
            },
            new List<Posicao>
            {
                Posicao.B1,
                Posicao.B2,
                Posicao.B3
            },
            new List<Posicao>
            {
                Posicao.C1,
                Posicao.C2,
                Posicao.C3
            },
            new List<Posicao>
            {
                Posicao.A1,
                Posicao.B2,
                Posicao.C3
            },
            new List<Posicao>
            {
                Posicao.A3,
                Posicao.B2,
                Posicao.C1
            }
        };
    }
}
