using System.Diagnostics;

namespace VoicesCalculatorApplication
{
    public class OutputWriter : IWriter
    {
        public void WriteLine(string text)
        {
            Debug.WriteLine(text);
        }
    }
}
