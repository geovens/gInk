
#### Introduction

gInk is an on-screen annotation software under Windows, used to help improving my presentations and demonstrations, and to help working on temperary thoughts which need to be noted beside something on the screen. The features are greatly inspired by another screen annotation software Epic Pen, but even more easy to use. gInk is made with the idea kept in mind that the interface should be as simple as possible and should not distract attention of both the presenter and the audience when used for presentations. Unlike in many other softwares in the same category, you select from pens to draw things instead of changing individual settings of color, transparency and tip width everytime. Each pen is a combination of these attributes and is configurable to your need.

#### Screen Shots

![screenshot](https://raw.githubusercontent.com/geovens/gInk/master/screenshot1.jpg)  
![screenshot](https://raw.githubusercontent.com/geovens/gInk/master/screenshot2.jpg)  

#### Download

https://github.com/geovens/gInk/releases/

#### How to use

Start gInk and an icon will appear in the system tray. Click the icon (or use a hotkey) to start drawing on screen.  
Click the exit button or press ESC to exit drawing.  

#### Features

- Compact and intuitive interface.  
- Inks rendered on dynamic desktops.  
- Stylus with eraser, touch screen and mouse compatible.  
- Click-through mode.  
- Multiple displays support.  
- Pen pressure support.  
- Snapshot support.  
- Hotkey support.    

#### Tips

- There is a known issue for multiple displays of unmatched DPI settings (100%, 125%, 150%, etc.). If you use gInk on a computer with multiple displays of unmatched DPI settings, or you encounter problems such as incorrect snapshot area, being unable to drag toolbar to locations etc., please do the following as a workaround (in Windows 10 version 1903 as an example): right-click gInk.exe, Properties, Compatibility, Change high DPI settings, Enable override high DPI scaling behavior scaling performed by: Application. (do this only for gInk version v1.1.0 and after)
- There are a few hidden options you can tweak in config.ini that are not shown in the options window.
- Many have asked for features to draw lines, arrows, squares, texts etc. I indeed wish to add these features, but currently I haven't found a way to implement them while keeping the UI simple, which I weight more. The good news is that someone else (pubpub-zz) is actively working on a project [ppInk](https://github.com/pubpub-zz/ppInk) which is based on gInk, adding many more functions to it including drawing lines, arrows, squared, texts etc. You could check whether the fork project meets your needs if you want these features.

#### How to contribute translation

gInk supports multiple languages now. Here is how you can contribute translation. Simply create a duplication of the file "en-us.txt" in "bin/lang" folder, rename it and then translate the strings in the file. Check in gInk to make sure your translation shows correctly, and then you can make a pull request to merge your translation to the next version of release for others to use.  


----
gInk  
https://github.com/geovens/gInk  
Weizhi Nai @ 2020  
