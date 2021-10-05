using System;
using System.IO;
using System.Linq;
namespace home1
{
    class Product
    {
        public Product(string name, double cost, double weight, DateTime manufactureDate, TimeSpan expireDays) 
            : this(name, cost, weight)
        {

            this.manufactureDate = manufactureDate;
            this.expireDays = expireDays;
        }
        public override string ToString()
        {
            return "class Product: name = " + name + ", cost = " +  Cost + ", weight= " + weight +
                ", manufactureDate = "+manufactureDate  +", days to expire = "+ expireDays.TotalDays+"\n\n";
        }
        public Product(string name, double cost, double weight)
        {
            this.name = name;
            this.Cost = cost;
            this.weight = weight;

        }
        public DateTime manufactureDate;
        public TimeSpan expireDays;
        public Product() { }
        public string name;
        protected double cost;
        public void Parse(string s)
        {
            string[] words = s.Split();
            
            if (words.Length != 5)
            {
                throw new FormatException("Invalid format to parse Product\n");
            }
            name = words[0];
            double temp;
           if( double.TryParse(words[1], out temp))
            {
                Cost = temp;
            }
            if (double.TryParse(words[2], out temp))
            {
                weight = temp;
            }
            if (words[3].Count(ch => ch == '/')==2){
                
                
                   if(! DateTime.TryParse(words[3], out manufactureDate))
                    {
                        throw new ArgumentException("Invalid parameters\n");
                    }
            }  
            
            if(!TimeSpan.TryParse(words[4], out expireDays))
            {
                throw new ArgumentException("Invalid parameters\n");

            }

        }
        virtual public void changeCost(double percent)
        {
            cost *= percent;
        }
        public double Cost
        {
            set
            {
                if (value >= 0)
                {
                    cost = value;
                }
                else {
                    cost = -value;
                }
            }
            get
            {
                return cost;
            }
        }
        public double weight;
    }
    class Meat : Product
    {
        public Meat(string name, double cost, double weight, string category, string type)
            : base(name, cost, weight)
        {
            this.category = category;
            this.type = type;
        }
        public override void changeCost(double percent = 1.5)
        {
            if (category == "1")
            {
                percent += 0.25;
            }
            else
            {
                percent += 0.05;
            }
            cost *= percent;
        }
        string category;
        string type;
        public void writeIntoConsole()
        {
            Console.WriteLine("class Meat: name = " + name + ", cost = " +
               Cost + ", weight= " + weight + ", category= " + category + ", type= " + type + "\n");
        }
    }
    class Dairy_products : Product
    {
        
        public Dairy_products(string name, double cost, double weight, DateTime manufactureDate,int daysExpire) :
            base(name, cost, weight,manufactureDate, new TimeSpan(daysExpire))
        {
        }

        public override void changeCost(double percent = 1.25)
        {
            cost *= percent - expireDays.Days / 10;
        }
    }
    class Storage
    {
        public Product[] arrProduct;
        public Storage(int size)
        {
            arrProduct = new Product[size];
            for (int i = 0; i < size; i++)
            {
                arrProduct[i] = new Product();
            }
        }
        public Product this[int index]
        {

            get => arrProduct[index];
            set => arrProduct[index] = value;
        }

