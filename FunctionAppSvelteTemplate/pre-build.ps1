Write-Host "Pre-build started..."
$startTime = Get-Date

cd ./Frontend
npm run build | Out-Null
npm i -g | Out-Null
cd ../

$endTime = Get-Date
$timeDif = ($endTime - $startTime)
$timeTaken = '{0:D2}.{1:D3}' -f $timeDif.Seconds, $timeDif.Milliseconds
"Pre-build completed in $($timeTaken) seconds."