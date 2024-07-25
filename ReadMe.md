![Klueber.Em.Brokers](https://github.com/KLMuc/Klueber.Em.Brokers/blob/master/documentation/images/kl_em_broker_git_logo.png?raw=true)

[![.NET](https://github.com/KLMuc/Klueber.Em.Brokers/actions/workflows/dotnet.yml/badge.svg)](https://github.com/KLMuc/Klueber.Em.Brokers/actions/workflows/dotnet.yml)
[![](https://img.shields.io/github/release/KLMuc/Klueber.Em.Brokers.svg?label=latest%20release&color=007edf)](https://github.com/KLMuc/Klueber.Em.Brokers/releases/latest)
[![open issues](https://img.shields.io/github/issues/KLMuc/Klueber.Em.Brokers)](https://github.com/KLMuc/Klueber.Em.Brokers/issues)

# Klueber.Em.Brokers
This library is designed & developed as a wrapped handler around the API to provide the following values:

1. Simplified API communication
2. Test-friendly implementation

You can get Klueber.Em.Brokers [NUGET](https://github.com/KLMuc/Klueber.Em.Brokers/pkgs/nuget/Klueber.Em.Brokers) package by typing:
```
dotnet add package Klueber.Em.Brokers
```
# How to Use
In your .Net Core application, you can initialize the broker by calling the *AddEmBrokers* extension method on the IServiceCollection instance.
```csharp
services.AddEmBroker(client =>
{
    // Configure your HttpClient here
    client.BaseAddress = new Uri("https://api.example.com");
    // Additional configurations...
},
handlerBuilder =>
{
    // Optional: Configure your HttpMessageHandler here
    // Example: handlerBuilder.AddHttpMessageHandler<YourCustomHandler>();
});
// Additional service registrations...
```

# How to Use the Implemented Services
The library provides a set of services to interact with the API.
The services are registered in the DI container and can be injected into your classes.

```csharp
public class YourService
{
    private readonly ILucaService _lucaService;

    public YourService(ILucaService lucaService)
    {
        _lucaService = lucaService;
    }
}
```

