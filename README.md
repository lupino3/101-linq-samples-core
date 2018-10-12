# 101 LINQ Examples in .NET Core

[101 LINQ Examples](https://code.msdn.microsoft.com/101-LINQ-Samples-3fb9811b)
is a great resource for those (like me) who are learning C# and LINQ.

However, I find the format of the examples a bit cumbersome: one solution file
for each set of examples, lots of distracting files in the archive, a web
interface that is hard to read. What I personally care about is just the bare
minimum to read and run the code.

Therefore, as I am also learning my way through .NET Core and Visual Studio
Code, I thought it would be nice to port those examples to .NET Core and
package them in a repo that makes it easy to run them.

**NOTE**: I am not the author of the examples. I am just repackaging the existing
samples to make sure they work under .NET Core and Visual Studio Code and
changing them as little as possible to make them work.

## Changes

Some minor changes were necessary to make the code samples work with .NET Core and
Visual Studio Code.

Most of the changes were done using the script `convert-samples.ps1`, a primitive
PowerShell script to do the heavy lifting.

The `.cs` files are the original files from the MSDN site. Changes made:

* uncomment all samples (done manually);
* read `customers.xml` as an `EmbeddedResource` (done via `convert-samples.ps1`).

All the `.csproj` files have been generated from scratch with the `dotnet` CLI by
the `convert-samples.ps1` script. Changes made from the bare-bones `.csproj` files
when needed:

* add reference to `customers.xml` (done via script);
* add reference to the `ObjectDumper` project (manually);
* add reference to `System.Data.DataSetExtensions` (manually).

A separate project was created for the `ObjectDumper` library.

The manual changes could have been automated into the `convert-samples.ps1` script,
but at that point it was just simpler to do the changes manually rather than
change and test the script again.

## How to run the examples

TODO: add instructions to execute each example for each platform (Windows,
Linux).

## Visual Studio Code

With VSCode, having the .NET Core SDK and the C# extension installed, open
the workspace file `samples.code-workspace`. It contains all projects, and
will allow you to open all files from VSCode and run every project from
the IDE itself, as opposed to opening the directory but not being able to
automatically launch each separate example.