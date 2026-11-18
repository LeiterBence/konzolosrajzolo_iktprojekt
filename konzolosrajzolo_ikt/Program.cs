﻿using System;
using System.IO;

namespace KonzolosRajzolo
{
    class Program
    {
        const int rajzoloFeluletSzelessege = 50;
        const int rajzoloFeluletMagassaga = 20;
        const int rajzoloFeluletKezdoKordinataX = 21;
        const int rajzoloFeluletKezdoKordinataY = 2;
        const int allapotPanelSzelesseg = 20;
        static char[,] rajzoloFeluletKarakterek = new char[rajzoloFeluletMagassaga, rajzoloFeluletSzelessege];
        static ConsoleColor[,] rajzoloFeluletSzinek = new ConsoleColor[rajzoloFeluletMagassaga, rajzoloFeluletSzelessege];

        static char aktualisKarakter = '*';
        static int kurzorHelyzetX = 0;
        static int kurzorHelyzetY = 0;
        static ConsoleColor aktualisSzin = ConsoleColor.Blue;

        static string kiiras = "Nyilak: mozgás, Szóköz: ecset fel-le emelése, M: menü";

        static ConsoleColor[] valaszthatoSzinek =
        {
            ConsoleColor.White,
            ConsoleColor.Yellow,
            ConsoleColor.Red,
            ConsoleColor.Green,
            ConsoleColor.Magenta,
            ConsoleColor.Cyan,
            ConsoleColor.Blue
        };

        static bool ecsetLeteve = false;

        static void Main(string[] args)
        {
            rajzlapTeljesTorlese();
            teljesKirajzolas();
            bool fut = true;
            while (fut)
            {
                rajzlapKirajzolasa();
                ConsoleKeyInfo mozgatasBekeres = Console.ReadKey(true);

                switch (mozgatasBekeres.Key)
                {

                    //mozgatas esetek
                    case ConsoleKey.UpArrow:
                        kurzorMozgatasa(0, -1);
                        break;
                    case ConsoleKey.LeftArrow:
                        kurzorMozgatasa(-1, 0);
                        break;
                    case ConsoleKey.DownArrow:
                        kurzorMozgatasa(0, 1);
                        break;
                    case ConsoleKey.RightArrow:
                        kurzorMozgatasa(1, 0);
                        break;

                    case ConsoleKey.Spacebar:
                        ecsetLeteve = !ecsetLeteve;
                        if (ecsetLeteve)
                        {
                            aktualisKarakterKiirasa(kurzorHelyzetY, kurzorHelyzetX);
                            kiiras = "Ecset letéve!";
                        }
                        else
                        {
                            kiiras = "Ecset felvéve!";
                        }
                        cellaKirajzolasa(kurzorHelyzetY, kurzorHelyzetX, true);
                        break;
                    //menü
                    case ConsoleKey.M:
                        MenuKezeles();
                        break;
                    //kilepes
                    case ConsoleKey.X:
                        fut = false;
                        break;

                    //nyomdak
                    case ConsoleKey.F1: nyomdaLerakasa("x", 3); break;
                    case ConsoleKey.F2: nyomdaLerakasa("x", 5); break;
                    case ConsoleKey.F3: nyomdaLerakasa("x", 7); break;

                    case ConsoleKey.F4: nyomdaLerakasa("kereszt", 3); break;
                    case ConsoleKey.F5: nyomdaLerakasa("kereszt", 5); break;
                    case ConsoleKey.F6: nyomdaLerakasa("kereszt", 7); break;

                    case ConsoleKey.F7: nyomdaLerakasa("negyzet", 3); break;
                    case ConsoleKey.F8: nyomdaLerakasa("negyzet", 5); break;
                    case ConsoleKey.F9: nyomdaLerakasa("negyzet", 7); break;

                    case ConsoleKey.F10: nyomdaLerakasa("rombusz", 3); break;
                    case ConsoleKey.F11: nyomdaLerakasa("rombusz", 5); break;
                    case ConsoleKey.F12: nyomdaLerakasa("rombusz", 7); break;
                }
            }
            Console.ResetColor();
            Console.Clear();
            Console.WriteLine("Kilépés...");
        }

