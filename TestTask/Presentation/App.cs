using TestTask.Application.Services;

namespace TestTask.Presentation
{
    internal class App
    {
        public void Run()
        {
            try
            {
                var handler = new Handler();
                handler.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
