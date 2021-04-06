using System;

namespace Tarefa2
{
    public class Program
    {
        private static void Main()
        {
            // Cria e imprime no console a árvore do cenário 1
            int[] cenario1 = { 3, 2, 1, 6, 0, 5 };
            Arvore arvore = Arvore.Criar(cenario1);
            Console.WriteLine("Árvore do cenário 1:");
            arvore.Imprimir();

            // Cria e imprime no console a árvore do cenário 2
            int[] cenario2 = { 7, 5, 13, 9, 1, 6, 4 };
            Arvore arvore2 = Arvore.Criar(cenario2);
            Console.WriteLine("\n\nÁrvore do cenário 2:");
            arvore2.Imprimir();
            Console.WriteLine("\n");
        }
    }

    public class Arvore
    {
        private readonly int valor;
        private readonly Arvore galhoEsquerda;
        private readonly Arvore galhoDireita;

        // Construtor da árvore
        public Arvore(int valor, Arvore galhoEsquerda, Arvore galhoDireita)
        {
            this.valor = valor;
            this.galhoEsquerda = galhoEsquerda;
            this.galhoDireita = galhoDireita;
        }

        // Cria a árvore com o node raiz
        public static Arvore Criar(int[] lista)
        {
            int indiceDoMaior = EncontrarIndiceMaior(lista, 0, lista.Length);

            // Definindo as arrays de cada lado da árvore
            int[] listaEsquerda = new int[indiceDoMaior], listaDireita = new int[lista.Length - indiceDoMaior - 1];
            Array.Copy(lista, 0, listaEsquerda, 0, indiceDoMaior);
            Array.Copy(lista, indiceDoMaior + 1, listaDireita, 0, listaDireita.Length);

            return new(lista[indiceDoMaior], CriarGalhos(listaEsquerda, true), CriarGalhos(listaDireita, false));
        }

        // Cria um galho que se expande até a array deste lado acabar
        private static Arvore CriarGalhos(int[] lista, bool esquerda)
        {
            // Se o galho não tiver mais sub-galhos
            if (lista.Length == 0)
            {
                return null;
            }

            int indiceDoMaior = EncontrarIndiceMaior(lista, 0, lista.Length);
            int valor = lista[indiceDoMaior];

            lista = Array.FindAll(lista, (n) => n != valor);

            // Cria os sub-galhos para o lado determinado
            if (esquerda)
            {
                return new(valor, CriarGalhos(lista, esquerda), null);
            }
            else
            {
                return new(valor, null, CriarGalhos(lista, esquerda));
            }
        }

        // Obtém o índice do maior valor da array passada
        private static int EncontrarIndiceMaior(int[] lista, int esquerda, int direita)
        {
            int maior = esquerda;

            for (int i = esquerda; i < direita; i++)
            {
                if (lista[i] > lista[maior])
                {
                    maior = i;
                }
            }

            return maior;
        }

        // Imprime no console a árvore
        // Pega os galhos dá extrema esquerda até a raiz e depois até a extrema direita
        public void Imprimir()
        {
            if (galhoEsquerda != null)
            {
                galhoEsquerda.Imprimir();
            }

            Console.Write($"{valor} ");

            if (galhoDireita != null)
            {
                galhoDireita.Imprimir();
            }
        }
    }
}