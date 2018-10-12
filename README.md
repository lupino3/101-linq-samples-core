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

The `.cs` files are the original files from the MSDN site. The changes can be found
in those files by looking for the `--- CHANGE ---` marker in comments.

All the `.csproj` files have been generated from scratch with the `dotnet` CLI.

Here is a high-level summary of changes to the `.cs` files:

* the `customers.xml` file is embedded into each project as an `EmbeddedResource`;
* all sample methods are executed rather than just some of them.

## How to run the examples

TODO: add instructions to execute each example for each platform (Windows,
Linux).

## Visual Studio Code

With VSCode, having the .NET Core SDK and the C# extension installed, open
the workspace file `samples.code-workspace`. It contains all projects, and
will allow you to open all files from VSCode and run every project from
the IDE itself, as opposed to opening the directory but not being able to
automatically launch each separate example.