package main

import (
	"bufio"
	"fmt"
	"os"
	"strings"
)

type Jogada struct {
	posicao  string
	caracter string
}

func main() {
	reader := bufio.NewReader(os.Stdin)

	var lista []Jogada
	jogadorQueComeca := ""
	fimJogo := false
	colunas := []string{"A", "B", "C"}
	linhas := []string{"1", "2", "3"}

	clear()

	for {

		fmt.Print("Quem começa (x/o): ")
		jogadorQueComeca, _ = reader.ReadString('\n')

		if jogadorQueComeca[0] == 'x' || jogadorQueComeca[0] == 'o' {
			break
		}

		clear()
		fmt.Println("Valor invalido")
		fmt.Println("")
	}

	clear()

	for !fimJogo {

		DesenharMapa(lista, colunas, linhas)

		fmt.Print("Jogada (ex. B2): ")
		jogada, _ := reader.ReadString('\n')

		if len(jogada) != 4 || !contains(colunas, strings.ToUpper(string(jogada[0]))) || !contains(linhas, string(jogada[1])) {

			clear()
			fmt.Println("Jogada errada")
			fmt.Println("")
			continue
		}

		posicao := strings.ToUpper(string(jogada[0])) + string(jogada[1])

		if jogadasContains(lista, posicao) {

			clear()
			fmt.Println("Posição já selecionada")
			fmt.Println("")
			continue
		}

		lista = append(lista, Jogada{caracter: string(jogadorQueComeca[0]), posicao: posicao})

		if string(jogadorQueComeca[0]) == "x" {
			jogadorQueComeca = "o"
		} else {
			jogadorQueComeca = "x"
		}

		clear()

		fimJogo = FimJogo(lista, colunas, linhas)
	}
}

func DesenharMapa(lista []Jogada, colunas []string, linhas []string) {

	fmt.Println("    A   B   C ")
	fmt.Println("")

	for linha := 0; linha < len(linhas); linha++ {

		fmt.Print(string(linha+49) + "  ")

		for coluna := 0; coluna < len(colunas); coluna++ {
			if jogadasContains(lista, colunas[coluna]+linhas[linha]) {
				fmt.Print(" " + Jogada(First(lista, colunas[coluna]+linhas[linha])).caracter + " ")
			} else {
				fmt.Print("   ")
			}

			if coluna < len(colunas)-1 {
				fmt.Print("|")
			}
		}

		fmt.Println("")
		fmt.Println("   -----------")
	}
}

func FimJogo(lista []Jogada, colunas []string, linhas []string) bool {

	return false
}

func First(lista []Jogada, posicao string) Jogada {

	for _, item := range lista {
		if item.posicao == posicao {
			return item
		}
	}

	return Jogada{}
}

func contains(list []string, value string) bool {

	for _, item := range list {
		if item == value {
			return true
		}
	}

	return false
}

func jogadasContains(list []Jogada, value string) bool {

	for _, item := range list {
		if item.posicao == value {
			return true
		}
	}

	return false
}

func clear() {
	print("\033[H\033[2J")
}
