using System.Drawing;
using System.Security.Cryptography.X509Certificates;

namespace DesafioJogoDaVelha
{
    internal class Program
    {

        static char[,] matrizJogoDaVelha = new char[3, 3];
        static int posicaoLinha = 0, posicaoColuna = 0;
        static char simboloDaPosicao = 'X';
        static char continuarJogo = 'S';
        static void Main(string[] args)
        {
            /*
             * Desenvolva um jogo da velha utilizando matrizes em C#. Faça com que cada jogador insira a sua jogada em uma interface amigavel. 
             * Teste se a posição é válida e caso não seja solicite ao jogador repetir a jogada. Após cada jogada, apresente o tabuleiro com as jogadas representadas por "X" e "O" e faça a verficação se algum jogador venceu.
             * Caso seja empate, apresente o resultado na tela. Possilibilite que o jogo seja reinicializado sem a necessidade de reiniciar o jogo. 
             * 
             * Desafio extra, pode valer por alguma atividade futura: Faça a implementação de um jogo contra o computador. Faça o possível para evitar que o jogador vença do computador. 
             * Para facilitar, faça com que o computador inicie jogando.
             * 
             * Prazo de entrega: 24/04
             * Forma e envio: Enviar pelo chat o link do github
             * 
             * **/
            Console.WriteLine("Bem vindo ao Jogo da Velha");

            // inicializo o jogo da velha e mostro ele com suas posicoes vazias
            InicializarJogoDaVelha();
            MostrarJogoDaVelha();

            Console.WriteLine();

            // entrada do nome dos jogadores
            Console.BackgroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Informe o nome do jogador que irá utilizar o X: ");
            Console.ResetColor();
            string jogadorX = Console.ReadLine();
            

            Console.Clear();

            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("Informe o nome do jogador que irá utilizar o O: ");
            Console.ResetColor();
            string jogadorO = Console.ReadLine();



            simboloDaPosicao = 'X';

            // leitura das posições. Como a matriz tem um tamanho de 3x3, isso totaliza 9 jogadas. Em cada jogada é informado a posição da linha e da coluna
            while(continuarJogo == 'S')
            {
                InicializarJogoDaVelha();
                for (int quantidadeJogadas = 0; quantidadeJogadas < 9; quantidadeJogadas++)
                {
                    do
                    {
                        MostrarJogoDaVelha();
                        Console.WriteLine();
                        if (simboloDaPosicao == 'X')
                        {
                            Console.BackgroundColor = ConsoleColor.Magenta;
                            Console.WriteLine($"{jogadorX} agora é sua vez! Pense bem antes de fazer sua jogada.");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.WriteLine($"{jogadorO} agora é a sua vez! Pense bem antes de fazer sua jogada.");
                            Console.ResetColor();
                        }

                        Console.WriteLine("Informe a a posição da linha: ( 1 a 3)");
                        posicaoLinha = int.Parse(Console.ReadLine()) - 1;

                        Console.WriteLine("Informe a posição da Coluna: ( 1 a 3)");
                        posicaoColuna = int.Parse(Console.ReadLine()) - 1;

                        // o -1 na matriz é para que a matriz não estoure seu tamanho. Ja que o jogador ira informa numero de 1 a 3. E uma matriz vai do 0 a 2. Pois 0 conta como uma posição
                        // como estou usando o do while dentro do for. Fiz duas validações das jogadas, a primeira é pra so exibir a mensagem se não for valido. A segunda no while é pra retornar o laço. Verifico tanto a posicao valida como se a posicao ja foi preenchida
                        if (posicaoLinha < 0 || posicaoLinha > 2 || posicaoColuna < 0 || posicaoColuna > 2 || matrizJogoDaVelha[posicaoLinha, posicaoColuna] != ' ')
                        {
                            Console.WriteLine("Jogada inválida. Informe uma jogada válida. Pressione <Enter> para continuar");
                            Console.ReadLine();
                        }
                        Console.WriteLine("Jogada efetuada");
                    } while (posicaoLinha < 0 || posicaoLinha > 2 || posicaoColuna < 0 || posicaoColuna > 2 || matrizJogoDaVelha[posicaoLinha, posicaoColuna] != ' ');


                    // uma vez valida a posicao. A mesma é preenchida com o simbolo do jogador.
                    matrizJogoDaVelha[posicaoLinha, posicaoColuna] = simboloDaPosicao;

                    if (VerificaQuemVenceu())
                    {
                        Console.Clear();
                        MostrarJogoDaVelha();
                        if (simboloDaPosicao == 'X')
                        {
                            Console.WriteLine($"O jogador {jogadorX} venceu");
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"O jogador {jogadorO} venceu");
                            break;
                        }
                    }


                    // if para alternar os jogadores a cada jogada
                    if (simboloDaPosicao == 'X')
                    {
                        simboloDaPosicao = 'O';
                    }
                    else
                    {
                        simboloDaPosicao = 'X';
                    }
                }
                // se todas jogadas forem feitas. Ou seja, sair do laço For. Então houve um empate.
                if (VerificaQuemVenceu() == false)
                {
                    Console.WriteLine("Empate!");
                }

                Console.WriteLine("Deseja jogar novamente? \nDigite: \nS - se sim \nN - se não.");
                continuarJogo = char.Parse(Console.ReadLine());
            }
            
        }


