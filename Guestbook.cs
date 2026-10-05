using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Uppgift_3
{
    class Guestbook
    {
        //skapa list objektet Posts med en get; set; från början
        public List<Post> Posts { get; set; } = new List<Post>();
        public bool AddPost(string name, string post)
        {
            Console.Clear();
            //kontroll om fält lämnats tomma, eller bara har mellanslag
            //ingen kontroll om abc eller 123, användaren kan ju heta/skriva vad de vill
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(post))
            {
                return false;
            }

            //skapa nytt Post objekt, spara namn och inlägg i det
            Post newPost = new Post(name, post);
            Posts.Add(newPost);

            //spara i json fil via metod
            Save();
            return true;
        }
        public void ShowAllPosts()
        {
            Console.Clear();
            //loopa igenom
            //för varje objekt i listan posts, visa
            for (int i = 0; i < Posts.Count; i++)
            {
                //skriv ut index, namn och inlägg. 
                //i+1 pga att senare, i RemovePost, kan användaren ta bort 1,2,3 istället för array index 0,1,2
                //ett streck mellan inlägg, för bättre överblick
                Console.WriteLine($"== [{i + 1}] av {Posts[i].Name} ==");
                Console.WriteLine(Posts[i].Message);
                Console.WriteLine("----------------");
            }
        }

        public void RemovePost(string userRemove)
        {
            Console.Clear();
            //kontroll om användaren lämnat fält tomt eller bara mellanslag
            if (string.IsNullOrWhiteSpace(userRemove))
            {
                Console.WriteLine("Du måste ange ett nummer för att ta bort ett inlägg.");
                Console.WriteLine("Tryck på en tangent för att återgå till menyn.");
                Console.ReadLine();
                return;
            }
            //går det inte att göra om string till int? visa felmeddelande, annars gå vidare

            if (!int.TryParse(userRemove, out int userIndex))
            {
                Console.WriteLine("Ange ett giltigt nummer för att ta bort ett inlägg.");
                Console.WriteLine("Tryck på en tangent för att återgå till menyn.");
                Console.ReadLine();
                return;
            }

            //skapa "nytt" användarval: om användare valde 1, gör till 0 (matcha array med poster)
            int listIndex = userIndex - 1;

            //om listIndex är större eller lika med 0 och om listIndex är mindre än antalet poster
            if (listIndex >= 0 && listIndex < Posts.Count)
            {
                Posts.RemoveAt(listIndex);
                Console.WriteLine($"Inlägg nummer {userIndex} har tagits bort");

                //spara förändringen
                Save();
            }
            else
            {
                Console.WriteLine("Det finns inget inlägg med det index numret.");
            }

            Console.WriteLine("Tryck på en tangent för att återvända till menyn.");
            Console.ReadLine();
            
        }
        //=== Några små metoder som underlättar ===
        //liten metod för att spara, så kod inte upprepas
        //private för att bara Guestbook ska kunna använda den
        private void Save()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(this, options);
            File.WriteAllText("guestbook.json", json);
        }
        public bool HasPosts()
        {
            return Posts.Count > 0;
        }
    }
}
