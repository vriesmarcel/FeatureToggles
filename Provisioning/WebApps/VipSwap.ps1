param (
	[string]$webAppName,
	[string]$toSlotName,
	[string]$fromSlotName
	)
Write-Host "Swapping slot on webapp: $webAppName from slot: $fromslotName to slot: $toSlotName"

Switch-AzureWebsiteSlot -Name $webAppName -Slot1 $toSlotName -Slot2 $fromSlotName -Force