provider "azurerm" {
  features {}
}

# Use the existing resource group
data "azurerm_resource_group" "existing" {
  name = "win10_group"
}

# Create a virtual network
resource "azurerm_virtual_network" "vnet" {
  name                = "univnet"
  address_space       = ["10.0.0.0/16"]
  location            = data.azurerm_resource_group.existing.location
  resource_group_name = data.azurerm_resource_group.existing.name
}

# Create subnets
resource "azurerm_subnet" "subnet1" {
  name                 = "sub1"
  resource_group_name  = data.azurerm_resource_group.existing.name
  virtual_network_name = azurerm_virtual_network.vnet.name
  address_prefixes     = ["10.0.1.0/24"]
}

resource "azurerm_subnet" "subnet2" {
  name                 = "sub2"
  resource_group_name  = data.azurerm_resource_group.existing.name
  virtual_network_name = azurerm_virtual_network.vnet.name
  address_prefixes     = ["10.0.2.0/24"]
}

# Create an Azure Container Registry
resource "azurerm_container_registry" "acr" {
  name                = "uniacr"
  resource_group_name = data.azurerm_resource_group.existing.name
  location            = data.azurerm_resource_group.existing.location
  sku                 = "Basic"
  admin_enabled       = true
}

# Create an AKS cluster
resource "azurerm_kubernetes_cluster" "aks" {
  name                = "uniaks"
  location            = data.azurerm_resource_group.existing.location
  resource_group_name = data.azurerm_resource_group.existing.name
  dns_prefix          = "uniaks"

  default_node_pool {
    name       = "default"
    node_count = 1
    vm_size    = "Standard_DS2_v2"
    vnet_subnet_id = azurerm_subnet.subnet1.id
  }

  identity {
    type = "SystemAssigned"
  }

  network_profile {
    network_plugin = "azure"
    network_policy = "azure"
  }

  role_based_access_control {
    enabled = true
  }

  addon_profile {
    azure_policy {
      enabled = true
    }
  }

  depends_on = [azurerm_subnet.subnet1]
}

# Assign ACR pull role to AKS
resource "azurerm_role_assignment" "aks_acr" {
  principal_id         = azurerm_kubernetes_cluster.aks.kubelet_identity[0].object_id
  role_definition_name = "AcrPull"
  scope                = azurerm_container_registry.acr.id
}
