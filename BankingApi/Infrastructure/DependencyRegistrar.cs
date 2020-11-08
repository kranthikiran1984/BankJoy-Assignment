using Autofac;
using BankingApi.Data;
using BankingApi.Services.Contracts;
using BankingApi.Services.Implementations;
using BankingApi.Services.Validations;
using System;

namespace BankingApi.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder)
        {
            //dbcontext
            builder.Register(context => new BankingDataContext("database.json"))
                                            .As<IDbContext>().InstancePerLifetimeScope();

            //repositories
            builder.RegisterGeneric(typeof(JsonRepository<>)).As(typeof(IJsonRepository<>))
                                                            .InstancePerLifetimeScope();

            //services
            builder.RegisterType<MemberService>().As<IMemberService>().InstancePerLifetimeScope();
            builder.RegisterType<AccountService>().As<IAccountService>().InstancePerLifetimeScope();
            builder.RegisterType<InstitutionDataService>().As<IInstitutionDataService>().InstancePerLifetimeScope();
            builder.RegisterType<TransactionService>().As<ITransactionService>().InstancePerLifetimeScope();

            //validators
            builder.RegisterType<MemberValidator>().AsSelf();
            builder.RegisterType<AccountValidator>().AsSelf();
            builder.RegisterType<InstitutionValidator>().AsSelf();
        }
    }
}
