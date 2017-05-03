using JogoDaVelha.Domain.Enums;

namespace JogoDaVelha.Domain
{
    public class Jogada
    {
        public int Id { get; set; }
        public Posicao Posicao { get; set; }
        public Jogador Jogador { get; set; }
    }
}
