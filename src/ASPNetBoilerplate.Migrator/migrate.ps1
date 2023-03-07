$path=$pwd
$config="Debug"
$isDown="false"
$version="all"

if ( $args[0] )
{
    $config=$args[0]
}
if ( $args[1] )
{
	if(($args[1] -ieq "true" -or $args[1] -ieq "false")) 
	{
		$isDown=$args[1]
	}
	else
	{
		write-error "The 2nd argument must either be 'true' or 'false' for down migrating."
		exit 1
	}
}
if ( $args[2] )
{
	if($args[2] -is [int64])
	{
		$version=$args[2]
	}
	else
	{
		write-error "Please enter a valid migration verion"
		exit 1
	}
}

write-output "Publishing..."
Start-Process -Wait -FilePath "C:\Program Files\dotnet\dotnet.exe" -argumentList @("publish $($path)\ASPNetBoilerplate.Migrator.csproj -c $($config)") -NoNewWindow
write-output ""
write-output "Running..."
[System.Environment]::SetEnvironmentVariable('ASPNETCORE_ENVIRONMENT', $config)
Start-Process -Wait -FilePath "$($path)\bin\$($config)\net7.0\publish\ASPNetBoilerplate.Migrator.exe" -ArgumentList "$($isDown)","$($version)" -NoNewWindow