        public string FindMeat()
        {
            string returnValue = "";
            Meat temp = new Meat("", 0, 0, "", "");
            for (int i = 0; i < arrProduct.Length; i++)
            {
                if (arrProduct[i].GetType() == temp.GetType())
                {
                    returnValue += Check.getInfoProduct(in arrProduct[i]);
                }

            }
            return returnValue;
        }
        public void changeCost(double percent)
        {
            foreach (Product a in arrProduct)
            {
                a.changeCost(percent);
            }
        }
        public override string ToString()
        {
            string value = "";
            for (int i = 0; i < arrProduct.Length; i++)
            {
                value +=  arrProduct[i];
            }
            return value;
        }
        public void getInfo()
        {
            for (int i = 0; i < arrProduct.Length; i++)
            {
                Console.WriteLine($"Poduct №{i + 1}Enter the name product\t");
                arrProduct[i].name = Console.ReadLine();
                Console.WriteLine("Enter the cost of  product\t");
                arrProduct[i].Cost = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Enter the weight of  product\t");
                arrProduct[i].weight = Convert.ToDouble(Console.ReadLine());

            }
        }
        public void deleteExpireDairyProduct()
        {
            DateTime currentDate = DateTime.Now;
            int amount = 0;
            Product[] temp = new Product[arrProduct.Length];
            for (int i = 0; i < arrProduct.Length; i++)
            {
                if (arrProduct[i].manufactureDate + arrProduct[i].expireDays > currentDate)
                {
                    temp[amount] = arrProduct[i];
                    ++amount;
                }
            }
            arrProduct = new Product[amount];
            for (int i = 0; i < amount; i++)
            {

                arrProduct[i] = temp[i];
            }


        }
        public void readFromFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                string[] lines = File.ReadAllLines(fileName);
                
                
                for (int i = 0, j = 0; j < arrProduct.Length && i < lines.Length; i++)
                {
                    try
                    {
                        arrProduct[j].Parse(lines[i]);
                        ++j;
                    }
                    catch (Exception ex)
                    {
                       // throw;
                        Console.WriteLine(ex.Message);
                        
                    }
                }
            }
        }
    }
    class Buy
    {
        public Buy(int amount, int cost, double weigth = 1)
        {
            this.Amount = amount;
            this.cost = cost;
            this.weight = weigth;
        }

        private int Amount;
        public int amount {

            get {

                return Amount;
            }
            set
            { if (value > 0) {
                    Amount = value;

                }
            }
        }
        public int cost;
        public double weight;
        public void writeInfo()
        {
            Console.WriteLine("cost = " + cost + ", weight= " + weight + ", amount= " + amount + "\n");
        }
    }
   sealed class Check
    {
        public static string getInfoProduct(in Product item)
        {
            string returnValue="class Product: name = " + item.name + ", cost = " +
                item.Cost + ", weight= " + item.weight + "\n\n";
            return returnValue;
        }
        public static string getInfoBuy(in Buy item)
        {
            return"class Buy: amount = " + item.amount + " ,cost = " +
                item.cost + ", weight= " + item.weight + "\n\n";
             
        }
    }
    enum Month:byte
    {
        January=1,
        February,
        March,
        April,
        May,
        June,
        July,
        August,
        September,
        October,
        November,
        December

    }
    enum Quater:byte
    {
        first=1,second,third,fourth
    }
   class Electricity
   {
        Month[] quaterMonths;
        readonly string lastName;
        readonly public uint roomNumber;
        readonly public double[] indicatorsIn;
        readonly public double[] indicatorsOut;
        public Electricity(Quater quater,string lastName,
            uint roomNumber,in double[] monthIndicatorsIn,in double[] monthIdicatorsOut )
        {
            if (monthIdicatorsOut.Length != 3 || monthIdicatorsOut.Length != 3)
            {
                throw new Exception("invalid parameters\n");
            }
            byte i;
            switch (quater)
            {
                case Quater.first:
                    i = 1;
                    break;
                case Quater.second:
                    i = 4;
                    break;
                case Quater.third:
                    i = 7;
                    break;
                case Quater.fourth:
                    i = 10;
                    break;
                default:
                    throw new Exception("Invalid quater number");
                    
            }
            quaterMonths = new Month[3];
            for (int j=0; j <  3; j++,i++)
            {
                quaterMonths[j] = (Month)i;
            }
            this.lastName = lastName;
            this.roomNumber = roomNumber;
            indicatorsIn = new[] { monthIndicatorsIn[0],
                monthIndicatorsIn[1], monthIndicatorsIn[2] };

            indicatorsOut = new[] { monthIdicatorsOut[0],
                monthIdicatorsOut[1], monthIdicatorsOut[2] };
        }
        public override string ToString()
        {
            string text = "";
            for(int i = 0; i < 3; i++)
            {
                text += $"{quaterMonths[i],-10}{indicatorsIn[i],-6}{indicatorsOut[i],-6}";
            }
            return ($"Room {roomNumber,-5} owner {lastName,-10} " + text+"\n");
                
        }
        public double getUsedElectricity()
        {

            return indicatorsOut[2] - indicatorsIn[0];
        }
    }
    class ElectricityList
    {
        Electricity[] arrElecricityAccount;
        public ElectricityList(in Electricity[] arr)
        {
            arrElecricityAccount = arr;
        }
        public ElectricityList(string fileName) {
           
            if (File.Exists(fileName))
            {
                string[] lines = File.ReadAllLines(fileName);
                string[] word = lines[0].Split();
                if (word.Length != 2)
                {
                    throw new Exception("file is uncorrect\n");
                }
                int number;Quater quater;
                string lName; uint room;double[] inIndicators = new double[3];
                double[] outIndicator=new double[3];
                if( int.TryParse(word[0], out number )&&Enum.TryParse(word[1],true,out quater)){
                    arrElecricityAccount = new Electricity[number];
                    for(int i = 0,j=1; i < number && j<lines.Length; ++j, i++)
                    {
                        word = lines[j].Split();
                        if (word.Length!=8)
                        {
                            --i;
                            continue;
                        }
                        lName = word[0];
                        uint.TryParse(word[1], out room);
                        for(int k = 0; k < 3; k++)
                        {
                            double.TryParse(word[2 + 2 * k], out inIndicators[k]);
                            double.TryParse(word[3 + 2 * k], out outIndicator[k]);
                        }
                        arrElecricityAccount[i] = new Electricity(quater, lName, room, inIndicators, outIndicator);

                    }

                }
                else
                {
                    throw new Exception("Uncorect file\n");
                }
            }

            
          
        }
        public Electricity this[int index]
        {
            get => arrElecricityAccount[index];
            set => arrElecricityAccount[index] = value;
        }
        public override string ToString()
        {
            string text = "";
           for(int i = 0; i < arrElecricityAccount.Length; i++)
            {
                text += arrElecricityAccount[i]+"\n";
            }
            return text;
        }
        public string  getInfoRoom(uint roomNumber)
        {
            for(int i = 0; i < arrElecricityAccount.Length; i++)
            {
                if (arrElecricityAccount[i].roomNumber == roomNumber)
                {
                    return arrElecricityAccount[i].ToString();
                }
            }
            return "no such account\n";
        }
        public string getMoreDebtRoom(double costKWatt) {
            double maxDebt= 0,currentPersonDept;
            int indexMaxDebt = 0;
            for(int i = 0; i < arrElecricityAccount.Length; i++)
            {
                
                    currentPersonDept = arrElecricityAccount[i].getUsedElectricity();
                
                if (currentPersonDept > maxDebt)
                {
                    maxDebt = currentPersonDept;
                    indexMaxDebt = i;
                }
            }
            return arrElecricityAccount[indexMaxDebt].ToString();
        }
        public uint getRoomUnused()
        {
            
            for(int i = 0; i < arrElecricityAccount.Length; i++)
            {
                if (arrElecricityAccount[i].getUsedElectricity()==0)
                {
                    return arrElecricityAccount[i].roomNumber;
                }
            }
            return 0;
        }

    }
    class MagicSquare
    {
        int size;
        int[][] matr;
        public MagicSquare(int Size)
        {
            size = Size;
            matr= new int[2*size][];
            for(int i = 0; i <2* size;i++)
            {
                matr[i] = new int[2*size];
            }
            int number;
            for (int J = 0, I = size - 1; J < size && I < 2 * size - 1; J++, I++)
            {

                number = size + J * size;

                for (int j = J, i = I, am = 0; am < size; am++, j++, i--)
                {
                    matr[j][i] = number;
                    number--;
                }
            }
            for (int j = size / 2; j < 2 * size - 1 - size / 2; j++)
            {
                for (int i = size / 2 + 1; i < 2 * size - 1 - size / 2; i++)
                {
                    if (matr[j][i] == 0)
                    {
                        if (j < size)
                        {
                            matr[j][i] = matr[j + size][i];
                            matr[j + size][i] = 0;
                        }

                        else
                        {
                            matr[j][i] = matr[j - size][i];
                            matr[j - size][i] = 0;
                        }
                    }
                }
            }
            for (int j = size / 2 + 1; j < 2 * size - 1 - size / 2; j++)
            {
                for (int i = size / 2; i < 2 * size - 1 - size / 2; i++)

                {
                    if (matr[j][i] == 0)
                    {
                        if (i < size)
                        {
                            matr[j][i] = matr[j][i + size];
                            matr[j][i + size] = 0;
                        }

                        else
                        {
                            matr[j][i] = matr[j][i - size];
                            matr[j][i - size] = 0;
                        }
                    }
                }
            }
        }

        public override string ToString()
        {
            string square = "";
            for(int i = size / 2; i < 2 * size - 1 - size / 2; i++)
            {
                for(int j = size / 2; j < 2 * size - 1 - size / 2; j++)
                {
                    square += matr[i][j]+" ";
                }
                square += "\n";
            }
            return square;
        }
    }
    class MagicSquare2
    {
        int size;
        int[][] matr;
        public MagicSquare2(int Size)
        {
            size = Size;
            matr = new int[2 * size-1][];
            for (int i = 0; i < size-1; i++)
            {
                matr[i] = new int[2*i+1];
            }
            for(int i = 0;i<size;++i)
            {
                matr[size + i-1] = new int[2*size - 1 - 2*i];
            }
            int number=size;
            for (int i = 0; i < size; ++i, --number)
            {
                for (int j = 0; j < matr[i].Length; j += 2)
                {
                    matr[i][j] = number  + j * (size / 2 + 1);
                }
            }
            number = size + 1;
            for(int i = size; i < size * 2-1; ++i,number+=5)
            {
                for (int j = 0; j < matr[i].Length; j += 2)
                {
                    matr[i][j] = number + j * (size / 2 + 1);
                }
            }
            int sizeCurArr = matr.Length;
            for (int j = size/2; j < matr.Length-size/2; j++)
            {
                for (int i =j-1 ; i < matr[j].Length-j+1; i++)
                {
                    if (matr[j][i] == 0)
                    {
                        if (j < size)
                        {
                            matr[j][i] = matr[j + size][i-j];
                            matr[j + size][i-j] = 0;
                        }

                        else
                        {
                            matr[j][i] = matr[j - size][i-j];
                            matr[j - size][i-j] = 0;
                        }
                    }
                }
            }

            for (int j = size / 2; j < matr.Length - size / 2; j++)
            {
                for (int i = j; i < matr[j].Length - j; i++)

                {
                    if (matr[j][i] == 0)
                    {
                        if (i < matr[j].Length/2)
                        {
                            matr[j][i] = matr[j][i + j];
                            matr[j][i + j] = 0;
                        }

                        else
                        {
                            matr[j][i] = matr[j][i - j];
                            matr[j][i - size] = 0;
                        }
                    }
                }
            }
          
        }
        
        
        public override string ToString()
        {
            string square = "";
            for(int i = 0; i < matr.Length; ++i)
            {
                for(int j = 0; j < matr[i].Length; ++j)
                {
                    square += matr[i][j]+" ";
                }
                square += "\n";
            }
            return square;
        }
    }

    class Polynomial
    {
        double[] cIndex;
        int polinomDegree;
        double x;
       public  Polynomial(int degree)
        {
            cIndex = new double[degree+1];
            polinomDegree = degree;

        }
        public double this[int index]
        {
            get=> cIndex[index];
            set
            {
                if (index <= polinomDegree)
                {
                   cIndex[index] = value;
                }
                else
                {
                    Array.Resize(ref cIndex, index);
                    cIndex[index] = value;
                }
               
            }
        }
        public static  Polynomial operator+(in Polynomial first,in Polynomial second)
        {
            int size, firstSize=first.polinomDegree,
                secondSize=second.polinomDegree;
            Polynomial bigger;
            if (firstSize > secondSize)
            {
                size = firstSize;
                bigger = first;
            }
            else
            {
                bigger = second;
                size = secondSize;
            }
            Polynomial result = new Polynomial(size);
            ++firstSize; ++secondSize;
            size -= Math.Abs(firstSize - secondSize);
            for(int i = 0; i <size+1;i++) {
                result[i] = first[i] + second[i];
            }
            for(int i = size+1; i < bigger.polinomDegree+1; i++)
            {
                result[i] = bigger[i];
            }

            return result;

        }
        public static Polynomial operator -(in Polynomial first, in Polynomial second)
        {
            int size, firstSize = first.polinomDegree,
                secondSize = second.polinomDegree;
            Polynomial bigger;
            if (firstSize > secondSize)
            {
                size = firstSize;
                bigger = first;
            }
            else
            {
                bigger = second;
                size = secondSize;
            }
            Polynomial result = new Polynomial(size);
            ++firstSize; ++secondSize;
            size -= Math.Abs(firstSize - secondSize);
            for (int i = 0; i < size + 1; i++)
            {
                result[i] = first[i] - second[i];
            }
            for (int i = size + 1; i < bigger.polinomDegree + 1; i++)
            {
                result[i] =- bigger[i];
            }

            return result;

        }
        public void Parse(string s)
        {
            char[] separators = {  '+' };
            string[] monomial = s.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            if ((monomial.Length - 1 != s.Count(ch => ch == '^'))
                ||
                (monomial.Length - 1 != s.Count(ch => ch == '*')))
            {
                throw new FormatException("Invalid parameters\n");
            }
            separators = new char[] { '^' };
            string[] sMonomial = monomial[monomial.Length - 1].Split(separators,StringSplitOptions.RemoveEmptyEntries);
            if (sMonomial.Length != 2)
            {
                throw new FormatException("Invalid parameters\n");
            }
            
            int size = int.Parse(sMonomial[1]);
            if (size > polinomDegree + 1) { cIndex = new double[size]; }
            cIndex[0] = double.Parse(monomial[0]);
            char[] separatorMult = new char[] { '*' };
            int curDeegre;

            for(int i = 1; i < monomial.Length; i++)
            {
                sMonomial=monomial[i].Split(separators, StringSplitOptions.RemoveEmptyEntries);
                if (sMonomial.Length != 2)
                {
                    throw new FormatException("Invalid parameters\n");
                }
                curDeegre = int.Parse(sMonomial[1]);
                sMonomial = monomial[i].Split(separatorMult, StringSplitOptions.RemoveEmptyEntries);
                if (sMonomial.Length != 2)
                {
                    throw new FormatException("Invalid parameters\n");
                }
                cIndex[curDeegre] = double.Parse(sMonomial[0]);
            }

        }
        public override string ToString()
        {
            string res="";
            if (cIndex[0] != 0)
            {
                res += cIndex[0];

            }
            for(int i = 1; i < polinomDegree + 1; i++)
            {
                if (cIndex[i] != 0)
                {
                    if (cIndex[i] > 0)
                    {
                        res += "+";
                    }
                    res += $" {cIndex[i]}*x^{i} ";

                }
            }
            return res;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {



            /*  Storage st = new Storage(5);
              try
              {
                  st.readFromFile(@"ProductsList.txt");
                  Console.WriteLine(st);
                  st.deleteExpireDairyProduct();
                  Console.Write("\n\n"+st);
              }catch(Exception ex)
              {
                  Console.WriteLine(ex.Message);
              }
              */


            ElectricityList electricityList = new ElectricityList(@"ElectricityAccounts");
            Console.WriteLine(electricityList);

           /* Polynomial p = new Polynomial(3);
            Polynomial p2 = new Polynomial(5);
            Polynomial p3=new Polynomial(1);
            try
            {
                p.Parse("2 + 3 * x ^ 2 + 4* x ^ 3");
                p2.Parse("4+2*x^1+4*x^3+4*x^5");
                p3 = p - p2;
                Console.WriteLine(p3);
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }*/

        }
        
       
        
        
    }
}
