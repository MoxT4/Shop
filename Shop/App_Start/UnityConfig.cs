using System;
using Microsoft.Practices.Unity;
using Shop.Infrastructure.Customer;
using Shop.Infrastructure.Repository;
using Shop.Order;
using Shop.Infrastructure.Product;

namespace Shop.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            /// Currently, we use singleton for repository and services. This is to handle function where object not being persisted into the database.
            container.RegisterType<IRepository<Customer>, CustomerRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IRepository<Order.Order>, OrderRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IRepository<Product>, ProductRepository>(new ContainerControlledLifetimeManager());


            container.RegisterType<IOrderService, OrderService>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICustomerService, CustomerService>(new ContainerControlledLifetimeManager());
        }
    }
}
