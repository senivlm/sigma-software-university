using System;
using System.IO;
using System.Text;
using System.Linq;
namespace stringType
{
    /// <summary>
    /// change some symbol on another
    /// </summary>
    class stringChange
    {
        static public void changeString(ref string[] textTochange, char whatChange, char newSymbolStart, char newSymbolEnd)
        {

            StringBuilder[] text = new StringBuilder[textTochange.Length];
            for (int k = 0; k < textTochange.Length; ++k)
            {
                text[k] = new StringBuilder(textTochange[k]);
            }
            int posStart = 0, posEnd = textTochange[textTochange.Length - 1].Length - 1;
            int j = textTochange.Length - 1, i = 0;
            while (j >= 0 && i <= textTochange.Length && j >= i)
            {
                if ((j == 0 && posEnd < 0) || (i == textTochange.Length - 1 && posStart > textTochange[i].Length)) break;
                while (i < textTochange.Length)
                {
                    if (posStart >= textTochange[i].Length && i < textTochange.Length)
                    {
                        ++i; posStart = 0;
                    }
                    else if (textTochange[i][posStart] == whatChange) break;
                    ++posStart;
                }
                while (j >= 0)
                {
                    if (posEnd < 0 && j >= 0)
                    {
                        if (j == 0) break;
                        --j;
                        if (textTochange[j].Length != 0)
                        {
                            posEnd = textTochange[j].Length - 1;
                        }
                    }
                    else if (textTochange[j][posEnd] == whatChange)
                    {
                        break;
                    }
                    posEnd--;

                }
                if (textTochange[i][posStart] == whatChange && ((posEnd > posStart && i == j) || i < j) && textTochange[j][posEnd] == whatChange)
                {
                    text[i][posStart] = newSymbolStart;
                    text[j][posEnd] = newSymbolEnd;
                }
                ++posStart; --posEnd;
            }
            for (int k = 0; k < textTochange.Length; ++k)
            {
                textTochange[k] = text[k].ToString();

            }

        }
    }

    class Proection3D
    {
        int[,,] arr3D;
        int n,//x-col
            m,//y-rows
            p;//z
        public Proection3D(int[,,] arr3D, int x, int y, int z)
        {
            this.arr3D = arr3D;
            n = x; m = y; p = z;
        }
        public int[,] getProectionXY()
        {
            int[,] proection = new int[m, n];
            for (int i = 0; i < m; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    for (int k = 0; k < p; ++k)
                    {
                        if (proection[i, j] != 0) { break; }
                        if (arr3D[k, i, j] != 0)
                        {
                            proection[i, j] = arr3D[k, i, j];
                        }
                        else
                        {
                            proection[i, j] = 0;

                        }
                    }
                }
            }
            return proection;
        }
        public int[,] getProectionYZ()
        {
            int[,] proection = new int[p, m];
            for (int i = 0; i < p; i++)
            {
                for (int j = 0; j < m; ++j)
                {
                    for (int k = 0; k < n; ++k)
                    {
                        if (proection[i, j] != 0) break;

                        proection[i, j] = arr3D[i, j, k];

                    }
                }
            }
            return proection;
        }
        public int[,] getProectionXZ()
        {
            int[,] proection = new int[p, n];
            for (int i = 0; i < p; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    for (int k = 0; k < m; k++)
                    {
                        if (proection[i, j] != 0) break;
                        proection[i, j] = arr3D[i, k, j];
                    }
                }
            }
            return proection;
        }
    }
    class filePath{
     
       public  static string getFileName(string file_path)
        {
            string ret;
            int slash=file_path.LastIndexOf('\\');
            ret = file_path.Substring(slash+1);
            int dot = ret.LastIndexOf('.');
            ret = ret.Substring(0, dot);
          
            return ret;
            
        }
       public static string getRoot(string filePath)
        {
            int slash = filePath.IndexOf('\\');
            string ret = filePath.Substring(slash + 1);
            slash = ret.IndexOf('\\');
            ret = ret.Substring(0, slash);
            return ret;
        }
    }
    class Program
    {
        
        
        static void Main(string[] args)
        {
            /*  Random r = new Random();
              int[,,] a = new int[2, 3, 4];
              for(int i = 0; i < 2; i++)
              {
                  for(int j = 0; j < 3; j++)
                  {
                      for(int k = 0; k < 4; k++)
                      {
                          a[i, j, k] = r.Next(0,2);
                      }
                  }
              }
              a[0, 0, 0] = 0;
              a[0, 0, 1] = 0;
              Proection3D proect = new Proection3D(a,4,3,2);
              int[,] pr=proect.getProectionYZ();
              for (int i = 0; i < 2; i++)
              {
                  for (int j = 0; j < 3; ++j)
                  {
                      Console.Write($"{pr[i, j],-4}");
                  }
                  Console.WriteLine();

              }

              Console.WriteLine("\n");
              pr = proect.getProectionXY();
              for (int i = 0; i < 2; i++)
              {
                  for (int j = 0; j < 3; ++j)
                  {
                      Console.Write($"{pr[i, j],-4}");
                  }
                  Console.WriteLine();

              }
              Console.WriteLine("\n");
              pr = proect.getProectionXZ();
              for (int i = 0; i < 2; i++)
              {
                  for (int j = 0; j < 3; ++j)
                  {
                      Console.Write($"{pr[i, j],-4}");
                  }
                  Console.WriteLine();

              }*/
            /* if (File.Exists(@"textStringBuilder.txt"))
             {
                 string[] text = File.ReadAllLines(@"textStringBuilder.txt");
                 stringChange.changeString(ref text, '#', '<', '>');
                 foreach(string a in text)
                 {
                     Console.WriteLine(a);
                 }
             }*/
            string s = @"c: \ WebServers \ home \ testsite \ www \ myfile.txt";
            Console.WriteLine(filePath.getRoot(s));
           
        }
        
        

    }
}