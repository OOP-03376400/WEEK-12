using System;

public delegate void EngineMonitor(String s);

public class Car
{
    private int currentSpeed = 0;
    private bool isDead = false;
    private int maxSpeed;
    private String name;

    public event EngineMonitor Exploded = null;
    public event EngineMonitor AboutToExplod = null;


    public Car(String name, int maxSpeed)
    {
        this.name = name;
        this.maxSpeed = maxSpeed;
    }

    public void Accelerate(int increment)
    {
        if (isDead)
        {
            if (Exploded != null)
                Exploded("The car has exploded");
        }
        else
        {
            currentSpeed += increment;
            if (currentSpeed >= maxSpeed)
            {
                isDead = true;
                if (Exploded != null)
                    Exploded("The car has exploded");
            }
            else if (currentSpeed + 20 >= maxSpeed && AboutToExplod != null)
                AboutToExplod("Dangerous Speed:" + currentSpeed + ", Car about to explod");
            else
                Console.WriteLine("Current Speed = " + currentSpeed);

        }
    }
}

public class EventExample
{
    public static void Main()
    {
        Car myCar = new Car("Corola", 200);

        //register with event source
        myCar.Exploded += new EngineMonitor(OnExplod);
        myCar.AboutToExplod += new EngineMonitor(OnAboutToExplod);

        //speed up
        for (int i = 0; i < 10; i++)
            myCar.Accelerate(20);

        //cancel registration to events
        myCar.Exploded -= new EngineMonitor(OnExplod);
        myCar.AboutToExplod -= new EngineMonitor(OnAboutToExplod);

        //no response
        for (int i = 0; i < 10; i++)
            myCar.Accelerate(20);
    }

    public static void OnExplod(String s)
    {
        Console.WriteLine("Message from car: " + s);
    }

    public static void OnAboutToExplod(String s)
    {
        Console.WriteLine("Message from car: " + s);
    }
}

