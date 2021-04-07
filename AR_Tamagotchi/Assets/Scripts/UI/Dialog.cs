namespace UI
{
    [System.Serializable]
    public class Dialog
    {
        public string Name;
        public string[] Sentences;

        public Dialog(string name, params string[] sentences)
        {
            Name = name;
            Sentences = sentences;
        }
    }
}