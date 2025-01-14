using ProiectPSSC_order_placement.Domain.Models;
using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        var cart = new Cart();

        while (true)
        {
            Console.WriteLine("1. Creare cos gol");
            Console.WriteLine("2. Adauga produs in cos");
            Console.WriteLine("3. Valideaza cosul");
            Console.WriteLine("4. Afiseaza cosul");
            Console.WriteLine("5. Iesire");
            Console.Write("Alege o optiune: ");
            var opt = Console.ReadLine();

            switch (opt)
            {
                case "1":
                    cart = new Cart();
                    Console.WriteLine("Coșul a fost creat.");
                    break;

                case "2":
                    Console.Write("Cod produs: ");
                    var code = Console.ReadLine();
                    Console.Write("Cantitate: ");
                    var quantity = decimal.Parse(Console.ReadLine());
                    cart.AddProduct(code, quantity);
                    Console.WriteLine("Produs adăugat.");
                    break;

                case "3":
                    try
                    {
                        cart.TransitionToValidated();
                        Console.WriteLine("Coșul a fost validat.");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Eroare: {e.Message}");
                    }
                    break;

                case "4":
                    cart.DisplayCart();
                    break;

                case "5":
                    return;

                default:
                    Console.WriteLine("Opțiune invalidă.");
                    break;
            }
        }
    }
}
