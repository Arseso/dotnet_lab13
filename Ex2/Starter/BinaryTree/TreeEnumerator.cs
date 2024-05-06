using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTree
{
    internal class TreeEnumerator<TItem> : IEnumerator<TItem> where TItem : IComparable<TItem>
    {
        private Tree<TItem> currentData = null;
        private TItem currentItem = default(TItem);
        private Queue<TItem> enumData = null;

        public TreeEnumerator(Tree<TItem> data)
        {
            this.currentData = data;
        }

        TItem IEnumerator<TItem>.Current
        {
            get
            {
                if (this.enumData == null)
                {
                    throw new InvalidOperationException("Use MoveNext before calling Current");
                }
                return this.currentItem;
            }
        }

        object IEnumerator.Current => throw new NotImplementedException();

        void IDisposable.Dispose()
        {
            this.enumData.Clear();
        }

        bool System.Collections.IEnumerator.MoveNext()
        {
            if (this.enumData == null)
            {
                this.enumData = new Queue<TItem>();
                Populate(this.enumData, this.currentData);
            }
            if (this.enumData.Count > 0)
            {
                this.currentItem = this.enumData.Dequeue();
                return true;
            }
            return false;
        }

        private void Populate(Queue<TItem> enumQueue, Tree<TItem> tree)
        {
            if (tree.LeftTree != null)
            {
                Populate(enumQueue, tree.LeftTree);
            }
            enumQueue.Enqueue(tree.NodeData);
            if (tree.RightTree != null)
            {
                Populate(enumQueue, tree.RightTree);
            }
        }

        void System.Collections.IEnumerator.Reset()
        {
            Populate(this.enumData, this.currentData);
        }
    }
}
