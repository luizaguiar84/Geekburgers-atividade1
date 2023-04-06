// using System.Configuration;
// using GeekBurger.Products.ExtensionMethods;
// using Microsoft.Azure.Management.ResourceManager.Fluent;
//
// namespace GeekBurger.Products.Service;
//
// public class ReceiveMessageService
// {
//     private static IConfiguration _configuration;
//     
//     static ReceiveMessageService()
//     {
//         _configuration = new ConfigurationBuilder()
//             .SetBasePath(Directory.GetCurrentDirectory())
//             .AddJsonFile("appsettings.json")
//             .Build();
//     }
//     
//     private static async void ReceiveMessages()
//     {
//         
//         
//         var serviceBusConfiguration = _configuration.GetSection("serviceBus")
//             .Get<ServiceBusConfiguration>();
//         
//         var subscriptionClient = new SubscriptionClient(serviceBusConfiguration.ConnectionString, TopicName, SubscriptionName);
//
//         //by default a 1=1 rule is added when subscription is created, so we need to remove it
//         await subscriptionClient.RemoveRuleAsync("$Default");
//
//         await subscriptionClient.AddRuleAsync(new RuleDescription
//         {
//             Filter = new CorrelationFilter { Label = _storeId },
//             Name = "filter-store"
//         });
//
//         var mo = new MessageHandlerOptions(ExceptionHandler) { AutoComplete = true };
//
//         subscriptionClient.RegisterMessageHandler(MessageHandler, mo);
//             
//         Console.ReadLine();
//     }
//
// }