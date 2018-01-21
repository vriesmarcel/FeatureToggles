param (
	[string]$webAppName,
	[string]$productionSlotName,
	[string]$canarySlotName,
	[string]$percentage
)
Write-Host "Setting slot on webapp $webAppName slotname $canarySlotName to $percentage % trafic"
$rule = New-Object Microsoft.WindowsAzure.Commands.Utilities.Websites.Services.WebEntities.RampUpRule
$rule.ActionHostName = "$webAppName-$canarySlotName.azurewebsites.net"
$rule.ReroutePercentage = $percentage
$rule.Name = $canarySlotName
#$rule.ChangeIntervalInMinutes = 1;
#$rule.ChangeStep = 5;
#$rule.MinReroutePercentage = 1;
#$rule.MaxReroutePercentage = $percentage;
#$rule.ChangeDecisionCallbackUrl = "http://mvcmusicstoretechdays.azurewebsites.net/TestInProduction/GetRoutingDirection"

Set-AzureWebsite $webAppName -Slot $productionSlotName -RoutingRules $rule