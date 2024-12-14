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

using System.Data;

using FluentMigrator.Exceptions;
using FluentMigrator.Expressions;
using FluentMigrator.Model;
using FluentMigrator.Runner.Generators.MySql;

using NUnit.Framework;

using Shouldly;

namespace FluentMigrator.Tests.Unit.Generators.MariaDB
{
    [TestFixture]
    public class MariaDBColumnTests
    {
        protected MariaDBGenerator Generator;

        [SetUp]
        public void Setup()
        {
            Generator = new MariaDBGenerator();
        }

        [Test]
        public void CanCreateGeneratedColumnWithoutNullable()
        {
            var column = new ColumnDefinition { Name = GeneratorTestHelper.TestColumnName1, Type = DbType.Int32, Generated = new GeneratedColumnMetadata("1", stored: true), IsNullable = null };
            var expression = new CreateColumnExpression { TableName = GeneratorTestHelper.TestTableName1, Column = column };

            var result = Generator.Generate(expression);

            result.ShouldBe("ALTER TABLE `TestTable1` ADD COLUMN `TestColumn1` INTEGER GENERATED ALWAYS AS (1) STORED");
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void CreateNullableGeneratedColumnNotSupported(bool nullable)
        {
            var column = new ColumnDefinition { Name = GeneratorTestHelper.TestColumnName1, Type = DbType.Int32, Generated = new GeneratedColumnMetadata("1", stored: false), IsNullable = nullable };
            var expression = new CreateColumnExpression { TableName = GeneratorTestHelper.TestTableName1, Column = column };

            Assert.Throws<DatabaseOperationNotSupportedException>(() => Generator.Generate(expression));
        }
    }
}
