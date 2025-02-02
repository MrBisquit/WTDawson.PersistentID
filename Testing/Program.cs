using WTDawson.PersistentID;

namespace Testing
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Persistent ID tester");
            Console.WriteLine("Run with the argument \"-f\" to change the list of items");
            Console.WriteLine("Run with the argument \"-fr\" to change the list of items randomly");
            Console.WriteLine("Run with the argument \"-c\" to compare the tests");
            Console.WriteLine("Run with the argument \"-r\" to refresh every second");
            Console.WriteLine("Persistent ID".PadRight(65) + "Length" + " Magic" + " Test");

            IDBuilder builder = new IDBuilder();
            builder.AddItems("WTDawson", "EventPipes", "NuGet", "CSharp", PublicUtils.RemoveSpecialCharacters(new DateTime(2025, 1, 1).ToString()));

            if(args.Contains("-f"))
            {
                builder.AddItems("This", "will", "make", "it", "fail");
            } else if(args.Contains("-fr"))
            {
                Random r = new Random();
                for (int i = 0; i < 15; i++)
                {
                    builder.AddItems(r.Next());
                }
                char[] chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890".ToCharArray();
                for (int i = 0; i < 15; i++)
                {
                    string str = string.Empty;
                    for (int j = 0; j < r.Next(0, 15); j++)
                    {
                        str += chars[r.Next(0, chars.Length)].ToString();
                    }
                    builder.AddItems(str);
                }
            }

            string[] tests =
            {
                "sDtCNeE0",
                "CvaDnSes",
                "nuhwiwo0",
                "SivWTuet",
                "EooNt0a0",
                "sDtCNeE0DNWsnuNo",
                "CvaDnSesWCWWPvou",
                "nuhwiwo0EvPSuWTr",
                "SivWTuetGspviCPi",
                "EooNt0a0pDETTtaD",
                "sDtCNeE0DNWsnuNopuu0DavSwSnSriGu",
                "CvaDnSesWCWWPvousGDTEnGrSN0Du0DW",
                "nuhwiwo0EvPSuWTrDhCtsptTePivtnsP",
                "SivWTuetGspviCPitChESueDDEaNnNPr",
                "EooNt0a0pDETTtaDSotThCwGsC0DWCnD",
                "sDtCNeE0DNWsnuNopuu0DavSwSnSriGuWShTPPouveuutsEPwEuva0DtNTDsDnin",
                "CvaDnSesWCWWPvousGDTEnGrSN0Du0DWEhuGuDWootiWtEtNpTrpeTeDnNrEPDC0",
                "nuhwiwo0EvPSuWTrDhCtsptTePivtnsPTurPehNSihpteEPepCTSDSGwCwDuuihD",
                "SivWTuetGspviCPitChESueDDEaNnNPrDW0EDWsiSvGWnuPPGhPPnnanruPSwDGu",
                "EooNt0a0pDETTtaDSotThCwGsC0DWCnDvwCwSEDhiwWPNroPwoeteGuEWtDwT0uP"
            };

            builder.SetLength(8);

            for (int i = 0; i < 5; i++)
            {
                builder.SetMagic(i + 1);
                if(args.Contains("-c"))
                {
                    char[] a = builder.ToString().ToCharArray();
                    char[] b = tests[i].ToCharArray();
                    for (int j = 0; j < a.Length; j++)
                    {
                        if (a[j] == b[j])
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        } else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }

                        Console.Write(a[j]);
                    }
                    Console.ResetColor();
                    Console.CursorLeft = 65;
                    Console.WriteLine(builder.Length.ToString().PadRight(7) + $"{i + 1}".PadRight(6) + $"{(builder.ToString() == tests[i])}");
                } else
                {
                    Console.WriteLine(builder.ToString().PadRight(65) + builder.Length.ToString().PadRight(7) + $"{i + 1}".PadRight(6) + $"{(builder.ToString() == tests[i])}");
                }
            }

            builder.SetLength(16);

            for (int i = 0; i < 5; i++)
            {
                builder.SetMagic(i + 1);
                if (args.Contains("-c"))
                {
                    char[] a = builder.ToString().ToCharArray();
                    char[] b = tests[i + 5].ToCharArray();
                    for (int j = 0; j < a.Length; j++)
                    {
                        if (a[j] == b[j])
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }

                        Console.Write(a[j]);
                    }
                    Console.ResetColor();
                    Console.CursorLeft = 65;
                    Console.WriteLine(builder.Length.ToString().PadRight(7) + $"{i + 1}".PadRight(6) + $"{(builder.ToString() == tests[i + 5])}");
                }
                else
                {
                    Console.WriteLine(builder.ToString().PadRight(65) + builder.Length.ToString().PadRight(7) + $"{i + 1}".PadRight(6) + $"{(builder.ToString() == tests[i + 5])}");
                }
            }

            builder.SetLength(32);

            for (int i = 0; i < 5; i++)
            {
                builder.SetMagic(i + 1);
                if (args.Contains("-c"))
                {
                    char[] a = builder.ToString().ToCharArray();
                    char[] b = tests[i + 10].ToCharArray();
                    for (int j = 0; j < a.Length; j++)
                    {
                        if (a[j] == b[j])
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }

                        Console.Write(a[j]);
                    }
                    Console.ResetColor();
                    Console.CursorLeft = 65;
                    Console.WriteLine(builder.Length.ToString().PadRight(7) + $"{i + 1}".PadRight(6) + $"{(builder.ToString() == tests[i + 10])}");
                } else
                {
                    Console.WriteLine(builder.ToString().PadRight(65) + builder.Length.ToString().PadRight(7) + $"{i + 1}".PadRight(6) + $"{(builder.ToString() == tests[i + 10])}");
                }
            }

            builder.SetLength(64);

            for (int i = 0; i < 5; i++)
            {
                builder.SetMagic(i + 1);
                if (args.Contains("-c"))
                {
                    char[] a = builder.ToString().ToCharArray();
                    char[] b = tests[i + 15].ToCharArray();
                    for (int j = 0; j < a.Length; j++)
                    {
                        if (a[j] == b[j])
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }

                        Console.Write(a[j]);
                    }
                    Console.ResetColor();
                    Console.CursorLeft = 65;
                    Console.WriteLine(builder.Length.ToString().PadRight(7) + $"{i + 1}".PadRight(6) + $"{(builder.ToString() == tests[i + 15])}");
                } else
                {
                    Console.WriteLine(builder.ToString().PadRight(65) + builder.Length.ToString().PadRight(7) + $"{i + 1}".PadRight(6) + $"{(builder.ToString() == tests[i + 15])}");
                }
            }

            if (args.Contains("-r"))
            {
                Thread.Sleep(1000);
                Console.CursorTop = Console.CursorTop - 26;
                Main(args);
            }
        }
    }
}
