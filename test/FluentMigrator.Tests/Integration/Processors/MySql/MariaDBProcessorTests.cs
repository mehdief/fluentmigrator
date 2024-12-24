#region License
// Copyright (c) 2024, Fluent Migrator Project
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
#endregion

using FluentMigrator.Runner.Processors.MySql;

using Microsoft.Extensions.DependencyInjection;

using NUnit.Framework;
using System;

using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;

using JetBrains.Annotations;

using Shouldly;

namespace FluentMigrator.Tests.Integration.Processors.MySql
{
    [TestFixture]
    [Category("Integration")]
    [Category("MariaDB")]
    public class MariaDBProcessorTests
    {
        private ServiceProvider ServiceProvider { get; set; }
        private IServiceScope ServiceScope { get; set; }
        private MariaDBProcessor Processor { get; set; }

        [Test]
        public void DatabaseExistsReturnsTrueIfDatabaseExists()
        {
            Processor.DatabaseExists().ShouldBeFalse();
        }

        [Test]
        public void CreateDatabaseIfNotExistsCreatesDatabase()
        {
            var exists = false;
            try
            {
                Processor.CreateDatabaseIfNotExists();
                exists = Processor.DatabaseExists();
            }
            finally
            {
                Processor.DropDatabaseIfExists();
            }

            exists.ShouldBeTrue();
        }

        [Test]
        public void DropDatabaseIfExistsDropsDatabase()
        {
            var exists = false;
            try
            {
                Processor.CreateDatabaseIfNotExists();
                Processor.DropDatabaseIfExists();
                exists = Processor.DatabaseExists();
            }
            finally
            {
                Processor.DropDatabaseIfExists();
            }

            exists.ShouldBeFalse();
        }

        private static ServiceProvider CreateProcessorServices([CanBeNull] Action<IServiceCollection> initAction)
        {
            var serivces = ServiceCollectionExtensions.CreateServices()
                .ConfigureRunner(builder => builder.AddMariaDB())
                .AddScoped<IConnectionStringReader>(
                    _ => new PassThroughConnectionStringReader(IntegrationTestOptions.MariaDB.ConnectionString));
            initAction?.Invoke(serivces);
            return serivces.BuildServiceProvider();
        }

        [OneTimeSetUp]
        public void ClassSetUp()
        {
            ServiceProvider = CreateProcessorServices(null);
        }

        [OneTimeTearDown]
        public void ClassTearDown()
        {
            ServiceProvider?.Dispose();
        }

        [SetUp]
        public void SetUp()
        {
            ServiceScope = ServiceProvider.CreateScope();
            Processor = ServiceScope.ServiceProvider.GetRequiredService<MariaDBProcessor>();
        }

        [TearDown]
        public void TearDown()
        {
            ServiceScope?.Dispose();
            Processor?.Dispose();
        }
    }
}
