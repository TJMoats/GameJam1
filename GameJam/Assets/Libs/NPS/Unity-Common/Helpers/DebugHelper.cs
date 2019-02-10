namespace NPS
{
    public class Debug : UnityEngine.Debug
    {
        public static void LogSpread(params object[] _items)
        {
            string output = "[";
            foreach(object message in _items)
            {
                if (message != null)
                {
                    output += message.ToString() + ", ";
                }
            }
            if (output != "[")
            {
                output = output.Substring(0, output.Length - 2);
            }
            output += "]";
            UnityEngine.Debug.Log(output);
        }
    }
}