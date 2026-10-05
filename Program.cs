/*Uppgift 3: Objektorienterad programmering med C#
 * 
 * Beskrivning:
 * Skapa en applikation som fungerar som gästbok.
 * 
 * Lösningen ska innehålla felhantering som kontrollerar om alla inmatningsfält innehåller data.
 * 
 * Steg 1:
 * Skapa en gästbok som en konsolapplikation med möjlighet att lägga till en post, ta bort en valfri post samt visa alla poster.
 * 
 * 2:
 * Ett enklare meny-system hanterar del val som ska kunna genomföras
 * - Lägg till inlägg ska ge dig valet att mata in ägare samt det nya inlägget. Dessa fält får inte vara tomma.
 * - Ta bort inlägg ska fråga efter valt index (till vänster i listan av inlägg på bild ovan) att ta bort innan radering av inlägget.
 * 
 * 3:
 * Inlägg ska innehålla två fält. "ägare" till inlägg samt text för inlägget
 * 
 * 4: Gästbokens inlägg ska serialiseras/deserialiseras samt sparas på fil antingen binärt eller som json, så att tidigare inmatad data finns lagrad.
 * 
 * 5:
 * Felhantering i form av en kontroll så att inmatningsfält inte är tomma
 * 
 * 6:
 * Efter varje genomfört menyval ska skärmen skrivas om. Sker enklast genom att man "rensar" konsolen och sedan ritar/skriver om den. 
 */

using System;
using System.Text.Json;
namespace Uppgift_3
{
    class Program
    {
        static void Main()
        {
            //skapa nytt objekt av klassen Guestbook - detta objekt kommer att innehålla alla inlägg som skapas i applikationen
            Guestbook guestbook = new Guestbook();
            //om guestbook.json finns, läs in och gör om sedan till C# objekt
            if (File.Exists("guestbook.json"))
            {
                string json = File.ReadAllText("guestbook.json");
                //använd guestbook variabeln: den ska omvandla filen från json format
                //skulle filen visa sig vara tom skapas en ny tom Guestbook objekt
                guestbook = JsonSerializer.Deserialize<Guestbook>(json) ?? new Guestbook();
            }
            while (true)
            {
                //clear i början: rensar bort tidigare "konversation" med menyn till exempel
                Console.Clear();
                Console.WriteLine("=== GÄSTBOK ===");
                Console.WriteLine("Välj vad du vill göra:");
                Console.WriteLine("");
                Console.WriteLine("1. Skapa nytt inlägg");
                Console.WriteLine("2. Visa alla inlägg");
                Console.WriteLine("3. Ta bort ett inlägg");
                Console.WriteLine("4. Avsluta");

                //spara användarens val i variabel
                string userChoice = Console.ReadLine() ?? "";
                //försök omvandla till ny variabel int numberChoice
                if (!int.TryParse(userChoice, out int numberChoice))
                {
                    //om abc, felmeddelande
                    Console.WriteLine("Tänk på att välja ett nummer mellan 1 och 4!");
                    Console.WriteLine("Tryck på en tangent för att försöka igen.");
                    Console.ReadLine();
                    continue;
                }
                //om valt nummer är mindre än 1 eller högre än 4, felmeddelande
                if (numberChoice < 1 || numberChoice > 4)
                {
                    Console.WriteLine("Vänligen välj ett nummer mellan 1 och 4 för att kunna utföra en handling!");
                    Console.WriteLine("Tryck på en tangent för att försöka igen.");
                    Console.ReadLine();
                    continue;
                }

                switch (numberChoice)
                {
                    //skapa ett nytt inlägg
                    case 1:
                        Console.Clear();
                        Console.WriteLine("Ditt namn:");
                        //för att inte lämna null: vänster värde eller (??) "". 
                        //detta kontrolleras i metod AddPost() 
                        string name = Console.ReadLine() ?? "";

                        Console.WriteLine("Ditt inlägg:");
                        string post = Console.ReadLine() ?? "";

                        //värden skickas med AddPost() som kontrollerar om värdena är tomma. Om tomt returneras false och felmeddelande skrivs ut. 
                        //Annars true och ett nytt inlägg skapas och sparas i guestbook.json filen
                        if(!guestbook.AddPost(name, post))
                        {
                            Console.WriteLine("Du kan inte lämna namn eller inlägg tomt!");
                            Console.WriteLine("Tryck på en tangent för att försöka igen.");
                            Console.ReadLine();
                        }
                        else
                        {
                            Console.WriteLine("Nytt inlägg skapat!");
                            Console.WriteLine("Tryck på en tangent för att återvända till menyn.");
                            Console.ReadLine();
                        }
                        break;

                    //visa alla inlägg
                    case 2:
                        Console.Clear();
                        if(!guestbook.HasPosts())
                        {
                            Console.WriteLine("Här var det tomt! Skriv ett inlägg för att inreda lite!");
                        }
                        else
                        {
                            guestbook.ShowAllPosts();
                        }
                        
                        Console.WriteLine("Tryck på en tangent för att fortsätta");
                        Console.ReadLine();
                        break;

                    //radera ett inlägg
                    case 3:
                        Console.Clear();
                        Console.WriteLine("Oj då! Vilket inlägg vill du radera?");
                        string userRemove = Console.ReadLine() ?? "";

                        guestbook.RemovePost(userRemove);
                        break;

                    //avsluta programmet
                    case 4:
                        Console.Clear();
                        Console.WriteLine("Bye bye!");
                        Thread.Sleep(2000);
                        Console.Clear();
                        return;
                    default:
                        break;
                }
            }
        }
    }
}