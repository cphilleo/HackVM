namespace HackVM
{
    public static class ParserContext
    {
        private static int _nextLabelId;
        private static bool _inFunction;
        private static string _currentFunction;
        private static string _fileName;


        public static string GetUniqueLabel(string name)
        {
            _nextLabelId++;
            return string.Format("__{0}_{1}", name, _nextLabelId);
        }

        public static string GetLabelFor(string name)
        {
            if (_inFunction)
            {
                return string.Format("{0}${1}", _currentFunction, name);
            }

            return name;
        }

        public static void SetFunction(string name)
        {
            _inFunction = true;
            _currentFunction = name;
        }

        public static void SetFile(string fileName)
        {
            _fileName = fileName;
        }

        public static string GetStatic(string index)
        {
            return string.Format("{0}.{1}", _fileName, index);
        }
    }
}