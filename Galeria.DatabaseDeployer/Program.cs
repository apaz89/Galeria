﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using AcklenAvenue.Data.NHibernate;
using DomainDrivenDatabaseDeployer;
using FluentNHibernate.Cfg.Db;
using Galeria.Data;
using Galeria.Domain;
using NHibernate;
using NHibernate.Linq;

namespace Galeria.DatabaseDeployer
{
    class Program
    {
        static void Main(string[] args)
        {
            MsSqlConfiguration databaseConfiguration = MsSqlConfiguration.MsSql2008.ShowSql().
               ConnectionString(x => x.FromConnectionStringWithKey("Galeria.Local"));

            DomainDrivenDatabaseDeployer.DatabaseDeployer dd = null;
            ISessionFactory sessionFactory = new SessionFactoryBuilder(new MappingScheme(), databaseConfiguration)
               .Build(cfg => { dd = new DomainDrivenDatabaseDeployer.DatabaseDeployer(cfg); });

            dd.Drop();
            Console.WriteLine("Database dropped.");
            Thread.Sleep(1000);

            dd.Create();
            Console.WriteLine("Database created.");

            ISession session = sessionFactory.OpenSession();
            using (ITransaction tx = session.BeginTransaction())
            {
                dd.Seed(new List<IDataSeeder>
                            {
                                new AccountSeeder(session)
                            });
                tx.Commit();
            }

            session.Close();
            sessionFactory.Close();
            Console.WriteLine("Seed data added.");
            Thread.Sleep(2000);

        }
    }
}
