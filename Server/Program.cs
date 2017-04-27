using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

using System.ServiceModel;

namespace Menhely
{
    class Program
    {
        static void Main(string[] args)
        {
            Vezerlo vezerlo = new Vezerlo();
            using(ServiceHost host = new ServiceHost(vezerlo))
            {
                host.Open();

                Console.WriteLine(" -- Cicabarát menhely szerver program -- ");
                Console.WriteLine("                      by AnDiktál Bt.    ");
                Console.WriteLine();
                Console.WriteLine(" segítséghez használja a \"help\" parancsot ");
                Console.WriteLine();
                string input = "";
                while (input != "kill")
                {
                    Console.Write("MenhelySzerver >>");
                    input = Console.ReadLine();

                    switch (input)
                    {
                        case "help":
                            Console.WriteLine("help:\tSúgó megjelenítés.e");
                            Console.WriteLine("felt:\tFeltöltés indítása.");
                            Console.WriteLine("test:\tTeszt indítása.");
                            Console.WriteLine("clear:\tKonzol előzmények törlése.");
                            Console.WriteLine("reset:\tAdatbázis törlése, telepítés indítása.");
                            Console.WriteLine("stop:\tSzerver leállítása.");
                            Console.WriteLine("cimek:\tTelephelyek kiírása a konzolra.");
                            Console.WriteLine();
                            break;
                        case "felt":
                            Feltoltes();
                            Console.WriteLine("Feltöltés kódból teszthez kész!");
                            Console.WriteLine();
                            break;
                        case "cimek":
                            Telephelyek();
                            break;
                        case "test":
                            Tesztelgetesek();
                            Console.WriteLine("Tesztelés kódból kész!");
                            Console.WriteLine();
                            break;
                        case "clear":
                            Console.Clear();
                            break;
                        case "reset":
                            Inicializalas();
                            //Console.WriteLine("Adatbázis törlése és inicializálás kész!");
                            Console.WriteLine();
                            break;
                        case "stop":
                            Console.WriteLine("Biztosan leállítja a szervert?");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("[N] NEM");
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.Write("    [I] Igen : ");
                            input = Console.ReadLine();
                            if (input == "I" || input == "i" || input == "igen")
                            {
                                input = "kill";
                                Console.WriteLine("Szerver leállítása: FOLYAMATBAN...");
                            }
                            break;
                        default:
                            break;
                    }
                
                }
            }
        }

