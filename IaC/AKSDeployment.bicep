param location string = 'westeurope'

param clusterName string = 'aks01'

@description('Optional DNS prefix to use with hosted Kubernetes API server FQDN.')
param dnsPrefix string  = 'AKS01-dns'

@description('Disk size (in GiB) to provision for each of the agent pool nodes. This value ranges from 0 to 1023. Specifying 0 will apply the default disk size for that agentVMSize.')
@minValue(0)
@maxValue(1023)
param osDiskSizeGB int = 0

@description('Boolean flag to turn on and off of RBAC.')
param enableRBAC bool = true

param ACRRG string = 'ADOCR01'

resource AKSDeployment 'Microsoft.ContainerService/managedClusters@2021-02-01' = {
  location: location
  name: clusterName
  properties: {
    kubernetesVersion: '1.20.9'
    enableRBAC: enableRBAC
    dnsPrefix: dnsPrefix
    agentPoolProfiles: [
      {
        name: 'agentpool'
        osDiskSizeGB: osDiskSizeGB
        count: 3
        enableAutoScaling: true
        minCount: 1
        maxCount: 5
        vmSize: 'Standard_B4ms'
        osType: 'Linux'        
        type: 'VirtualMachineScaleSets'
        mode: 'System'
        maxPods: 110
        availabilityZones: []
        vnetSubnetID: '/subscriptions/2579d2a0-24cb-465f-a993-7ce815bd8937/resourceGroups/AKS01RG/providers/Microsoft.Network/virtualNetworks/AKSVnet/subnets/default'
      }
    ]
    networkProfile: {
      loadBalancerSku: 'standard'
      networkPlugin: 'azure'
      serviceCidr: '10.0.0.0/16'
      dnsServiceIP: '10.0.0.10'
      dockerBridgeCidr: '172.17.0.1/16'
    }
    apiServerAccessProfile: {
      enablePrivateCluster: false
    }
    addonProfiles: {
      httpApplicationRouting: {
        enabled: false
      }
      azurepolicy: {
        enabled: false
      }
    }
  }
  tags: {}
  identity: {
    type: 'SystemAssigned'
  }
  dependsOn: [
    AKSVnet
  ]
}

resource AKSVnet 'Microsoft.Network/virtualNetworks@2020-11-01' = {
  name: 'AKSVnet'
  location: 'westeurope'
  properties: {
    subnets: [
      {
        name: 'default'        
        properties: {
          addressPrefix: '10.240.0.0/16'
        }
      }
    ]
    addressSpace: {
      addressPrefixes: [
        '10.0.0.0/8'
      ]
    }
  }
  tags: {}
}

module ACRRoleAssign 'ACRRoleAssign.bicep' = {
  name: 'ACRRoleAssign'
  scope: resourceGroup(ACRRG)
  params: {    
    AKSPrincipalID: AKSDeployment.properties.identityProfile.kubeletidentity.objectId
  }
  
}


output controlPlaneFQDN string = AKSDeployment.properties.fqdn
output AKSVnetSubnetID string = AKSVnet.properties.subnets[0].id
