msbuild /p:Configuration=Release /t:restore,build src/Ide/LightSwitch.Ide.Core/LightSwitch.Ide.Core.csproj
msbuild /p:Configuration=Release /t:restore,build src/Ide/LightSwitch.Ide.VSMac/LightSwitch.Ide.VSMac.csproj
/Applications/Visual\ Studio.app/Contents/MacOS/vstool setup pack src/Ide/LightSwitch.Ide.VSMac/bin/Release/net47/LightSwitch.Ide.VSMac.dll