        static void Inicializalas()
        {
            // Van-e jogosultság Reset-elni?
            string resetPass = "password";
            string resetpassInput = "";
            Console.Write("Jelszó: ");
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    resetpassInput += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && resetpassInput.Length > 0)
                    {
                        resetpassInput = resetpassInput.Substring(0, (resetpassInput.Length - 1));
                        Console.Write("\b \b");
                    }
                }
            }
            while (key.Key != ConsoleKey.Enter);
            Console.WriteLine();

            // Ha van, akkor megkérdezzük, hogy biztos akarja-e törölni az adatbázist
            if (resetPass == resetpassInput)
            {
                Console.WriteLine("Biztosan törlöd az adatbázist?");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("[N] NEM");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("    [I] Igen : ");
                string valasz = Console.ReadLine();
                if (valasz == "I" || valasz == "i" || valasz == "igen")
                {
                    Console.WriteLine("Adatbázis törlése: FOLYAMATBAN...");
                    AdatbazisTorles();
                    Console.WriteLine("Adatbázis törlése: KÉSZ!");
                    Console.WriteLine();

                    // Ha törlődött az adatbázis, akkor bekérjük az első használat miatt feltétlen szükséges adatokat és azokat felvisszük az adatbázisba

                    string adminPassInput1;
                    string adminPassInput2;

                    Console.Write("Első telephely címe: ");
                    string elsoTelephelyCime = Console.ReadLine();

                    Console.Write("Első admin neve: ");
                    string elsoAdminNeve = Console.ReadLine();

                    do
                    {
                        adminPassInput1 = "";
                        adminPassInput2 = "";
                        Console.WriteLine();
                        Console.Write("Első admin jelszava: ");
                        do
                        {
                            key = Console.ReadKey(true);
                            if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                            {
                                adminPassInput1 += key.KeyChar;
                                Console.Write("*");
                            }
                            else
                            {
                                if (key.Key == ConsoleKey.Backspace && adminPassInput1.Length > 0)
                                {
                                    adminPassInput1 = adminPassInput1.Substring(0, (adminPassInput1.Length - 1));
                                    Console.Write("\b \b");
                                }
                            }
                        }
                        while (key.Key != ConsoleKey.Enter);
                        Console.WriteLine();

                        Console.Write("Első admin jelszava ismét: ");
                        do
                        {
                            key = Console.ReadKey(true);
                            if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                            {
                                adminPassInput2 += key.KeyChar;
                                Console.Write("*");
                            }
                            else
                            {
                                if (key.Key == ConsoleKey.Backspace && adminPassInput2.Length > 0)
                                {
                                    adminPassInput2 = adminPassInput2.Substring(0, (adminPassInput2.Length - 1));
                                    Console.Write("\b \b");
                                }
                            }
                        }
                        while (key.Key != ConsoleKey.Enter);
                        Console.WriteLine();

                    } while (adminPassInput1 != adminPassInput2);


                    if (adminPassInput1 != "" && adminPassInput2 != "")
                    {
                        Console.WriteLine("Inicializáció: FOLYAMATBAN...");
                        Telepites(elsoTelephelyCime,elsoAdminNeve,adminPassInput1);
                        Console.WriteLine("Inicializáció: KÉSZ!");
                        Console.WriteLine("Mostmár be tud jelentkezni az Adminisztrátor a kliensbe,\nahol további telephelyeket és dolgozókat tud felvenni!");
                    }
                    else
                    {
                        Console.WriteLine("Jelszó nem hagyható üresen, az inicializáció most kilép.");
                    }

                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Hibás jelszó!");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            
        }

        static void AdatbazisTorles()
        {
            using (Menhelyek DB = new Menhelyek())
            {
                // Táblák tartalmának törlése
                var tempAllatok = DB.Allatok;
                foreach (var item in tempAllatok)
                {
                    DB.Allatok.Remove(item);
                }

                var tempGondozok = DB.Gondozok;
                foreach (var item in tempGondozok)
                {
                    DB.Gondozok.Remove(item);
                }

                var tempKetrecek = DB.Ketrecek;
                foreach (var item in tempKetrecek)
                {
                    DB.Ketrecek.Remove(item);
                }

                var tempOrokbefogadok = DB.Orokbefogadok;
                foreach (var item in tempOrokbefogadok)
                {
                    DB.Orokbefogadok.Remove(item);
                }

                var tempTelephelyek = DB.Telephelyek;
                foreach (var item in tempTelephelyek)
                {
                    DB.Telephelyek.Remove(item);
                }
                DB.SaveChanges();
            }
        }

        static void Telepites(string telephelyCim, string adminNev, string adminJelszo)
        {
            Vezerlo vezerlo = new Vezerlo();
            vezerlo.TelephelyFelvetel(telephelyCim);
            vezerlo.GondozoLetrehozas(adminNev, GondozoBeosztas.Admin, adminJelszo, vezerlo.TelephelyListazasEgy(telephelyCim).First());
        }

        static void Telephelyek()
        {
            Vezerlo vezerlo = new Vezerlo();
            var telepek = vezerlo.TelephelyListazas();
            Console.WriteLine();
            foreach (var t in telepek)
            {
                Console.WriteLine(" -" + t.Cim);
                Console.WriteLine("   -Ketrecek száma: " + t.Ketrecek.Count());
                Console.WriteLine("   -Alkalmazottak száma: " + t.Dolgozok.Count());
                Console.WriteLine();
            }
        }

        static void Feltoltes()
        {

            Vezerlo vezerlo = new Vezerlo();
       // Telephelyet felvveni, aztán ketrec, aztán gondozót, aztán jöhet az állat végül örökbeogadó ha már minden jó

       // Telephely
            vezerlo.TelephelyFelvetel("Paks");
            vezerlo.TelephelyFelvetel("Monor");
            vezerlo.TelephelyFelvetel("Vecsés");
            vezerlo.TelephelyFelvetel("Budapest");

       // Ketrecek
            vezerlo.KetrecHozzaadas(vezerlo.TelephelyListazas().Where(x => x.Cim == "Paks").First(), 5, AllatFaj.Süni);
            vezerlo.KetrecHozzaadas(vezerlo.TelephelyListazas().Where(x => x.Cim == "Paks").First(), 10, AllatFaj.Róka);
            vezerlo.KetrecHozzaadas(vezerlo.TelephelyListazas().Where(x => x.Cim == "Paks").First(), 5, AllatFaj.Róka);
            vezerlo.KetrecHozzaadas(vezerlo.TelephelyListazas().Where(x => x.Cim == "Paks").First(), 14, AllatFaj.Kutya);
            vezerlo.KetrecHozzaadas(vezerlo.TelephelyListazas().Where(x => x.Cim == "Paks").First(), 15, AllatFaj.Macska);
            vezerlo.KetrecHozzaadas(vezerlo.TelephelyListazas().Where(x => x.Cim == "Paks").First(), 8, AllatFaj.Macska);
            vezerlo.KetrecHozzaadas(vezerlo.TelephelyListazas().Where(x => x.Cim == "Paks").First(), 12, AllatFaj.Macska);

            vezerlo.KetrecHozzaadas(vezerlo.TelephelyListazas().Where(x => x.Cim == "Monor").First(), 7, AllatFaj.Egyéb);
            vezerlo.KetrecHozzaadas(vezerlo.TelephelyListazas().Where(x => x.Cim == "Monor").First(), 5, AllatFaj.Egyéb);
            vezerlo.KetrecHozzaadas(vezerlo.TelephelyListazas().Where(x => x.Cim == "Monor").First(), 9, AllatFaj.Róka);
            vezerlo.KetrecHozzaadas(vezerlo.TelephelyListazas().Where(x => x.Cim == "Monor").First(), 12, AllatFaj.Kutya);
            vezerlo.KetrecHozzaadas(vezerlo.TelephelyListazas().Where(x => x.Cim == "Monor").First(), 21, AllatFaj.Kutya);

            vezerlo.KetrecHozzaadas(vezerlo.TelephelyListazas().Where(x => x.Cim == "Vecsés").First(), 3, AllatFaj.Egyéb);
            vezerlo.KetrecHozzaadas(vezerlo.TelephelyListazas().Where(x => x.Cim == "Vecsés").First(), 5, AllatFaj.Süni);
            vezerlo.KetrecHozzaadas(vezerlo.TelephelyListazas().Where(x => x.Cim == "Vecsés").First(), 11, AllatFaj.Süni);
            vezerlo.KetrecHozzaadas(vezerlo.TelephelyListazas().Where(x => x.Cim == "Vecsés").First(), 18, AllatFaj.Macska);
            vezerlo.KetrecHozzaadas(vezerlo.TelephelyListazas().Where(x => x.Cim == "Vecsés").First(), 5, AllatFaj.Macska);

            vezerlo.KetrecHozzaadas(vezerlo.TelephelyListazas().Where(x => x.Cim == "Budapest").First(), 11, AllatFaj.Egyéb);
            vezerlo.KetrecHozzaadas(vezerlo.TelephelyListazas().Where(x => x.Cim == "Budapest").First(), 5, AllatFaj.Egyéb);
            vezerlo.KetrecHozzaadas(vezerlo.TelephelyListazas().Where(x => x.Cim == "Budapest").First(), 3, AllatFaj.Süni);
            vezerlo.KetrecHozzaadas(vezerlo.TelephelyListazas().Where(x => x.Cim == "Budapest").First(), 11, AllatFaj.Macska);
            vezerlo.KetrecHozzaadas(vezerlo.TelephelyListazas().Where(x => x.Cim == "Budapest").First(), 21, AllatFaj.Macska);
            vezerlo.KetrecHozzaadas(vezerlo.TelephelyListazas().Where(x => x.Cim == "Budapest").First(), 5, AllatFaj.Macska);
            vezerlo.KetrecHozzaadas(vezerlo.TelephelyListazas().Where(x => x.Cim == "Budapest").First(), 8, AllatFaj.Kutya);
            vezerlo.KetrecHozzaadas(vezerlo.TelephelyListazas().Where(x => x.Cim == "Budapest").First(), 13, AllatFaj.Kutya);
            vezerlo.KetrecHozzaadas(vezerlo.TelephelyListazas().Where(x => x.Cim == "Budapest").First(), 20, AllatFaj.Kutya);
            vezerlo.KetrecHozzaadas(vezerlo.TelephelyListazas().Where(x => x.Cim == "Budapest").First(), 3, AllatFaj.Kutya);


       // Gondozók
            vezerlo.GondozoLetrehozas("Lajos Bácsi", GondozoBeosztas.Admin, "papamaci", vezerlo.TelephelyListazasEgy("Vecsés").First());
            vezerlo.GondozoLetrehozas("Anti Bácsi", GondozoBeosztas.Admin, "apamaci", vezerlo.TelephelyListazasEgy("Monor").First());
            vezerlo.GondozoLetrehozas("Marika Néni", GondozoBeosztas.Admin, "mamamaci", vezerlo.TelephelyListazas().Where(x => x.Cim == "Paks").First());

            vezerlo.GondozoLetrehozas("Gergely Dániel", GondozoBeosztas.Befogo, "asd", vezerlo.TelephelyListazas().Where(x => x.Cim == "Vecsés").First());
            vezerlo.GondozoLetrehozas("Kis Barnabás", GondozoBeosztas.Befogo, "asd", vezerlo.TelephelyListazas().Where(x => x.Cim == "Budapest").First());
            vezerlo.GondozoLetrehozas("Nagy Tamás", GondozoBeosztas.Befogo, "asd", vezerlo.TelephelyListazas().Where(x => x.Cim == "Monor").First());
            vezerlo.GondozoLetrehozas("Kavalkó Andrea", GondozoBeosztas.Befogo, "asd", vezerlo.TelephelyListazas().Where(x => x.Cim == "Paks").First());

            vezerlo.GondozoLetrehozas("Magyar Eszter", GondozoBeosztas.Gondozo, "gondi", vezerlo.TelephelyListazas().Where(x => x.Cim == "Vecsés").First());
            vezerlo.GondozoLetrehozas("Honvéd Klára", GondozoBeosztas.Gondozo, "gondi", vezerlo.TelephelyListazas().Where(x => x.Cim == "Monor").First());
            vezerlo.GondozoLetrehozas("Kandisz Nóra", GondozoBeosztas.Gondozo, "gondi", vezerlo.TelephelyListazas().Where(x => x.Cim == "Monor").First());
            vezerlo.GondozoLetrehozas("Exzakt Tamás", GondozoBeosztas.Gondozo, "gondi", vezerlo.TelephelyListazas().Where(x => x.Cim == "Paks").First());
            vezerlo.GondozoLetrehozas("Juhász Irén", GondozoBeosztas.Gondozo, "gondi", vezerlo.TelephelyListazas().Where(x => x.Cim == "Paks").First());
            vezerlo.GondozoLetrehozas("Fúró Tailor", GondozoBeosztas.Gondozo, "gondi", vezerlo.TelephelyListazas().Where(x => x.Cim == "Budapest").First());
            vezerlo.GondozoLetrehozas("Lakatos Gyula", GondozoBeosztas.Gondozo, "gondi", vezerlo.TelephelyListazas().Where(x => x.Cim == "Budapest").First());
            vezerlo.GondozoLetrehozas("Kondiszki Vivien", GondozoBeosztas.Gondozo, "gondi", vezerlo.TelephelyListazas().Where(x => x.Cim == "Budapest").First());


       // Állatok
            vezerlo.AllatFelvetel("Ricsi Hörrencs", "vemhes", 9, AllatFaj.Egyéb, "Hörcsög", vezerlo.TelephelyListazas().Where(x => x.Cim == "Monor").First().Ketrecek.Where(x => x.Allatok.Count() < x.Meret && x.Faj == AllatFaj.Egyéb).First(), vezerlo.GondozoListazas().Where(x => x.Nev == "Nagy Tamás").First());
            vezerlo.AllatFelvetel("Szultán Kutta", "ivartalanított", 4, AllatFaj.Kutya, "Kuvasz", vezerlo.TelephelyListazas().Where(x => x.Cim == "Monor").First().Ketrecek.Where(x => x.Allatok.Count() < x.Meret && x.Faj == AllatFaj.Kutya).First(), vezerlo.GondozoListazas().Where(x => x.Nev == "Honvéd Klára").First());
            vezerlo.AllatFelvetel("Dzsuszi Munci", "nem veszett", 3, AllatFaj.Róka, "Vad", vezerlo.TelephelyListazas().Where(x => x.Cim == "Monor").First().Ketrecek.Where(x => x.Allatok.Count() < x.Meret && x.Faj == AllatFaj.Róka).First(), vezerlo.GondozoListazas().Where(x => x.Nev == "Nagy Tamás").First());
            vezerlo.AllatFelvetel("Molnár Rókuci", "selymes bundás", 3, AllatFaj.Róka, "Házi", vezerlo.TelephelyListazas().Where(x => x.Cim == "Monor").First().Ketrecek.Where(x => x.Allatok.Count() < x.Meret && x.Faj == AllatFaj.Róka).First(), vezerlo.GondozoListazas().Where(x => x.Nev == "Nagy Tamás").First());
            vezerlo.AllatFelvetel("Ilus Cuncus", "kopasz", 3, AllatFaj.Egyéb, "Degu", vezerlo.TelephelyListazas().Where(x => x.Cim == "Monor").First().Ketrecek.Where(x => x.Allatok.Count() < x.Meret && x.Faj == AllatFaj.Egyéb).First(), vezerlo.GondozoListazas().Where(x => x.Nev == "Nagy Tamás").First());
            vezerlo.AllatFelvetel("Morzsi", "nem veszett", 13, AllatFaj.Kutya, "Keverék", vezerlo.TelephelyListazas().Where(x => x.Cim == "Monor").First().Ketrecek.Where(x => x.Allatok.Count() < x.Meret && x.Faj == AllatFaj.Kutya).First(), vezerlo.GondozoListazas().Where(x => x.Nev == "Nagy Tamás").First());
            vezerlo.AllatFelvetel("Szulti", "nem veszett", 8, AllatFaj.Kutya, "Németjuhász", vezerlo.TelephelyListazas().Where(x => x.Cim == "Monor").First().Ketrecek.Where(x => x.Allatok.Count() < x.Meret && x.Faj == AllatFaj.Kutya).First(), vezerlo.GondozoListazas().Where(x => x.Nev == "Kandisz Nóra").First());
            vezerlo.AllatFelvetel("Molnár Roki", "nem veszett", 1, AllatFaj.Róka, "Vad", vezerlo.TelephelyListazas().Where(x => x.Cim == "Monor").First().Ketrecek.Where(x => x.Allatok.Count() < x.Meret && x.Faj == AllatFaj.Róka).First(), vezerlo.GondozoListazas().Where(x => x.Nev == "Kandisz Nóra").First());


            vezerlo.GondozottAllatHozzaadas(vezerlo.GondozoListazas().Where(x => x.Nev == "Nagy Tamás").First(), vezerlo.AllatListazas().Where(x => x.Nev == "Szultán Kutta").First());

            // vezerlo.GondozottAllatEltavolitas(vezerlo.GondozoListazas().Where(x => x.Nev == "Nagy Tamás").First(), vezerlo.AllatListazas().Where(x => x.Nev == "Szultán Kutta").First());

            vezerlo.AllatFelvetel("Sanyi Sünike", "beteg", 5, AllatFaj.Süni, "Házi", vezerlo.TelephelyListazas().Where(x => x.Cim == "Paks").First().Ketrecek.Where(x => x.Allatok.Count() < x.Meret && x.Faj == AllatFaj.Süni).First(), vezerlo.GondozoListazas().Where(x => x.Nev == "Kavalkó Andrea").First());
            vezerlo.AllatFelvetel("Ravasz Rókácska", "bagzó", 1, AllatFaj.Róka, "Házi", vezerlo.TelephelyListazas().Where(x => x.Cim == "Paks").First().Ketrecek.Where(x => x.Allatok.Count() < x.Meret && x.Faj == AllatFaj.Róka).First(), vezerlo.GondozoListazas().Where(x => x.Nev == "Kavalkó Andrea").First());
            vezerlo.AllatFelvetel("Huncut Pista", "rosszalkodó", 10, AllatFaj.Róka, "Keverék", vezerlo.TelephelyListazas().Where(x => x.Cim == "Paks").First().Ketrecek.Where(x => x.Allatok.Count() < x.Meret && x.Faj == AllatFaj.Macska).First(), vezerlo.GondozoListazas().Where(x => x.Nev == "Juhász Irén").First());
            vezerlo.AllatFelvetel("Selymi", "tüzel", 8, AllatFaj.Macska, "Norvég-erdei", vezerlo.TelephelyListazas().Where(x => x.Cim == "Paks").First().Ketrecek.Where(x => x.Allatok.Count() < x.Meret && x.Faj == AllatFaj.Macska).First(), vezerlo.GondozoListazas().Where(x => x.Nev == "Kavalkó Andrea").First());
            vezerlo.AllatFelvetel("Cili", "tüzel", 13, AllatFaj.Macska, "Raggamofin", vezerlo.TelephelyListazas().Where(x => x.Cim == "Paks").First().Ketrecek.Where(x => x.Allatok.Count() < x.Meret && x.Faj == AllatFaj.Macska).First(), vezerlo.GondozoListazas().Where(x => x.Nev == "Exzakt Tamás").First());
            vezerlo.AllatFelvetel("Maszat", "tüzel", 20, AllatFaj.Macska, "Egyiptomi", vezerlo.TelephelyListazas().Where(x => x.Cim == "Paks").First().Ketrecek.Where(x => x.Allatok.Count() < x.Meret && x.Faj == AllatFaj.Macska).First(), vezerlo.GondozoListazas().Where(x => x.Nev == "Kavalkó Andrea").First());
            vezerlo.AllatFelvetel("Félix", "tüzel", 15, AllatFaj.Macska, "Rövidlábú", vezerlo.TelephelyListazas().Where(x => x.Cim == "Paks").First().Ketrecek.Where(x => x.Allatok.Count() < x.Meret && x.Faj == AllatFaj.Macska).First(), vezerlo.GondozoListazas().Where(x => x.Nev == "Juhász Irén").First());
            vezerlo.AllatFelvetel("Rex", "tüzel", 3, AllatFaj.Kutya, "Yorkie", vezerlo.TelephelyListazas().Where(x => x.Cim == "Paks").First().Ketrecek.Where(x => x.Allatok.Count() < x.Meret && x.Faj == AllatFaj.Kutya).First(), vezerlo.GondozoListazas().Where(x => x.Nev == "Kavalkó Andrea").First());
            vezerlo.AllatFelvetel("Szexi Alejandro", "tüzel", 1, AllatFaj.Kutya, "Spániel", vezerlo.TelephelyListazas().Where(x => x.Cim == "Paks").First().Ketrecek.Where(x => x.Allatok.Count() < x.Meret && x.Faj == AllatFaj.Kutya).First(), vezerlo.GondozoListazas().Where(x => x.Nev == "Kavalkó Andrea").First());

            vezerlo.AllatFelvetel("Teqila Tita", "őrizni", 13, AllatFaj.Macska, "Keverék", vezerlo.TelephelyListazas().Where(x => x.Cim == "Vecsés").First().Ketrecek.Where(x => x.Allatok.Count() < x.Meret && x.Faj == AllatFaj.Macska).First(), vezerlo.GondozoListazas().Where(x => x.Nev == "Gergely Dániel").First());
            vezerlo.AllatFelvetel("Csizmás Kandúr", "kullancsos", 3, AllatFaj.Macska, "Vad", vezerlo.TelephelyListazas().Where(x => x.Cim == "Vecsés").First().Ketrecek.Where(x => x.Allatok.Count() < x.Meret && x.Faj == AllatFaj.Macska).First(), vezerlo.GondozoListazas().Where(x => x.Nev == "Magyar Eszter").First());
            vezerlo.AllatFelvetel("Sanyi Süncsi", "gyógyult", 2, AllatFaj.Süni, "Vad", vezerlo.TelephelyListazas().Where(x => x.Cim == "Vecsés").First().Ketrecek.Where(x => x.Allatok.Count() < x.Meret && x.Faj == AllatFaj.Süni).First(), vezerlo.GondozoListazas().Where(x => x.Nev == "Gergely Dániel").First());
            vezerlo.AllatFelvetel("Andikaa", "gyógyult", 5, AllatFaj.Süni, "Vad", vezerlo.TelephelyListazas().Where(x => x.Cim == "Vecsés").First().Ketrecek.Where(x => x.Allatok.Count() < x.Meret && x.Faj == AllatFaj.Süni).First(), vezerlo.GondozoListazas().Where(x => x.Nev == "Magyar Eszter").First());
            vezerlo.AllatFelvetel("Dancsó", "pici és fekete", 7, AllatFaj.Egyéb, "Tücsök", vezerlo.TelephelyListazas().Where(x => x.Cim == "Vecsés").First().Ketrecek.Where(x => x.Allatok.Count() < x.Meret && x.Faj == AllatFaj.Egyéb).First(), vezerlo.GondozoListazas().Where(x => x.Nev == "Gergely Dániel").First());
            vezerlo.AllatFelvetel("Buksi", "gyógyult", 11, AllatFaj.Egyéb, "Vadászgörény", vezerlo.TelephelyListazas().Where(x => x.Cim == "Vecsés").First().Ketrecek.Where(x => x.Allatok.Count() < x.Meret && x.Faj == AllatFaj.Egyéb).First(), vezerlo.GondozoListazas().Where(x => x.Nev == "Magyar Eszter").First());
            vezerlo.AllatFelvetel("Koca", "gyógyult", 3, AllatFaj.Egyéb, "Disznó", vezerlo.TelephelyListazas().Where(x => x.Cim == "Vecsés").First().Ketrecek.Where(x => x.Allatok.Count() < x.Meret && x.Faj == AllatFaj.Egyéb).First(), vezerlo.GondozoListazas().Where(x => x.Nev == "Gergely Dániel").First());

            vezerlo.AllatFelvetel("RágCsávó", "fehér", 3, AllatFaj.Egyéb, "Patkány", vezerlo.TelephelyListazas().Where(x => x.Cim == "Budapest").First().Ketrecek.Where(x => x.Allatok.Count() < x.Meret && x.Faj == AllatFaj.Egyéb).First(), vezerlo.GondozoListazas().Where(x => x.Nev == "Kis Barnabás").First());
            vezerlo.AllatFelvetel("Pati, a patkány", "szürke", 8, AllatFaj.Egyéb, "Patkány", vezerlo.TelephelyListazas().Where(x => x.Cim == "Budapest").First().Ketrecek.Where(x => x.Allatok.Count() < x.Meret && x.Faj == AllatFaj.Egyéb).First(), vezerlo.GondozoListazas().Where(x => x.Nev == "Kis Barnabás").First());
            vezerlo.AllatFelvetel("Deni, a vérszívó", "szőrtelen és csíntalan", 1, AllatFaj.Egyéb, "Denevér", vezerlo.TelephelyListazas().Where(x => x.Cim == "Budapest").First().Ketrecek.Where(x => x.Allatok.Count() < x.Meret && x.Faj == AllatFaj.Egyéb).First(), vezerlo.GondozoListazas().Where(x => x.Nev == "Lakatos Gyula").First());
            vezerlo.AllatFelvetel("IdeGyere", "átlagos", 20, AllatFaj.Kutya, "Németjuhász", vezerlo.TelephelyListazas().Where(x => x.Cim == "Budapest").First().Ketrecek.Where(x => x.Allatok.Count() < x.Meret && x.Faj == AllatFaj.Kutya).First(), vezerlo.GondozoListazas().Where(x => x.Nev == "Kis Barnabás").First());
            vezerlo.AllatFelvetel("Anyuci-pici-drágája", "félős", 4, AllatFaj.Kutya, "Yorkie", vezerlo.TelephelyListazas().Where(x => x.Cim == "Budapest").First().Ketrecek.Where(x => x.Allatok.Count() < x.Meret && x.Faj == AllatFaj.Kutya).First(), vezerlo.GondozoListazas().Where(x => x.Nev == "Lakatos Gyula").First());
            vezerlo.AllatFelvetel("BébiBogyó", "baba-pofi", 14, AllatFaj.Kutya, "Pitbull", vezerlo.TelephelyListazas().Where(x => x.Cim == "Budapest").First().Ketrecek.Where(x => x.Allatok.Count() < x.Meret && x.Faj == AllatFaj.Kutya).First(), vezerlo.GondozoListazas().Where(x => x.Nev == "Lakatos Gyula").First());
            vezerlo.AllatFelvetel("Alien", "UFO-fejű", 19, AllatFaj.Macska, "Keverék", vezerlo.TelephelyListazas().Where(x => x.Cim == "Budapest").First().Ketrecek.Where(x => x.Allatok.Count() < x.Meret && x.Faj == AllatFaj.Macska).First(), vezerlo.GondozoListazas().Where(x => x.Nev == "Kondiszki Vivien").First());
            vezerlo.AllatFelvetel("Ölj", "házőrző, agresszív", 11, AllatFaj.Macska, "Sziami", vezerlo.TelephelyListazas().Where(x => x.Cim == "Budapest").First().Ketrecek.Where(x => x.Allatok.Count() < x.Meret && x.Faj == AllatFaj.Macska).First(), vezerlo.GondozoListazas().Where(x => x.Nev == "Kis Barnabás").First());
            vezerlo.AllatFelvetel("Szappan", "sima bundájú", 8, AllatFaj.Macska, "Perzsa", vezerlo.TelephelyListazas().Where(x => x.Cim == "Budapest").First().Ketrecek.Where(x => x.Allatok.Count() < x.Meret && x.Faj == AllatFaj.Macska).First(), vezerlo.GondozoListazas().Where(x => x.Nev == "Fúró Tailor").First());
            vezerlo.AllatFelvetel("SzőrGombóc", "gubancos", 6, AllatFaj.Macska, "Perzsa", vezerlo.TelephelyListazas().Where(x => x.Cim == "Budapest").First().Ketrecek.Where(x => x.Allatok.Count() < x.Meret && x.Faj == AllatFaj.Macska).First(), vezerlo.GondozoListazas().Where(x => x.Nev == "Fúró Tailor").First());


       // Örökbefogadó
            vezerlo.Regisztracio("Örök Benő", "csattan");
            vezerlo.Regisztracio("Megbízhat Jónás", "csattan");
            vezerlo.Regisztracio("Huncut Gáspár", "csattan");
            vezerlo.Regisztracio("Szónok Fruzsina", "csattan");
            vezerlo.Regisztracio("Álmos Anna", "csattan");
            vezerlo.Regisztracio("Vidám Margit", "csattan");

        }

        
        static void Tesztelgetesek()
        {
            Vezerlo vezerlo = new Vezerlo();
            var probaTemp = vezerlo.TelephelyListazas();
            Console.WriteLine("Telephelyeink: ");
            foreach (var proba in probaTemp)
            {
                Console.WriteLine(" - " + proba.Cim);
            }

            var temp = vezerlo.TelephelyListazasEgy("Vecsés").First();

            foreach (var item in temp.Ketrecek)
            {
                var ketrectemp = vezerlo.KetrecListazasEgy(item.KetrecID).First();
                foreach (var allat in ketrectemp.Allatok)
                {
                    var allatTemp = vezerlo.AllatListazasEgy(allat.Nev).First();
                    Console.WriteLine(allatTemp.Nev);
                }
            }
        }
    }
}
