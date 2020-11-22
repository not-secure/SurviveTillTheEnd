using System;
using System.Collections.Generic;

namespace Common {
    public class PriorityQueue<TKey, TValue> where TKey: IComparable<TKey> {
        private List<TValue> inner;
        
        public void Enqueue(TKey key, TValue value) {
            
        }

        public TValue Dequeue() {
            throw new System.NotImplementedException();
        }

        public int FindLocation(TKey key) {
            // Do a binary search to find where to insert
            int start = 0;
            int end = this.inner.Count;
            
            throw new System.NotImplementedException();
        }

        public int Count() {
            return inner.Count;
        }
    }
}