// ReSharper disable once CheckNamespace
namespace Autofac
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    using global::Autofac.Core;
    using global::Autofac.Core.Registration;

    /// <summary>
    /// Extensiones para todas las instancias de ContainerBuilder.
    /// </summary>
    public static class ContainerBuilderExtensions
    {
        #region Declarations

        /// <summary>Cache de los modulos registrados.</summary>
        private static readonly IDictionary<Type, IModuleRegistrar> Cache = new ConcurrentDictionary<Type, IModuleRegistrar>();

        /// <summary>Sincroniza el acceso concurrente.</summary>
        private static readonly object SyncLock = new object();

        #endregion

        #region Methods

        /// <summary>Add a module to the container.</summary>
        /// <param name="builder">The builder to register the module with.</param>
        /// <typeparam name="TModule">The module to add.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// Thrown if <paramref name="builder" /> is <see langword="null" />.
        /// </exception>
        /// <returns>
        /// The <see cref="T:Autofac.Core.Registration.IModuleRegistrar" /> to allow
        /// additional chained module registrations.
        /// </returns>
        public static IModuleRegistrar SafeRegisterModule<TModule>(this ContainerBuilder builder)
            where TModule : IModule, new()
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            IModuleRegistrar ret;
            Type type = typeof(TModule);
            lock (SyncLock)
            {
                // Verifica que el módulo no este registrado.
                if (!Cache.TryGetValue(type, out ret))
                {
                    ret = builder.RegisterModule<TModule>();
                    Cache.Add(type, ret);
                }
            }

            return ret;
        }

        /// <summary>Add a module to the container.</summary>
        /// <param name="registrar">The module registrar that will make the registration into the container.</param>
        /// <typeparam name="TModule">The module to add.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// Thrown if <paramref name="registrar" /> is <see langword="null" />.
        /// </exception>
        /// <returns>
        /// The <see cref="T:Autofac.Core.Registration.IModuleRegistrar" /> to allow
        /// additional chained module registrations.
        /// </returns>
        public static IModuleRegistrar SafeRegisterModule<TModule>(this IModuleRegistrar registrar)
            where TModule : IModule, new()
        {
            if (registrar == null)
            {
                throw new ArgumentNullException(nameof(registrar));
            }

            IModuleRegistrar ret;
            Type type = typeof(TModule);
            lock (SyncLock)
            {
                // Verifica que el módulo no este registrado.
                if (!Cache.TryGetValue(type, out ret))
                {
                    ret = registrar.RegisterModule<TModule>();
                    Cache.Add(type, ret);
                }
            }

            return ret;
        }

        /// <summary>Add a module to the container.</summary>
        /// <param name="builder">The builder to register the module with.</param>
        /// <param name="module">The module to add.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// Thrown if <paramref name="builder" /> or <paramref name="module" /> is <see langword="null" />.
        /// </exception>
        /// <returns>
        /// The <see cref="T:Autofac.Core.Registration.IModuleRegistrar" /> to allow
        /// additional chained module registrations.
        /// </returns>
        public static IModuleRegistrar SafeRegisterModule(this ContainerBuilder builder, IModule module)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (module == null)
            {
                throw new ArgumentNullException(nameof(module));
            }

            IModuleRegistrar ret;
            Type type = module.GetType();
            lock (SyncLock)
            {
                // Verifica que el módulo no este registrado.
                if (!Cache.TryGetValue(type, out ret))
                {
                    ret = builder.RegisterModule(module);
                    Cache.Add(type, ret);
                }
            }

            return ret;
        }

        #endregion
    }
}