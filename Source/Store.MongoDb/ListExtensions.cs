using System;
using System.Collections.Generic;
using System.Threading;

namespace Affecto.PositiveFeedback.Store.MongoDb
{
    internal static class ListExtensions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            int i = list.Count;
            while (i > 1)
            {
                i--;
                int random = ThreadSafeRandom.ThisThreadsRandom.Next(i + 1);
                T value = list[random];
                list[random] = list[i];
                list[i] = value;
            }
        }
    }

    internal static class ThreadSafeRandom
    {
        [ThreadStatic]
        private static Random local;

        public static Random ThisThreadsRandom
        {
            get { return local ?? (local = new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
        }
    }
}