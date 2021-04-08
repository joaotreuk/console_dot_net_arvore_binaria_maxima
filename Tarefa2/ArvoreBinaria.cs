/// <summary>
/// Binary Tree vs Binary Search Tree
/// Typically don't use binary tree... instead we need to use a binary search tree in which
/// Left node is less than parent  and right node is greater than its parent
/// Binary Search Tree is a logical structure (abstract data type) - so it can be implemented in different ways
/// Balanced vs unbalanced
//like all right nodes instead of left and right nodes evenly distributed

//height
// 2^h-1 = n -> h = log(n+1) -> O logn)
//      O     Level 0
//     / \
//    O   O   Level 1
//   /\   /\
//  O  O O  O Level 2
//time complexity for delete and insert are also log n because essentially they are finding a node 
//and then adding constant time to do the operation so we can forget about the added constant time

//Data structures that use BinarySearchTree in their implementation are the C# sortedDictionary, Java Treemap and the C++ set class
//Also the heep is an implementation of a Binary Search Tree
/// </summary>
namespace Tarefa2
{
    public class ArvoreBinaria
    {
        public Node Raiz { get; private set; }

        public Node Buscar(int data)
        {
            //if the root is not null then we call the find method on the root
            if (Raiz != null)
            {
                // call node method Find
                return Raiz.Buscar(data);
            }
            else
            {//the root is null so we return null, nothing to find
                return null;
            }
        }

        public Node BuscarRecursivo(int data)
        {
            //if the root is not null then we call the recursive find method on the root
            if (Raiz != null)
            {
                //call Node Method FindRecursive
                return Raiz.BuscarRecursivo(data);
            }
            else
            {//the root is null so we return null, nothing to find
                return null;
            }

        }

        public void Inserir(int data)
        {
            //if the root is not null then we call the Insert method on the root node
            if (Raiz != null)
            {
                Raiz.Inserir(data);
            }
            else
            {//if the root is null then we set the root to be a new node based on the data passed in
                Raiz = new Node(data);
            }
        }

        public void Remover(int data)
        {
            //Set the current and parent node to root, so when we remove we can remove using the parents reference
            Node current = Raiz;
            Node parent = Raiz;
            bool isLeftChild = false;//keeps track of which child of parent should be removed

            //empty tree
            if (current == null)
            {//nothing to be removed, end method
                return;
            }

            //Find the Node
            //loop through until node is not found or if we found the node with matching data
            while (current != null && current.Valor != data)
            {
                //set current node to be new parent reference, then we look at its children
                parent = current;

                //if the data we are looking for is less than the current node then we look at its left child
                if (data < current.Valor)
                {
                    current = current.NoEsquerdo;
                    isLeftChild = true;//Set the variable to determin which child we are looking at
                }
                else
                {//Otherwise we look at its right child
                    current = current.NoDireito;
                    isLeftChild = false;//Set the variable to determin which child we are looking at
                }
            }

            //if the node is not found nothing to delete just return
            if (current == null)
            {
                return;
            }

            //We found a Leaf node aka no children
            if (current.NoDireito == null && current.NoEsquerdo == null)
            {
                //The root doesn't have parent to check what child it is,so just set to null
                if (current == Raiz)
                {
                    Raiz = null;
                }
                else
                {
                    //When not the root node
                    //see which child of the parent should be deleted
                    if (isLeftChild)
                    {
                        //remove reference to left child node
                        parent.NoEsquerdo = null;
                    }
                    else
                    {   //remove reference to right child node
                        parent.NoDireito = null;
                    }
                }
            }
            else if (current.NoDireito == null) //current only has left child, so we set the parents node child to be this nodes left child
            {
                //If the current node is the root then we just set root to Left child node
                if (current == Raiz)
                {
                    Raiz = current.NoEsquerdo;
                }
                else
                {
                    //see which child of the parent should be deleted
                    if (isLeftChild)//is this the right child or left child
                    {
                        //current is left child so we set the left node of the parent to the current nodes left child
                        parent.NoEsquerdo = current.NoEsquerdo;
                    }
                    else
                    {   //current is right child so we set the right node of the parent to the current nodes left child
                        parent.NoDireito = current.NoEsquerdo;
                    }
                }
            }
            else if (current.NoEsquerdo == null) //current only has right child, so we set the parents node child to be this nodes right child
            {
                //If the current node is the root then we just set root to Right child node
                if (current == Raiz)
                {
                    Raiz = current.NoDireito;
                }
                else
                {
                    //see which child of the parent should be deleted
                    if (isLeftChild)
                    {   //current is left child so we set the left node of the parent to the current nodes right child
                        parent.NoEsquerdo = current.NoDireito;
                    }
                    else
                    {   //current is right child so we set the right node of the parent to the current nodes right child
                        parent.NoDireito = current.NoDireito;
                    }
                }
            }
            else//Current Node has both a left and a right child
            {
                //When both child nodes exist we can go to the right node and then find the leaf node of the left child as this will be the least number
                //that is greater than the current node. It may have right child, so the right child would become..left child of the parent of this leaf aka successer node

                //Find the successor node aka least greater node
                Node successor = ObterSucessor(current);
                //if the current node is the root node then the new root is the successor node
                if (current == Raiz)
                {
                    Raiz = successor;
                }
                else if (isLeftChild)
                {//if this is the left child set the parents left child node as the successor node
                    parent.NoEsquerdo = successor;
                }
                else
                {//if this is the right child set the parents right child node as the successor node
                    parent.NoDireito = successor;
                }
            }
        }

        private static Node ObterSucessor(Node node)
        {
            Node parentOfSuccessor = node;
            Node successor = node;
            Node current = node.NoDireito;

            //starting at the right child we go down every left child node
            while (current != null)
            {
                parentOfSuccessor = successor;
                successor = current;
                current = current.NoEsquerdo;// go to next left node
            }
            //if the succesor is not just the right node then
            if (successor != node.NoDireito)
            {
                //set the Left node on the parent node of the succesor node to the right child node of the successor in case it has one
                parentOfSuccessor.NoEsquerdo = successor.NoDireito;
                //attach the right child node of the node being deleted to the successors right node
                successor.NoDireito = node.NoDireito;
            }
            //attach the left child node of the node being deleted to the successors leftnode node
            successor.NoEsquerdo = node.NoEsquerdo;

            return successor;
        }

        // Mark Node as deleted
        public void SoftDelete(int data)
        {
            //find node then set property isdeleted to true
            Node toDelete = Buscar(data);
            if (toDelete != null)
            {
                toDelete.Deletar();
            }
        }

        // Find smallest value in the tree
        public int? Menor()
        {
            //if we have a root node then we can search for the smallest node
            return Raiz?.MenorValor();
        }

        // Find largest value in the tree
        public int? Maior()
        {
            //if we have a root node then we can search for the largest node
            return Raiz?.MaiorValor();
        }

        public int NumeroDeFolhas()
        {
            //if root is null then  number of leafs is zero
            return Raiz == null ? 0 : Raiz.ObterNumeroDeFolhas();
        }

        public int ObterAltura()
        {
            //if root is null then height is zero
            return Raiz == null ? 0 : Raiz.ObterAltura();
        }

        // Check if the binary tree is balanced. A balanced tree occurs when the height of two subtrees of any node do not differe more than 1.
        public bool EstaBalanceado()
        {
            if (Raiz == null)//Empty Tree
            {
                return true;
            }

            return Raiz.EstaBalanceado();
        }

        //There are many self balancing trees
        //Some to look at are
        //Red Black Trees
        //AVL Trees
    }
}
