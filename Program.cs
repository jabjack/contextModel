using EntityFramework;
using System.Linq;
using EntityFramework.Models;
using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;

namespace EntityFramework
{
    class Program
    {
        static void Main(string[] args)
        {


            DbContextOptionsBuilder<BeerDbContext> optionsBuilder =
            new DbContextOptionsBuilder<BeerDbContext>();

            optionsBuilder.UseSqlServer("server=DESKTOP-20B895B;Database= C#; Trusted_Connection=True; TrustServerCertificate=True;");


            bool again = true;

            int op = 0;

            do
            {
                ShowMenu();
                Console.WriteLine("Elige una opcion.");

                op = int.Parse(Console.ReadLine());

                switch (op)
                {
                    case 1:                        
                        Show(optionsBuilder);                    
                        break;

                    case 2:
                        Add(optionsBuilder);
                        break;

                    case 3:
                        Edit(optionsBuilder);
                        break;

                    case 4:
                        Delete(optionsBuilder);
                        break;


                }

            }
            while (again);

        }

        public static void Show(DbContextOptionsBuilder<BeerDbContext> optionsBuilder)
        {
        
                Console.Clear();
                Console.WriteLine("Beer to the database");

                using(var context = new BeerDbContext(optionsBuilder.Options))
                {
                    List<Beer> beers = context.Beers.ToList();


                    List<Beer> beers2 = ( from b in context.Beers
                                          where  b.BrandId == 2
                                          orderby b.Name select b).Include(b => b.Brand).ToList();


                    foreach (var beer in  beers)
                    {

                       Console.WriteLine($"{beer.Id} {beer.Name} ");

                    }
                  


                }
        }

        public static void Add(DbContextOptionsBuilder<BeerDbContext> optionsBuilder){

            Console.Clear();
            Console.WriteLine("Add the new Beer");
            Console.WriteLine("Write the name of beer");

            string name = Console.ReadLine();

            Console.WriteLine("Escribe el ID de la Marca");

            int brandId = int.Parse(Console.ReadLine());

            using (var context = new BeerDbContext(optionsBuilder.Options))
            {

                Beer beer = new Beer(){

                    Name = name,
                    BrandId =  brandId

                };


                //add to the context

                context.Add(beer);

                // add to the database

                context.SaveChanges();
                
            }

        }

        public static void Edit(DbContextOptionsBuilder<BeerDbContext> optionsBuilder){

                Console.Clear();

                Show(optionsBuilder);

                Console.WriteLine("Edit to the Beer");

                Console.WriteLine("Edit the ID of Beer");

                int id = int.Parse(Console.ReadLine());

                using( var context = new BeerDbContext(optionsBuilder.Options) ){

                    Beer beer = context.Beers.Find(id);

                    if (beer != null)
                    {
                        Console.WriteLine("Write the name?");

                        string name = Console.ReadLine();

                        Console.WriteLine("Escribe el Id de la marca");

                        int brandId = int.Parse(Console.ReadLine());

                        beer.Name = name;

                        beer.BrandId = brandId;

                        context.Entry(beer).State = EntityState.Modified;

                        context.SaveChanges();


                    }
                    else
                    {
                        Console.WriteLine("Beer not exist");
                    }



                }


        }

        public static void Delete(DbContextOptionsBuilder<BeerDbContext> optionsBuilder){

              Console.Clear();

              Show(optionsBuilder);

              Console.WriteLine("Delete beer");

              Console.WriteLine("Escribe el Id de la Cerveza a Eliminar");

              int id = int.Parse(console.ReadLine());

               using (var context = new BeerDbContext(optionsBuilder.Options))
               {

                Beer beer = new context.Beers.Find(id);

                if (beer != null)
                {
                    context.Beers.Remove(beer);
                    context.SaveChanges();
                    
                }
                else
                {
                    Console.WriteLine("Dont Exist Beer");
                }



               }








        }

        public static void ShowMenu()
        {

            Console.WriteLine("*******Menu******");
            Console.WriteLine("1. -Mostrar");
            Console.WriteLine("2. -Agregar");
            Console.WriteLine("3.  -Editar");
            Console.WriteLine("4. -Eliminar");
            Console.WriteLine("5. -Salir");

        }

    }



}







