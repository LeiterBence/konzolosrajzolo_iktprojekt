namespace konzolosrajzolo_ikt
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Kiírás();
        }
        static void Kiírás()
        {
            string[] menuk = { "M - Menü", "X - Kilépés", "Karakter: " };
            int i = 0;
            foreach (var k in menuk)
            {
                
                Console.WriteLine($"{k}");
                Console.SetCursorPosition(25,i );
                i++;
                Console.Write("|");
                Console.WriteLine("");
            }
            Console.ReadLine();
        }
    }
}
