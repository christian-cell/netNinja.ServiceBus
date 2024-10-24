1 create azure subscription and rg

2 install azure cli

https://learn.microsoft.com/en-us/cli/azure/install-azure-cli-windows?tabs=azure-cli#install-or-update

3 login

az login

4 set subscription

az account set --subscription "your-subscription-id"

5 run to apply ARM

az deployment group create --resource-group netNinjaRg --template-file template.json --parameters parameters.json

