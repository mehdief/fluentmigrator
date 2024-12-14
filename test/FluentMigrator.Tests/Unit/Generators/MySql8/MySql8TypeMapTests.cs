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

using FluentMigrator.Runner.Generators.MySql;

using NUnit.Framework;

using Shouldly;

namespace FluentMigrator.Tests.Unit.Generators.MySql8
{
    [TestFixture]
    [Category("MySql")]
    [Category("Generator")]
    [Category("TypeMap")]
    public class MySql8TypeMapTests
    {
        private MySql8TypeMap _typeMap;

        [SetUp]
        public void SetUp()
        {
            _typeMap = new MySql8TypeMap();
        }

        [Test]
        public void TimeWithoutSizeIsTimeWithoutPrecision()
        {
            _typeMap.GetTypeMap(DbType.Time, size: null, precision: null).ShouldBe("TIME");
        }

        [Test]
        public void TimeWithSizeIsTimeWithPrecision()
        {
            _typeMap.GetTypeMap(DbType.Time, size: 3, precision: null).ShouldBe("TIME(3)");
        }
    }
}
