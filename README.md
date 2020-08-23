# Phone-Hub

Simple GUI to connect your phone to your pc

## Releases

Here's the link to the latest release : [Download PhoneHub](https://github.com/LolnationCH/Phone-Hub/releases/latest/download/PhoneHub.zip)

## How it works

Phone-Hub uses two other project :

- [gnirehtet](https://github.com/Genymobile/gnirehtet)
- [scrcpy](https://github.com/Genymobile/scrcpy)

Phone-Hub is a project I made because I didn't want to remember command line option for two programs, and wanted to provide a simple GUI for a user.

## Features

Phone-Hub provides a interface to access certain features of both scrpcy and gnirehtet, namely :

- Copy the display of a phone (through usb or wifi)
- Connect the phone to the internet connection of the PC (through usb)

It allow the user to select which device it wants to use for both of these program.

A feature unique to Phone-Hub is connecting you phone for scrcpy (allowing to see the display) through wifi with only a button click (see more details below).

## So, why not just use the programs directly with a script

I wanted to be able to allow easier access for options without having to do invent options of my own and also didn't want to bother trying to figure out how to connect with wifi using a scripting language.

By no means this is perfect, and you have options not represented here that you can use if you use a cmd. But for me, this was more efficient.

## Setup

You need to download the latest version of both programs :

- [gnirehtet](https://github.com/Genymobile/gnirehtet)
- [scrcpy](https://github.com/Genymobile/scrcpy)

and you will also need these files:

- adb.exe
- AdbWinApi.dll
- AdbWinUsbApi.dll

You should already have theses downloaded, since you need them for gnirehtet and scrcpy.

Now, once everything is downloaded, you repository with PhoneHub.exe should look like this :

```txt
- PhoneHub
    - PhoneHub.exe
    - ADB
        - adb.exe
        - AdbWinApi.dll
        - AdbWinUsbApi.dll
    - Gnirehtet
    - Scrcpy
    ...
```

Before launching PhoneHub.exe, make sure of the following :

> The Android device requires at least API 21 (Android 5.0).
>
> Make sure you [enabled adb debugging][enable-adb] on your device(s).
>
> [enable-adb]: https://developer.android.com/studio/command-line/adb.html#Enabling
>
> On some devices, you also need to enable [an additional option][control] to
> control it using keyboard and mouse.
>
> [control]: https://github.com/Genymobile/scrcpy/issues/70#issuecomment-373286323

If everything is ready, you can launch PhoneHub.exe now.

## How to use

Here's the interface of the application :

![phone-hub-explain.png](/phone-hub-explain.png)

### Section 1

This section correspond to gnirehtet, allowing to connect your phone to the same internet connection of your pc.

You can use the dropdown to select you phone, if you have many, and click connect to `connect`.

When you press connect, you should see a `cmd` window pop up. This window needs to stay open during the usage of the connection. When you are done, select disconnect and now you can close this cmd window if you wish.

### Section 2

This section allow the user to connect is phone trough wifi.
This allow the user to not have a usb connection when using [section 3](#section-3).

First, plug in your phone. Then press connect. Once your device is connected with the button, you can disconnect your phone.

When you are done, select disconnect to remove the wifi connection between your phone and your pc.

> Note : Your phone needs to be on the same network as your pc, otherwise it will not work.

### Section 3

This section correspond to scrcpy, allowing to interact with your phone on your pc.
You can lock the position of the screen to always be displayed as portrait on you pc.
You can also set to always display the screen on top of every application on your pc.

### Other notes

The refresh button in each section allows the user to refresh the list of device, in case it was not automatically detected upon connecting your phone with usb.

The dropdown for the `devices connected` for each section contains all devices connected, either trough wifi or usb, except section 1, since it needs a usb connection to work. Devices connected trough wifi are identified with "(wifi)" after their name.
