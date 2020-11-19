using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScoreManager.Controllers;


namespace ScoreManager
{
    class Program
    {
        static void Main(string[] args)
        {
            ScoreController scoreController = new ScoreController();
            scoreController.Run();
        }
    }
}
