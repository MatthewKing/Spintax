# Spintax

A simple library to spin text in the 'spintax' format.

## Usage

Get a single permutation:

```csharp
var testimonial = Spinner.Spin("This library is {cool|rad|awesome}.");
```

Get all permutations

```csharp
var testimonials = Spinner.SpinAll("This library is {cool|rad|awesome}.");
```


## Installation

Install it from [NuGet](https://www.nuget.org/packages/Spintax/)

```
PM> Install-Package Spintax
```

```
$ dotnet add package Spintax
```

## License and copyright

Copyright Matthew King.
Distributed under the [MIT License](http://opensource.org/licenses/MIT). Refer to license.txt for more information.
