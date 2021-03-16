using System;
using System.Linq;

namespace BookCollection.Core
{
    public static class DbInitializer
    {
        public static void Initialize(BookCollectionContext context)
        {
            context.Database.EnsureCreated();

            if (context.Books.Any())
            {
                return;
            }

            var books = new Book[]
            {
                new Book{Title = "Skills, Drills & Strategies for Badminton", Author = "Don Paup", Description = "This book illustrates correct techniques and demonstrates how to achieve optimal results in Badminton. It focuses on skills and drills or program design, and addresses a broad range of strategies specifically designed to improve performance now and in the future."},
                new Book{Title = "How to Avoid a Climate Disaster", Author = "Bill Gates", Description = "In this urgent, authoritative book, Bill Gates sets out a wide-ranging, practical - and accessible - plan for how the world can get to zero greenhouse gas emissions in time to avoid a climate catastrophe. Bill Gates has spent a decade investigating the causes and effects of climate change. With the help of experts in the fields of physics, chemistry, biology, engineering, political science, and finance, he has focused on what must be done in order to stop the planet's slide toward certain environmental disaster."},
                new Book{Title = "The Easy Solution to The Rubik's Cube", Author = "Chad Bomberger", Description = "Inside How to Solve a Rubik’s Cube, you’ll discover simple, easy-to-understand instructions for wrapping your brain around this fascinating and intriguing puzzle. Even if you’re a complete beginner, you can easily solve one of the world’s top-ranked and most-beloved puzzles – in the wink of an eye!"},
                new Book{Title = "Musta hevonen", Author = "Rauli Partanen", Description = "Jos ilmaston lämpenemistä ei saada hillittyä, maailmaa uhkaa mittava ympäristökriisi ja globaali yhteiskuntien sekasorto. Onnistuaksemme urakassa tarvitsemme kaikki keinot, myös ydinvoiman. Ydinvoimaan liittyy värikästä retoriikkaa, politiikkaa, pelkoa, usein skandaalihakuista uutisointia ja mielikuvia vakavista onnettomuuksista ja radioaktiivisesta säteilystä. Mutta kuinka monet näistä peloista ovat aiheettomia?"},
                new Book{Title = "Energy Transitions", Author = "Vaclav Smil", Description = "This book provides a detailed, global examination of energy transitions, supplying a long-term historical perspective, an up-to-date assessment of recent and near-term advances in energy production technology and implementation, and an explanation of why efforts to limit global warming and to shift away from fossil fuels have been gradual."},
                new Book{Title = "The Big Book of Bicycling", Author = "Emily Furia", Description = "The world's authority on cycling provides a comprehensive guide to the sport for cyclists of all levels"}
            };
            foreach (Book book in books)
            {
                context.Books.Add(book);
            }
            context.SaveChanges();
        }
    }
}