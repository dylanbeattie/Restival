$webAdminModule = get-module -ListAvailable | ? { $_.Name -eq "webadministration" }
If ($webAdminModule -ne $null) {
    import-module WebAdministration
} else {
    Add-PSSnapin WebAdministration -ErrorAction SilentlyContinue
}

$scriptPath = split-path $MyInvocation.MyCommand.Definition -parent 

# New-Item IIS:\Sites\Restival -bindings @{protocol="http";bindingInformation=":80:restival.local"} -physicalPath "$scriptPath\Restival.Website"

# New-Website -Name "Restival" -ApplicationPool "DefaultAppPool" -HostHeader "restival.local" -PhysicalPath "$scriptPath\Restival.Website"

New-Website -Name Restival -ApplicationPool DefaultAppPool -HostHeader restival.local -PhysicalPath c:\