$directoryPath = Split-Path ((Get-Variable MyInvocation).Value).MyCommand.Path
$parentDirectoryPath = (Get-Item $directoryPath).parent.FullName
$assemblyFile = "$parentDirectoryPath\Source\SharedFiles\SharedAssemblyInfo.cs"

$RegularExpression = [regex] 'AssemblyInformationalVersion\(\"(.*)\"\)'

$fileContent = Get-Content $assemblyFile

foreach($content in $fileContent)
{
    $match = [System.Text.RegularExpressions.Regex]::Match($content, $RegularExpression)
    if($match.Success)
    {
        $match.groups[1].value
    }
}