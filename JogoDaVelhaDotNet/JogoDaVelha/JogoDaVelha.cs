using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoDaVelha
{
    public class JogoDaVelha
    {
        private int NumeroJogada = 1;
        public List<string> Colunas { get; private set; } = new List<string> { "A", "B", "C" };
        public List<string> Linhas { get; private set; } = new List<string> { "1", "2", "3" };
        public List<string> PosicoesPossiveis { get; private set; } = new List<string>();
        public List<Jogada> Jogo { get; private set; } = new List<Jogada>();
        public Jogador JogadorDaVez { get; private set; }

        public JogoDaVelha(Jogador jogadorQueComeca)
        {
            JogadorDaVez = jogadorQueComeca;

            CarregarPosicoesPossiveis();
        }

        public Tuple<bool, string> AdicionarJogada(string posicao)
        {
            var fimJogo = FimDoJogo();
            if (fimJogo.Item1)
            {
                return new Tuple<bool, string>(false, "Jogo já acabou");
            }

            var jogadaValida = JogadaValida(posicao);
            if (!jogadaValida.Item1)
            {
                return jogadaValida;
            }

            Jogo.Add(new Jogada
            {
                Jogador = JogadorDaVez,
                Posicao = posicao,
                NumeroJogada = NumeroJogada++
            });

            JogadorDaVez = JogadorDaVez == Jogador.X ? Jogador.O : Jogador.X;

            return new Tuple<bool, string>(true, string.Empty);
        }

        public Tuple<bool, Jogador?> FimDoJogo()
        {
            var fimJogoHorizontal = FimJogoHorizontal();
            if (fimJogoHorizontal.Item1) return fimJogoHorizontal;

            var fimJogoVertical = FimJogoVertical();
            if (fimJogoVertical.Item1) return fimJogoVertical;

            var fimJogoDiagonalEsquerdaDireita = FimJogoDiagonalEsquerdaDireita();
            if (fimJogoDiagonalEsquerdaDireita.Item1) return fimJogoDiagonalEsquerdaDireita;

            var fimJogoDiagonalDireitaEsquerda = FimJogoDiagonalDireitaEsquerda();
            if (fimJogoDiagonalDireitaEsquerda.Item1) return fimJogoDiagonalDireitaEsquerda;

            var empate = Empate();
            if (empate.Item1) return empate;

            return new Tuple<bool, Jogador?>(false, null);
        }

        private Tuple<bool, Jogador?> FimJogoHorizontal()
        {
            for (int coluna = 0; coluna < Colunas.Count; coluna++)
            {
                var quantidadePreenchida = Jogo.Where(x => x.Posicao.Contains(Colunas[coluna])).ToList();

                if (quantidadePreenchida.Count == Colunas.Count)
                {
                    if (quantidadePreenchida.Count(x => x.Jogador == Jogador.X) == Colunas.Count)
                    {
                        return new Tuple<bool, Jogador?>(true, Jogador.X);
                    }

                    if (quantidadePreenchida.Count(x => x.Jogador == Jogador.O) == Colunas.Count)
                    {
                        return new Tuple<bool, Jogador?>(true, Jogador.O);
                    }
                }
            }

            return new Tuple<bool, Jogador?>(false, null);
        }

        private Tuple<bool, Jogador?> FimJogoVertical()
        {
            for (var linha = 0; linha < Linhas.Count; linha++)
            {
                var quantidadePreenchida = Jogo.Where(x => x.Posicao.Contains(Linhas[linha])).ToList();

                if (quantidadePreenchida.Count == Linhas.Count)
                {
                    if (quantidadePreenchida.Count(x => x.Jogador == Jogador.X) == Linhas.Count)
                    {
                        return new Tuple<bool, Jogador?>(true, Jogador.X);
                    }

                    if (quantidadePreenchida.Count(x => x.Jogador == Jogador.O) == Linhas.Count)
                    {
                        return new Tuple<bool, Jogador?>(true, Jogador.O);
                    }
                }
            }

            return new Tuple<bool, Jogador?>(false, null);
        }

        private Tuple<bool, Jogador?> FimJogoDiagonalEsquerdaDireita()
        {
            var diagonalEsquerda = new List<string>();

            for (int i = 0; i < Colunas.Count; i++)
            {
                diagonalEsquerda.Add(Colunas[i] + Linhas[i]);
            }

            var quantidadeEsquerda = Jogo.Where(x => diagonalEsquerda.Contains(x.Posicao)).ToList();

            if (quantidadeEsquerda.Count == diagonalEsquerda.Count)
            {
                if (quantidadeEsquerda.Count(x => x.Jogador == Jogador.X) == Linhas.Count)
                {
                    return new Tuple<bool, Jogador?>(true, Jogador.X);
                }

                if (quantidadeEsquerda.Count(x => x.Jogador == Jogador.O) == Linhas.Count)
                {
                    return new Tuple<bool, Jogador?>(true, Jogador.O);
                }
            }

            return new Tuple<bool, Jogador?>(false, null);
        }

        private Tuple<bool, Jogador?> FimJogoDiagonalDireitaEsquerda()
        {
            var diagonalDireita = new List<string>();

            for (int coluna = Colunas.Count - 1, linha = 0; coluna >= 0; coluna--, linha++)
            {
                diagonalDireita.Add(Colunas[coluna] + Linhas[linha]);
            }

            var quantidadeDireita = Jogo.Where(x => diagonalDireita.Contains(x.Posicao)).ToList();

            if (quantidadeDireita.Count == diagonalDireita.Count)
            {
                if (quantidadeDireita.Count(x => x.Jogador == Jogador.X) == Linhas.Count)
                {
                    return new Tuple<bool, Jogador?>(true, Jogador.X);
                }

                if (quantidadeDireita.Count(x => x.Jogador == Jogador.O) == Linhas.Count)
                {
                    return new Tuple<bool, Jogador?>(true, Jogador.O);
                }
            }

            return new Tuple<bool, Jogador?>(false, null);
        }

        private Tuple<bool, Jogador?> Empate()
        {
            if (Jogo.Count == PosicoesPossiveis.Count)
            {
                return new Tuple<bool, Jogador?>(true, null);
            }

            return new Tuple<bool, Jogador?>(false, null);
        }

        private Tuple<bool, string> JogadaValida(string posicao)
        {
            if (!PosicoesPossiveis.Contains(posicao))
            {
                return new Tuple<bool, string>(false, "Posição invalida");
            }

            if (Jogo.Any(x => x.Posicao == posicao))
            {
                return new Tuple<bool, string>(false, "Posição já foi marcada");
            }

            return new Tuple<bool, string>(true, string.Empty);
        }

        private void CarregarPosicoesPossiveis()
        {
            Colunas.ForEach(coluna =>
            {
                Linhas.ForEach(linha =>
                {
                    PosicoesPossiveis.Add($"{coluna}{linha}");
                });
            });
        }
    }
}