        // inicializo o jogo da velha com suas posições vazias
        static void InicializarJogoDaVelha()
        {
            for (int linha = 0; linha < 3; linha++)
            {
                for (int coluna = 0; coluna < 3; coluna++)
                {
                    matrizJogoDaVelha[linha, coluna] = ' '; 
                }
            }
        }


        static bool VerificaQuemVenceu()
        {
            // for para verifica vitoria em alguma linha -  
            for (int linha = 0; linha < 3; linha++)
            {
                if (matrizJogoDaVelha[linha, 0] != ' ' && matrizJogoDaVelha[linha, 0] == matrizJogoDaVelha[linha, 1] && matrizJogoDaVelha[linha, 1] == matrizJogoDaVelha[linha, 2])
                {
                    return true;
                }
            }

            // for para verifica vitoria em alguma coluna |
            for (int coluna = 0; coluna < 3; coluna++)
            {
                if (matrizJogoDaVelha[0, coluna] != ' ' && matrizJogoDaVelha[0, coluna] == matrizJogoDaVelha[1, coluna] && matrizJogoDaVelha[1, coluna] == matrizJogoDaVelha[2, coluna])
                {
                    return true;
                }
            }

            // verifica vitoria diagonal \
            if (matrizJogoDaVelha[0, 0] != ' ' && matrizJogoDaVelha[0, 0] == matrizJogoDaVelha[1, 1] && matrizJogoDaVelha[1, 1] == matrizJogoDaVelha[2, 2])
            {
                return true;
            }

            // verifica vitória diagonal /
            if (matrizJogoDaVelha[0, 2] != ' ' && matrizJogoDaVelha[0, 2] == matrizJogoDaVelha[1, 1] && matrizJogoDaVelha[1, 1] == matrizJogoDaVelha[2, 0])
            {
                return true;
            }
            // se nenhuma condição foi verdadeira, retorna false. Ou seja, não teve vitória.
            return false;
        }


        static void MostrarJogoDaVelha()
        {
            Console.Clear();

            // percorre as posicoes e mostra o simbolo
            for (int linha = 0; linha < 3; linha++)
            {
                for (int coluna = 0; coluna < 3; coluna++)
                {
                    //imprimir simbolo dentro da posicao da matriz
                    Console.Write($" {matrizJogoDaVelha[linha, coluna]} ");

                    // formatação/desenho do jogo da velha - coluna
                    if (coluna < 2)
                    {
                        Console.Write("|"); 
                    }
                }
                Console.WriteLine();
                // formatação/desenho do jogo da velha - linhas
                if (linha < 2)
                {
                    Console.WriteLine("---+---+---");
                }
            }
        }
    }
}