# MvcMefProvider

This library helps make it easier to use Microsoft.Composition package (a.k.a. MEF2) with ASP.NET MVC 5.

## License
[MIT License](https://github.com/sherif-elmetainy/MvcMefProvider/blob/master/LICENCE)

## Installing from Nuget

```
Install-Package CodeArt.MvcMefProvider
```

## Using with MVC 5

To use with in your ASP.NET MVC 5 project, you need to perform the following steps.

1. Install the Microsoft.Composition package and refer to [MEF documentation](https://mef.codeplex.com/documentation) to learn how to setup your container. You can either setup your dependencies' imports and exports via code or with attributes. I personally prefer the code approach as it makes the components container agnostic making it easier to switch later to a different IOC container.
2. To setup your component's lifetime to be instant per HTTP request you can either
	* Add `[Shared("InstancePerHttpRequest")]` attribute to the class
	* Or call extension method `InstancePerHttpRequest()` on the `PartConventionBuilder` if you are defining your dependencies with code (the preferred method).
3. After the `CompositionHost` (i.e. the composition container) object is created call the `UseWithMvc` extension method which would setup the depedency resolver and controller factory with implementations that would use the container.

Please refer to the [sample project](https://github.com/sherif-elmetainy/MvcMefProvider/tree/master/Src/CodeArt.MvcMefProvider.Sample) for more information.

