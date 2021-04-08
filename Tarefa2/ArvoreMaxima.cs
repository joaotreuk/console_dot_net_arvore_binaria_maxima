/// <summary>
/// Esta árvore é formada a partir de um array de inteiros sem repetição
/// A raiz da árvore é sempre o maior valor
/// Os valores são ramificados para apenas uma direção continuamente em ordem decrescente
/// </summary>
using System;

namespace Tarefa2
{
    public class ArvoreMaxima
    {
        private readonly int valor;
        private readonly ArvoreMaxima galhoEsquerda;
        private readonly ArvoreMaxima galhoDireita;

        // Construtor da árvore
        public ArvoreMaxima(int valor, ArvoreMaxima galhoEsquerda, ArvoreMaxima galhoDireita)
        {
            this.valor = valor;
            this.galhoEsquerda = galhoEsquerda;
            this.galhoDireita = galhoDireita;
        }

        // Cria a árvore com o node raiz
        public static ArvoreMaxima Criar(int[] lista)
        {
            int indiceDoMaior = EncontrarIndiceMaior(lista, 0, lista.Length);

            // Definindo as arrays de cada lado da árvore
            int[] listaEsquerda = new int[indiceDoMaior], listaDireita = new int[lista.Length - indiceDoMaior - 1];
            Array.Copy(lista, 0, listaEsquerda, 0, indiceDoMaior);
            Array.Copy(lista, indiceDoMaior + 1, listaDireita, 0, listaDireita.Length);

            return new(lista[indiceDoMaior], CriarGalhos(listaEsquerda, true), CriarGalhos(listaDireita, false));
        }

        // Cria um galho que se expande até a array deste lado acabar
        private static ArvoreMaxima CriarGalhos(int[] lista, bool esquerda)
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
        // Ordenando os nós dá extrema esquerda até a raiz e depois até os nós da extrema direita
        public void InOrderTraversal()
        {
            if (galhoEsquerda != null)
            {
                galhoEsquerda.InOrderTraversal();
            }

            Console.Write($"{valor} ");

            if (galhoDireita != null)
            {
                galhoDireita.InOrderTraversal();
            }
        }

        // Ordenando os nós em: raiz -> esquerda -> direita
        public void PreOrderTraversal()
        {
            Console.Write($"{valor} ");

            if (galhoEsquerda != null)
            {
                galhoEsquerda.PreOrderTraversal();
            }

            if (galhoDireita != null)
            {
                galhoDireita.PreOrderTraversal();
            }
        }

        // Ordenando os nós em: esquerda -> direita -> raiz
        public void PostorderTraversal()
        {
            if (galhoEsquerda != null)
            {
                galhoEsquerda.PostorderTraversal();
            }

            if (galhoDireita != null)
            {
                galhoDireita.PostorderTraversal();
            }

            Console.Write($"{valor} ");
        }
    }
}