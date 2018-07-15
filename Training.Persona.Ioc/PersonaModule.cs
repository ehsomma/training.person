namespace Training.Persona.Ioc
{
    using System.Reflection;
    using Autofac;

    /// <inheritdoc />
    public class PersonaModule : Autofac.Module
    {
        /// <inheritdoc />
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Apollo.NetCore.Core.Data.Database>().AsImplementedInterfaces().SingleInstance();

            Assembly assemblyData = typeof(Training.Persona.Data.PersonaRepository).GetTypeInfo().Assembly;
            Assembly assemblyBusiness = typeof(Training.Persona.Business.PersonaManager).GetTypeInfo().Assembly;
            Assembly assemblyValidators = typeof(Training.Persona.Business.Validators.PersonaValidator).GetTypeInfo().Assembly;

            builder.RegisterAssemblyTypes(assemblyData).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(assemblyBusiness).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(assemblyValidators).AsImplementedInterfaces();
        }
    }
}
