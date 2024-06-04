using System;
using System.Collections.Generic;

public interface IObserver
{
    void Update(float temp, float humidity, float pressure);
}


public interface ISubject
{
    void RegisterObserver(IObserver o);
    void RemoveObserver(IObserver o);
    void NotifyObservers();
}

public class WeatherStation : ISubject
{
    private List<IObserver> observers;
    private float temperature;
    private float humidity;
    private float pressure;

    public WeatherStation()
    {
        observers = new List<IObserver>();
    }

    public void RegisterObserver(IObserver o)
    {
        observers.Add(o);
    }

    public void RemoveObserver(IObserver o)
    {
        observers.Remove(o);
    }

    public void NotifyObservers()
    {
        foreach (var observer in observers)
        {
            observer.Update(temperature, humidity, pressure);
        }
    }

    public void SetMeasurements(float temp, float humidity, float pressure)
    {
        this.temperature = temp;
        this.humidity = humidity;
        this.pressure = pressure;
        NotifyObservers();
    }
}


public class CurrentConditions : IObserver
{
    public void Update(float temp, float humidity, float pressure)
    {
        Console.WriteLine($"Current conditions: {temp}F degrees and {humidity}% humidity");
    }
}


public class ForecastDisplay : IObserver
{
    public void Update(float temp, float humidity, float pressure)
    {
        if (pressure > 1000)
        {
            Console.WriteLine("Forecast: Improving weather on the way!");
        }
        else
        {
            Console.WriteLine("Forecast: More of the same");
        }
    }
}


class Program
{
    static void Main(string[] args)
    {
        WeatherStation weatherStation = new WeatherStation();

        CurrentConditions currentConditions = new CurrentConditions();
        ForecastDisplay forecastDisplay = new ForecastDisplay();

        weatherStation.RegisterObserver(currentConditions);
        weatherStation.RegisterObserver(forecastDisplay);

        weatherStation.SetMeasurements(80, 65, 1012);
        weatherStation.SetMeasurements(82, 70, 998);

        Console.ReadLine();
    }
}
