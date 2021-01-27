using FlooringOrderingSystem.UI.Workflows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrderingSystem.UI
{
    public class Menu
    {

        public static void Start()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Flooring Ordering Program");
                Console.WriteLine("------------------------");
                Console.WriteLine("1. Display Orders");
                Console.WriteLine("2. Add an Order");
                Console.WriteLine("3. Edit an Order");
                Console.WriteLine("4. Remove an Order");
                Console.WriteLine("Q to Quit");

                Console.Write("\nEnter selection: ");

                string userinput = Console.ReadLine();

                switch (userinput)
                {
                    case "1":
                        DisplayOrderWorkflow displayWorkflow = new DisplayOrderWorkflow();
                        displayWorkflow.Execute();
                        break;
                    case "2":
                        AddOrderWorkflow addOrderWorkflow = new AddOrderWorkflow();
                        addOrderWorkflow.Execute();
                        break;
                    case "3":
                        EditOrderWorkflow editOrderWorkflow = new EditOrderWorkflow();
                        editOrderWorkflow.Execute();
                        break;
                    case "4":
                        RemoveOrderWorkflow removeOrderWorkflow = new RemoveOrderWorkflow();
                        removeOrderWorkflow.Execute();
                        break;
                    case "Q":
                        return;
                }

            }

        }
    }
}
