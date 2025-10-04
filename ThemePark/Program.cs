namespace ThemePark
{
    internal class Program
    {
        static void Main(string[] args)
        {
           

            Visitor luffy = new Visitor("luffy", 18, 160);

            Console.WriteLine(luffy.CheckHeight((Ride)Zones.North[0]));
            Console.WriteLine(Zones.North[0].Name);

            StaffMember n = new StaffMember();
            Console.WriteLine(n.Name);

            bool inPark = true;
            while(inPark)
            {
                Console.WriteLine("Welcome to the park! \nWhere would you like to go? ");
                Console.WriteLine("1: North Section \n2: South Section \n3: East Section \n4: West Section ");
                Console.Write("Chose number or type exit to leave: ");
                string choice = Console.ReadLine();
                switch(choice)
                {
                    case "1":
                        ShowcaseZones(Zones.North);

                        Console.ReadKey();
                        break;
                    case "2":
                        ShowcaseZones(Zones.South);
                        break;
                    case "3":
                        ShowcaseZones(Zones.East);
                        break;
                    case "4":
                        ShowcaseZones(Zones.West);
                        break;
                    case "exit":
                        inPark = false;
                        break;
                }
            }
           
            Console.ReadKey();
         }
        

        public static void NorthZone()
        {

        }

        public static void SelectAttraction()
        {
            Console.WriteLine("Select the attraction you want to experience");
            int choice = int.Parse(Console.ReadLine());

        }

        public static void ShowcaseZones(Attractions[] a)
        {
            for (int i = 0; i < a.Length; i++)
            {
                Console.WriteLine($"{i+1}: {a[i].Name}");
            }
        }

        public void RestirctionControl(bool a, bool h)
        {
            if (a && h) Console.WriteLine("Welcome");
            else Console.WriteLine("Unable to ride");
        }
    }
}
