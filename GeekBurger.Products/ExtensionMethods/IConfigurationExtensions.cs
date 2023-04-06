using GeekBurger.Products.Service;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ServiceBus.Fluent;

namespace GeekBurger.Products.ExtensionMethods;

public static class IConfigurationExtensions
{
    public static IServiceBusNamespace GetServiceBusNamespace(this IConfiguration configuration)
    {
        var config = configuration.GetSection("serviceBus")
            .Get<ServiceBusConfiguration>();

        var credentials = SdkContext.AzureCredentialsFactory        
            .FromServicePrincipal(config.ClientId, 
                config.ClientSecret,
                config.TenantId,                  
                AzureEnvironment.AzureGlobalCloud);

        var serviceBusManager = ServiceBusManager
            .Authenticate(credentials, config.SubscriptionId);
        return serviceBusManager.Namespaces
            .GetByResourceGroup(config.ResourceGroup,
                config.NamespaceName);
    }
}