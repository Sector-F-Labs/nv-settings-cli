# nv-settings-cli
Interact with Nvidia GPUs via the windows command line.

## Brief
The state of command line utilities for controlling an Nvidia graphics card on Windows is a little sad. `nvidia-smi` will give you your monitoring stats (although I have found these disagree with both MSI afterburner and this utility for some stats) but the command is unable to actually set the fan speed in response to the information it is gathering. While those using linux are able to utilize `nvidia-settings` to acheive custom programatic fan curves, those on Windows are left to battle with a C library: `nvapi` which is very difficult to follow with a lot of undocumented functions and also has [compatibility issues](https://github.com/falahati/NvAPIWrapper/issues/1) with RTX cards.

`nv-settings-cli` makes use of [NvAPIWrapper](https://github.com/falahati/NvAPIWrapper) to access headscratchingly hard to find in documentaton functions to do things that MSIAfterburner makes look easy.

## Building
`nv-settings-cli` is a standard C# Console project and was built in Visual Studio. Simply clone the repo, open it with Visual Studio and fetch the NuGet packages.

## Testing
Since this utility sets the GPU fan to a single speed. Its recommended that you install [MSI Afterburner](https://www.msi.com/Landing/afterburner/graphics-cards) so that you can set the fan speed back to Automatic or Curve based once you are finished with testing.