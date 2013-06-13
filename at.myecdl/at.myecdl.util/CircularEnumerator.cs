using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace at.myecdl.util {
    public class PredicateEnumerator<T> : IEnumerator<T> {
        private IEnumerator<T> baseEnumberator;
        private Predicate<T> predicate;

        public PredicateEnumerator(IEnumerator<T> baseEnumerator, Predicate<T> predicate) {
            this.baseEnumberator = baseEnumerator;
            this.predicate = predicate;
        }

        public T Current {
            get { return baseEnumberator.Current; }
        }

        public void Dispose() {
            baseEnumberator.Dispose();
        }

        object System.Collections.IEnumerator.Current {
            get { return baseEnumberator.Current; }
        }

        public bool MoveNext() {
            bool hasNext = false;
            bool acceptsPredicate = false;
            do {
                hasNext = baseEnumberator.MoveNext();
                if (hasNext) {
                    acceptsPredicate = predicate(Current);
                }
            } while (!acceptsPredicate && hasNext);
            return hasNext;
        }

        public void Reset() {
            baseEnumberator.Reset();
        }
    }

    public class CircularEnumerator<T> : IEnumerator<T> {
        private IEnumerator<T> baseEnumerator;

        public CircularEnumerator(IEnumerable<T> baseEnumerable) : this(baseEnumerable.GetEnumerator()) {
        }


        public CircularEnumerator(IEnumerator<T> baseEnumerator) {
            this.baseEnumerator = baseEnumerator;
        }

        public T Current {
            get { return baseEnumerator.Current; }
        }

        public void Dispose() {
            baseEnumerator.Dispose();
        }

        object System.Collections.IEnumerator.Current {
            get { return baseEnumerator.Current; }
        }

        public bool MoveNext() {
            bool hasNext = baseEnumerator.MoveNext();
            if (!hasNext) {
                Reset();
                hasNext = baseEnumerator.MoveNext();
            }
            return hasNext;
        }

        public void Reset() {
            baseEnumerator.Reset();
        }
    }
}
