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

using FluentMigrator.Expressions;
using FluentMigrator.Model;
using FluentMigrator.Runner.Generators.MySql;

using NUnit.Framework;

using Shouldly;

namespace FluentMigrator.Tests.Unit.Generators.MySql8
{
    [TestFixture]
    public class MySql8ColumnTests
    {
        protected MySql8Generator Generator;

        [SetUp]
        public void Setup()
        {
            Generator = new MySql8Generator();
        }

        [Test]
        public void CanCreateColumnWithTimeAndDefaultPrecisionType()
        {
            var column = new ColumnDefinition { Name = GeneratorTestHelper.TestColumnName1, Type = DbType.Time };
            var expression = new CreateColumnExpression { TableName = GeneratorTestHelper.TestTableName1, Column = column };

            var result = Generator.Generate(expression);

            result.ShouldBe("ALTER TABLE `TestTable1` ADD COLUMN `TestColumn1` TIME NOT NULL");
        }

        [Test]
        public void CanCreateColumnWithTimeAndPrecisionType()
        {
            var column = new ColumnDefinition { Name = GeneratorTestHelper.TestColumnName1, Type = DbType.Time, Precision = 3 };
            var expression = new CreateColumnExpression { TableName = GeneratorTestHelper.TestTableName1, Column = column };

            var result = Generator.Generate(expression);

            result.ShouldBe("ALTER TABLE `TestTable1` ADD COLUMN `TestColumn1` TIME(3) NOT NULL");
        }
    }
}
