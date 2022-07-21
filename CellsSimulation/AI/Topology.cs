namespace MonoGameWindowsDesktopApplication1.AI
{
    public class Topology
    {
        public int InputNeuronsCount { get; }
        public int[] HiddenNeuronsArray { get; }
        public int OutputNeuronsCount { get; }

        public Topology(int inputNeuronsCount, int outputNeuronsCount, params int[] hiddenNeurons)
        {
            InputNeuronsCount = inputNeuronsCount;
            HiddenNeuronsArray = hiddenNeurons;
            OutputNeuronsCount = outputNeuronsCount;
        }
    }
}