        static void kurzorMozgatasa(int x, int y)
        {

            //hatarak ellenorzese
            if (kurzorHelyzetX + x >= rajzoloFeluletSzelessege || kurzorHelyzetX + x < 0 || kurzorHelyzetY + y >= rajzoloFeluletMagassaga || kurzorHelyzetY + y < 0)
            {
                //hiba
                Console.Beep();
                return;
            }

            int regiX = kurzorHelyzetX;
            int regiY = kurzorHelyzetY;
            kurzorHelyzetX += x;
            kurzorHelyzetY += y;
            cellaKirajzolasa(regiY, regiX, false);

            if(ecsetLeteve)
            {
                aktualisKarakterKiirasa(kurzorHelyzetY, kurzorHelyzetX);
            }
            cellaKirajzolasa(kurzorHelyzetY, kurzorHelyzetX, true);
            
            return;
        }
        static void nyomdaLerakasa(string tipus, int nyomdaMerete)
        {
            int sugar = nyomdaMerete / 2;

            if (kurzorHelyzetX + sugar >= rajzoloFeluletSzelessege || kurzorHelyzetX - sugar < 0 ||
                kurzorHelyzetY + sugar >= rajzoloFeluletMagassaga || kurzorHelyzetY - sugar < 0)
            {
                Console.Beep();
                kiiras = "";
                return;
            }

            int balSzelsoX = kurzorHelyzetX - sugar;
            int felsoY = kurzorHelyzetY - sugar;

            if (tipus == "kereszt")
            {
                for (int i = 0; i < nyomdaMerete; i++)
                {
                    aktualisKarakterKiirasa(kurzorHelyzetY, balSzelsoX + i);
                    aktualisKarakterKiirasa(felsoY + i, kurzorHelyzetX);
                }
            }
            else if (tipus == "x")
            {
                for (int i = 0; i < nyomdaMerete; i++)
                {
                    aktualisKarakterKiirasa(felsoY + i, balSzelsoX + i);
                    aktualisKarakterKiirasa(felsoY + i, balSzelsoX + nyomdaMerete - 1 - i);
                }
            }
            else if (tipus == "negyzet")
            {
                for (int i = 0; i < nyomdaMerete; i++)
                {
                    aktualisKarakterKiirasa(felsoY, balSzelsoX + i);
                    aktualisKarakterKiirasa(felsoY + nyomdaMerete - 1, balSzelsoX + i);
                    aktualisKarakterKiirasa(felsoY + i, balSzelsoX);
                    aktualisKarakterKiirasa(felsoY + i, balSzelsoX + nyomdaMerete - 1);
                }
            }
            else if (tipus == "rombusz")
            {
                for (int i = 0; i <= sugar; i++)
                {
                    aktualisKarakterKiirasa(felsoY + i, kurzorHelyzetX - i);
                    aktualisKarakterKiirasa(felsoY + i, kurzorHelyzetX + i);

                    aktualisKarakterKiirasa(felsoY + nyomdaMerete - 1 - i, kurzorHelyzetX - i);
                    aktualisKarakterKiirasa(felsoY + nyomdaMerete - 1 - i, kurzorHelyzetX + i);
                }
            }

            rajzlapKirajzolasa();
        }
        static void allapotPanelTorles()
        {
            for (int i = 1; i <= rajzoloFeluletMagassaga + 2; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(new string(' ', allapotPanelSzelesseg));
            }
        }
        static void rajzlapKirajzolasa()
        {
            for (int y = 0; y < rajzoloFeluletMagassaga; y++)
            {
                for (int x = 0; x < rajzoloFeluletSzelessege; x++)
                {
                    bool jelenlegiAKurzor = false;
                    if (x == kurzorHelyzetX && y == kurzorHelyzetY) jelenlegiAKurzor = true;
                    cellaKirajzolasa(y, x, jelenlegiAKurzor);

                }
            }
        }
        static void cellaKirajzolasa(int magassag, int szelesseg, bool kurzorLeVanTeve)
        {
            Console.SetCursorPosition(rajzoloFeluletKezdoKordinataX + szelesseg, rajzoloFeluletKezdoKordinataY + magassag);
            char jelenlegiKarakter = rajzoloFeluletKarakterek[magassag, szelesseg];
            if (kurzorLeVanTeve)
            {
                if (jelenlegiKarakter == ' ') Console.Write('+');
                else Console.Write(jelenlegiKarakter);
            }
            else
            {
                Console.ForegroundColor = rajzoloFeluletSzinek[magassag, szelesseg];
                Console.Write(jelenlegiKarakter);
                Console.ResetColor();
            }
        }
        static void karakterValasztas()
        {
            Console.WriteLine("KARAKTER VALASZTAS");
            while (true)
            {
                allapotPanelTorles();
                ConsoleKeyInfo ujKarakter = Console.ReadKey(true);
                if (ujKarakter.Key == ConsoleKey.Escape) return;

                if (char.IsControl(ujKarakter.KeyChar))
                {
                    // hiba nem megengedett a karakter
                    continue;
                }
                aktualisKarakter = ujKarakter.KeyChar;
                break;
            }
        }
        static void szinKivalasztas()
        {
            while (true)
            {
                ConsoleKeyInfo valasztottSzin = Console.ReadKey(true);
                if (valasztottSzin.Key == ConsoleKey.Escape) return;

                if (valasztottSzin.KeyChar < '1' || valasztottSzin.KeyChar > '7')
                {
                    //hiba nem megengedett a karakter
                    continue;
                }
                aktualisSzin = valaszthatoSzinek[valasztottSzin.KeyChar - '1'];
                break;

            }
        }
        static void teljesKirajzolas()
        {
            Console.ResetColor();
            Console.Clear();

            Console.SetCursorPosition(1, 0);
            Console.Write("Konzolos Rajzoló program");

            Console.SetCursorPosition(rajzoloFeluletSzelessege + rajzoloFeluletKezdoKordinataX, 0);
            Console.Write("RAJZLAP");

            for (int i = 1; i < rajzoloFeluletMagassaga; i++)
            {
                Console.SetCursorPosition(rajzoloFeluletKezdoKordinataX - 1, i);
                Console.Write('|');
            }
        }
        static void aktualisKarakterKiirasa(int y, int x)
        {
            rajzoloFeluletKarakterek[y, x] = aktualisKarakter;
            rajzoloFeluletSzinek[y, x] = aktualisSzin;
        }
        static void fajlBetoltese()
{
    allapotPanelTorles();
    Console.SetCursorPosition(1, 1);
    Console.Write("Minta Betöltése");
    Console.SetCursorPosition(1, 3);
    Console.Write("(Üres Enter = Mégse):");
    Console.SetCursorPosition(1, 5);
    string fajlneve = Console.ReadLine().Trim();
    string[] fajlSorai = File.ReadAllLines(fajlneve);

    if (!File.Exists(fajlneve))
    {
        hibaKezeles("Nem található a fájl!");
    }

    if (fajlSorai.Length > rajzoloFeluletMagassaga)
    {
        hibaKezeles("A minta túl magas");
    }

    foreach (string sor in fajlSorai)
    {
        if (sor.Length > rajzoloFeluletSzelessege)
        {
            hibaKezeles("A minta túl széles");
        }
    }
    for (int y = 0; y < fajlSorai.Length; y++)
    {
        for (int x = 0; x < fajlSorai[y].Length; x++)
        {
            rajzoloFeluletKarakterek[y, x] = fajlSorai[y][x];
            rajzoloFeluletSzinek[y, x] = aktualisSzin;
        }
    }
}
static void hibaKezeles(string hibaUzenet)
{
    Console.Beep();
    kiiras = "HIBA " + hibaUzenet;
    Console.SetCursorPosition(1, rajzoloFeluletKezdoKordinataY + rajzoloFeluletMagassaga + 2);
    Console.ForegroundColor = ConsoleColor.Red;
    Console.Write(kiiras.PadRight(74));
    Console.ResetColor();
}
    }
}