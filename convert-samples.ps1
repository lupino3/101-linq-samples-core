param(
    [Parameter(Mandatory=$true)][string]$sourceDir,
    [Parameter(Mandatory=$true)][string]$dstDir
)

# Check for availability of .NET Core SDK.
try {
    $_  = Get-Command dotnet -ErrorAction stop
} catch {
    Write-Error "The .NET Core SDK is not installed or not reachable through PATH."
    exit
}

function MigrateProject($srcProject, [string]$dstDir, [string]$workspacePath) {
    $dstProjectDir = Join-Path $dstDir $srcProject.project
    if (Test-Path $dstProjectDir) {
        Write-Output "$($dstProjectDir) exists, skipping it."
        return
    }

    # Create the directory and the .NET project.
    Push-Location
    mkdir $dstProjectDir
    Set-Location $dstProjectDir
    dotnet new console
    
    # Copy over the sample program.
    $program = Join-Path $srcProject.path "Program.cs" 
    Copy-Item $program $dstProjectDir -Force

    # Add directory to vscode workspace.
    $wsContent = Get-Content -Raw $workspacePath | Out-String
    $jsWsContent = ConvertFrom-Json $wsContent

    $jsonElement = @{'path' = $srcProject.project}
    $jsWsContent.folders += $jsonElement
    $newWs = ConvertTo-json $jsWsContent
    Set-Content -Path $workspacePath -Value $newWs

    $dstProgramPath = Join-Path $dstProjectDir "Program.cs"
    $programContents = Get-Content $dstProgramPath
    if ($programContents -match "Customers.xml") {
        # Make customers.xml available as an EmbeddedResource.
        # Elements to add: <ItemGroup><EmbeddedResource Include="../Common/customers.xml" /></ItemGroup>
        $csprojName = (Join-Path $dstProjectDir $srcProject.project) + ".csproj"
        [xml]$csproj = Get-Content $csprojName
        $igElement = $csproj.CreateElement("ItemGroup")
        $erElement = $csproj.CreateElement("EmbeddedResource")
        $attr = $csproj.CreateAttribute("Include")
        $attr.Value = "../Common/customers.xml"
        $erElement.Attributes.Append($attr)
        $igElement.AppendChild($erElement)
        $csproj.Project.AppendChild($igElement)
        $csproj.Save($csprojName)

        # Change Program.cs to use the linked customer.xml.
        $code = "System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(`"$($srcProject.project).customers.xml`")"
        $newProgramContents = $programContents -replace "`"Customers.xml`"", $code
        Set-Content -Path $dstProgramPath -Value $newProgramContents
    }
    Pop-Location
}

# Find the subdirs of the source directory that start with "LINQ", navigate in the "C#"
# directory and get its subdirectories (there is only one of them).
$exampleDirs = Get-ChildItem $sourceDir | Where-Object { $_.Name.StartsWith("LINQ")}
$csharpDirs = $exampleDirs | Get-ChildItem | Where-Object {$_.Name.Equals("C#")}

# Note that we know that there is only one subdir within the "C#" dir, so we can just get its
# basename as the identifier of the project and to get the full path to the destination
# directory of the project.
$sourceDirs = $csharpDirs |
    Select-Object @{N="path";E={Join-Path $_.FullName ($_.GetDirectories() | Select-Object -First 1)}},
                  @{N="project";E={($_.GetDirectories() | Select-Object -First 1) | Select-Object -ExpandProperty BaseName}}

$workSpacePath = Join-Path $dstDir "samples.code-workspace"
foreach ($sample in $sourceDirs) {
    MigrateProject $sample $dstDir $workSpacePath
}
