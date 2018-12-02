using System;
using System.Collections.Generic;

namespace VoicesCalculatorApplication
{
    public class Messager
    {
        #region Fields

        private readonly IList<IWriter> _writers;

        #endregion

        #region Instance

        private static Messager _instance;

        private static Messager Instance
        {
            get { return _instance ?? (_instance = new Messager()); }
        }

        #endregion

        #region Ctor

        private Messager()
        {
            _writers = new List<IWriter>();
        }

        #endregion

        public static void WriteLine(string format, params object[] args)
        {
            var message = string.Format(format, args);
            foreach (var writer in Instance._writers)
                writer.WriteLine(message);
        }

        public static void WriteLine(string text)
        {
            foreach (var writer in Instance._writers)
                writer.WriteLine(text);
        }

        public static void WriteLine()
        {
            foreach (var writer in Instance._writers)
                writer.WriteLine(string.Empty);
        }

        public static void AddWriter(IWriter writer)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");
            Instance._writers.Add(writer);
        }
    }
}
