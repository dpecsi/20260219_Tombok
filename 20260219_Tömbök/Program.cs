namespace _20260219_Tömbök
{
    internal class Program
    {
        static List<int> ListaMetszet(List<int> elsőTömb, List<int> másodikTömb)
        {
            List<int> segéd = new List<int>();
            for (int elsőIndex = 0; elsőIndex < elsőTömb.Count(); elsőIndex++)
            {
                for (int másodikIndex = 0; másodikIndex < másodikTömb.Count(); másodikIndex++)
                {
                    if (elsőTömb[elsőIndex] == másodikTömb[másodikIndex])
                    {
                        segéd.Add(elsőTömb[elsőIndex]);
                        //elképzelhető, hogy az elemek eltávolítása index hibát okoz, a megoldás a csökkenő indexű for ciklus
                        elsőTömb.RemoveAt(elsőIndex);
                        elsőIndex--;
                        másodikTömb.RemoveAt(másodikIndex);
                        másodikIndex--;
                    }
                }
            }
            return segéd;
        }

        static int[] ElemEltávolítása(int[] tömb, int sorszám)
        {
            for (int index = sorszám; index < tömb.Count()-1; index++)
            {
                tömb[index] = tömb[index+1];
            }
            Array.Resize(ref tömb, tömb.Count() - 1);
            return tömb;
        }

        static int[] Metszet(int[] elsőTömb, int[] másodikTömb)
        {
            int[] elsőTömbMásolata = new int[elsőTömb.Count()];
            int[] másodikTömbMásolata = new int[másodikTömb.Count()];
            Array.Copy(elsőTömb, elsőTömbMásolata, elsőTömb.Count());
            Array.Copy(másodikTömb, másodikTömbMásolata, másodikTömb.Count());
            int[] segéd = new int[Math.Min(elsőTömbMásolata.Count(), másodikTömbMásolata.Count())];
            int segédIndex = 0;
            for (int elsőIndex = 0; elsőIndex < elsőTömbMásolata.Count(); elsőIndex++)
            {
                for (int másodikIndex = 0; másodikIndex < másodikTömbMásolata.Count(); másodikIndex++)
                {
                    if (elsőTömbMásolata[elsőIndex] == másodikTömbMásolata[másodikIndex])
                    {
                        segéd[segédIndex] = elsőTömbMásolata[elsőIndex];
                        segédIndex++;
                        ElemEltávolítása(elsőTömbMásolata, elsőIndex);
                        elsőIndex--;
                        ElemEltávolítása(másodikTömbMásolata, másodikIndex);
                        másodikIndex--;
                    }
                }
            }
            Array.Resize(ref segéd, segédIndex);
            return segéd;
        }

        /// <summary>
        /// Tömb elemeket generáló függvény
        /// </summary>
        /// <param name="db">A generálandó tömb elemeinek száma</param>
        /// <param name="min">A generálandó számok minimum értéke</param>
        /// <param name="max">A generálandó számok maximum értéke</param>
        /// <returns>Egy db hosszúságú tömb, melyben a min és max közötti véletlen számok vannak</returns>
        static int[] TömbGenerálás(int db, int min = 0, int max = 100)
        {
            int[] segéd = new int[db];
            Random rnd = new Random(104);
            for (int index = 0; index < db; index++)
            {
                segéd[index] = rnd.Next(min, max + 1);
            }
            return segéd;
        }

        static void Main(string[] args)
        {
            int[] tömb1 = TömbGenerálás(10, 1, 20);
            int[] tömb2 = TömbGenerálás(15);

            Array.Sort(tömb1);
            Array.Sort(tömb2);
            //tömb1 = tömb1.OrderBy(tömbelem => tömbelem).ToArray();
            Console.WriteLine(string.Join(", ",
                tömb1.Select(
                    tömbelem => tömbelem.ToString().PadLeft(2, ' ')
                    )
                )
            );
            Console.WriteLine(string.Join(", ",tömb2
                .Select(tömbelem => tömbelem.ToString().PadLeft(2, ' '))
            ));

            int[] segéd1 = Metszet(tömb1, tömb2);
            Console.WriteLine("Metszet: {0}", string.Join(", ", segéd1
                .Select(tömbelem => tömbelem.ToString().PadLeft(2, ' '))
            ));

            List<int> segéd3 = ListaMetszet(tömb1.ToList(), tömb2.ToList());
            Console.WriteLine("Lista Metszet: {0}", string.Join(", ", segéd3
                .Select(tömbelem => tömbelem.ToString().PadLeft(2, ' '))
            ));

            int[] segéd2 = tömb1.Intersect(tömb2).ToArray();
            Console.WriteLine("Gyári Metszet: {0}", string.Join(", ", segéd2
                .Select(tömbelem => tömbelem.ToString().PadLeft(2, ' '))
            ));
        }
    }
}
