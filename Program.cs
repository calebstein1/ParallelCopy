namespace ParallelCopy
{
    class Program
    {
        private static List<Task> _copies = [];

        internal static async Task<int> Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Must provide at least 2 arguments");
                return -1;
            }
            
            var outPath = args[^1];

            for (var i = 0; i < args.Length - 1; i++)
            {
                var inName = Path.GetFileName(args[i]);
                var inStream = new FileStream(args[i], FileMode.Open);
                var outStream = new FileStream(Path.Combine(outPath, inName), FileMode.OpenOrCreate);
                
                Console.WriteLine($"Copying {args[i]} to {outPath}");
                _copies.Add(inStream.CopyToAsync(outStream));
            }
            
            await Task.WhenAll(_copies);
            Console.WriteLine("Done!");
            return 0;
        }
    }
}
