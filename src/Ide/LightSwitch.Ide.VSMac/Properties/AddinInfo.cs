using System;
using Mono.Addins;
using Mono.Addins.Description;

[assembly: Addin(
    "LightSwitch",
    Namespace = "LightSwitch",
    Version = "1.0"
)]

[assembly: AddinName("LightSwitch 💡")]
[assembly: AddinCategory("IDE extensions")]
[assembly: AddinDescription("Easily toggle dark/light theme overrides from your IDE. Install the matching NuGet package into your project to use.")]
[assembly: AddinAuthor("Ryan Davis")]
