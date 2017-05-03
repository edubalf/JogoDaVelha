using JogoDaVelha.Domain;
using JogoDaVelha.Domain.Enums;
using Xunit;

namespace JogoDaVelha.Test
{
    public class JogoTest
    {
        [Fact]
        public void Jogador_x_ganha_horizontal()
        {
            var jogo = new Jogo();

            jogo.Jogar("a1");
            jogo.Jogar("a2");
            jogo.Jogar("b1");
            jogo.Jogar("b2");
            jogo.Jogar("c1");

            Assert.Equal(SituacaoJogoRetorno.XVenceu, jogo.SituacaoJogo);
        }

        [Fact]
        public void Jogador_o_ganha_vertical()
        {
            var jogo = new Jogo();

            jogo.Jogar("c3");
            jogo.Jogar("a1");
            jogo.Jogar("b1");
            jogo.Jogar("a2");
            jogo.Jogar("b2");
            jogo.Jogar("a3");

            Assert.Equal(SituacaoJogoRetorno.OVenceu, jogo.SituacaoJogo);
        }

        [Fact]
        public void Empate()
        {
            var jogo = new Jogo();

            jogo.Jogar("b2");
            jogo.Jogar("a1");
            jogo.Jogar("b1");
            jogo.Jogar("c1");
            jogo.Jogar("a2");
            jogo.Jogar("c2");
            jogo.Jogar("a3");
            jogo.Jogar("b3");
            jogo.Jogar("c3");

            Assert.Equal(SituacaoJogoRetorno.Empate, jogo.SituacaoJogo);
        }
    }
}
