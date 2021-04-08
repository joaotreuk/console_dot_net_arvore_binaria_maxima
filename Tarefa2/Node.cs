using System;

namespace Tarefa2
{
    public class Node
    {
        public int Valor { get; }
        public Node NoDireito { get; set; }
        public Node NoEsquerdo { get; set; }
        public bool EstaDeletado { get; private set; }

        public Node(int valor)
        {
            Valor = valor;
        }

        // Deleção suave no nó
        public void Deletar()
        {
            EstaDeletado = true;
        }

        // Buscar um nó
        public Node Buscar(int valor)
        {
            //this node is the starting current node
            Node currentNode = this;

            //loop through this node and all of the children of this node
            while (currentNode != null)
            {
                //if the current nodes data is equal to the value passed in return it
                if (valor == currentNode.Valor && EstaDeletado == false)//soft delete check
                {
                    return currentNode;
                }
                else
                {
                    currentNode = valor > currentNode.Valor ? currentNode.NoDireito : currentNode.NoEsquerdo;
                }
            }

            //Node not found
            return null;
        }

        // Buscar um nó de forma recursiva
        public Node BuscarRecursivo(int valor)
        {
            //value passed in matches nodes data return the node
            return valor == Valor && EstaDeletado == false
                ? this
                : valor < Valor && NoEsquerdo != null ? NoEsquerdo.BuscarRecursivo(valor) : NoDireito?.BuscarRecursivo(valor);
        }

        // Chama recursivamente o método inserir para baixo na árvore até encontrar um ponto aberto
        public void Inserir(int value)
        {
            //if the value passed in is greater or equal to the data then insert to right node
            if (value >= Valor)
            {
                //if right child node is null create one
                if (NoDireito == null)
                {
                    NoDireito = new Node(value);
                }
                else
                {
                    //if right node is not null recursivly call insert on the right node
                    NoDireito.Inserir(value);
                }
            }
            else
            {
                //if the value passed in is less than the data then insert to left node
                if (NoEsquerdo == null)
                {
                    //if the leftnode is null then create a new node
                    NoEsquerdo = new Node(value);
                }
                else
                {
                    //if the left node is not null then recursively call insert on the left node
                    NoEsquerdo.Inserir(value);
                }
            }
        }

        public int? MenorValor()
        {
            // once we reach the last left node we return its data
            return NoEsquerdo == null ? Valor : NoEsquerdo.MenorValor();
        }

        internal int? MaiorValor()
        {
            // once we reach the last right node we return its data
            return NoDireito == null ? Valor : NoDireito.MaiorValor();
        }

        public int ObterAltura()
        {
            //return 1 when leaf node is found
            if (NoEsquerdo == null && NoDireito == null)
            {
                return 1; //found a leaf node
            }

            int left = 0, right = 0;

            //recursively go through each branch
            if (NoEsquerdo != null)
            {
                left = NoEsquerdo.ObterAltura();
            }

            if (NoDireito != null)
            {
                right = NoDireito.ObterAltura();
            }

            //return the greater height of the branch
            if (left > right)
            {
                return (left + 1);
            }
            else
            {
                return (right + 1);
            }
        }

        public int ObterNumeroDeFolhas()
        {
            //return 1 when leaf node is found
            if (NoEsquerdo == null && NoDireito == null)
            {
                return 1; //found a leaf node
            }

            int leftLeaves = 0;
            int rightLeaves = 0;

            //recursively call NumOfLeafNodes returning 1 for each leaf found
            if (NoEsquerdo != null)
            {
                leftLeaves = NoEsquerdo.ObterNumeroDeFolhas();
            }
            if (NoDireito != null)
            {
                rightLeaves = NoDireito.ObterNumeroDeFolhas();
            }

            //add values together 
            return leftLeaves + rightLeaves;
        }

        public bool EstaBalanceado()
        {
            int LeftHeight = NoEsquerdo != null ? NoEsquerdo.ObterAltura() : 0,
                RightHeight = NoDireito != null ? NoDireito.ObterAltura() : 0;

            int heightDifference = LeftHeight - RightHeight;

            return Math.Abs(heightDifference) <= 1
                && (NoEsquerdo == null || NoEsquerdo.EstaBalanceado())
                && (NoDireito == null || NoDireito.EstaBalanceado());
        }
    }
}
