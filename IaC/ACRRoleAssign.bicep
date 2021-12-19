param ACRName string = 'adocr01'

param AKSPrincipalID string ='d5396110-912d-4849-a2c8-74f95040c152'



param roleDefinitionID string = '/subscriptions/${subscription().subscriptionId}/providers/Microsoft.Authorization/roleDefinitions/b24988ac-6180-42a0-ab88-20f7382dd24c'

resource ACR 'Microsoft.ContainerRegistry/registries@2020-11-01-preview' existing = {
  name: ACRName 
}
resource  AssignAcrPullToAks 'Microsoft.Authorization/roleAssignments@2020-04-01-preview' = {
  name: guid(resourceGroup().id,ACRName,'AssignContributorRoleToAks')       // want consistent GUID on each run
  scope: ACR
  properties: {
    principalId: AKSPrincipalID
    principalType: 'ServicePrincipal'
    roleDefinitionId: roleDefinitionID
  }
}
