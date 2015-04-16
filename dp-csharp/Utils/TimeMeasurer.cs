namespace DpCSharp
{
    using System;
    using System.Diagnostics;

    public static class TimeMeasurer
    {
        /// <summary>
        /// Compute the elapsed time of an action.
        /// </summary>
        /// <param name="action"></param>
        /// <returns>Time in Millisecond.</returns>
        public static long ElapsedMilliseconds(Action action)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            action.Invoke();
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }
    }
}
