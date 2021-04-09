using System;

namespace Tarefa2
{
    public class Program
    {
        private static void Main()
        {
            // Cria e imprime no console a árvore do cenário 1
            int[] cenario1 = { 3, 2, 1, 6, 0, 5 };
            ArvoreMaxima arvore = ArvoreMaxima.Criar(cenario1);
            Console.WriteLine("Árvore do cenário 1:");
            arvore.InOrderTraversal();
            Console.WriteLine();
            arvore.PreOrderTraversal();
            Console.WriteLine();
            arvore.PostorderTraversal();

            // Cria e imprime no console a árvore do cenário 2
            int[] cenario2 = { 7, 5, 13, 9, 1, 6, 4 };
            arvore = ArvoreMaxima.Criar(cenario2);
            Console.WriteLine("\n\nÁrvore do cenário 2:");
            arvore.InOrderTraversal();
            Console.WriteLine();
            arvore.PreOrderTraversal();
            Console.WriteLine();
            arvore.PostorderTraversal();

            // Exemplo usando uma árvore binária cujos valores maiores vão para a direita e os valores menores para a esquerda
            ArvoreBinaria raiz = new();
            raiz.Inserir(75);
            raiz.Inserir(57);
            raiz.Inserir(90);
            raiz.Inserir(32);
            raiz.Inserir(7);
            raiz.Inserir(44);
            raiz.Inserir(60);
            raiz.Inserir(86);
            raiz.Inserir(93);
            raiz.Inserir(99);
            raiz.Inserir(100);
            Node no = raiz.Buscar(44);
            no = raiz.BuscarRecursivo(44);
            int altura = no.ObterAltura();
            int folhas = raiz.NumeroDeFolhas();
            int? menorValor = raiz.Menor();
            int? maiorValor = raiz.Maior();
            bool arvoreBalanceada = raiz.EstaBalanceado();
        }
    }
}