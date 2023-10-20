Write-Host "Pre-build started..."
$startTime = Get-Date

npm --prefix .\Frontend run build | Out-Null

$endTime = Get-Date
$timeDif = ($endTime - $startTime)
$timeTaken = '{0:D2}.{1:D3}' -f $timeDif.Seconds, $timeDif.Milliseconds
"Pre-build completed in $($timeTaken) seconds."