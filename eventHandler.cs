using System;

delegate void Handler();
class Incrementer
{
    public event Handler CountADozen;
    const int length = 100;
    public void DoCount()
    {
        for (int i = 1; i < length; i++)
        {
            if (i%12 == 0 && CountADozen != null)
            {
                CountADozen();
            }
        }    
    }
}

class Dozens
{
    public int DozenCount { get; private set; }
    public Dozens(Incrementer incrementer)
    { DozenCount = 0;
        incrementer.CountADozen += IncrementerDozenCount;
    }

    private void IncrementerDozenCount()
    {
        DozenCount++;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Incrementer incrementer = new Incrementer();
        Dozens dozenCounter = new Dozens(incrementer);
        incrementer.DoCount();
        Console.WriteLine("number of dozen = {0}", dozenCounter.DozenCount);
    }
}
