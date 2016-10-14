# AnimaPaper

AnimaPaper is a software that allow you to display a video (support most of the common extensions) behind the desktop's icons.

Check the reddit topic for more informations:
https://www.reddit.com/r/LivingBackgrounds/comments/57br0q/i_made_this_software_that_you_may_be_interested_in/

## Installation

Download the 4 files ready to use.
There is not installer.

## Compile

Require VS2015 and .NET Framework 4.5.

## Known issues

Work only for Windows 8, 8.1 and 10 for now.

## Current

- Load and play most video files like a Live Wallpaper (Looping)
- Can Minimize to Tray
- Multiple Monitor support (Maybe ? need some test, I have only 1, coding blindly)
- Can enable / disable sound while playing and change volume
- WiP different video on each monitor NOT FULLY IMPLEMENTED Basically the UI and some controls are here but not working, may crash if you try.

## Upcoming

- Automatic patching ?
- Library management (Maybe ?)
- automatically change wallpapers/videos based on time? Like, one for the morning, one for afternoon, one for night, etc. (From xrailgun )
- 1 different video per monitor (Yes it's possible, it requires some coding but I can do it :b) (It will also increase the CPU and Memory Load.


## Usage

https://www.youtube.com/watch?v=Jj--GahPvUQ&feature=youtu.be

## Contributing

1. Fork it!
2. Create your feature branch: `git checkout -b my-new-feature`
3. Commit your changes: `git commit -am 'Add some feature'`
4. Push to the branch: `git push origin my-new-feature`
5. Submit a pull request :D

## Changelog

	v0.2.5: 
	- Add Sound Control
	- Add UI and some code for 1 video per monitor DOES NOT WORK YET
	- Fix for Windows 7 and below users (hopefully it's working, I don't have Windows 7 anymore) Reverted because not working
	
	v0.2.4: 
	- Add Gif files should properly loop now
	
	v0.2.3.3:
	- I changed some stuff about the memory management and the objects, even with run stop run stop etc, you should not feel lags.
	- The minimizing to Tray Icon SHOULD work properly and not crash or stay in the Tray even when the application is closed.
	- Minimizing using Minimize button top-right. Closing button will actually close everything properly now. (Unless you want to change it ?)
	- Closing using X button top-right. Closing button will actually close everything properly now. (It was staying in the Task manager actually but now it should work as intended) 
	
## Credits

Creator Idolkeg.

Thank you, [contributors]!
[contributors]: https://github.com/thoughtbot/$(REPO_NAME)/graphs/contributors

## License

AnimaPaper is Copyright (c) 2016.

It is free software, and may be redistributed

under the terms specified in the [LICENSE] file.

[LICENSE]: /LICENSE
