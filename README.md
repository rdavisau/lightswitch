# lightswitch 💡
Toggle light/dark visual override for Xamarin apps from the IDE.

![switching between dark and light override](https://ryandavis.io/content/images/2019/09/lightswitch.gif)

## what
iOS13 and Android 10 both introduce new system-wide dark mode visual options, which is awesome. Maybe less awesome is that we have to update our apps to observe these settings nicely. Switching between dark and light themes generally takes a few taps and/or swipes depending on whether you're using a device, simulator or emulator, and that's kind of a pain. LightSwitch is a rough and ready tool that makes toggling dark/light themes for your app quicker, by moving the pain to an upfront IDE plugin and NuGet install. Is it worth it? Not sure, but guess what I've made it now. 

LightSwitch will probably provide the most value when used in conjunction with some form of hot reload, by removing the disruption of switching to and from the app and platform settings. 

**_lightly tested (works on my machine), please report issues!_**

## installation

### in IDE (one time)

* Download the IDE plugin for Visual Studio or Visual Studio for Mac from the [releases](https://github.com/rdavisau/lightswitch/releases) page and install

* Show the LightSwitch 💡pad and put it somewhere convenient (or, learn the keyboard shortcuts).

### in app (for each app)

* Install the [LightSwitch NuGet](https://www.nuget.org/packages/LightSwitch) into your projects

* Somewhere near startup, call `LightSwitchAgent.Init();`. Putting this inside an `#if DEBUG` is a good idea, but not doing so might be more fun. Up to you.


* Make sure you meet the light/dark theme requirements for your platform
    * iOS: target iOS13 or above and run on iOS13 device/simulator

    * Android: target Android 10 or above and make sure your app theme derives from something like `Theme.AppCompat.DayNight`. On Android, LightSwitch depends on the [CurrentActivity plugin](https://www.nuget.org/packages/Plugin.CurrentActivity/); if you don't already use this in your project, make sure to initialize it at startup.

## use 

Once both the IDE and app are set up, you can use the LightSwitch 💡 pad or keyboard shortcuts to switch the theme override for running apps. The 'Light' and 'Dark' options force apps to apply the respective visual overrides. The 'Interval' option periodically switches between light and dark theme overrides at the interval specified. Since it doesn't seem to work very reliably at lower values, the shortest interval LightSwitch will use is 1000ms.

### keyboard shortcuts:

(These might be less useful than they sound because the IDE needs to have focus)

**VSMac:**

* Ctrl|Shift|T - Switch between light/dark override
* Ctrl|Shift|I - Toggle interval-based override
* Alt|Ctrl|Shift|T - Disable all overrides 

**VS:**

none right now

## how it works

The agent listens for commands on udp 20100 and 20101. It uses each platform's 'visual override' mechanism to force the visual style for the running app to match the style you ask for. This means it is not changing the overall theme setting for the device, and the override will not persist between restarts. On Android, only the current and future activities have their visual style overridden - activities in the backstack do not.

The ide broadcasts commands over all interfaces, so this might be a interesting if you work with colleagues using the same plugin, or if you don't `#if DEBUG` the agent and QA is working with tests builds while you're also developing 🤓. 

## contributing

I made this mostly as a means to procrastinate actually adding dark mode support to an existing app, in the hope that it might be useful for my workflow. It has many rough edges so contributions are welcome. Some ideas: 

* investigate uwp support
* improve networking story (see 'how it works')
* fix bad gtk ui in the VSMac plugin
* fix bad wpf ui in the VS plugin
* make it possible for activities in the back stack on Android to adopt the new theme setting 

## acknowledgements

* I spied a (new?) [plugin template](https://github.com/jamesmontemagno/plugin-template) from [@jamesmontemagno](https://twitter.com/JamesMontemagno), which I used for the agent. Thanks it worked good 👍
* Everything I know about GTK (not a lot) I got from the VSMac plugin for [@praeclarum](https://twitter.com/praeclarum)'s [Continuous](https://github.com/praeclarum/Continuous).
* Everything I know about VSIX (not a lot) I got from the VS plugin for [@praeclarum](https://twitter.com/praeclarum)'s [Continuous](https://github.com/praeclarum/Continuous).
* Theme toggling for the Xamarin.Forms sample straight out of [@davidortinau](https://twitter.com/davidortinau)'s [Xappy](https://github.com/davidortinau/Xappy). 
