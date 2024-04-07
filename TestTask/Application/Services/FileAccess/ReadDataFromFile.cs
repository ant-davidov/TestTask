namespace TestTask.Application.Services.FileAccess
{
    internal static class ReadDataFromFile
    {
        public static IEnumerable<List<string>> ReadBlocks(string filePath, int blockSize)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                List<string> block = new List<string>();
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    block.Add(line);

                    if (block.Count >= blockSize)
                    {
                        yield return block;
                        block = new List<string>();
                    }
                }
                if (block.Count > 0)
                {
                    yield return block;
                }
            }
        }
    }
}
